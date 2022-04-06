﻿using DayZeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

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
        //public BindingList<MissionTemplate> AvailableTemplates { get; set; }
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
            SaveProject();
        }
        public Project getprojectfromname(string name)
        {
            return Projects.FirstOrDefault(x => x.ProjectName == name);
        }
        public void SaveProject()
        {
            var options = new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
            string jsonString = JsonSerializer.Serialize(this, options);
            Directory.CreateDirectory(Application.StartupPath + "\\Project");
            File.WriteAllText(Application.StartupPath + "\\Project\\Projects.json", jsonString);
        }
        public void SetActiveProject(string profilename)
        {
            Project p = getprojectfromname(profilename);
            ActiveProject = profilename;
            SaveProject();
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
        public bool isExpansion { get; set; }
        public bool usingDrJoneTrader { get; set; }
        public bool usingexpansionMarket { get; set; }
        public bool usingtraderplus { get; set; }
        public string MapPath { get; set; }
        public int MapSize { get; set; }
        public string mpmissionpath { get; set; }

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
        public globalsconfig gloabsconfig { get; set; }
        [JsonIgnore]
        public BindingList<Spawnabletypesconfig> spawnabletypesList { get; set; }
        [JsonIgnore]
        public cfgrandompresetsconfig cfgrandompresetsconfig { get; set; }

        [JsonIgnore]
        public int TotalNomCount { get; set; }

        private TypesFile vanillaTypes;
        private BindingList<TypesFile> ModTypesList;


        public Project()
        {
   
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
        public void SetModListtypes()
        {
            ModTypesList = new BindingList<TypesFile>();
            foreach(ce mods in EconomyCore.economycore.ce)
            {
                string path = projectFullName + "\\mpmissions\\" + mpmissionpath + "\\" + mods.folder;
                foreach(file file in mods.file)
                {
                    if (file.type == "types")
                        SetmodTypes(path + "\\" + file.name);
                }
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
            ModEventsList.Add(new eventscofig(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\db\\events.xml"));
            foreach (ce mods in EconomyCore.economycore.ce)
            {
                string path = projectFullName + "\\mpmissions\\" + mpmissionpath + "\\" + mods.folder;
                foreach (file file in mods.file)
                {
                    if (file.type == "events")
                    {
                        ModEventsList.Add(new eventscofig(path + "\\" + file.name));
                    }
                }
            }
        }
        internal void SetSpawnabletypes()
        {
            spawnabletypesList = new BindingList<Spawnabletypesconfig>();
            spawnabletypesList.Add(new Spawnabletypesconfig(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\cfgspawnabletypes.xml"));
            foreach (ce mods in EconomyCore.economycore.ce)
            {
                string path = projectFullName + "\\mpmissions\\" + mpmissionpath + "\\" + mods.folder;
                foreach (file file in mods.file)
                {
                    if (file.type == "spawnabletypes")
                    {
                        spawnabletypesList.Add(new Spawnabletypesconfig(path + "\\" + file.name));
                    }
                }
            }
        }
        internal void SetRandompresets()
        {
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
        internal void SetGlobals()
        {
            gloabsconfig = new globalsconfig(projectFullName + "\\mpmissions\\" + mpmissionpath + "\\db\\globals.xml");
        }

        internal void SetTotNomCount()
        {
            TotalNomCount = 0;
            List<type> typelistforcount = new List<type>();
            foreach(type _type in vanillaTypes.types.type)
            {
                if (_type.nominal != null)
                {
                    if(typelistforcount.Any(x => x.name == _type.name))
                    {
                        type otype = typelistforcount.First(x => x.name == _type.name);

                        TotalNomCount -= otype.nominal;
                        typelistforcount.Remove(otype);
                    }
                    TotalNomCount += _type.nominal;
                    typelistforcount.Add(_type);
                }
            }
            foreach(TypesFile tf in ModTypesList)
            {
                foreach(type _type in tf.types.type)
                {
                    if (_type.nominal != null)
                    {
                        if (typelistforcount.Any(x => x.name == _type.name))
                        {
                            type otype = typelistforcount.First(x => x.name == _type.name);

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
            type vantype = vanillaTypes.types.type.FirstOrDefault(x => x.name.ToLower() == className.ToLower());
            if (vantype != null)
                return vantype.name;
            else
            {
                foreach(TypesFile tfile in ModTypesList)
                {
                    type modvantype = tfile.types.type.FirstOrDefault(x => x.name.ToLower() == className.ToLower());
                    if (modvantype != null)
                        return modvantype.name;
                }
            }
            return "*** MISSING ITEM TYPE (" + className + ")***";
        }
    }

}
