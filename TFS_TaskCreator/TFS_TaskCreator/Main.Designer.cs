namespace TFS_TaskCreator
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.StartCreateBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.reloadSettingsLbl = new System.Windows.Forms.LinkLabel();
            this.openSettingsLbl = new System.Windows.Forms.LinkLabel();
            this.StatusLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartCreateBtn
            // 
            this.StartCreateBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StartCreateBtn.Location = new System.Drawing.Point(12, 111);
            this.StartCreateBtn.Name = "StartCreateBtn";
            this.StartCreateBtn.Size = new System.Drawing.Size(101, 23);
            this.StartCreateBtn.TabIndex = 0;
            this.StartCreateBtn.Text = "Create Now";
            this.StartCreateBtn.UseVisualStyleBackColor = true;
            this.StartCreateBtn.Click += new System.EventHandler(this.StartCreateBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.reloadSettingsLbl);
            this.groupBox1.Controls.Add(this.openSettingsLbl);
            this.groupBox1.Location = new System.Drawing.Point(346, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(119, 45);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // reloadSettingsLbl
            // 
            this.reloadSettingsLbl.AutoSize = true;
            this.reloadSettingsLbl.Location = new System.Drawing.Point(46, 21);
            this.reloadSettingsLbl.Name = "reloadSettingsLbl";
            this.reloadSettingsLbl.Size = new System.Drawing.Size(41, 13);
            this.reloadSettingsLbl.TabIndex = 1;
            this.reloadSettingsLbl.TabStop = true;
            this.reloadSettingsLbl.Text = "Reload";
            this.reloadSettingsLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.reloadSettingsLbl_LinkClicked);
            // 
            // openSettingsLbl
            // 
            this.openSettingsLbl.AutoSize = true;
            this.openSettingsLbl.Location = new System.Drawing.Point(7, 21);
            this.openSettingsLbl.Name = "openSettingsLbl";
            this.openSettingsLbl.Size = new System.Drawing.Size(33, 13);
            this.openSettingsLbl.TabIndex = 0;
            this.openSettingsLbl.TabStop = true;
            this.openSettingsLbl.Text = "Open";
            this.openSettingsLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.openSettingsLbl_LinkClicked);
            // 
            // StatusLbl
            // 
            this.StatusLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StatusLbl.AutoSize = true;
            this.StatusLbl.Location = new System.Drawing.Point(120, 120);
            this.StatusLbl.Name = "StatusLbl";
            this.StatusLbl.Size = new System.Drawing.Size(46, 13);
            this.StatusLbl.TabIndex = 3;
            this.StatusLbl.Text = "Status...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(322, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "Copy strings in the current format:\r\nTitle|Mini-Disc(Acceptrance Criteria)|Story " +
    "Points|Hours|Descriptions";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 146);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StatusLbl);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.StartCreateBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TFS TaskCreator";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartCreateBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel reloadSettingsLbl;
        private System.Windows.Forms.LinkLabel openSettingsLbl;
        private System.Windows.Forms.Label StatusLbl;
        private System.Windows.Forms.Label label1;
    }
}

