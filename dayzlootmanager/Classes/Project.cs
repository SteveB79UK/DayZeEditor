﻿using DayZeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DayZeEditor
{
    public class MissionTemplate
    {
        public string m_TemplatePath { get; set; }
        public string m_DisplayName { get; set; }
        public string m_mpmissionName { get; set; }
        public int m_Mapsize { get; set; }
        public override string ToString()
        {
            return m_DisplayName;
        }
    }
    public class ProjectList
    {
        public bool ShowChangeLog { get; set; }
        public string ActiveProject { get; set; }
        public List<Project> Projects { get; set; }

        public ProjectList()
        {
            Projects = new List<Project>();
        }
        public void addtoprojects(Project project, bool setactive = true)
        {
            Projects.Add(project);
            if(setactive)
                ActiveProject = project.ProjectName;
            SaveProject(false, false);
        }
        public Project getprojectfromname(string name)
        {
            if (name == null || name == "")
                return null;
            Project p = new Project();
            if (name.Contains(":"))
            {
                string[] stuff = name.Split(':');
                p = Projects.FirstOrDefault(x => x.ProjectName == stuff[0] && x.mpmissionpath.Split('.')[1] == stuff[1]);
            }
            else
            {
                p = Projects.FirstOrDefault(x => x.ProjectName == name);
                ActiveProject = p.ProjectName + ":" + p.mpmissionpath.Split('.')[1];
                SaveProject(false, false);
            }
            return p;
        }
        public void SaveProject(bool create = false, bool showmessage = true)
        {
            var options = new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
            string jsonString = JsonSerializer.Serialize(this, options);
            Directory.CreateDirectory(Application.StartupPath + "\\Project");
            File.WriteAllText(Application.StartupPath + "\\Project\\Projects.json", jsonString);
            var form = Application.OpenForms["SplashForm"];
            if (form != null)
            {
                form.Invoke(new Action(() => { form.Close(); }));
            }
            if (showmessage)
            {
                if (create)
                    MessageBox.Show("Projects Config Created.");
                else
                    MessageBox.Show("Projects Config Saved.");
            }
        }
        public void SetActiveProject(Project p = null)
        {
            if (p == null)
            {
                ActiveProject = "";
                return;
            }
            if(p.MapNetworkDrive == true)
            {
                NetworkDrive.MapNetworkDrive(p.Networkdriveletter , @"\\sshfs\" + p.NetworkUsername + p.NetworkHost);
                while (!NetworkDrive.IsDriveMapped(p.Networkdriveletter))
                {

                }
            }
           
            Console.WriteLine("INFO Network drive Mapped : " + p.Networkdriveletter);
            ActiveProject = p.ProjectName + ":" + p.mpmissionpath.Split('.')[1];
            SaveProject(false, false);
        }
        public Project getActiveProject()
        {
            return getprojectfromname(ActiveProject);
        }
        internal void DeleteProject(string profilename)
        {
            Project p = getprojectfromname(profilename);
            Projects.Remove(p);
        }
    }
    public class Project
    {
        public string ProjectName { get; set; }
        public string projectFullName { get; set; }
        public string ProfilePath { get; set; }
        public bool usingDrJoneTrader { get; set; }
        public bool usingexpansionMarket { get; set; }
        public bool usingtraderplus { get; set; }
        public string MapPath { get; set; }
        public int MapSize { get; set; }
        public string mpmissionpath { get; set; }
        public bool Createbackups { get; set; }
        public bool MapNetworkDrive { get; set; }
        public string Networkdriveletter { get; set; }
        public string NetworkUsername { get; set; }
        public string NetworkHost { get; set; }


        [JsonIgnore]
        public bool haswarnings { get; set; }
        [JsonIgnore]
        public economycoreconfig EconomyCore { get; set; }
        [JsonIgnore]
        public Limitsdefinitions limitfefinitions { get; set; }
        [JsonIgnore]
        public Limitsdefinitionsuser limitfefinitionsuser { get; set; }
        [JsonIgnore]
        public cfgplayerspawnpoints cfgplayerspawnpoints { get; set; }
        [JsonIgnore]
        public BindingList<eventscofig> ModEventsList { get; set; }
        [JsonIgnore]
        public cfgeventspawns cfgeventspawns { get; set; }
        [JsonIgnore]
        public cfgeventgroups cfgeventgroups { get; set; }
        [JsonIgnore]
        public globalsconfig gloabsconfig { get; set; }
        [JsonIgnore]
        public cfgignorelist cfgignorelist { get; set; }
        [JsonIgnore]
        public BindingList<Spawnabletypesconfig> spawnabletypesList { get; set; }
        [JsonIgnore]
        public cfgrandompresetsconfig cfgrandompresetsconfig { get; set; }
        [JsonIgnore]
        public CFGGameplayConfig CFGGameplayConfig { get; set; }
        [JsonIgnore]
        public cfgEffectAreaConfig cfgEffectAreaConfig { get; set; }
        [JsonIgnore]
        public weatherconfig weatherconfig { get; set; }
        [JsonIgnore]
        public mapgroupproto mapgroupproto { get; set; }
        [JsonIgnore]
        public mapgrouppos mapgrouppos { get; set; }

        [JsonIgnore]
        public int TotalNomCount { get; set; }

        private TypesFile vanillaTypes;
        private BindingList<TypesFile> ModTypesList;
        public BindingList<territoriesConfig> territoriesList;

        public Project()
        {
            ProjectName = "";
            projectFullName = "";
            ProfilePath = "";
            MapPath = "";
            MapSize = 0;
            mpmissionpath = "";
            Createbackups = false;
            MapNetworkDrive = false;
            NetworkHost = "";
            NetworkUsername = "";
            Networkdriveletter = "";
            usingDrJoneTrader = false;
            usingexpansionMarket = false;
            usingtraderplus = false;
        }
        public override string ToString()
        {
            return ProjectName + ":" + mpmissionpath.Split('.')[1];
        }

        public void AddNames(string _ProjectName, string fullname)
        {
            projectFullName = fullname;
            ProjectName = _ProjectName;
        }
        public void setVanillaTypes()
        {
            vanillaTypes = new TypesFile(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\db\\types.xml");
        }
        public void SetmodTypes(string filename)
        {
            if (ModTypesList == null)
                ModTypesList = new BindingList<TypesFile>();
            TypesFile tf = new TypesFile(filename);
            tf.modname = Path.GetFileNameWithoutExtension(filename);
            ModTypesList.Add(tf);
        }
        public TypesFile getvanillatypes()
        {
            return vanillaTypes;
        }
        public List<TypesFile> getModList()
        {
            return ModTypesList.ToList();
        }
        public TypesFile GetModTypebyname(string name)
        {
            return ModTypesList.FirstOrDefault(x => x.modname == name);
        }
        public eventscofig geteventbyname(string name)
        {
            return ModEventsList.FirstOrDefault(x => x.Filename == name);
        }
        private Spawnabletypesconfig getspawnablwetyprbyname(string filename)
        {
            return spawnabletypesList.FirstOrDefault(x => x.Filename == filename);
        }
        public void SetModListtypes()
        {
            bool needsave = false;
            ModTypesList = new BindingList<TypesFile>();
            if (EconomyCore.economycore == null) return;
            Console.WriteLine("\n***Starting custom types from Economycore***");
            foreach (economycoreCE mods in EconomyCore.economycore.ce)
            {
                if(mods.folder.Contains("\\"))
                {
                    mods.folder = mods.folder.Replace("\\", "/");
                    needsave = true;
                }
                string path = projectFullName + "\\mpmissions\\" + mpmissionpath + "\\" + mods.folder;
                if (!Directory.Exists(path))
                {
                    haswarnings = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Environment.NewLine + "### Warning ### ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(path + " does not exis, please remove full ce section from economy core." + Environment.NewLine);
                    continue;
                }
                foreach(economycoreCEFile file in mods.file)
                {
                    if (!File.Exists(path + "\\" + file.name))
                    {
                        haswarnings = true;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(Environment.NewLine + "### Warning ### ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(path + "\\" + file.name + " Does not exist, please remove from economy core."+ Environment.NewLine);
                        continue;
                    }
                    if (file.type == "types")
                        SetmodTypes(path + "\\" + file.name);
                }
            }
            Console.WriteLine("***End custom types from Economycore***");
            Console.WriteLine(Environment.NewLine);
            if (needsave)
            {
                EconomyCore.SaveEconomycore();
            }
        }
        internal void removeMod(string modname)
        {
            ModTypesList.Remove(GetModTypebyname(modname));
        }
        internal void removeevent(string filename)
        {
            ModEventsList.Remove(geteventbyname(filename));
        }
        internal void removeSpawnabletype(string filename)
        {
            spawnabletypesList.Remove(getspawnablwetyprbyname(filename));
        }
        internal void seteconomycore()
        {
            EconomyCore = new economycoreconfig(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\cfgeconomycore.xml");
        }
        internal void seteconomydefinitions()
        {
            limitfefinitions = new Limitsdefinitions(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\cfglimitsdefinition.xml");
        }
        internal void SetEvents()
        {
            ModEventsList = new BindingList<eventscofig>();
            if(!File.Exists(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\db\\events.xml"))
            {
                XDocument xmlFile = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
                xmlFile.Add(new XElement("events"));
                xmlFile.Save(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\db\\events.xml");
                Console.WriteLine("Vanilla events.xml File not found.... Creating blank XML");
            }
            ModEventsList.Add(new eventscofig(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\db\\events.xml"));
            if (EconomyCore.economycore == null) return;
            foreach (economycoreCE mods in EconomyCore.economycore.ce)
            {
                string path = projectFullName + "\\mpmissions\\" + mpmissionpath + "\\" + mods.folder;
                foreach (economycoreCEFile file in mods.file)
                {
                    if (!Directory.Exists(path))
                    {
                        haswarnings = true;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(Environment.NewLine + "### Warning ### ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(path + " does not exis, please remove full ce section from economy core." + Environment.NewLine);
                        continue;
                    }
                    if (file.type == "events")
                    {
                        if(!File.Exists(path + "\\" + file.name))
                        {
                            haswarnings = true;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(Environment.NewLine + "### Warning ### ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(path + "\\" + file.name + " Does not exist, please remove from economy core." + Environment.NewLine);
                            continue;
                        }
                        ModEventsList.Add(new eventscofig(path + "\\" + file.name));
                    }
                }
            }
        }
        internal bool isUsingExpansion()
        {
            if (Directory.Exists(projectFullName + "\\" + ProfilePath + "\\ExpansionMod"))
                return true;
            else
                return false;
        }
        internal void SetSpawnabletypes()
        {
            spawnabletypesList = new BindingList<Spawnabletypesconfig>();
            if (!File.Exists(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\cfgspawnabletypes.xml"))
            {
                XDocument xmlFile = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
                xmlFile.Add(new XElement("spawnabletypes"));
                xmlFile.Save(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\cfgspawnabletypes.xml");
                Console.WriteLine("Vanilla cfgspawnabletypes.xml File not found.... Creating blank XML");
            }
            spawnabletypesList.Add(new Spawnabletypesconfig(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\cfgspawnabletypes.xml"));
            if (EconomyCore.economycore == null) return;
            foreach (economycoreCE mods in EconomyCore.economycore.ce)
            {
                string path = projectFullName + "\\mpmissions\\" + mpmissionpath + "\\" + mods.folder;
                if (!Directory.Exists(path))
                {
                    haswarnings = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Environment.NewLine + "### Warning ### ");
                    Console.ForegroundColor = ConsoleColor.White; 
                    Console.WriteLine(path + " Does not exist. please remove full ce section from economycore" + Environment.NewLine);
                    continue;
                }
                foreach (economycoreCEFile file in mods.file)
                {
                    if (file.type == "spawnabletypes")
                    {
                        if (!File.Exists(path + "\\" + file.name))
                        {
                            haswarnings = true;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(Environment.NewLine + "### Warning ### ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(path + "\\" + file.name + "\\" + file.name + " Does not exist, please remove from economy core." + Environment.NewLine);
                            continue;
                        }
                        spawnabletypesList.Add(new Spawnabletypesconfig(path + "\\" + file.name));
                    }
                }
            }
        }
        internal void SetTerritories()
        {
            territoriesList = new BindingList<territoriesConfig>();
            string[] Territoryfiles = Directory.GetFiles(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\env");
            Console.WriteLine("****Starting Territory files****");
            foreach (string file in Territoryfiles)
            {
                if(Path.GetExtension(file) == ".xml")
                    territoriesList.Add(new territoriesConfig(file));
            }
            Console.WriteLine("****End Territory files****");
        }
        internal void SetRandompresets()
        {
            if (!File.Exists(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\cfgrandompresets.xml"))
            {
                XDocument xmlFile = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
                xmlFile.Add(new XElement("randompresets"));
                xmlFile.Save(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\cfgrandompresets.xml");
                Console.WriteLine("Vanilla cfgrandompresets.xml File not found.... Creating blank XML");
            }
            cfgrandompresetsconfig = new cfgrandompresetsconfig(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\cfgrandompresets.xml");
        }
        internal void setuserdefinitions()
        {
            limitfefinitionsuser  = new Limitsdefinitionsuser(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\cfglimitsdefinitionuser.xml");
        }
        internal void setplayerspawns()
        {
            cfgplayerspawnpoints = new cfgplayerspawnpoints(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\cfgplayerspawnpoints.xml");
        }
        internal void seteventspawns()
        {
            cfgeventspawns = new cfgeventspawns(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\cfgeventspawns.xml");
        }
        internal void seteventgroups()
        {
            cfgeventgroups = new cfgeventgroups(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\cfgeventgroups.xml");
        }
        internal void SetIgnoreList()
        {
            cfgignorelist = new cfgignorelist(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\cfgignorelist.xml");
        }
        internal void SetGlobals()
        {
            gloabsconfig = new globalsconfig(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\db\\globals.xml");
        }
        internal void Setmapgrouproto()
        {
            mapgroupproto = new mapgroupproto(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\mapgroupproto.xml");
        }
        internal void Setmapgroupos()
        {
            mapgrouppos = new mapgrouppos(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\mapgrouppos.xml");
        }
        internal void SetWeather()
        {
            weatherconfig = new weatherconfig(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\cfgweather.xml");
        }
        internal void SetTotNomCount()
        {
            TotalNomCount = 0;
            List<typesType> typelistforcount = new List<typesType>();
            foreach(typesType _type in vanillaTypes.types.type)
            {
                if (_type.nominalSpecified)
                {
                    if(typelistforcount.Any(x => x.name == _type.name))
                    {
                        typesType otype = typelistforcount.First(x => x.name == _type.name);

                        TotalNomCount -= otype.nominal;
                        typelistforcount.Remove(otype);
                    }
                    TotalNomCount += _type.nominal;
                    typelistforcount.Add(_type);
                }
            }
            foreach(TypesFile tf in ModTypesList)
            {
                foreach(typesType _type in tf.types.type)
                {
                    if (_type.nominalSpecified)
                    {
                        if (typelistforcount.Any(x => x.name == _type.name))
                        {
                            typesType otype = typelistforcount.First(x => x.name == _type.name);

                            TotalNomCount -= otype.nominal;
                            typelistforcount.Remove(otype);
                        }
                        TotalNomCount += _type.nominal;
                        typelistforcount.Add(_type);
                    }
                }
            }
        }
        internal string getcorrectclassamefromtypes(string className)
        {
            typesType vantype = vanillaTypes.types.type.FirstOrDefault(x => x.name.ToLower() == className.ToLower());
            if (vantype != null)
                return vantype.name;
            else
            {
                foreach(TypesFile tfile in ModTypesList)
                {
                    typesType modvantype = tfile.types.type.FirstOrDefault(x => x.name.ToLower() == className.ToLower());
                    if (modvantype != null)
                        return modvantype.name;
                }
            }
            return "*** MISSING ITEM TYPE (" + className + ")***";
        }
        internal void SetCFGGameplayConfig()
        {
            CFGGameplayConfig = new CFGGameplayConfig(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\cfggameplay.json");
            CFGGameplayConfig.GetSpawnGearFiles(projectFullName + "\\mpmissions\\" + mpmissionpath);
        }
        internal void SetcfgEffectAreaConfig()
        {
            cfgEffectAreaConfig = new cfgEffectAreaConfig(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\cfgEffectArea.json");
        }
        internal bool Setmappedrive()
        {
            if (MapNetworkDrive == true)
            {
                NetworkDrive.MapNetworkDrive(Networkdriveletter, @"\\sshfs\" + NetworkUsername + NetworkHost);
                Stopwatch s = new Stopwatch();
                s.Start();
                while (!NetworkDrive.IsDriveMapped(Networkdriveletter) && s.Elapsed < TimeSpan.FromSeconds(30))
                {
                    
                }
                if (s.Elapsed >= TimeSpan.FromSeconds(30))
                {
                    s.Stop();
                    Console.WriteLine("ERROR: Network Drive Not Mapped : " + Networkdriveletter);
                    return false;
                }
                Console.WriteLine("INFO: Network Drive Mapped : " + Networkdriveletter);
                return true;
            }
            return true;
        }
    }
}
