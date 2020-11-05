using G1Tool.Formats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace THAT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AllowDrop = RTB_Output.AllowDrop = true;
            DragEnter += Form1_DragEnter;
            RTB_Output.DragEnter += Form1_DragEnter;
            DragDrop += Form1_DragDrop;
            RTB_Output.DragDrop += Form1_DragDrop;
        }

        private volatile int threads;
        private string Selected_Path;

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (threads > 0)
            {
                MessageBox.Show("Please wait until all operations are finished.");
                return;
            }
            new Thread(() =>
            {
                threads++;
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                    Open(file);
                threads--;
            }).Start();
        }

        private void B_Open_Click(object sender, EventArgs e)
        {
            if (threads > 0)
            {
                MessageBox.Show("Please wait until all operations are finished.");
                return;
            }
            B_Go.Enabled = false;
            CommonDialog dialog;
            if (ModifierKeys == Keys.Alt)
                dialog = new FolderBrowserDialog();
            else
                dialog = new OpenFileDialog();
            if (dialog.ShowDialog() != DialogResult.OK) return;

            if (dialog is OpenFileDialog)
                TB_FilePath.Text = (dialog as OpenFileDialog).FileName;
            else TB_FilePath.Text = (dialog as FolderBrowserDialog).SelectedPath;
            B_Go.Enabled = true;
        }

        private void B_Go_Click(object sender, EventArgs e)
        {
            if (threads > 0)
            {
                MessageBox.Show("Please wait until all operations are finished.");
                return;
            }
            new Thread(() =>
            {
                threads++;
                Open(Selected_Path);
                threads--;
            }).Start();
        }

        private void Open(string path)
        {
            if (Directory.Exists(path))
            {
                if (ModifierKeys == Keys.Control)
                {
                    string outfile = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileName(path) + ".bin";
                    Bin.FileList = new List<byte[]>();
                    List<string> list = new List<string>();
                    string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        string filename = Path.GetFileName(file);
                        list.Add(filename);
                    }
                    list = list.OrderBy(x => int.Parse(x.Split('.')[0])).ToList();
                    foreach (string file in list) {
                        byte[] filetoadd = File.ReadAllBytes(path + Path.DirectorySeparatorChar + file);
                        Bin.FileList.Add(filetoadd);
                    }
                    Bin.Build(Bin.FileList, outfile);
                    AddLine(RTB_Output, string.Format("Packed {0} to {1}", Path.GetFileName(path), Path.GetFileName(path) + ".bin"));
                }
            }
            if (File.Exists(path))
            {
                string ext = Path.GetExtension(path).ToLower();
                string filename = Path.GetFileNameWithoutExtension(path).ToLower();

                if (ModifierKeys == Keys.Control)
                {
                    File.WriteAllBytes(path + ".gz", GZip.Compress(File.ReadAllBytes(path)));
                    AddLine(RTB_Output, string.Format("KTGZ compressed {0} to {1}", Path.GetFileName(path), Path.GetFileName(path) + ".gz"));
                }
                else if (ext == ".gz")
                {
                    byte[] filedata = File.ReadAllBytes(path);
                    string decpath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path);
                    try
                    {
                        File.WriteAllBytes(decpath, GZip.Decompress(filedata));
                        AddLine(RTB_Output, string.Format("Successfully decompressed {0}.", Path.GetFileName(decpath)));
                    }
                    catch (Exception ex)
                    {
                        AddLine(RTB_Output, string.Format("Unable to automatically decompress {0}.", Path.GetFileName(path)));
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (ext == ".bin" || ext == ".datatable")
                {
                    string decpath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path);
                    Bin.Extract(path, decpath);
                    AddLine(RTB_Output, string.Format("Successfully extracted {0}.", Path.GetFileName(path)));
                }
            }
        }

        private void TB_FilePath_TextChanged(object sender, EventArgs e)
        {
            Selected_Path = TB_FilePath.Text;
        }

        private void AddText(RichTextBox RTB, string msg)
        {
            if (RTB.InvokeRequired)
                RTB.Invoke(new Action(() => RTB.AppendText(msg)));
            else
                RTB.AppendText(msg);
        }

        public static void AddLine(RichTextBox RTB, string line)
        {
            if (RTB.InvokeRequired)
                RTB.Invoke(new Action(() => RTB.AppendText(line + Environment.NewLine)));
            else
                RTB.AppendText(line + Environment.NewLine);
        }

        private void RTB_Output_Click(object sender, EventArgs e)
        {
            RTB_Output.Clear();
            RTB_Output.Text = "Open a file, or Drag/Drop several! Click this box to clear its text." + Environment.NewLine;

        }
    }
}
