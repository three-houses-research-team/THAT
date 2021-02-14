using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;

namespace THAT
{
    public partial class Settings : Form
    {
        public static string json;
        public static SettingsInfo settingsInfo;
        public Settings()
        {
            InitializeComponent();
            ReadSettings("Settings.json");

            TB_patchpath.Text = settingsInfo.PatchPath;
            TB_modpath.Text = settingsInfo.ModPath;
            TB_gustpath.Text = settingsInfo.GustPath;
        }

        public class SettingsInfo
        {
            public string PatchPath { get; set; }
            public string ModPath { get; set; }
            public string GustPath { get; set; }
        }

        public static void ReadSettings(string path)
        {
            if (!File.Exists("Settings.json"))
                return;
            if (File.ReadAllText("Settings.json") == null)
                return;

            using (StreamReader streamReader = new StreamReader("Settings.json"))
            {
                json = streamReader.ReadToEnd();
                streamReader.Close();
            }

            settingsInfo = JsonConvert.DeserializeObject<SettingsInfo>(json);
        }

        public void WriteSettings(string path)
        {
            settingsInfo = new SettingsInfo();
            settingsInfo.PatchPath = TB_patchpath.Text;
            settingsInfo.ModPath = TB_modpath.Text;
            settingsInfo.GustPath = TB_gustpath.Text;

            using (StreamWriter steamwriter = new StreamWriter("Settings.json"))
            {
                json = JsonConvert.SerializeObject(settingsInfo, Formatting.Indented);
                steamwriter.Write(json);
                steamwriter.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WriteSettings("Settings.json");
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void B_InPath_Click(object sender, EventArgs e)
        {
            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    TB_patchpath.Text = dialog.FileName;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    TB_modpath.Text = dialog.FileName;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    TB_gustpath.Text = dialog.FileName;
                }
            }
        }
    }
}
