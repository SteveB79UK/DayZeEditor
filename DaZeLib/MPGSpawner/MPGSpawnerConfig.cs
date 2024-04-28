﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace DayZeLib
{
    public class MPG_SPWNR_ModConfig
    {
        const int CurrentVersion = 1;
        [JsonIgnore]
        public string Filename { get; set; }
        [JsonIgnore]
        public bool isDirty;
        [JsonIgnore]
        public BindingList<int> UsedPOintIDS { get; set; }
        [JsonIgnore]
        public BindingList<MPG_Spawner_PointsConfig> MPG_Spawner_PointsConfigs { get; set; }
        [JsonIgnore]
        public List<MPG_Spawner_PointsConfig> Markedfordelete { get; set; }

        public int configVersion { get; set; }
        public int isModDisabled { get; set; }
        public int isDebugEnabled { get; set; }
        public BindingList<string> pointsConfigs { get; set; }


        public bool checkver()
        {
            if (configVersion != CurrentVersion)
            {
                configVersion = CurrentVersion;
                isDirty = true;
                return true;
            }
            return false;
        }
        public void SetSpawnerPointFiles()
        {
            pointsConfigs = new BindingList<string>();
            foreach (MPG_Spawner_PointsConfig SPCF in MPG_Spawner_PointsConfigs)
            {
                pointsConfigs.Add(SPCF.Filename);
            }
        }
        public void GetallSpawnerpointsfiles(string Spawnerpointspath)
        {
            MPG_Spawner_PointsConfigs = new BindingList<MPG_Spawner_PointsConfig>();
            Console.Write("## Starting MPG Spawner points files ##" + Environment.NewLine);
            foreach (string file in pointsConfigs) 
            {
                MPG_Spawner_PointsConfig MPG_Spawner_PointsConfig = new MPG_Spawner_PointsConfig();
                try
                {
                    MPG_Spawner_PointsConfig.Points = new BindingList<MPG_Spawner_PointConfig>(JsonSerializer.Deserialize<BindingList<MPG_Spawner_PointConfig>>(File.ReadAllText(Spawnerpointspath + file + ".json")));
                    if (MPG_Spawner_PointsConfig != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("  OK....");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    MPG_Spawner_PointsConfig.Filename = file;
                    MPG_Spawner_PointsConfigs.Add(MPG_Spawner_PointsConfig);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  Failed....");
                    Console.ForegroundColor = ConsoleColor.White;
                    var form = Application.OpenForms["SplashForm"];
                    if (form != null)
                    {
                        form.Invoke(new Action(() => { form.Close(); }));
                    }
                    MessageBox.Show("Error in " + Path.GetFileName(Filename) + "\n" + ex.Message.ToString() + "\n" + ex.InnerException.Message.ToString());
                }
            }
            Console.Write("## End MPG Spawner points files ##" + Environment.NewLine);
        }
        public List<int> GetAllSpawnerIDS()
        {
            List<int> NumberofSpanerspoints = new List<int>();
            foreach (MPG_Spawner_PointsConfig Spawnerfile in MPG_Spawner_PointsConfigs)
            {
                foreach(MPG_Spawner_PointConfig mPG_Spawner_PointConfig in Spawnerfile.Points)
                {
                    NumberofSpanerspoints.Add(mPG_Spawner_PointConfig.pointId);
                }
            }
            NumberofSpanerspoints.Sort();
            return NumberofSpanerspoints;
        }
        public List<MPG_Spawner_PointConfig> GetPointlist()
        {
            List<MPG_Spawner_PointConfig> pointslist = new List<MPG_Spawner_PointConfig>();
            foreach (MPG_Spawner_PointsConfig Spawnerfile in MPG_Spawner_PointsConfigs)
            {
                foreach (MPG_Spawner_PointConfig mPG_Spawner_PointConfig in Spawnerfile.Points)
                {
                    pointslist.Add(mPG_Spawner_PointConfig);
                }
            }
            return pointslist;
        }
        public void RemovePointsfile(MPG_Spawner_PointsConfig MPG_Spawner_PointsConfigfordelete)
        {
            if (Markedfordelete == null) Markedfordelete = new List<MPG_Spawner_PointsConfig>();
            Markedfordelete.Add(MPG_Spawner_PointsConfigfordelete);
            MPG_Spawner_PointsConfigs.Remove(MPG_Spawner_PointsConfigfordelete);
            isDirty = true;
            List<int> Removedid = new List<int>();
            foreach(MPG_Spawner_PointConfig removepoint in MPG_Spawner_PointsConfigfordelete.Points)
            {
                Removedid.Add(removepoint.pointId);
            }
            foreach (MPG_Spawner_PointsConfig Pointsfile in MPG_Spawner_PointsConfigs)
            {
                foreach (MPG_Spawner_PointConfig point in Pointsfile.Points)
                {
                    if(point.triggerDependencies.Intersect(Removedid).Any())
                    {
                        foreach (int i in Removedid)
                        {
                            point.triggerDependencies.Remove(i);
                            Pointsfile.isDirty = true;
                        }
                    }
                    if (point.triggersToEnableOnEnter.Intersect(Removedid).Any())
                    {
                        foreach (int i in Removedid)
                        {
                            point.triggersToEnableOnEnter.Remove(i);
                            Pointsfile.isDirty = true;
                        }
                    }
                    if (point.triggersToEnableOnFirstSpawn.Intersect(Removedid).Any())
                    {
                        foreach (int i in Removedid)
                        {
                            point.triggersToEnableOnFirstSpawn.Remove(i);
                            Pointsfile.isDirty = true;
                        }
                    }
                    if (point.triggersToEnableOnWin.Intersect(Removedid).Any())
                    {
                        foreach (int i in Removedid)
                        {
                            point.triggersToEnableOnWin.Remove(i);
                            Pointsfile.isDirty = true;
                        }
                    }
                    if (point.triggersToEnableOnLeave.Intersect(Removedid).Any())
                    {
                        foreach (int i in Removedid)
                        {
                            point.triggersToEnableOnLeave.Remove(i);
                            Pointsfile.isDirty = true;
                        }
                    }
                }
            }
        }
        public void RemovePointfile(MPG_Spawner_PointConfig currentSpawnerPoint)
        {
            int Removedid = currentSpawnerPoint.pointId;
            foreach (MPG_Spawner_PointsConfig Pointsfile in MPG_Spawner_PointsConfigs)
            {
                if(Pointsfile.Points.Any(x => x.pointId == Removedid))
                {
                    Pointsfile.Points.Remove(currentSpawnerPoint);
                    Pointsfile.isDirty = true;
                }
            }
            foreach (MPG_Spawner_PointsConfig Pointsfile in MPG_Spawner_PointsConfigs)
            {
                foreach (MPG_Spawner_PointConfig point in Pointsfile.Points)
                {
                    if (point.triggerDependencies.Contains(Removedid))
                    {
                        point.triggerDependencies.Remove(Removedid);
                        Pointsfile.isDirty = true;
                    }
                    if (point.triggersToEnableOnEnter.Contains(Removedid))
                    {
                        point.triggersToEnableOnEnter.Remove(Removedid);
                        Pointsfile.isDirty = true;
                    }
                    if (point.triggersToEnableOnFirstSpawn.Contains(Removedid))
                    {
                        point.triggersToEnableOnFirstSpawn.Remove(Removedid);
                        Pointsfile.isDirty = true;
                    }
                    if (point.triggersToEnableOnWin.Contains(Removedid))
                    {
                        point.triggersToEnableOnWin.Remove(Removedid);
                        Pointsfile.isDirty = true;
                    }
                    if (point.triggersToEnableOnLeave.Contains(Removedid))
                    {
                        point.triggersToEnableOnLeave.Remove(Removedid);
                        Pointsfile.isDirty = true;
                    }
                }
            }
        }

        public void Getalllists()
        {
            foreach (MPG_Spawner_PointsConfig Spawnerfile in MPG_Spawner_PointsConfigs)
            {
                Spawnerfile.GetAlllists(GetPointlist());
            }
        }
    }


    public class MPG_Spawner_PointsConfig
    {
        [JsonIgnore]
        public string Filename { get; set; }
        [JsonIgnore]
        public bool isDirty;

        public BindingList<MPG_Spawner_PointConfig> Points { get; set; }

        public MPG_Spawner_PointsConfig()
        {
            Points = new BindingList<MPG_Spawner_PointConfig>();
        }
        public override string ToString()
        {
            return Filename;
        }
        public void backupandDelete(string mPG_Spawner_PointsConfigPath)
        {
            string SaveTime = DateTime.Now.ToString("ddMMyy_HHmm");
            string Fullfilename = mPG_Spawner_PointsConfigPath + "\\" + Filename + ".json";
            if (File.Exists(Fullfilename))
            {
                Directory.CreateDirectory(mPG_Spawner_PointsConfigPath + "\\DeletedFiles\\" + SaveTime);
                File.Copy(Fullfilename, mPG_Spawner_PointsConfigPath + "\\DeletedFiles\\" + SaveTime + "\\" + Filename + ".bak");
                File.Delete(Fullfilename);
            }
        }

        public void GetAlllists(List<MPG_Spawner_PointConfig> mPG_Spawner_PointConfigs)
        {
            foreach(MPG_Spawner_PointConfig file in Points)
            {
                file.getalllists(mPG_Spawner_PointConfigs);
                file.getTriggerposition();
                file.getSpawnpositions();
            }
        }
        public void SetAllLists()
        {
            foreach (MPG_Spawner_PointConfig file in Points)
            {
                file.setalllists();
                file.setTriggerposition();
                file.setSpawnpositions();
            }
        }
    }

    public class MPG_Spawner_PointConfig
    {
        [JsonIgnore]
        public BindingList<MPG_Spawner_PointConfig> ListtriggerDependencies { get; set; }
        [JsonIgnore]
        public BindingList<MPG_Spawner_PointConfig> ListtriggersToEnableOnEnter { get; set; }
        [JsonIgnore]
        public BindingList<MPG_Spawner_PointConfig> ListtriggersToEnableOnFirstSpawn { get; set; }
        [JsonIgnore]
        public BindingList<MPG_Spawner_PointConfig> ListtriggersToEnableOnWin { get; set; }
        [JsonIgnore]
        public BindingList<MPG_Spawner_PointConfig> ListtriggersToEnableOnLeave { get; set; }


        public int pointId { get; set; }
        public int isDebugEnabled { get; set; }
        public string notificationTitle { get; set; }
        public string notificationTextEnter { get; set; }
        public string notificationTextExit { get; set; }
        public string notificationTextSpawn { get; set; }
        public string notificationTextWin { get; set; }
        public int notificationTime { get; set; }
        public string notificationIcon { get; set; }
        public BindingList<int> triggerDependencies { get; set; }
        public int triggerDependenciesAnyOf { get; set; }
        public BindingList<int> triggersToEnableOnEnter { get; set; }
        public BindingList<int> triggersToEnableOnFirstSpawn { get; set; }
        public BindingList<int> triggersToEnableOnWin { get; set; }
        public BindingList<int> triggersToEnableOnLeave { get; set; }
        public string triggerPosition { get; set; }
        public string triggerDebugColor { get; set; }
        public decimal triggerRadius { get; set; }
        public decimal triggerHeight { get; set; }
        public decimal triggerWidthX { get; set; }
        public decimal triggerWidthY { get; set; }
        public int triggerFirstDelay { get; set; }
        public decimal triggerCooldown { get; set; }
        public decimal triggerSafeDistance { get; set; }
        public int triggerEnterDelay { get; set; }
        public int triggerCleanupOnLeave { get; set; }
        public int triggerCleanupOnLunchTime { get; set; }
        public int triggerCleanupDelay { get; set; }
        public string triggerWorkingTime { get; set; }
        public int triggerDisableOnWin { get; set; }
        public int triggerDisableOnLeave { get; set; }
        public BindingList<string> spawnPositions { get; set; }
        public decimal spawnRadius { get; set; }
        public int spawnMin { get; set; }
        public int spawnMax { get; set; }
        public int spawnCountLimit { get; set; }
        public int spawnLoopInside { get; set; }
        public BindingList<string> spawnList { get; set; }
        public int clearDeathAnimals { get; set; }
        public int clearDeathZombies { get; set; }
        public BindingList<MPG_Spawner_mappingData> mappingData { get; set; }

        [JsonIgnore]
        public Vec3PandR _triggerPosition { get; set; }
        [JsonIgnore]
        public BindingList<Vec3PandR> _spawnPositions { get; set; }


        public MPG_Spawner_PointConfig()
        {

        }
        public override string ToString()
        {
            return "Points:" + pointId.ToString() + "-" + notificationTitle;
        }
        public int[] getworkinghours()
        {
            string[] fasplit = triggerWorkingTime.Split('-');
            return new int[] { Convert.ToInt32(fasplit[0]), Convert.ToInt32(fasplit[1]) };
        }
        public void setworkinghours(int[] _workinghours)
        {
            triggerWorkingTime = _workinghours[0].ToString() + "-" + _workinghours[1].ToString();
        }
        public void getTriggerposition()
        {
            _triggerPosition = new Vec3PandR(triggerPosition);
        }
        public void setTriggerposition()
        {
            triggerPosition = _triggerPosition.GetString();
        }
        public void getSpawnpositions()
        {
            _spawnPositions = new BindingList<Vec3PandR>();
            foreach(string s in spawnPositions)
            {
                _spawnPositions.Add(new Vec3PandR(s));
            }
        }
        public void setSpawnpositions()
        {
            spawnPositions = new BindingList<string>();
            foreach(Vec3PandR vec3PandR in _spawnPositions)
            {
                spawnPositions.Add(vec3PandR.GetString());
            }
        }
        public void setalllists()
        {
            triggerDependencies = new BindingList<int>();
            foreach (MPG_Spawner_PointConfig config in ListtriggerDependencies)
            {
                triggerDependencies.Add(config.pointId);
            }
            triggersToEnableOnEnter = new BindingList<int>();
            foreach (MPG_Spawner_PointConfig config in ListtriggersToEnableOnEnter)
            {
                triggersToEnableOnEnter.Add(config.pointId);
            }
            triggersToEnableOnFirstSpawn = new BindingList<int>();
            foreach (MPG_Spawner_PointConfig config in ListtriggersToEnableOnFirstSpawn)
            {
                triggersToEnableOnFirstSpawn.Add(config.pointId);
            }
            triggersToEnableOnWin = new BindingList<int>();
            foreach (MPG_Spawner_PointConfig config in ListtriggersToEnableOnWin)
            {
                triggersToEnableOnWin.Add(config.pointId);
            }
            triggersToEnableOnLeave = new BindingList<int>();
            foreach (MPG_Spawner_PointConfig config in ListtriggersToEnableOnLeave)
            {
                triggersToEnableOnLeave.Add(config.pointId);
            }
        }
        public void getalllists(List<MPG_Spawner_PointConfig> mPG_Spawner_PointConfigs)
        {
            ListtriggerDependencies = new BindingList<MPG_Spawner_PointConfig>();
            foreach (int id in triggerDependencies)
            {
                ListtriggerDependencies.Add(mPG_Spawner_PointConfigs.FirstOrDefault(x => x.pointId == id));
            }
            ListtriggersToEnableOnEnter = new BindingList<MPG_Spawner_PointConfig>();
            foreach (int id in triggersToEnableOnEnter)
            {
                ListtriggersToEnableOnEnter.Add(mPG_Spawner_PointConfigs.FirstOrDefault(x => x.pointId == id));
            }
            ListtriggersToEnableOnFirstSpawn = new BindingList<MPG_Spawner_PointConfig>();
            foreach (int id in triggersToEnableOnFirstSpawn)
            {
                ListtriggersToEnableOnFirstSpawn.Add(mPG_Spawner_PointConfigs.FirstOrDefault(x => x.pointId == id));
            }
            ListtriggersToEnableOnWin = new BindingList<MPG_Spawner_PointConfig>();
            foreach (int id in triggersToEnableOnWin)
            {
                ListtriggersToEnableOnWin.Add(mPG_Spawner_PointConfigs.FirstOrDefault(x => x.pointId == id));
            }
            ListtriggersToEnableOnLeave = new BindingList<MPG_Spawner_PointConfig>();
            foreach (int id in triggersToEnableOnLeave)
            {
                ListtriggersToEnableOnLeave.Add(mPG_Spawner_PointConfigs.FirstOrDefault(x => x.pointId == id));
            }
        }

        public void ImportDZE(DZE Importfile, bool importTrigger, bool importrotation)
        {
            if (_spawnPositions == null)
                _spawnPositions = new BindingList<Vec3PandR>();
            foreach (Editorobject eo in Importfile.EditorObjects)
            {
                if (eo.DisplayName == "GiftBox_Large_1")
                {
                    if (importTrigger)
                    {
                        _triggerPosition = new Vec3PandR(eo.Position, eo.Orientation, importrotation);
                    }
                }
                else if (eo.DisplayName == "GiftBox_Small_1")
                {
                    _spawnPositions.Add(new Vec3PandR(eo.Position, eo.Orientation, importrotation));
                }
            }
        }
    }
    public class MPG_Spawner_mappingData
    {
        public int addOnStartup { get; set; }
        public int addOnEnter { get; set; }
        public int addOnFirstSpawn { get; set; }
        public int addOnWin { get; set; }
        public decimal addDelay { get; set; }
        public int removeOnEnter { get; set; }
        public int removeOnFirstSpawn { get; set; }
        public int removeOnWin { get; set; }
        public decimal removeDelay { get; set; }
        public BindingList<ITEM_SpawnerObject> mappingObjects { get; set; }
    };
    public class ITEM_SpawnerObject
    {
        public string name { get; set; }
        public decimal[] pos { get; set; }
        public decimal[] ypr { get; set; }
        public decimal scal { get; set; }
        public bool enableCEPersistency { get; set; }
    };
}