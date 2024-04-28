﻿using DarkUI.Forms;
using DayZeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DayZeEditor
{
    public partial class MPGSpawnerManager : DarkForm
    {
        public Project currentproject { get; set; }
        public string Projectname { get; private set; }

        public TypesFile vanillatypes;
        public TypesFile Expansiontypes;
        public List<TypesFile> ModTypes;
        public string MPGSpawnexConfigPath { get; private set; }
        public string MPG_Spawner_PointsConfigPath { get;set;}
        public MPG_SPWNR_ModConfig MPG_SPWNR_ModConfig;
        public MPG_Spawner_PointsConfig currentSpawnerPointsFile { get; set; }
        public MPG_Spawner_PointConfig currentSpawnerPoint { get; set; }

        public  Vec3PandR currentspawnPosition { get; set; }
        public bool useraction = false;
        public void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            ListBox lb = sender as ListBox;
            var CurrentItemWidth = (int)this.CreateGraphics().MeasureString(lb.Items[lb.Items.Count - 1].ToString(), lb.Font, TextRenderer.MeasureText(lb.Items[lb.Items.Count - 1].ToString(), new Font("Arial", 20.0F))).Width;
            lb.HorizontalExtent = CurrentItemWidth + 5;
            e.DrawBackground();
            Brush myBrush = Brushes.Black;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            }
            else
            {
                myBrush = Brushes.White;
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, 63, 65)), e.Bounds);
            }
            e.Graphics.DrawString(lb.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds);
            e.DrawFocusRectangle();

        }

        public MPGSpawnerManager()
        {
            InitializeComponent();
        }
        private void MPGSpawnerManager_Load(object sender, EventArgs e)
        {
            useraction = false;

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            customCulture.NumberFormat.NumberGroupSeparator = "";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            Projectname = currentproject.ProjectName;
            vanillatypes = currentproject.getvanillatypes();
            ModTypes = currentproject.getModList();

            useraction = false;
            MPGSpawnexConfigPath = currentproject.projectFullName + "\\" + currentproject.ProfilePath + "\\MPG_Spawner\\Config.json";
            MPG_Spawner_PointsConfigPath = currentproject.projectFullName + "\\" + currentproject.ProfilePath + "\\MPG_Spawner\\Points\\";
            MPG_SPWNR_ModConfig = JsonSerializer.Deserialize<MPG_SPWNR_ModConfig>(File.ReadAllText(MPGSpawnexConfigPath));
            MPG_SPWNR_ModConfig.GetallSpawnerpointsfiles(MPG_Spawner_PointsConfigPath);
            MPG_SPWNR_ModConfig.Getalllists();
            MPG_SPWNR_ModConfig.isDirty = false;
            MPG_SPWNR_ModConfig.Filename = MPGSpawnexConfigPath;

            configVersionNUD.Value = MPG_SPWNR_ModConfig.configVersion;
            isModDisabledCB.Checked = MPG_SPWNR_ModConfig.isModDisabled == 1 ? true : false;
            isDebugEnabledCB.Checked = MPG_SPWNR_ModConfig.isDebugEnabled == 1 ? true : false;

            SpawnerPointfilesLB.DisplayMember = "DisplayName";
            SpawnerPointfilesLB.ValueMember = "Value";
            SpawnerPointfilesLB.DataSource = MPG_SPWNR_ModConfig.MPG_Spawner_PointsConfigs;

            pictureBox1.BackgroundImage = Image.FromFile(Application.StartupPath + currentproject.MapPath); // Livonia maop size is 12800 x 12800, 0,0 bottom left, center 6400 x 6400
            pictureBox1.Size = new Size(currentproject.MapSize, currentproject.MapSize);
            pictureBox1.Paint += new PaintEventHandler(DrawMPGSpawner);
            trackBar1.Value = 1;
            SetMPGSpawnerScale();

            tabControl1.ItemSize = new Size(0, 1);
            toolStripButton12.Checked = true;
            useraction = true;
        }
        private void MPGSpawnerManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MPG_SPWNR_ModConfig.isDirty)
            {
                DialogResult dialogResult = MessageBox.Show("You have Unsaved Changes, do you wish to save", "Unsaved Changes found", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Savefiles();
                }
            }
        }
        private void Savefiles()
        {
            List<string> midifiedfiles = new List<string>();
            string SaveTime = DateTime.Now.ToString("ddMMyy_HHmm");
            if (MPG_SPWNR_ModConfig.isDirty)
            {
                if (currentproject.Createbackups && File.Exists(MPG_SPWNR_ModConfig.Filename))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(MPG_SPWNR_ModConfig.Filename) + "\\Backup\\" + SaveTime);
                    File.Copy(MPG_SPWNR_ModConfig.Filename, Path.GetDirectoryName(MPG_SPWNR_ModConfig.Filename) + "\\Backup\\" + SaveTime + "\\" + Path.GetFileNameWithoutExtension(MPG_SPWNR_ModConfig.Filename) + ".bak", true);
                }
                MPG_SPWNR_ModConfig.SetSpawnerPointFiles();
                MPG_SPWNR_ModConfig.isDirty = false;
                var options = new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
                string jsonString = JsonSerializer.Serialize(MPG_SPWNR_ModConfig, options);
                File.WriteAllText(MPG_SPWNR_ModConfig.Filename, jsonString);
                midifiedfiles.Add(Path.GetFileName(MPG_SPWNR_ModConfig.Filename));
            }
            foreach(MPG_Spawner_PointsConfig pointsfile in MPG_SPWNR_ModConfig.MPG_Spawner_PointsConfigs)
            {
                if (pointsfile.isDirty)
                {
                    pointsfile.SetAllLists();
                    pointsfile.isDirty = false;
                    if (currentproject.Createbackups && File.Exists(currentproject.projectFullName + "\\" + currentproject.ProfilePath + "\\MPG_Spawner\\Points\\" + pointsfile.Filename + ".json"))
                    {
                        Directory.CreateDirectory(MPG_SPWNR_ModConfig + "\\Backup\\" + SaveTime);
                        File.Copy(MPG_Spawner_PointsConfigPath + pointsfile.Filename + ".json", Path.GetDirectoryName(MPG_SPWNR_ModConfig.Filename) + "\\Backup\\" + SaveTime + "\\" + pointsfile.Filename + ".bak", true);
                    }
                    var options = new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
                    string jsonString = JsonSerializer.Serialize(pointsfile.Points, options);
                    File.WriteAllText(MPG_Spawner_PointsConfigPath + pointsfile.Filename + ".json", jsonString);
                    midifiedfiles.Add(Path.GetFileName(pointsfile.Filename + ".json"));
                }
            }
            string message = "The Following Files were saved....\n";
            int i = 0;
            foreach (string l in midifiedfiles)
            {
                if (i == 5)
                {
                    message += l + "\n";
                    i = 0;
                }
                else
                {
                    message += l + ", ";
                    i++;
                }
            }
            if (midifiedfiles.Count == 0)
                message = "";
            if (MPG_SPWNR_ModConfig.Markedfordelete != null)
            {
                message += "\nThe following Points file(s) were Removed\n";
                i = 0;
                foreach (MPG_Spawner_PointsConfig del in MPG_SPWNR_ModConfig.Markedfordelete)
                {
                    del.backupandDelete(MPG_Spawner_PointsConfigPath);
                    midifiedfiles.Add(del.Filename);
                    if (i == 5)
                    {
                        message += del.Filename + "\n";
                        i = 0;
                    }
                    else
                    {
                        message += del.Filename + ", ";
                        i++;
                    }
                }
                MPG_SPWNR_ModConfig.Markedfordelete = null;
            }
            if (midifiedfiles.Count > 0)
                MessageBox.Show(message, "Done", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            else
                MessageBox.Show("No changes were made.", "Nothing Saved", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        private void SaveFileButton_Click(object sender, EventArgs e)
        {
            Savefiles();
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Process.Start(currentproject.projectFullName + "\\" + currentproject.ProfilePath + "\\MPG_Spawner\\");
        }
        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }
        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripButton12.Checked = false;
            toolStripButton13.Checked = false;
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    toolStripButton12.Checked = true;
                    break;
                case 1:
                    toolStripButton13.Checked = true;
                    break;
            }
        }
        private void SpawnerPointfilesLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SpawnerPointfilesLB.SelectedItems.Count < 1) return;
            currentSpawnerPointsFile = SpawnerPointfilesLB.SelectedItem as MPG_Spawner_PointsConfig;
            useraction = false;
            SpawnerPointsLB.DisplayMember = "DisplayName";
            SpawnerPointsLB.ValueMember = "Value";
            SpawnerPointsLB.DataSource = currentSpawnerPointsFile.Points;
            pictureBox1.Invalidate();
            useraction = true;
        }
        private void darkButton5_Click(object sender, EventArgs e)
        {
            AddNewfileName addnewfilename = new AddNewfileName();
            DialogResult result = addnewfilename.ShowDialog();
            if (result == DialogResult.OK)
            {
                MPG_Spawner_PointsConfig newMPG_Spawner_PointsConfig = new MPG_Spawner_PointsConfig()
                {
                    Points = new BindingList<MPG_Spawner_PointConfig>(),
                    Filename = addnewfilename.NewFileName,
                    isDirty = true
                };
                MPG_SPWNR_ModConfig.MPG_Spawner_PointsConfigs.Add(newMPG_Spawner_PointsConfig);
                MPG_SPWNR_ModConfig.isDirty = true;
                SpawnerPointfilesLB.Invalidate();
            }
        }
        private void darkButton6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This Will Remove All reference to this SpawnerPointsfile, Are you sure you want to do this?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                MPG_SPWNR_ModConfig.RemovePointsfile(currentSpawnerPointsFile);
            }
        }
        private void isModDisabledCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            MPG_SPWNR_ModConfig.isModDisabled = isModDisabledCB.Checked == true ? 1 : 0;
            MPG_SPWNR_ModConfig.isDirty = true;
        }
        private void isDebugEnabledCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            MPG_SPWNR_ModConfig.isDebugEnabled = isDebugEnabledCB.Checked == true ? 1 : 0;
            MPG_SPWNR_ModConfig.isDirty = true;
        }

        private void SpawnerPointsLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SpawnerPointsLB.SelectedItems.Count < 1) return;
            currentSpawnerPoint = SpawnerPointsLB.SelectedItem as MPG_Spawner_PointConfig;
            useraction = false;
            PointConfigisDebugEnabledCB.Checked = currentSpawnerPoint.isDebugEnabled == 1 ? true : false;
            PointConfigWorkingHoursStartNUD.Value = currentSpawnerPoint.getworkinghours()[0];
            PointConfigWorkingHoursEndNUD.Value = currentSpawnerPoint.getworkinghours()[1];

            PointConfigNotificationTitleTB.Text = currentSpawnerPoint.notificationTitle;
            PointConfigNotificationTextEnterTB.Text = currentSpawnerPoint.notificationTextEnter;
            PointConfigNotificationTextExitTB.Text = currentSpawnerPoint.notificationTextExit;
            PointConfigNotificationTextSpawnTB.Text = currentSpawnerPoint.notificationTextSpawn;
            PointConfigNotificationTextWinTB.Text = currentSpawnerPoint.notificationTextWin;
            PointConfigNotificationTimeNUD.Value = currentSpawnerPoint.notificationTime;
            PointConfigNotificationIconTB.Text = currentSpawnerPoint.notificationIcon;
            PointConfigTriggerPosXNUD.Value = (decimal)currentSpawnerPoint._triggerPosition.Position.X;
            PointConfigTriggerPosYNUD.Value = (decimal)currentSpawnerPoint._triggerPosition.Position.Y;
            PointConfigTriggerPosZNUD.Value = (decimal)currentSpawnerPoint._triggerPosition.Position.Z;
            if (PointConfigTriggerPositionRotSpecifiedCB.Checked = currentSpawnerPoint._triggerPosition.rotspecified)
            {
                PointConfigTriggerRotXNUD.Value = (decimal)currentSpawnerPoint._triggerPosition.Rotation.X;
                PointConfigTriggerRotYNUD.Value = (decimal)currentSpawnerPoint._triggerPosition.Rotation.Y;
                PointConfigTriggerRotZNUD.Value = (decimal)currentSpawnerPoint._triggerPosition.Rotation.Z;
            }
            Getdependancies();

            PointConfigtriggerDependenciesAnyOfCB.Checked = currentSpawnerPoint.triggerDependenciesAnyOf == 1 ? true : false;
            PointConfigtriggerDebugColorCB.SelectedIndex = PointConfigtriggerDebugColorCB.FindStringExact(currentSpawnerPoint.triggerDebugColor);
            PointConfigtriggerradiusNUD.Value = (decimal)currentSpawnerPoint.triggerRadius;
            PointConfigtriggerHeightNUD.Value = (decimal)currentSpawnerPoint.triggerHeight;
            PointConfigtriggerWidthXNUD.Value = (decimal)currentSpawnerPoint.triggerWidthX;
            PointConfigtriggerWidthYNUD.Value = (decimal)currentSpawnerPoint.triggerWidthY;
            PointConfigtriggerFirstDelayNUD.Value = (decimal)currentSpawnerPoint.triggerFirstDelay;
            PointConfigtriggerCooldownNUD.Value = (decimal)currentSpawnerPoint.triggerCooldown;
            PointConfigtriggerSafeDistanceNUD.Value = (decimal)currentSpawnerPoint.triggerSafeDistance;
            PointConfigtriggerEnterDelayNUD.Value = (decimal)currentSpawnerPoint.triggerEnterDelay;
            PointConfigtriggerCleanupDelayNUD.Value = (decimal)currentSpawnerPoint.triggerCleanupDelay;
            PointConfigtriggerCleanupOnLeaveCB.Checked = currentSpawnerPoint.triggerCleanupOnLeave == 1 ? true : false;
            pointConfigtriggerCleanupOnLunchTimeCB.Checked = currentSpawnerPoint.triggerCleanupOnLunchTime == 1 ? true : false;
            PointConfigtriggerDisableOnWinCB.Checked = currentSpawnerPoint.triggerDisableOnWin == 1 ? true : false;
            PointConfigtriggerDisableOnLeaveCB.Checked = currentSpawnerPoint.triggerDisableOnLeave == 1 ? true : false;

            PointConfigSpawnRadiusNUD.Value = currentSpawnerPoint.spawnRadius;
            PointConfigSpawnMinNUD.Value = currentSpawnerPoint.spawnMin;
            PointConfigSpawnMaxNUD.Value = currentSpawnerPoint.spawnMax;
            PointConfigSpawnCountLimitNUD.Value = currentSpawnerPoint.spawnCountLimit;
            PointConfigSpawnLoopInsideCB.Checked = currentSpawnerPoint.spawnLoopInside == 1 ? true : false;
            PointConfigClearDeathAnimalsNUD.Value = currentSpawnerPoint.clearDeathAnimals;
            PointConfigClearDeathZombiesNUD.Value = currentSpawnerPoint.clearDeathZombies;


            PointConfigTrigerdependanciesCB.SelectedIndex = -1;
            PointConfigTrigerdependanciesCB.SelectedIndex = 0;


            PointConfigSpawnPositionsLB.DisplayMember = "DisplayName";
            PointConfigSpawnPositionsLB.ValueMember = "Value";
            PointConfigSpawnPositionsLB.DataSource = currentSpawnerPoint._spawnPositions;

            PointConfigSpawnListLB.DisplayMember = "DisplayName";
            PointConfigSpawnListLB.ValueMember = "Value";
            PointConfigSpawnListLB.DataSource = currentSpawnerPoint.spawnList;

            pictureBox1.Invalidate();
            useraction = true;
        }
        private void darkButton1_Click(object sender, EventArgs e)
        {
            List<int> AllspawnerIDS = MPG_SPWNR_ModConfig.GetAllSpawnerIDS();
            AddNewQuestID form = new AddNewQuestID
            {
                NumberofquestsIDs = AllspawnerIDS
            };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                MPG_Spawner_PointConfig newMPG_Spawner_PointConfig = new MPG_Spawner_PointConfig()
                {
                    pointId = form.SelectedID,
                    _triggerPosition = new Vec3PandR("0 0 0"),
                    _spawnPositions = new BindingList<Vec3PandR>(),
                    triggerDependencies = new BindingList<int>(),
                    triggersToEnableOnEnter = new BindingList<int>(),
                    triggersToEnableOnFirstSpawn = new BindingList<int>(),
                    triggersToEnableOnLeave = new BindingList<int>(),
                    triggersToEnableOnWin = new BindingList<int>(),
                    spawnList = new BindingList<string>(),
                    mappingData = new BindingList<MPG_Spawner_mappingData>(),
                    isDebugEnabled = 1,
                    notificationTitle = "Point " + form.SelectedID,
                    notificationTextEnter = "You have entered point " + form.SelectedID + ". Everything is waiting for you here!",
                    notificationTextExit = "You have left point " + form.SelectedID + ". We rely on our own two feet!",
                    notificationTextSpawn = "New entities appeared somewhere nearby.",
                    notificationTextWin = "Amazing! You killed all the enemies :)!",
                    notificationTime = 8,
                    notificationIcon = "set:dayz_gui image:iconSkull",
                    triggerDebugColor = "blue",
                    triggerRadius = 20,
                    triggerHeight = 0,
                    triggerFirstDelay = 0,
                    triggerCooldown = 15,
                    triggerSafeDistance = 25,
                    triggerEnterDelay = 0,
                    triggerWorkingTime = "0-24",
                    spawnRadius = 2,
                    spawnMin = 2,
                    spawnMax = 5,
                    spawnCountLimit = 30,
                    spawnLoopInside = 1,
                    clearDeathAnimals = 5,
                    clearDeathZombies = 0
                };
                currentSpawnerPointsFile.Points.Add(newMPG_Spawner_PointConfig);
                currentSpawnerPointsFile.isDirty = true;
                SpawnerPointsLB.Invalidate();
            }
            
        }
        private void darkButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This Will Remove All reference to this SpawnerPoint, Are you sure you want to do this?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                MPG_SPWNR_ModConfig.RemovePointfile(currentSpawnerPoint);
            }
        }
        private void PointConfigisDebugEnabledCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.isDebugEnabled = PointConfigisDebugEnabledCB.Checked == true ? 1 : 0;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigSetWorkingHours_ValueChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.setworkinghours(new int[] { (int)PointConfigWorkingHoursStartNUD.Value, (int)PointConfigWorkingHoursEndNUD.Value });
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigNotificationTitleTB_TextChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.notificationTitle = PointConfigNotificationTitleTB.Text;
            currentSpawnerPointsFile.isDirty = true;
            SpawnerPointsLB.Invalidate();
        }
        private void PointConfigNotificationTextEnterTB_TextChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.notificationTextEnter = PointConfigNotificationTextEnterTB.Text;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigNotificationTextExitTB_TextChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.notificationTextExit = PointConfigNotificationTextExitTB.Text;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigNotificationTextSpawnTB_TextChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.notificationTextSpawn = PointConfigNotificationTextSpawnTB.Text;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigNotificationTextWinTB_TextChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.notificationTextWin = PointConfigNotificationTextWinTB.Text;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigNotificationTimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.notificationTime = (int)PointConfigNotificationTimeNUD.Value;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigNotificationIconTB_TextChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.notificationIcon = PointConfigNotificationIconTB.Text;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigTriggerPositionRotSpecifiedCB_CheckedChanged(object sender, EventArgs e)
        {
            if (PointConfigTriggerPositionRotSpecifiedCB.Checked)
            {
                TrigerposRotationLabel.Visible = true;
                PointConfigTriggerRotXNUD.Visible = true;
                PointConfigTriggerRotYNUD.Visible = true;
                PointConfigTriggerRotZNUD.Visible = true;
                currentSpawnerPoint._triggerPosition.rotspecified = true;
                PointConfigTriggerRotXNUD.Value = (decimal)currentSpawnerPoint._triggerPosition.Rotation.X;
                PointConfigTriggerRotYNUD.Value = (decimal)currentSpawnerPoint._triggerPosition.Rotation.Y;
                PointConfigTriggerRotZNUD.Value = (decimal)currentSpawnerPoint._triggerPosition.Rotation.Z;

            }
            else
            {
                currentSpawnerPoint._triggerPosition.rotspecified = false;
                TrigerposRotationLabel.Visible = false;
                PointConfigTriggerRotXNUD.Visible = false;
                PointConfigTriggerRotYNUD.Visible = false;
                PointConfigTriggerRotZNUD.Visible = false;
            }
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigSetTrigger_ValueChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            if (currentSpawnerPoint._triggerPosition.rotspecified = PointConfigTriggerPositionRotSpecifiedCB.Checked)
            {
                currentSpawnerPoint._triggerPosition.Rotation = new Vec3((float)PointConfigTriggerRotXNUD.Value, (float)PointConfigTriggerRotYNUD.Value, (float)PointConfigSpawnRotationZNUD.Value);
            }
            currentSpawnerPoint._triggerPosition.Position = new Vec3((float)PointConfigTriggerPosXNUD.Value, (float)PointConfigTriggerPosYNUD.Value, (float)PointConfigTriggerPosZNUD.Value );
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigTrigerdependanciesCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Getdependancies();
        }
        private void Getdependancies()
        {
            string Info = "";
            switch (PointConfigTrigerdependanciesCB.SelectedIndex)
            {
                case 0:
                    Info = "IDs of the triggers this trigger depends on. If the field is filled, the current trigger will not fire until the trigger specified as a dependency fires.";
                    PointConfigTrigerDependanciesLB.DisplayMember = "DisplayName";
                    PointConfigTrigerDependanciesLB.ValueMember = "Value";
                    PointConfigTrigerDependanciesLB.DataSource = currentSpawnerPoint.ListtriggerDependencies;
                    break;
                case 1:
                    Info = "List of trigger IDs that will be activated if the player enters the current trigger.";
                    PointConfigTrigerDependanciesLB.DisplayMember = "DisplayName";
                    PointConfigTrigerDependanciesLB.ValueMember = "Value";
                    PointConfigTrigerDependanciesLB.DataSource = currentSpawnerPoint.ListtriggersToEnableOnEnter;
                    break;
                case 2:
                    Info = "List of trigger IDs that will be activated if the first spawn occurs in the current trigger.";
                    PointConfigTrigerDependanciesLB.DisplayMember = "DisplayName";
                    PointConfigTrigerDependanciesLB.ValueMember = "Value";
                    PointConfigTrigerDependanciesLB.DataSource = currentSpawnerPoint.ListtriggersToEnableOnFirstSpawn;
                    break;
                case 3:
                    Info = "List of trigger IDs that will be activated if all the critters in the current trigger have been killed.";
                    PointConfigTrigerDependanciesLB.DisplayMember = "DisplayName";
                    PointConfigTrigerDependanciesLB.ValueMember = "Value";
                    PointConfigTrigerDependanciesLB.DataSource = currentSpawnerPoint.ListtriggersToEnableOnWin;
                    break;
                case 4:
                    Info = "List of trigger IDs that will be activated if all players have left the current trigger.";
                    PointConfigTrigerDependanciesLB.DisplayMember = "DisplayName";
                    PointConfigTrigerDependanciesLB.ValueMember = "Value";
                    PointConfigTrigerDependanciesLB.DataSource = currentSpawnerPoint.ListtriggersToEnableOnLeave;
                    break;
            }
            richTextBox1.Text = Info;
        }
        private void darkButton22_Click(object sender, EventArgs e)
        {
            AddIDFromList form = new AddIDFromList();
            form.IDlist = new BindingList<MPG_Spawner_PointConfig>( MPG_SPWNR_ModConfig.GetPointlist());
            if(form.ShowDialog() == DialogResult.OK)
            {
                foreach(MPG_Spawner_PointConfig i in form.Selectedids)
                {
                    switch (PointConfigTrigerdependanciesCB.SelectedIndex)
                    {
                        case 0:
                            currentSpawnerPoint.ListtriggerDependencies.Add(i);
                            break;
                        case 1:
                            currentSpawnerPoint.ListtriggersToEnableOnEnter.Add(i);
                            break;
                        case 2:
                            currentSpawnerPoint.ListtriggersToEnableOnFirstSpawn.Add(i);
                            break;
                        case 3:
                            currentSpawnerPoint.ListtriggersToEnableOnWin.Add(i);
                            break;
                        case 4:
                            currentSpawnerPoint.ListtriggersToEnableOnLeave.Add(i);
                            break;
                    }
                    currentSpawnerPointsFile.isDirty = true;
                }
            }

        }
        private void darkButton21_Click(object sender, EventArgs e)
        {
            MPG_Spawner_PointConfig removeid = PointConfigTrigerDependanciesLB.SelectedItem as MPG_Spawner_PointConfig;
            switch (PointConfigTrigerdependanciesCB.SelectedIndex)
            {
                case 0:
                    currentSpawnerPoint.ListtriggerDependencies.Remove(removeid);
                    break;
                case 1:
                    currentSpawnerPoint.ListtriggersToEnableOnEnter.Remove(removeid);
                    break;
                case 2:
                    currentSpawnerPoint.ListtriggersToEnableOnFirstSpawn.Remove(removeid);
                    break;
                case 3:
                    currentSpawnerPoint.ListtriggersToEnableOnWin.Remove(removeid);
                    break;
                case 4:
                    currentSpawnerPoint.ListtriggersToEnableOnLeave.Remove(removeid);
                    break;
            }
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigSpawnPositionsLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PointConfigSpawnPositionsLB.SelectedItems.Count < 1) return;
            currentspawnPosition = PointConfigSpawnPositionsLB.SelectedItem as Vec3PandR;
            useraction = false;
            PointConfigSpawnPositionXNUD.Value = (decimal)currentspawnPosition.Position.X;
            PointConfigSpawnPositionYNUD.Value = (decimal)currentspawnPosition.Position.Y;
            PointConfigSpawnPositionZNUD.Value = (decimal)currentspawnPosition.Position.Z;
            if (PointConfigSpawnPositionsUserotationCB.Checked = currentspawnPosition.rotspecified)
            {
                PointConfigSpawnRotationXNUD.Value = (decimal)currentspawnPosition.Rotation.X;
                PointConfigSpawnRotationYNUD.Value = (decimal)currentspawnPosition.Rotation.Y;
                PointConfigSpawnRotationZNUD.Value = (decimal)currentspawnPosition.Rotation.Z;
            }

            useraction = true;
        }
        private void PointConfigSpawnPositionsUserotationCB_CheckedChanged(object sender, EventArgs e)
        {
            if (PointConfigSpawnPositionsUserotationCB.Checked)
            {
                SpawnPositionRotaionLabel.Visible = true;
                PointConfigSpawnRotationXNUD.Visible = true;
                PointConfigSpawnRotationYNUD.Visible = true;
                PointConfigSpawnRotationZNUD.Visible = true;
                currentspawnPosition.rotspecified = true;
                PointConfigSpawnRotationXNUD.Value = (decimal)currentspawnPosition.Rotation.X;
                PointConfigSpawnRotationYNUD.Value = (decimal)currentspawnPosition.Rotation.Y;
                PointConfigSpawnRotationZNUD.Value = (decimal)currentspawnPosition.Rotation.Z;

            }
            else
            {
                currentspawnPosition.rotspecified = false;
                SpawnPositionRotaionLabel.Visible = false;
                PointConfigSpawnRotationXNUD.Visible = false;
                PointConfigSpawnRotationYNUD.Visible = false;
                PointConfigSpawnRotationZNUD.Visible = false;
            }
            currentSpawnerPointsFile.isDirty = true;
            PointConfigSpawnPositionsLB.Invalidate();
        }
        private void PointConfigSetSpawnPosition_ValueChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            if (currentspawnPosition.rotspecified)
            {
                currentspawnPosition.Rotation = new Vec3((float)PointConfigSpawnRotationXNUD.Value, (float)PointConfigSpawnRotationYNUD.Value, (float)PointConfigSpawnRotationZNUD.Value);
            }
            currentspawnPosition.Position = new Vec3((float)PointConfigSpawnPositionXNUD.Value, (float)PointConfigSpawnPositionYNUD.Value, (float)PointConfigSpawnPositionZNUD.Value);
            currentSpawnerPointsFile.isDirty = true;
            PointConfigSpawnPositionsLB.Invalidate();
        }
        private void PointConfigtriggerDependenciesAnyOfCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.triggerDependenciesAnyOf = PointConfigtriggerDependenciesAnyOfCB.Checked == true ? 1 : 0;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigtriggerDebugColorCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.triggerDebugColor = PointConfigtriggerDebugColorCB.GetItemText(PointConfigtriggerDebugColorCB.SelectedItem);
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigtriggerradiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.triggerRadius = PointConfigtriggerradiusNUD.Value;
            currentSpawnerPointsFile.isDirty = true;
            pictureBox1.Invalidate();
        }
        private void PointConfigtriggerHeightNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.triggerHeight = PointConfigtriggerHeightNUD.Value;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigtriggerWidthXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.triggerWidthX = PointConfigtriggerWidthXNUD.Value;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigtriggerWidthYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.triggerWidthY = PointConfigtriggerWidthYNUD.Value;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigtriggerFirstDelayNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.triggerFirstDelay = (int)PointConfigtriggerFirstDelayNUD.Value;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigtriggerCooldownNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.triggerCooldown = PointConfigtriggerCooldownNUD.Value;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigtriggerSafeDistanceNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.triggerSafeDistance = PointConfigtriggerSafeDistanceNUD.Value;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigtriggerEnterDelayNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.triggerEnterDelay = (int)PointConfigtriggerEnterDelayNUD.Value;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigtriggerCleanupDelayNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.triggerCleanupDelay = (int)PointConfigtriggerCleanupDelayNUD.Value;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigtriggerCleanupOnLeaveCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.triggerCleanupOnLeave = PointConfigtriggerCleanupOnLeaveCB.Checked == true ? 1 : 0;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void pointConfigtriggerCleanupOnLunchTimeCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.triggerCleanupOnLunchTime = pointConfigtriggerCleanupOnLunchTimeCB.Checked == true ? 1 : 0;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigtriggerDisableOnWinCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.triggerDisableOnWin = PointConfigtriggerDisableOnWinCB.Checked == true ? 1 : 0;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigtriggerDisableOnLeaveCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.triggerDisableOnLeave = PointConfigtriggerDisableOnLeaveCB.Checked == true ? 1 : 0;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigSpawnRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.spawnRadius = PointConfigSpawnRadiusNUD.Value;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigSpawnMinNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.spawnMin = (int)PointConfigSpawnMinNUD.Value;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigSpawnMaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.spawnMax = (int)PointConfigSpawnMaxNUD.Value;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigSpawnCountLimitNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.spawnCountLimit = (int)PointConfigSpawnCountLimitNUD.Value;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigSpawnLoopInsideCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.spawnLoopInside = PointConfigSpawnLoopInsideCB.Checked == true ? 1 : 0;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigClearDeathAnimalsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.clearDeathAnimals = (int)PointConfigClearDeathAnimalsNUD.Value;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void PointConfigClearDeathZombiesNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!useraction) return;
            currentSpawnerPoint.clearDeathZombies = (int)PointConfigClearDeathZombiesNUD.Value;
            currentSpawnerPointsFile.isDirty = true;
        }
        private void darkButton3_Click(object sender, EventArgs e)
        {
            Vec3PandR newvec3pandr = new Vec3PandR()
            {
                Position = new Vec3(0, 0, 0),
                Rotation = new Vec3(0, 0, 0),
                rotspecified = false
            };
            currentSpawnerPoint._spawnPositions.Add(newvec3pandr);
            currentSpawnerPointsFile.isDirty = true;
            PointConfigSpawnPositionsLB.Invalidate();
            PointConfigSpawnPositionsLB.SelectedIndex = currentSpawnerPoint._spawnPositions.Count - 1;
        }
        private void darkButton10_Click(object sender, EventArgs e)
        {
            int index = PointConfigSpawnPositionsLB.SelectedIndex;
            currentSpawnerPoint._spawnPositions.Remove(currentspawnPosition);
            PointConfigSpawnPositionsLB.Invalidate();
            PointConfigSpawnPositionsLB.SelectedIndex =  -1;
            if(currentSpawnerPoint._spawnPositions.Count > 0)
            {
                PointConfigSpawnPositionsLB.SelectedIndex = index - 1;
            }
        }
        private void darkButton12_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    DZE importfile = DZEHelpers.LoadFile(filePath);
                    bool ImportTrigger = false;
                    var result = MessageBox.Show("Would you like to import the trigger as well?", "Import options", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                        ImportTrigger = true;
                    bool importrtotation = false;
                    result = MessageBox.Show("Would you like to import rotations?", "Import options", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                        importrtotation = true;
                    result = MessageBox.Show("Would you like to clear existing Spawn Points??", "Import options", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if ((result == DialogResult.Cancel))
                    {
                        return;
                    }
                    else if (result == DialogResult.Yes)
                    {
                        currentSpawnerPoint._spawnPositions = new BindingList<Vec3PandR>();
                    }
                    currentSpawnerPoint.ImportDZE(importfile, ImportTrigger, importrtotation);
                    useraction = false;
                    
                    PointConfigTriggerPosXNUD.Value = (decimal)currentSpawnerPoint._triggerPosition.Position.X;
                    PointConfigTriggerPosYNUD.Value = (decimal)currentSpawnerPoint._triggerPosition.Position.Y;
                    PointConfigTriggerPosZNUD.Value = (decimal)currentSpawnerPoint._triggerPosition.Position.Z;
                    PointConfigSpawnPositionsLB.DataSource = currentSpawnerPoint._spawnPositions;
                    currentSpawnerPointsFile.isDirty = true;
                    useraction = true;
                    PointConfigTriggerPositionRotSpecifiedCB.Checked = importrtotation;
                }
            }
        }
        private void darkButton11_Click(object sender, EventArgs e)
        {
            DZE newdze = new DZE()
            {
                MapName = Path.GetFileNameWithoutExtension(currentproject.MapPath).Split('_')[0]
            };
            int m_Id = 0;
            string filename = "";
            var result = MessageBox.Show("Would yo ulike to export the trigger as well?", "Export options", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            else if (result == DialogResult.Yes)
            {
                Editorobject Triggerobject = new Editorobject()
                {
                    Type = "GiftBox_Large_1",
                    DisplayName = "GiftBox_Large_1",
                    Position = currentSpawnerPoint._triggerPosition.GetPositionFloatArray(),
                    Orientation = currentSpawnerPoint._triggerPosition.GetRotationFloatArray(),
                    Scale = 1.0f,
                    Flags = 2147483647,
                    m_Id = m_Id
                };
                newdze.EditorObjects.Add(Triggerobject);
                m_Id++;
            }
            foreach (Vec3PandR vec3pandr in currentSpawnerPoint._spawnPositions)
            {
                Editorobject SpawnObject = new Editorobject()
                {
                    Type = "GiftBox_Small_1",
                    DisplayName = "GiftBox_Small_1",
                    Position = vec3pandr.GetPositionFloatArray(),
                    Orientation = vec3pandr.GetRotationFloatArray(),
                    Scale = 1.0f,
                    Flags = 2147483647,
                    m_Id = m_Id
                };
                newdze.EditorObjects.Add(SpawnObject);
                m_Id++;
            }
            filename = "MPG Spawner Points - " + currentSpawnerPoint.notificationTitle;
            newdze.CameraPosition = newdze.EditorObjects[0].Position;
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = filename;
            if (save.ShowDialog() == DialogResult.OK)
            {
                var options = new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
                string jsonString = JsonSerializer.Serialize(newdze, options);
                File.WriteAllText(save.FileName + ".dze", jsonString);
            }
        }
        private void darkButton4_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes
            {
                vanillatypes = vanillatypes,
                ModTypes = ModTypes,
                currentproject = currentproject,
                UseOnlySingleitem = false
            };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.addedtypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!currentSpawnerPoint.spawnList.Contains(l))
                    {
                        currentSpawnerPoint.spawnList.Add(l);
                        currentSpawnerPointsFile.isDirty = true;
                    }
                }
                PointConfigSpawnListLB.Refresh();
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }
        private void darkButton7_Click(object sender, EventArgs e)
        {
            if (PointConfigSpawnListLB.SelectedItems.Count < 1) return;
            List<string> removelist = new List<string>();
            foreach (var item in PointConfigSpawnListLB.SelectedItems)
            {
                removelist.Add(PointConfigSpawnListLB.GetItemText(item));
            }
            foreach (string bubac in removelist)
            {
                currentSpawnerPoint.spawnList.Remove(bubac);
            }
            currentSpawnerPointsFile.isDirty = true;
        }

        public int MPGSpawnerScale = 1;
        private Point _mouseLastPosition;
        private Point _newscrollPosition;

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            MPGSpawnerScale = trackBar1.Value;
            SetMPGSpawnerScale();
        }
        private void SetMPGSpawnerScale()
        {
            float scalevalue = MPGSpawnerScale * 0.05f;
            float mapsize = currentproject.MapSize;
            int newsize = (int)(mapsize * scalevalue);
            pictureBox1.Size = new Size(newsize, newsize);
        }
        private void DrawMPGSpawner(object sender, PaintEventArgs e)
        {
            if (checkBox9.Checked == false)
            {
                if (checkBox8.Checked == true)
                {
                    foreach (MPG_Spawner_PointConfig spc in currentSpawnerPointsFile.Points)
                    {
                        float scalevalue = MPGSpawnerScale * 0.05f;
                        int centerX = (int)(Math.Round(spc._triggerPosition.Position.X) * scalevalue);
                        int centerY = (int)(currentproject.MapSize * scalevalue) - (int)(Math.Round(spc._triggerPosition.Position.Z, 0) * scalevalue);
                        int eventradius = (int)((float)spc.triggerRadius * scalevalue);
                        Point center = new Point(centerX, centerY);
                        Pen pen = new Pen(Color.Red, 4);
                        if (spc == currentSpawnerPoint)
                            pen = new Pen(Color.Green, 4);
                        getCircleDynamicAI(e.Graphics, pen, center, eventradius, "\n" + spc.notificationTitle);
                        foreach (Vec3PandR v3pandr in spc._spawnPositions)
                        {
                            scalevalue = MPGSpawnerScale * 0.05f;
                            centerX = (int)(Math.Round(v3pandr.Position.X) * scalevalue);
                            centerY = (int)(currentproject.MapSize * scalevalue) - (int)(Math.Round(v3pandr.Position.Z, 0) * scalevalue);
                            eventradius = (int)(Math.Round(1f, 0) * scalevalue);
                            center = new Point(centerX, centerY);
                            pen = new Pen(Color.Yellow, 4);
                            getCircleDynamicAI(e.Graphics, pen, center, eventradius, "");
                        }
                    }
                }
                else
                {
                    float scalevalue = MPGSpawnerScale * 0.05f;
                    int centerX = (int)(Math.Round(currentSpawnerPoint._triggerPosition.Position.X) * scalevalue);
                    int centerY = (int)(currentproject.MapSize * scalevalue) - (int)(Math.Round(currentSpawnerPoint._triggerPosition.Position.Z, 0) * scalevalue);
                    int eventradius = (int)((float)currentSpawnerPoint.triggerRadius * scalevalue);
                    Point center = new Point(centerX, centerY);
                    Pen pen = new Pen(Color.Green, 4);
                    getCircleDynamicAI(e.Graphics, pen, center, eventradius, "\n" + currentSpawnerPoint.notificationTitle);
                    foreach (Vec3PandR v3 in currentSpawnerPoint._spawnPositions)
                    {
                        scalevalue = MPGSpawnerScale * 0.05f;
                        centerX = (int)(Math.Round(v3.Position.X) * scalevalue);
                        centerY = (int)(currentproject.MapSize * scalevalue) - (int)(Math.Round(v3.Position.Z, 0) * scalevalue);
                        eventradius = (int)(Math.Round(1f, 0) * scalevalue);
                        center = new Point(centerX, centerY);
                        pen = new Pen(Color.Yellow, 4);
                        getCircleDynamicAI(e.Graphics, pen, center, eventradius, "");
                    }
                }

            }
            else if (checkBox9.Checked == true)
            {
                foreach(MPG_Spawner_PointConfig spc in MPG_SPWNR_ModConfig.GetPointlist())
                {
                    float scalevalue = MPGSpawnerScale * 0.05f;
                    int centerX = (int)(Math.Round(spc._triggerPosition.Position.X) * scalevalue);
                    int centerY = (int)(currentproject.MapSize * scalevalue) - (int)(Math.Round(spc._triggerPosition.Position.Z, 0) * scalevalue);
                    int eventradius = (int)((float)spc.triggerRadius * scalevalue);
                    Point center = new Point(centerX, centerY);
                    Pen pen = new Pen(Color.Red, 4);
                    if (spc == currentSpawnerPoint)
                        pen = new Pen(Color.Green, 4);
                    getCircleDynamicAI(e.Graphics, pen, center, eventradius, "\n" + spc.notificationTitle);
                    foreach (Vec3PandR v3pandr in spc._spawnPositions)
                    {
                        scalevalue = MPGSpawnerScale * 0.05f;
                        centerX = (int)(Math.Round(v3pandr.Position.X) * scalevalue);
                        centerY = (int)(currentproject.MapSize * scalevalue) - (int)(Math.Round(v3pandr.Position.Z, 0) * scalevalue);
                        eventradius = (int)(Math.Round(1f, 0) * scalevalue);
                        center = new Point(centerX, centerY);
                        pen = new Pen(Color.Yellow, 4);
                        getCircleDynamicAI(e.Graphics, pen, center, eventradius, "");
                    }
                }
            }
        }
        private void getCircleDynamicAI(Graphics drawingArea, Pen penToUse, Point center, int radius, string c)
        {
            Rectangle rect = new Rectangle(center.X - 1, center.Y - 1, 2, 2);
            drawingArea.DrawEllipse(penToUse, rect);
            Rectangle rect2 = new Rectangle(center.X - radius, center.Y - radius, radius * 2, radius * 2);
            drawingArea.DrawEllipse(penToUse, rect2);
            Rectangle rect3 = new Rectangle(center.X - radius, center.Y - (radius + 30), 200, 40);
            drawingArea.DrawString(c, new Font("Tahoma", 9), Brushes.White, rect3);
        }
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (pictureBox1.Focused == false)
            {
                pictureBox1.Focus();
                panelEx1.AutoScrollPosition = _newscrollPosition;
                pictureBox1.Invalidate();
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point changePoint = new Point(e.Location.X - _mouseLastPosition.X, e.Location.Y - _mouseLastPosition.Y);
                _newscrollPosition = new Point(-panelEx1.AutoScrollPosition.X - changePoint.X, -panelEx1.AutoScrollPosition.Y - changePoint.Y);
                if (_newscrollPosition.X <= 0)
                    _newscrollPosition.X = 0;
                if (_newscrollPosition.Y <= 0)
                    _newscrollPosition.Y = 0;
                panelEx1.AutoScrollPosition = _newscrollPosition;
                pictureBox1.Invalidate();
            }
            decimal scalevalue = MPGSpawnerScale * (decimal)0.05;
            decimal mapsize = currentproject.MapSize;
            int newsize = (int)(mapsize * scalevalue);
            label2.Text = Decimal.Round((decimal)(e.X / scalevalue), 4) + "," + Decimal.Round((decimal)((newsize - e.Y) / scalevalue), 4);
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Cursor.Current = Cursors.SizeAll;
                _mouseLastPosition = e.Location;
            }
        }
        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                pictureBox1_ZoomOut();
            }
            else
            {
                pictureBox1_ZoomIn();
            }

        }
        private void pictureBox1_ZoomIn()
        {
            int oldpictureboxhieght = pictureBox1.Height;
            int oldpitureboxwidht = pictureBox1.Width;
            Point oldscrollpos = panelEx1.AutoScrollPosition;
            int tbv = trackBar1.Value;
            int newval = tbv + 1;
            if (newval >= 20)
                newval = 20;
            trackBar1.Value = newval;
            MPGSpawnerScale = trackBar1.Value;
            SetMPGSpawnerScale();
            if (pictureBox1.Height > panelEx1.Height)
            {
                decimal newy = ((decimal)oldscrollpos.Y / (decimal)oldpictureboxhieght);
                int y = (int)(pictureBox1.Height * newy);
                _newscrollPosition.Y = y * -1;
                panelEx1.AutoScrollPosition = _newscrollPosition;
            }
            if (pictureBox1.Width > panelEx1.Width)
            {
                decimal newy = ((decimal)oldscrollpos.X / (decimal)oldpitureboxwidht);
                int x = (int)(pictureBox1.Width * newy);
                _newscrollPosition.X = x * -1;
                panelEx1.AutoScrollPosition = _newscrollPosition;
            }
            pictureBox1.Invalidate();
        }
        private void pictureBox1_ZoomOut()
        {
            int oldpictureboxhieght = pictureBox1.Height;
            int oldpitureboxwidht = pictureBox1.Width;
            Point oldscrollpos = panelEx1.AutoScrollPosition;
            int tbv = trackBar1.Value;
            int newval = tbv - 1;
            if (newval <= 1)
                newval = 1;
            trackBar1.Value = newval;
            MPGSpawnerScale = trackBar1.Value;
            SetMPGSpawnerScale();
            if (pictureBox1.Height > panelEx1.Height)
            {
                decimal newy = ((decimal)oldscrollpos.Y / (decimal)oldpictureboxhieght);
                int y = (int)(pictureBox1.Height * newy);
                _newscrollPosition.Y = y * -1;
                panelEx1.AutoScrollPosition = _newscrollPosition;
            }
            if (pictureBox1.Width > panelEx1.Width)
            {
                decimal newy = ((decimal)oldscrollpos.X / (decimal)oldpitureboxwidht);
                int x = (int)(pictureBox1.Width * newy);
                _newscrollPosition.X = x * -1;
                panelEx1.AutoScrollPosition = _newscrollPosition;
            }
            pictureBox1.Invalidate();
        }
        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }
        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }
    }
}
