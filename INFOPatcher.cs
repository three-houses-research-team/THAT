using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using THAT.Formats.KTGL;

namespace THAT
{
    public partial class INFOPatcher : Form
    {
        public INFOPatcher()
        {
            InitializeComponent();
        }

        private INFO0.Info0 infofile;
        private INFO0.INFO0Entry NewEntry;
        private List<INFO0.INFO0Entry> NewEntryList;

        private void UIUsage(bool toggle)
        {
            TB_InPath.Enabled = toggle;
            TB_OriPath.Enabled = toggle;
            B_InPath.Enabled = toggle;
            B_OriPath.Enabled = toggle;
            B_Patch.Enabled = toggle;
            CB_Log.Enabled = toggle;
        }

        private string FindPatch(string inpath)
        {
            string Patch = "none";
            for (int i = 4; i >= 1; i--)
            {
                switch (i)
                {
                    case 4:
                        if (Directory.Exists(inpath + Path.DirectorySeparatorChar + "patch4"))
                            Patch = "patch4";
                        break;
                    case 3:
                        if (Directory.Exists(inpath + Path.DirectorySeparatorChar + "patch3"))
                            Patch = "patch3";
                        break;
                    case 2:
                        if (Directory.Exists(inpath + Path.DirectorySeparatorChar + "patch2"))
                            Patch = "patch2";
                        break;
                    case 1:
                        if (Directory.Exists(inpath + Path.DirectorySeparatorChar + "patch1"))
                            Patch = "patch1";
                        break;
                    default:
                        break;
                }
                return Patch;
            }
            return "none";
        }

        private void B_InPath_Click(object sender, EventArgs e)
        {
            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    TB_InPath.Text = dialog.FileName;
                }
            }
        }

        private void B_OriPath_Click(object sender, EventArgs e)
        {
            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    TB_OriPath.Text = dialog.FileName;
                }
            }
        }

        private void B_Patch_Click(object sender, EventArgs e)
        {
            //Setup Paths
            UIUsage(false);
            string ModPath = TB_InPath.Text;
            string PatchPath = TB_OriPath.Text;
            if (PatchPath.Length == 0 || PatchPath == null || ModPath.Length == 0 || ModPath == null)
            {
                AddLine(richTextBox1, "Invaid Folders");
                UIUsage(true);
                return;
            }
            string PatchNum = FindPatch(PatchPath);
            string info0file = ModPath + Path.DirectorySeparatorChar + PatchNum + Path.DirectorySeparatorChar + "INFO0.bin";
            NewEntryList = new List<INFO0.INFO0Entry>();
            if (PatchNum == "none")
            {
                AddLine(richTextBox1, "Invaid Original Patch Folder");
                UIUsage(true);
                return;
            }

            //detele existing files
            if (File.Exists(ModPath + Path.DirectorySeparatorChar + PatchNum + Path.DirectorySeparatorChar + "INFO0.bin"))
                File.Delete(ModPath + Path.DirectorySeparatorChar + PatchNum + Path.DirectorySeparatorChar + "INFO0.bin");
            if (File.Exists(ModPath + Path.DirectorySeparatorChar + PatchNum + Path.DirectorySeparatorChar + "INFO2.bin"))
                File.Delete(ModPath + Path.DirectorySeparatorChar + PatchNum + Path.DirectorySeparatorChar + "INFO2.bin");

            //Load Info
            infofile = new INFO0.Info0();
            infofile.ReadData(PatchPath + Path.DirectorySeparatorChar + PatchNum + Path.DirectorySeparatorChar + "INFO0.bin");
            if (CB_Log.Checked)
                AddLine(richTextBox1, $"Original Patch Entry Count: {infofile.numOfEntries}");

            //Load modded files
            string[] files = Directory.GetFiles(ModPath, "*", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                long entryid;
                string newfile = file.Replace(ModPath, "rom:").Replace("\\","/");
                try
                {
                    entryid = Convert.ToInt64(Path.GetFileName(newfile).Split('-')[0]);
                }
                catch (FormatException)
                {
                    continue;
                }
                if (CB_Log.Checked)
                    AddLine(richTextBox1, $"File Found: {newfile}");

                //Write new Entry
                byte[] bfile = File.ReadAllBytes(file);
                NewEntry = new INFO0.INFO0Entry(){ EntryID = entryid, CompressedSize = bfile.Length, DecompressedSize = bfile.Length, IsCompressed = 0, FileName = newfile};
                NewEntryList.Add(NewEntry);
                if (CB_Log.Checked)
                    AddLine(richTextBox1, $"New Entry Created: {entryid}");
            }

            //Check for existing entries and remove is needed
            if (CB_Log.Checked)
                AddLine(richTextBox1, $"Removing dupicates and sorting list");
            foreach (var entry in NewEntryList)
            {
                long id = entry.EntryID;
                if (infofile.EntryIDs.Contains(id))
                {
                    int index = infofile.EntryIDs.FindIndex(x => x == id);
                    infofile.INFO0Entries.RemoveAt(index);
                    infofile.EntryIDs.RemoveAt(index);
                }
                infofile.INFO0Entries.Add(entry);
            }
            //Sort entires based on IDs 
            List<INFO0.INFO0Entry> finallist = infofile.INFO0Entries.OrderBy(o => o.EntryID).ToList();

            //Write Each entry to the info0
            using (EndianBinaryWriter bw = new EndianBinaryWriter(File.Create(ModPath + Path.DirectorySeparatorChar + PatchNum + Path.DirectorySeparatorChar + "INFO0.bin"), Endianness.Little))
            {
                if (CB_Log.Checked)
                    AddLine(richTextBox1, $"Writing INFO0.bin...");
                foreach (var item in finallist)
                {
                    item.Write(bw);
                }
            }

            //Write Info2 file
            using (EndianBinaryWriter bw = new EndianBinaryWriter(File.Create(ModPath + Path.DirectorySeparatorChar + PatchNum + Path.DirectorySeparatorChar + "INFO2.bin"), Endianness.Little))
            {
                if (CB_Log.Checked)
                    AddLine(richTextBox1, $"Writing INFO2.bin...");
                bw.WriteInt64(infofile.INFO0Entries.Count);
                bw.WriteInt64(4062);
            }

            if (CB_Log.Checked)
                AddLine(richTextBox1, $"Done");

            UIUsage(true);
        }

        private void AddLine(RichTextBox RTB, string line)
        {
            if (RTB.InvokeRequired)
                RTB.Invoke(new Action(() => RTB.AppendText(line + Environment.NewLine)));
            else
                RTB.AppendText(line + Environment.NewLine);
        }
    }
}
