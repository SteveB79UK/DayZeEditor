﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using DarkUI.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Globalization;
using DayZeLib;

namespace DayZeEditor
{
    public partial class MainForm : DarkForm
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        private static bool hidden;
        public static String ProjectsJson = Application.StartupPath + "\\Project\\Projects.json";
        public ProjectList Projects;

        public MainForm()
        {
            InitializeComponent();
            Form_Controls.InitializeForm_Controls
            (
                this,
                panel1,
                panel2,
                TitleLabel,
                CloseButton
            );
            SlidePanel.Width = 30;
            hidden = true;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            var culture = CultureInfo.GetCultureInfo("en-GB");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            if (File.Exists(ProjectsJson))
            {
                Projects = (JsonSerializer.Deserialize<ProjectList>(File.ReadAllText(ProjectsJson)));

                if (Projects.getActiveProject() != null)
                {
                    if (Projects.getActiveProject().ProfilePath == null)
                    {
                        Projects.getActiveProject().ProfilePath = "profile";
                        Projects.SaveProject();
                    }
                    Projects.getActiveProject().seteconomycore();
                    Projects.getActiveProject().seteconomydefinitions();
                    Projects.getActiveProject().setuserdefinitions();
                    Projects.getActiveProject().SetEvents();
                    Projects.getActiveProject().setVanillaTypes();
                    Projects.getActiveProject().SetModListtypes();
                    Console.WriteLine(Projects.ActiveProject + " is the Current Active Project");
                    Console.WriteLine("Project is running Expansion..... " + Projects.getActiveProject().isExpansion.ToString());
                    Console.WriteLine("Project is Running Dr Jones Trader...." + Projects.getActiveProject().usingDrJoneTrader.ToString());
                    Console.WriteLine("Project is Running Dr Expansion Market...." + Projects.getActiveProject().usingexpansionMarket.ToString());
                    Console.WriteLine("Project is Running the following Mods....");
                    toolStripStatusLabel1.Text = Projects.ActiveProject + " is the Current Active Project";
                }
                else
                {
                    TypeManButton.Visible = false;
                    TraderManButton.Visible = false;
                    ExpansionSettingsButton.Visible = false;
                    MarketButton.Visible = false;
                    Console.WriteLine("No Active Project Found, Please Load a Project from the Projects panel.....");
                    toolStripStatusLabel1.Text = "No Active Project Found, Please Load a Project from the Projects panel.....";
                }
            }
            else
            {
                Projects = new ProjectList{};
                Projects.SaveProject();
                TypeManButton.Visible = false;
                TraderManButton.Visible = false;
                ExpansionSettingsButton.Visible = false;
                MarketButton.Visible = false;
                Console.WriteLine("No Projects Found, Please Create a new Project from the Projects panel.....");
                toolStripStatusLabel1.Text = "No Projects Found, Please Create a new Project from the Projects panel.....";
            }
        }
        private void closemdichildren()
        {
            if (MdiChildren.Length >= 1)
                MdiChildren[0].Close();
        }
        private void Slide_Click(object sender, EventArgs e)
        {
            if(sender is PictureBox)
            {
                PictureBox pb = sender as PictureBox;
                if (pb.Name == "HidePBox")
                {
                    ToolStrip1.Visible = false;
                    if (!hidden)
                        timer1.Start();
                }
                else if (pb.Name == "Slidelabel")
                {
                    ShowButtons();
                    timer1.Start();
                }
            }
            else if (sender is Panel)
            {
                Panel p = sender as Panel;
                if (p.Name == "SlidePanel")
                {
                    ShowButtons();
                    timer1.Start();
                }
            }
        }

