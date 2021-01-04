namespace THAT
{
    partial class INFOPatcher
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.B_InPath = new System.Windows.Forms.Button();
            this.TB_InPath = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.B_OriPath = new System.Windows.Forms.Button();
            this.TB_OriPath = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.CB_Log = new System.Windows.Forms.CheckBox();
            this.B_Patch = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.B_InPath);
            this.groupBox1.Controls.Add(this.TB_InPath);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(351, 49);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mods Folder";
            // 
            // B_InPath
            // 
            this.B_InPath.Location = new System.Drawing.Point(311, 17);
            this.B_InPath.Name = "B_InPath";
            this.B_InPath.Size = new System.Drawing.Size(34, 23);
            this.B_InPath.TabIndex = 2;
            this.B_InPath.Text = "...";
            this.B_InPath.UseVisualStyleBackColor = true;
            this.B_InPath.Click += new System.EventHandler(this.B_InPath_Click);
            // 
            // TB_InPath
            // 
            this.TB_InPath.Location = new System.Drawing.Point(6, 19);
            this.TB_InPath.Name = "TB_InPath";
            this.TB_InPath.Size = new System.Drawing.Size(299, 20);
            this.TB_InPath.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.B_OriPath);
            this.groupBox2.Controls.Add(this.TB_OriPath);
            this.groupBox2.Location = new System.Drawing.Point(12, 67);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(351, 49);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Original Patch";
            // 
            // B_OriPath
            // 
            this.B_OriPath.Location = new System.Drawing.Point(311, 17);
            this.B_OriPath.Name = "B_OriPath";
            this.B_OriPath.Size = new System.Drawing.Size(34, 23);
            this.B_OriPath.TabIndex = 2;
            this.B_OriPath.Text = "...";
            this.B_OriPath.UseVisualStyleBackColor = true;
            this.B_OriPath.Click += new System.EventHandler(this.B_OriPath_Click);
            // 
            // TB_OriPath
            // 
            this.TB_OriPath.Location = new System.Drawing.Point(6, 19);
            this.TB_OriPath.Name = "TB_OriPath";
            this.TB_OriPath.Size = new System.Drawing.Size(299, 20);
            this.TB_OriPath.TabIndex = 2;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox1.Location = new System.Drawing.Point(12, 156);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(351, 234);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // CB_Log
            // 
            this.CB_Log.AutoSize = true;
            this.CB_Log.Checked = true;
            this.CB_Log.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_Log.Location = new System.Drawing.Point(12, 128);
            this.CB_Log.Name = "CB_Log";
            this.CB_Log.Size = new System.Drawing.Size(79, 17);
            this.CB_Log.TabIndex = 5;
            this.CB_Log.Text = "Log Output";
            this.CB_Log.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CB_Log.UseVisualStyleBackColor = true;
            // 
            // B_Patch
            // 
            this.B_Patch.Location = new System.Drawing.Point(244, 124);
            this.B_Patch.Name = "B_Patch";
            this.B_Patch.Size = new System.Drawing.Size(113, 23);
            this.B_Patch.TabIndex = 6;
            this.B_Patch.Text = "Patch";
            this.B_Patch.UseVisualStyleBackColor = true;
            this.B_Patch.Click += new System.EventHandler(this.B_Patch_Click);
            // 
            // INFOPatcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 398);
            this.Controls.Add(this.B_Patch);
            this.Controls.Add(this.CB_Log);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "INFOPatcher";
            this.Text = "INFO Patcher";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button B_InPath;
        private System.Windows.Forms.TextBox TB_InPath;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button B_OriPath;
        private System.Windows.Forms.TextBox TB_OriPath;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox CB_Log;
        private System.Windows.Forms.Button B_Patch;
    }
}