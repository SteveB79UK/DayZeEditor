﻿
namespace DayZeEditor
{
    partial class AddItemfromTypes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddItemfromTypes));
            this.panel1 = new System.Windows.Forms.Panel();
            this.CloseButton = new System.Windows.Forms.Button();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.darkButton1 = new DarkUI.Controls.DarkButton();
            this.darkButton2 = new DarkUI.Controls.DarkButton();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.darkButton3 = new DarkUI.Controls.DarkButton();
            this.darkButton4 = new DarkUI.Controls.DarkButton();
            this.darkButton5 = new DarkUI.Controls.DarkButton();
            this.darkTextBox1 = new DarkUI.Controls.DarkTextBox();
            this.darkButton6 = new DarkUI.Controls.DarkButton();
            this.darkButton7 = new DarkUI.Controls.DarkButton();
            this.treeViewMS1 = new TreeViewMS.TreeViewMS();
            this.panel1.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(665, 28);
            this.panel1.TabIndex = 8;
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.BackColor = System.Drawing.Color.Black;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.Color.DarkRed;
            this.CloseButton.Location = new System.Drawing.Point(621, -1);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(41, 28);
            this.CloseButton.TabIndex = 7;
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
            this.TitleLabel.Size = new System.Drawing.Size(124, 15);
            this.TitleLabel.TabIndex = 6;
            this.TitleLabel.Text = "Add Items from Types";
            // 
            // darkButton1
            // 
            this.darkButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.darkButton1.Location = new System.Drawing.Point(547, 408);
            this.darkButton1.Name = "darkButton1";
            this.darkButton1.Padding = new System.Windows.Forms.Padding(5);
            this.darkButton1.Size = new System.Drawing.Size(106, 23);
            this.darkButton1.TabIndex = 10;
            this.darkButton1.Text = "OK";
            // 
            // darkButton2
            // 
            this.darkButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.darkButton2.Location = new System.Drawing.Point(435, 408);
            this.darkButton2.Name = "darkButton2";
            this.darkButton2.Padding = new System.Windows.Forms.Padding(5);
            this.darkButton2.Size = new System.Drawing.Size(106, 23);
            this.darkButton2.TabIndex = 11;
            this.darkButton2.Text = "Cancel";
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox1.ForeColor = System.Drawing.SystemColors.Control;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(433, 60);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(218, 342);
            this.listBox1.TabIndex = 12;
            this.listBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox_DrawItem);
            // 
            // darkButton3
            // 
            this.darkButton3.Location = new System.Drawing.Point(435, 34);
            this.darkButton3.Name = "darkButton3";
            this.darkButton3.Padding = new System.Windows.Forms.Padding(5);
            this.darkButton3.Size = new System.Drawing.Size(106, 23);
            this.darkButton3.TabIndex = 14;
            this.darkButton3.Text = "Remove from List";
            this.darkButton3.Click += new System.EventHandler(this.RemoveItemsButton_Click);
            // 
            // darkButton4
            // 
            this.darkButton4.Location = new System.Drawing.Point(547, 34);
            this.darkButton4.Name = "darkButton4";
            this.darkButton4.Padding = new System.Windows.Forms.Padding(5);
            this.darkButton4.Size = new System.Drawing.Size(106, 23);
            this.darkButton4.TabIndex = 13;
            this.darkButton4.Text = "Add To List";
            this.darkButton4.Click += new System.EventHandler(this.darkButton4_Click);
            // 
            // darkButton5
            // 
            this.darkButton5.Location = new System.Drawing.Point(321, 34);
            this.darkButton5.Name = "darkButton5";
            this.darkButton5.Padding = new System.Windows.Forms.Padding(5);
            this.darkButton5.Size = new System.Drawing.Size(106, 23);
            this.darkButton5.TabIndex = 15;
            this.darkButton5.Tag = "HideUsed";
            this.darkButton5.Text = "Hide Used Types";
            this.darkButton5.Click += new System.EventHandler(this.darkButton5_Click);
            // 
            // darkTextBox1
            // 
            this.darkTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.darkTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.darkTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkTextBox1.Location = new System.Drawing.Point(8, 34);
            this.darkTextBox1.Name = "darkTextBox1";
            this.darkTextBox1.Size = new System.Drawing.Size(193, 20);
            this.darkTextBox1.TabIndex = 16;
            // 
            // darkButton6
            // 
            this.darkButton6.Location = new System.Drawing.Point(207, 34);
            this.darkButton6.Name = "darkButton6";
            this.darkButton6.Padding = new System.Windows.Forms.Padding(5);
            this.darkButton6.Size = new System.Drawing.Size(41, 23);
            this.darkButton6.TabIndex = 17;
            this.darkButton6.Text = "Find";
            this.darkButton6.Click += new System.EventHandler(this.darkButton6_Click);
            // 
            // darkButton7
            // 
            this.darkButton7.Location = new System.Drawing.Point(251, 34);
            this.darkButton7.Name = "darkButton7";
            this.darkButton7.Padding = new System.Windows.Forms.Padding(5);
            this.darkButton7.Size = new System.Drawing.Size(64, 23);
            this.darkButton7.TabIndex = 18;
            this.darkButton7.Text = "Find Next";
            this.darkButton7.Visible = false;
            this.darkButton7.Click += new System.EventHandler(this.darkButton7_Click);
            // 
            // treeViewMS1
            // 
            this.treeViewMS1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.treeViewMS1.ForeColor = System.Drawing.SystemColors.Control;
            this.treeViewMS1.HideSelection = false;
            this.treeViewMS1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.treeViewMS1.Location = new System.Drawing.Point(8, 60);
            this.treeViewMS1.Name = "treeViewMS1";
            this.treeViewMS1.SelectedNodes = ((System.Collections.ArrayList)(resources.GetObject("treeViewMS1.SelectedNodes")));
            this.treeViewMS1.SetMultiselect = true;
            this.treeViewMS1.Size = new System.Drawing.Size(419, 371);
            this.treeViewMS1.TabIndex = 19;
            this.treeViewMS1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeViewMS1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // AddItemfromTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 443);
            this.Controls.Add(this.treeViewMS1);
            this.Controls.Add(this.darkButton7);
            this.Controls.Add(this.darkButton6);
            this.Controls.Add(this.darkTextBox1);
            this.Controls.Add(this.darkButton5);
            this.Controls.Add(this.darkButton3);
            this.Controls.Add(this.darkButton4);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.darkButton2);
            this.Controls.Add(this.darkButton1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddItemfromTypes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddItemfromTypes";
            this.Load += new System.EventHandler(this.AddItemfromTypes_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label TitleLabel;
        private DarkUI.Controls.DarkButton darkButton1;
        private DarkUI.Controls.DarkButton darkButton2;
        private System.Windows.Forms.ListBox listBox1;
        private DarkUI.Controls.DarkButton darkButton3;
        private DarkUI.Controls.DarkButton darkButton4;
        private System.Windows.Forms.Button CloseButton;
        private DarkUI.Controls.DarkButton darkButton5;
        private DarkUI.Controls.DarkTextBox darkTextBox1;
        private DarkUI.Controls.DarkButton darkButton6;
        private DarkUI.Controls.DarkButton darkButton7;
        private TreeViewMS.TreeViewMS treeViewMS1;
    }
}