        private void ShowButtons()
        {
            ToolStrip1.Visible = true;
            if (Projects.getActiveProject() != null)
            {
                TypeManButton.Visible = true;
                if (Projects.getActiveProject().usingDrJoneTrader)
                    TraderManButton.Visible = true;
                else
                    TraderManButton.Visible = false;
                if (Projects.getActiveProject().usingexpansionMarket)
                    MarketButton.Visible = true;
                else
                    MarketButton.Visible = false;
                if (Projects.getActiveProject().isExpansion)
                    ExpansionSettingsButton.Visible = true;
                else
                    ExpansionSettingsButton.Visible = false;
                if (Projects.getActiveProject().usingexpansionMarket)
                    MarketButton.Visible = true;
                else
                    MarketButton.Visible = false;
                if (File.Exists(Projects.getActiveProject().projectFullName + "\\" + Projects.getActiveProject().ProfilePath + "\\CJ_LootChests\\LootChests_V103.json"))
                    LootchestButton.Visible = true;
                else
                    LootchestButton.Visible = false;




            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (hidden)
            {
                SlidePanel.Width = SlidePanel.Width + 10;
                if (SlidePanel.Width == 140)
                {
                    timer1.Stop();
                    hidden = false;
                    Refresh();
                }
            }
            else
            {
                SlidePanel.Width = SlidePanel.Width - 10;
                if (SlidePanel.Width == 30)
                {
                    timer1.Stop();
                    hidden = true;
                    Refresh();
                }
            }
        }
        private void ProjectsButton_Click(object sender, EventArgs e)
        {
            ProjectPanel _TM = Application.OpenForms["ProjectPanel"] as ProjectPanel;
            if (_TM != null)
            {
                _TM.WindowState = FormWindowState.Normal;
                _TM.BringToFront();
                _TM.Activate();
            }
            else
            {
                closemdichildren();
                _TM = new ProjectPanel
                {
                    MdiParent = this,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right,
                    Location = new System.Drawing.Point(30, 0),
                    Size = Form_Controls.Formsize - new System.Drawing.Size(37, 61),
                    projects = Projects
                };
                _TM.Show();
            }
            timer1.Start();
        }
        private void EconomyManagerButton_Click(object sender, EventArgs e)
        {
            Economy_Manager _TM = Application.OpenForms["Types_Manager"] as Economy_Manager;
            if (_TM != null)
            {
                _TM.WindowState = FormWindowState.Normal;
                _TM.BringToFront();
                _TM.Activate();
            }
            else
            {
                closemdichildren();
                _TM = new Economy_Manager
                {
                    MdiParent = this,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right,
                    Location = new System.Drawing.Point(30, 0),
                    Size = Form_Controls.Formsize - new System.Drawing.Size(37, 61),
                    currentproject = Projects.getActiveProject()
                };
                _TM.Show();
            }
            timer1.Start();
        }
        private void TraderManButton_Click(object sender, EventArgs e)
        {
            DRJonesTrader_Manager _TM = Application.OpenForms["Trader_Manager"] as DRJonesTrader_Manager;
            if (_TM != null)
            {
                _TM.WindowState = FormWindowState.Normal;
                _TM.BringToFront();
                _TM.Activate();
            }
            else
            {
                closemdichildren();
                _TM = new DRJonesTrader_Manager
                {
                    MdiParent = this,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right,
                    Location = new System.Drawing.Point(30, 0),
                    Size = Form_Controls.Formsize - new System.Drawing.Size(37, 61),
                    currentproject = Projects.getActiveProject()
                };
                _TM.Show();
                Console.WriteLine("loading Dr Jones Trader manager....");
            }
            timer1.Start();
        }
        private void ExpansionSettingsButton_Click(object sender, EventArgs e)
        {
            ExpansionSettings _TM = Application.OpenForms["ExpansionSettings"] as ExpansionSettings;
            if (_TM != null)
            {
                _TM.WindowState = FormWindowState.Normal;
                _TM.BringToFront();
                _TM.Activate();
            }
            else
            {
                closemdichildren();
                _TM = new ExpansionSettings
                {
                    MdiParent = this,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right,
                    Location = new System.Drawing.Point(30, 0),
                    Size = Form_Controls.Formsize - new System.Drawing.Size(37, 61),
                    currentproject = Projects.getActiveProject()
                };
                _TM.Show();
            }
            timer1.Start();
        }
        private void MarketButton_Click(object sender, EventArgs e)
        {
            ExpansionMarket _TM = Application.OpenForms["ExpansionMarket"] as ExpansionMarket;
            if (_TM != null)
            {
                _TM.WindowState = FormWindowState.Normal;
                _TM.BringToFront();
                _TM.Activate();
            }
            else
            {
                closemdichildren();
                _TM = new ExpansionMarket
                {
                    MdiParent = this,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right,
                    Location = new System.Drawing.Point(30, 0),
                    Size = Form_Controls.Formsize - new System.Drawing.Size(37, 61),
                    currentproject = Projects.getActiveProject()
                };
                _TM.Show();
            }
            timer1.Start();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var handle = GetConsoleWindow();
            if (checkBox1.Checked)
                ShowWindow(handle, SW_SHOW);
            else
                ShowWindow(handle, SW_HIDE);
        }

        //        MapData data = new MapData(Application.StartupPath + "//Maps//map_output.txt");
        //        data.CreateNewData();

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Lootchest _TM = Application.OpenForms["Lootchest"] as Lootchest;
            if (_TM != null)
            {
                _TM.WindowState = FormWindowState.Normal;
                _TM.BringToFront();
                _TM.Activate();
            }
            else
            {
                closemdichildren();
                _TM = new Lootchest
                {
                    MdiParent = this,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right,
                    Location = new System.Drawing.Point(30, 0),
                    Size = Form_Controls.Formsize - new System.Drawing.Size(37, 61),
                    currentproject = Projects.getActiveProject()
                };
                _TM.Show();
            }
            timer1.Start();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ToxicZone _TM = Application.OpenForms["ToxicZone"] as ToxicZone;
            if (_TM != null)
            {
                _TM.WindowState = FormWindowState.Normal;
                _TM.BringToFront();
                _TM.Activate();
            }
            else
            {
                closemdichildren();
                _TM = new ToxicZone
                {
                    MdiParent = this,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right,
                    Location = new System.Drawing.Point(30, 0),
                    Size = Form_Controls.Formsize - new System.Drawing.Size(37, 61),
                    currentproject = Projects.getActiveProject()
                };
                _TM.Show();
            }
            timer1.Start();
        }
    }
}
