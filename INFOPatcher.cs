using G1Tool.Formats;
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
            Settings.ReadSettings("Settings.json");

            TB_InPath.Text = Settings.settingsInfo.ModPath;
            TB_OriPath.Text = Settings.settingsInfo.PatchPath;
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
            string Patch;
            // Check if user ignored instrutions and selected the patch folder instead
            if (File.Exists(inpath + Path.DirectorySeparatorChar + "INFO0.bin") && File.Exists(inpath + Path.DirectorySeparatorChar + "INFO2.bin"))
            {
                Patch = Path.GetFileName(inpath);
                return Patch;
            }

            // Get latest patch in patch folder
            int patchnum = 0;
            string[] patchdirs = Directory.GetDirectories(inpath, "patch*", SearchOption.TopDirectoryOnly);
            foreach (string dir in patchdirs)
            {
                int num = Convert.ToInt32(Path.GetFileName(dir).Replace("patch", ""));
                if (num > patchnum)
                    patchnum = num;
                else
                    continue;
            }
            Patch = $"patch{patchnum}";
            return Patch;
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
            richTextBox1.Clear();
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
            if (PatchNum == null)
            {
                AddLine(richTextBox1, "Invaid Original Patch Folder");
                UIUsage(true);
                return;
            }
            string info0file = ModPath + Path.DirectorySeparatorChar + PatchNum + Path.DirectorySeparatorChar + "INFO0.bin";
            NewEntryList = new List<INFO0.INFO0Entry>();

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
                    if (Path.GetFileName(newfile).Contains('-'))
                        entryid = Convert.ToInt64(Path.GetFileName(newfile).Split('-')[0].Replace(" ",""));
                    else
                        entryid = Convert.ToInt64(Path.GetFileName(newfile).Split('.')[0].Replace(" ", ""));
                    Console.WriteLine();
                }
                catch (FormatException)
                {
                    if (CB_Log.Checked)
                        AddLine(richTextBox1, $"File Found with no ID: {newfile}");
                    continue;
                }
                if (CB_Log.Checked)
                    AddLine(richTextBox1, $"File Found: {newfile}");

                //Write new Entry
                byte[] bfile = File.ReadAllBytes(file);
                if (Bin.GetMagic(bfile) == ".gz")
                {
                    byte[] dec = GZip.Decompress(File.ReadAllBytes(file));
                    NewEntry = new INFO0.INFO0Entry() { EntryID = entryid, CompressedSize = bfile.Length, DecompressedSize = dec.Length, IsCompressed = 1, FileName = newfile };
                    if (CB_Log.Checked)
                        AddLine(richTextBox1, $"Created Compressed Entry: {entryid}");
                }
                else
                {
                    NewEntry = new INFO0.INFO0Entry() { EntryID = entryid, CompressedSize = bfile.Length, DecompressedSize = bfile.Length, IsCompressed = 0, FileName = newfile };
                    if (CB_Log.Checked)
                        AddLine(richTextBox1, $"Created Decompressed Entry: {entryid}");
                }
                if (!CB_Log.Checked)
                    AddLine(richTextBox1, $"Added {entryid}");
                NewEntryList.Add(NewEntry);
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
            if (!Directory.Exists($"{ModPath}\\{PatchNum}"))
                Directory.CreateDirectory($"{ModPath}\\{PatchNum}");
            using (EndianBinaryWriter bw = new EndianBinaryWriter(File.Create($"{ModPath}\\{PatchNum}\\INFO0.bin"), Endianness.Little))
            {
                if (CB_Log.Checked)
                    AddLine(richTextBox1, $"Writing INFO0.bin...");
                foreach (var item in finallist)
                {
                    item.Write(bw);
                }
            }

            //Write Info2 file
            if (!Directory.Exists($"{ModPath}\\{PatchNum}"))
                Directory.CreateDirectory($"{ModPath}\\{PatchNum}");
            using (EndianBinaryWriter bw = new EndianBinaryWriter(File.Create($"{ModPath}\\{PatchNum}\\INFO2.bin"), Endianness.Little))
            {
                if (CB_Log.Checked)
                    AddLine(richTextBox1, $"Writing INFO2.bin...");
                bw.WriteInt64(infofile.INFO0Entries.Count);
                bw.WriteInt64(4062);
            }

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
