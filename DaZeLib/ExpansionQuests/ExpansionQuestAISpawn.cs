﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayZeLib
{
    public class ExpansionQuestAISpawn
    {
        public int NumberOfAI { get; set; }
        public string NPCName { get; set; }
        public BindingList<decimal[]> Waypoints { get; set; }
        public int Behaviour { get; set; }
        public string Formation { get; set; }
        public string Loadout { get; set; }
        public string Faction { get; set; }
        public decimal Speed { get; set; }
        public decimal ThreatSpeed { get; set; }
        public decimal MinAccuracy { get; set; }
        public decimal MaxAccuracy { get; set; }
        public int CanBeLooted { get; set; }
        public int UnlimitedReload { get; set; }
        public decimal ThreatDistanceLimit { get; set; }
        public decimal DamageMultiplier { get; set; }
        public decimal DamageReceivedMultiplier { get; set; }
        public BindingList<string> ClassNames { get; set; }
	    public decimal SniperProneDistanceThreshold { get; set; }

        public decimal RespawnTime { get; set; }
        public decimal DespawnTime { get; set; }
        public decimal MinDistanceRadius { get; set; }
        public decimal MaxDistanceRadius { get; set; }
        public decimal DespawnRadius { get; set; }

        public ExpansionQuestAISpawn()
        {
            Waypoints = new BindingList<decimal[]>();
            ClassNames = new BindingList<string>();
        }

        public void CreateDefaultAISpawn()
        {
            NumberOfAI = 1;
            NPCName = "Quest Target";
            Waypoints = new BindingList<decimal[]>()
            {
                new decimal[] {0,0,0}
            };
            Behaviour = 0;
            Formation = "RANDOM";
            Loadout = "BanditLoadout";
            Faction = "West";
            Speed = (decimal)0.0;
            ThreatSpeed = (decimal)3.0;
            MinAccuracy = (decimal)0.0;
            MaxAccuracy = (decimal)0.0;
            CanBeLooted = 1;
            UnlimitedReload = 1;
            ThreatDistanceLimit = (decimal)1000.0;
            DamageMultiplier = (decimal)1.0;
            DamageReceivedMultiplier = (decimal)1.0;
            ClassNames = new BindingList<string>() {
                            "eAI_SurvivorF_Eva",
                            "eAI_SurvivorF_Frida",
                            "eAI_SurvivorF_Gabi",
                            "eAI_SurvivorF_Helga",
                            "eAI_SurvivorF_Irena",
                            "eAI_SurvivorF_Judy",
                            "eAI_SurvivorF_Keiko",
                            "eAI_SurvivorF_Linda",
                            "eAI_SurvivorF_Maria",
                            "eAI_SurvivorF_Naomi",
                            "eAI_SurvivorF_Baty",
                            "eAI_SurvivorM_Boris",
                            "eAI_SurvivorM_Cyril",
                            "eAI_SurvivorM_Denis",
                            "eAI_SurvivorM_Elias",
                            "eAI_SurvivorM_Francis",
                            "eAI_SurvivorM_Guo",
                            "eAI_SurvivorM_Hassan",
                            "eAI_SurvivorM_Indar",
                            "eAI_SurvivorM_Jose",
                            "eAI_SurvivorM_Kaito",
                            "eAI_SurvivorM_Lewis",
                            "eAI_SurvivorM_Manua",
                            "eAI_SurvivorM_Mirek",
                            "eAI_SurvivorM_Niki",
                            "eAI_SurvivorM_Oliver",
                            "eAI_SurvivorM_Peter",
                            "eAI_SurvivorM_Quinn",
                            "eAI_SurvivorM_Rolf",
                            "eAI_SurvivorM_Seth",
                            "eAI_SurvivorM_Taiki"
                        };
            SniperProneDistanceThreshold = (decimal)300.0;
            RespawnTime = (decimal)1.0;
            DespawnTime = (decimal)1.0;
            MinDistanceRadius = (decimal)50.0;
            MaxDistanceRadius = (decimal)150.0;
            DespawnRadius = (decimal)500.0;
        }

        public override string ToString()
        {
            return NPCName;
        }
    }
}
