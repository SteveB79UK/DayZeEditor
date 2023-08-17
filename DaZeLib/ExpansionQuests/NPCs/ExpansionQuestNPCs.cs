﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DayZeLib
{
    public class QuestNPCLists
    {
        const int m_NPCConfigVersion = 4;
        public static int getNPCConfigVersion
        {
            get { return m_NPCConfigVersion; }
        }

        public BindingList<ExpansionQuestNPCs> NPCList { get; set; }
        public string NPCPath { get; set; }
        public List<ExpansionQuestNPCs> Markedfordelete { get; set; }

        public QuestNPCLists()
        {
            NPCList = new BindingList<ExpansionQuestNPCs>();
        }
        public QuestNPCLists(string m_Path, bool createfolder = true)
        {
            NPCPath = m_Path;
            NPCList = new BindingList<ExpansionQuestNPCs>();
            if (createfolder)
            {
                if (!Directory.Exists(m_Path))
                    Directory.CreateDirectory(m_Path);
            }
            DirectoryInfo dinfo = new DirectoryInfo(m_Path);
            FileInfo[] Files = dinfo.GetFiles("*.json");
            Console.WriteLine("Getting expansion Quests");
            Console.WriteLine(Files.Length.ToString() + " Found");
            foreach (FileInfo file in Files)
            {
                try
                {
                    Console.WriteLine("serializing " + file.Name);
                    ExpansionQuestNPCs NPC = JsonSerializer.Deserialize<ExpansionQuestNPCs>(File.ReadAllText(file.FullName));
                    NPC.Filename = Path.GetFileNameWithoutExtension(file.Name);
                    if (NPC.ConfigVersion != QuestNPCLists.getNPCConfigVersion)
                    {
                        NPC.ConfigVersion = QuestNPCLists.getNPCConfigVersion;
                        NPC.isDirty = true;
                    }
                    NPCList.Add(NPC);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("there is an error in the following file\n" + file.FullName + Environment.NewLine + ex.InnerException.Message);

                }
            }
            NPCList = new BindingList<ExpansionQuestNPCs>(NPCList.OrderBy(x => x.ID).ToList());
        }
        public int GetNextID()
        {
            List<int> NumberofNPC = new List<int>();
            foreach (ExpansionQuestNPCs npcs in NPCList)
            {
                NumberofNPC.Add(npcs.ID);
            }
            NumberofNPC.Sort();
            if (NumberofNPC.Count == 0)
                return 1;
            List<int> result = Enumerable.Range(1, NumberofNPC.Max() + 1).Except(NumberofNPC).ToList();
            result.Sort();
            return result[0];
        }
        public void CreateNewNPC()
        {
            int NEWID = GetNextID();
            ExpansionQuestNPCs newnpc = new ExpansionQuestNPCs()
            {
                isDirty = true,
                Filename = "QuestNPC_" + NEWID.ToString(),
                ConfigVersion = m_NPCConfigVersion,
                ID = NEWID,
                ClassName = "ExpansionQuestNPCDenis",
                IsAI = 0,
                Position = new float[] { 0, 0, 0 },
                Orientation = new float[] { 0, 0, 0 },
                NPCName = "Phil McCracken",
                DefaultNPCText = "You Looking at me!!!",
                Waypoints = new BindingList<float[]>(),
                NPCEmoteID = 46,
                NPCEmoteIsStatic = 0,
                NPCLoadoutFile = "NBCLoadout",
                IsStatic = 0,
                NPCInteractionEmoteID = 1,
                NPCQuestCancelEmoteID = 60,
                NPCQuestStartEmoteID = 58,
                NPCQuestCompleteEmoteID = 39,
                NPCFaction = "InvincibleObservers"
            };
            NPCList.Add(newnpc);
        }
        public void RemoveNPC(ExpansionQuestNPCs currentQuestNPC)
        {
            if (Markedfordelete == null) Markedfordelete = new List<ExpansionQuestNPCs>();
            Markedfordelete.Add(currentQuestNPC);
            NPCList.Remove(currentQuestNPC);
        }
        public ExpansionQuestNPCs GetNPCFromID(int id)
        {
            if(NPCList.Any(x => x.ID == id))
                return NPCList.First(x => x.ID == id);
            return null;
        }
    }
    public class ExpansionQuestNPCs
    {
        [JsonIgnore]
        public string Filename { get; set; }
        [JsonIgnore]
        public bool isDirty = false;

        public int ConfigVersion { get; set; }
        public int ID { get; set; }
        public string ClassName { get; set; }
        public int IsAI { get; set; }
        public float[] Position { get; set; }
        public float[] Orientation { get; set; }
        public string NPCName { get; set; }
        public string DefaultNPCText { get; set; }
        public BindingList<float[]> Waypoints { get; set; }
        public int NPCEmoteID { get; set; }
        public int NPCEmoteIsStatic { get; set; }
        public string NPCLoadoutFile { get; set; }
        public int IsStatic { get; set; }
        public int NPCInteractionEmoteID { get; set; }
        public int NPCQuestCancelEmoteID { get; set; }
        public int NPCQuestStartEmoteID { get; set; }
        public int NPCQuestCompleteEmoteID { get; set; }
        public string NPCFaction { get; set; }

        public ExpansionQuestNPCs()
        {
            ConfigVersion = QuestNPCLists.getNPCConfigVersion;
        }
        public void backupandDelete(string NPCPath)
        {
            string SaveTime = DateTime.Now.ToString("ddMMyy_HHmm");
            string Fullfilename = NPCPath + "\\" + Filename + ".json";
            if (File.Exists(Fullfilename))
            {
                Directory.CreateDirectory(NPCPath + "\\Backup\\" + SaveTime);
                File.Copy(Fullfilename, NPCPath + "\\Backup\\" + SaveTime + "\\" + Filename + ".bak");
                File.Delete(Fullfilename);
            }
        }
        public override string ToString()
        {
            return NPCName;
        }



    }

}
