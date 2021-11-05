﻿
namespace DayZeEditor
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.CloseButton = new System.Windows.Forms.Button();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SlidePanel = new System.Windows.Forms.Panel();
            this.Slidelabel = new System.Windows.Forms.PictureBox();
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ProjectsButton = new System.Windows.Forms.ToolStripButton();
            this.TypeManButton = new System.Windows.Forms.ToolStripButton();
            this.TraderManButton = new System.Windows.Forms.ToolStripButton();
            this.ExpansionSettingsButton = new System.Windows.Forms.ToolStripButton();
            this.MarketButton = new System.Windows.Forms.ToolStripButton();
            this.LootchestButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.HidePBox = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.darkStatusStrip1 = new DarkUI.Controls.DarkStatusStrip();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SlidePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Slidelabel)).BeginInit();
            this.ToolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HidePBox)).BeginInit();
            this.darkStatusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.CloseButton);
            this.panel1.Controls.Add(this.TitleLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1148, 28);
            this.panel1.TabIndex = 7;
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.BackColor = System.Drawing.Color.Black;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.Color.DarkRed;
            this.CloseButton.Location = new System.Drawing.Point(1107, 0);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(41, 28);
            this.CloseButton.TabIndex = 6;
            this.CloseButton.Text = "X";
            this.CloseButton.UseVisualStyleBackColor = false;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(110)))), ((int)(((byte)(175)))));
            this.TitleLabel.Location = new System.Drawing.Point(5, 6);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(74, 15);
            this.TitleLabel.TabIndex = 6;
            this.TitleLabel.Text = "DayZeEditor";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Conflict";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Type";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Priority";
            // 
            // SlidePanel
            // 
            this.SlidePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SlidePanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.SlidePanel.Controls.Add(this.Slidelabel);
            this.SlidePanel.Controls.Add(this.ToolStrip1);
            this.SlidePanel.Controls.Add(this.HidePBox);
            this.SlidePanel.Location = new System.Drawing.Point(0, 28);
            this.SlidePanel.Name = "SlidePanel";
            this.SlidePanel.Size = new System.Drawing.Size(140, 639);
            this.SlidePanel.TabIndex = 43;
            this.SlidePanel.Click += new System.EventHandler(this.Slide_Click);
            // 
            // Slidelabel
            // 
            this.Slidelabel.BackColor = System.Drawing.Color.Transparent;
            this.Slidelabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Slidelabel.Image = ((System.Drawing.Image)(resources.GetObject("Slidelabel.Image")));
            this.Slidelabel.Location = new System.Drawing.Point(5, 11);
            this.Slidelabel.Name = "Slidelabel";
            this.Slidelabel.Size = new System.Drawing.Size(20, 100);
            this.Slidelabel.TabIndex = 41;
            this.Slidelabel.TabStop = false;
            this.Slidelabel.Click += new System.EventHandler(this.Slide_Click);
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ToolStrip1.AutoSize = false;
            this.ToolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.ToolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProjectsButton,
            this.TypeManButton,
            this.TraderManButton,
            this.ExpansionSettingsButton,
            this.MarketButton,
            this.LootchestButton,
            this.toolStripButton2});
            this.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.ToolStrip1.Location = new System.Drawing.Point(26, 29);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Padding = new System.Windows.Forms.Padding(5);
            this.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ToolStrip1.Size = new System.Drawing.Size(114, 629);
            this.ToolStrip1.TabIndex = 39;
            this.ToolStrip1.Text = "toolStrip4";
            // 
            // ProjectsButton
            // 
            this.ProjectsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ProjectsButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ProjectsButton.Image = ((System.Drawing.Image)(resources.GetObject("ProjectsButton.Image")));
            this.ProjectsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ProjectsButton.Name = "ProjectsButton";
            this.ProjectsButton.Size = new System.Drawing.Size(103, 19);
            this.ProjectsButton.Text = "Projects";
            this.ProjectsButton.Click += new System.EventHandler(this.ProjectsButton_Click);
            // 
            // TypeManButton
            // 
            this.TypeManButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TypeManButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.TypeManButton.Image = ((System.Drawing.Image)(resources.GetObject("TypeManButton.Image")));
            this.TypeManButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TypeManButton.Name = "TypeManButton";
            this.TypeManButton.Size = new System.Drawing.Size(103, 19);
            this.TypeManButton.Text = "Economy Manager";
            this.TypeManButton.Click += new System.EventHandler(this.EconomyManagerButton_Click);
            // 
            // TraderManButton
            // 
            this.TraderManButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TraderManButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.TraderManButton.Image = ((System.Drawing.Image)(resources.GetObject("TraderManButton.Image")));
            this.TraderManButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TraderManButton.Name = "TraderManButton";
            this.TraderManButton.Size = new System.Drawing.Size(103, 19);
            this.TraderManButton.Text = "Trader Manager";
            this.TraderManButton.Click += new System.EventHandler(this.TraderManButton_Click);
            // 
            // ExpansionSettingsButton
            // 
            this.ExpansionSettingsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ExpansionSettingsButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ExpansionSettingsButton.Image = ((System.Drawing.Image)(resources.GetObject("ExpansionSettingsButton.Image")));
            this.ExpansionSettingsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExpansionSettingsButton.Name = "ExpansionSettingsButton";
            this.ExpansionSettingsButton.Size = new System.Drawing.Size(103, 19);
            this.ExpansionSettingsButton.Text = "Expansion Settings";
            this.ExpansionSettingsButton.Visible = false;
            this.ExpansionSettingsButton.Click += new System.EventHandler(this.ExpansionSettingsButton_Click);
            // 
            // MarketButton
            // 
            this.MarketButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MarketButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.MarketButton.Image = ((System.Drawing.Image)(resources.GetObject("MarketButton.Image")));
            this.MarketButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MarketButton.Name = "MarketButton";
            this.MarketButton.Size = new System.Drawing.Size(103, 19);
            this.MarketButton.Text = "Expansion Market";
            this.MarketButton.Click += new System.EventHandler(this.MarketButton_Click);
            // 
            // LootchestButton
            // 
            this.LootchestButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.LootchestButton.ForeColor = System.Drawing.SystemColors.Control;
            this.LootchestButton.Image = ((System.Drawing.Image)(resources.GetObject("LootchestButton.Image")));
            this.LootchestButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LootchestButton.Name = "LootchestButton";
            this.LootchestButton.Size = new System.Drawing.Size(103, 19);
            this.LootchestButton.Text = "Loot Chest Table";
            this.LootchestButton.Click += new System.EventHandler(this.Lootchest_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(103, 19);
            this.toolStripButton2.Text = "Toxic Zone";
            this.toolStripButton2.Click += new System.EventHandler(this.ToxicZone_Click);
            // 
            // HidePBox
            // 
            this.HidePBox.BackColor = System.Drawing.Color.Transparent;
            this.HidePBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("HidePBox.BackgroundImage")));
            this.HidePBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.HidePBox.InitialImage = null;
            this.HidePBox.Location = new System.Drawing.Point(52, 6);
            this.HidePBox.Name = "HidePBox";
            this.HidePBox.Size = new System.Drawing.Size(45, 20);
            this.HidePBox.TabIndex = 38;
            this.HidePBox.TabStop = false;
            this.HidePBox.Click += new System.EventHandler(this.Slide_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(10, 15);
            this.toolStripStatusLabel2.Text = " ";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(33, 15);
            this.toolStripStatusLabel1.Text = "hello";
            // 
            // darkStatusStrip1
            // 
            this.darkStatusStrip1.AutoSize = false;
            this.darkStatusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.darkStatusStrip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkStatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel1});
            this.darkStatusStrip1.Location = new System.Drawing.Point(0, 667);
            this.darkStatusStrip1.Name = "darkStatusStrip1";
            this.darkStatusStrip1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 3);
            this.darkStatusStrip1.Size = new System.Drawing.Size(1148, 28);
            this.darkStatusStrip1.SizingGrip = false;
            this.darkStatusStrip1.TabIndex = 41;
            this.darkStatusStrip1.Text = "darkStatusStrip1";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBox1.Location = new System.Drawing.Point(1023, 675);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(94, 17);
            this.checkBox1.TabIndex = 40;
            this.checkBox1.Text = "Show Console";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Location = new System.Drawing.Point(1123, 670);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(25, 25);
            this.panel2.TabIndex = 45;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 695);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.SlidePanel);
            this.Controls.Add(this.darkStatusStrip1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.SlidePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Slidelabel)).EndInit();
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HidePBox)).EndInit();
            this.darkStatusStrip1.ResumeLayout(false);
            this.darkStatusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Panel SlidePanel;
        private System.Windows.Forms.ToolStrip ToolStrip1;
        private System.Windows.Forms.ToolStripButton TypeManButton;
        private System.Windows.Forms.PictureBox HidePBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripButton TraderManButton;
        private System.Windows.Forms.ToolStripButton ExpansionSettingsButton;
        private System.Windows.Forms.ToolStripButton MarketButton;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private DarkUI.Controls.DarkStatusStrip darkStatusStrip1;
        private System.Windows.Forms.ToolStripButton ProjectsButton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.PictureBox Slidelabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripButton LootchestButton;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
    }
}

