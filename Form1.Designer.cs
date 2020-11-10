namespace THAT
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.B_File = new System.Windows.Forms.ToolStripDropDownButton();
            this.B_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.B_Go = new System.Windows.Forms.ToolStripMenuItem();
            this.B_Options = new System.Windows.Forms.ToolStripDropDownButton();
            this.TB_FilePath = new System.Windows.Forms.TextBox();
            this.RTB_Output = new System.Windows.Forms.RichTextBox();
            this.B_BatchCompress = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.B_File,
            this.B_Options});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(463, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // B_File
            // 
            this.B_File.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.B_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.B_Open,
            this.B_Go});
            this.B_File.Image = ((System.Drawing.Image)(resources.GetObject("B_File.Image")));
            this.B_File.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.B_File.Name = "B_File";
            this.B_File.Size = new System.Drawing.Size(38, 22);
            this.B_File.Text = "File";
            // 
            // B_Open
            // 
            this.B_Open.Name = "B_Open";
            this.B_Open.Size = new System.Drawing.Size(103, 22);
            this.B_Open.Text = "Open";
            this.B_Open.Click += new System.EventHandler(this.B_Open_Click);
            // 
            // B_Go
            // 
            this.B_Go.Name = "B_Go";
            this.B_Go.Size = new System.Drawing.Size(103, 22);
            this.B_Go.Text = "Go";
            this.B_Go.Click += new System.EventHandler(this.B_Go_Click);
            // 
            // B_Options
            // 
            this.B_Options.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.B_Options.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.B_BatchCompress});
            this.B_Options.Image = ((System.Drawing.Image)(resources.GetObject("B_Options.Image")));
            this.B_Options.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.B_Options.Name = "B_Options";
            this.B_Options.Size = new System.Drawing.Size(62, 22);
            this.B_Options.Text = "Options";
            // 
            // TB_FilePath
            // 
            this.TB_FilePath.Location = new System.Drawing.Point(118, 3);
            this.TB_FilePath.Name = "TB_FilePath";
            this.TB_FilePath.ReadOnly = true;
            this.TB_FilePath.Size = new System.Drawing.Size(336, 20);
            this.TB_FilePath.TabIndex = 1;
            this.TB_FilePath.TextChanged += new System.EventHandler(this.TB_FilePath_TextChanged);
            // 
            // RTB_Output
            // 
            this.RTB_Output.Location = new System.Drawing.Point(12, 29);
            this.RTB_Output.Name = "RTB_Output";
            this.RTB_Output.ReadOnly = true;
            this.RTB_Output.Size = new System.Drawing.Size(442, 291);
            this.RTB_Output.TabIndex = 2;
            this.RTB_Output.Text = "Open a file, or Drag/Drop several! Click this box to clear its text.\n";
            this.RTB_Output.Click += new System.EventHandler(this.RTB_Output_Click);
            // 
            // B_BatchCompress
            // 
            this.B_BatchCompress.CheckOnClick = true;
            this.B_BatchCompress.Name = "B_BatchCompress";
            this.B_BatchCompress.Size = new System.Drawing.Size(180, 22);
            this.B_BatchCompress.Text = "Batch Compress";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 332);
            this.Controls.Add(this.RTB_Output);
            this.Controls.Add(this.TB_FilePath);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Three Houses Archive Tool";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton B_File;
        private System.Windows.Forms.ToolStripDropDownButton B_Options;
        private System.Windows.Forms.TextBox TB_FilePath;
        private System.Windows.Forms.ToolStripMenuItem B_Open;
        private System.Windows.Forms.ToolStripMenuItem B_Go;
        private System.Windows.Forms.RichTextBox RTB_Output;
        private System.Windows.Forms.ToolStripMenuItem B_BatchCompress;
    }
}

