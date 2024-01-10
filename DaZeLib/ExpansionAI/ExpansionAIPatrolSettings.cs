﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DayZeLib
{

    public class ExpansionAIPatrolSettings
    {
        [JsonIgnore]
        const int CurrentVersion = 16;
        [JsonIgnore]
        public string Filename { get; set; }
        [JsonIgnore]
        public bool isDirty { get; set; }

        public int m_Version { get; set; }
        public int Enabled { get; set; }
        public decimal DespawnTime { get; set; }
        public decimal RespawnTime { get; set; }
        public decimal MinDistRadius { get; set; }
        public decimal MaxDistRadius { get; set; }
        public decimal DespawnRadius { get; set; }
        public decimal AccuracyMin { get; set; }
        public decimal AccuracyMax { get; set; }
        public decimal ThreatDistanceLimit { get; set; }
        public decimal NoiseInvestigationDistanceLimit { get; set; }
        public decimal DamageMultiplier { get; set; }
        public BindingList<ExpansionAIObjectPatrol> ObjectPatrols { get; set; }
        public BindingList<ExpansionAIPatrol> Patrols { get; set; }


        public ExpansionAIPatrolSettings()
        {
            m_Version = CurrentVersion;
            Enabled = 1;
            DespawnTime = (decimal)600.0;
            RespawnTime = (decimal)-1.0;
            MinDistRadius = (decimal)400.0;
            MaxDistRadius = (decimal)1000.0;
            DespawnRadius = (decimal)1100.0;
            AccuracyMin = (decimal)-1.0;
            AccuracyMax = (decimal)-1.0;
            ThreatDistanceLimit = (decimal)-1.0;
            DamageMultiplier = (decimal)-1.0;
            NoiseInvestigationDistanceLimit = (decimal)-1.0;
            ObjectPatrols = new BindingList<ExpansionAIObjectPatrol>();
            Patrols = new BindingList<ExpansionAIPatrol>();
            DefaultCrashPatrols();
            DefaultPatrols();
        }

        private void DefaultPatrols()
        {
            Patrols.Add(new ExpansionAIPatrol(3, "WALK", "SPRINT", "ALTERNATE", "West", "", true, false, (decimal)1.0, -2, -2, -2, -100, new List<float[]>() { new float[] { 4211.222656f, 109.023384f, 6382.064453f }, new float[] { 4151.662598f, 105.450653f, 6080.294434f }, new float[] { 4160.971680f, 105.412598f, 6035.103027f }, new float[] { 4160.699219f, 106.251480f, 5906.830078f }, new float[] {4107.862793f, 108.930527f, 5898.482910f },new float[] { 4057.258057f, 114.174736f, 5584.595703f },new float[] {4084.560059f, 113.232422f, 5494.540039f }, new float[] { 4079f, .308105f, 113.801163f, 5435.953125f }, new float[] {4081.735840f, 113.402504f, 5385.576172f }, new float[] { 4067.266113f, 109.788383f, 4904.508789f },new float[] { 4126.504883f, 107.371178f, 4647.128906f } }));
            Patrols.Add(new ExpansionAIPatrol(3, "WALK", "SPRINT", "ALTERNATE", "West", "", true, false, (decimal)1.0, -2, -2, -2, -100, new List<float[]>() { new float[] { 4508.860352f, 109.347275f, 6230.751465f }, new float[] { 4448.490723f, 109.377792f, 6025.892578f }, new float[] { 4546.902832f, 112.057549f, 6126.590332f },new float[] {4613.975586f, 110.163887f, 6112.869629f }, new float[] { 4563.825684f, 110.696106f, 5797.450195f }, new float[] { 4551.805664f, 110.555084f, 5652.876953f }, new float[] { 4312.203613f, 110.752151f, 5366.941895f } }));
            Patrols.Add(new ExpansionAIPatrol(1, "WALK", "SPRINT", "ALTERNATE", "Raiders", "", true, false, (decimal)1.0, -2, -2, -2, -100, new List<float[]>() { new float[] { 10545.687500f, 38.037155f, 10384.205078f }, new float[] { 10502.615234f, 40.377762f, 10464.754883f }, new float[] { 10700.634766f, 34.346542f, 10461.470703f }, new float[] { 10645.350586f, 36.451836f, 10377.769531f } }));
            Patrols.Add(new ExpansionAIPatrol(1, "WALK", "SPRINT", "ALTERNATE", "Raiders", "", true, false, (decimal)1.0, -2, -2, -2, -100, new List<float[]>() { new float[] { 8588.209961f, 103.304726f, 13439.002930f }, new float[] { 8578.585938f, 106.485222f, 13465.281250f }, new float[] { 8605.717773f, 112.455276f, 13521.625977f }, new float[] { 8640.509766f, 120.118225f, 13590.360352f }, new float[] {8641.976563f, 127.327774f, 13644.069336f }, new float[] { 8643.523438f, 123.581627f, 13609.848633f },new float[] { 8724.077148f, 121.008499f, 13532.352539f }, new float[] {8792.484375f, 119.344810f, 13479.867188f }, new float[] { 8843.358398f, 122.085854f, 13469.464844f }, new float[] {8881.161133f, 121.293594f, 13413.066406f }, new float [] {8837.004883f, 121.072372f, 13470.006836f }, new float[] { 8742.039063f, 121.246811f, 13516.587891f },new float[] {8704.238281f, 119.762787f, 13546.516602f }, new float[] { 8609.144531f, 113.184517f, 13527.601563f }, new float[] { 8573.016602f, 112.247055f, 13530.662109f }, new float[] {8563.083984f, 118.173904f, 13576.212891f }, new float[] { 8500.525391f, 135.590210f, 13653.769531f }, new float[] {8456.375000f, 139.365845f, 13677.127930f }, new float[] { 8546.610352f, 126.129440f, 13615.544922f },new float[] {8566.747070f, 116.355377f, 13563.688477f }, new float[] { 8573.123047f, 112.562714f, 13532.739258f }, new float[] { 8563.700195f, 107.298088f, 13476.465820f }, new float[] {8586.047852f, 103.664093f, 13442.43557f} }));
            Patrols.Add(new ExpansionAIPatrol(4, "WALK", "SPRINT", "ALTERNATE", "Civilian", "", true, false, (decimal)1.0, -2, -2, -2, -100, new List<float[]>() { new float[] { 7841.334961f, 76.696114f, 105.506462f }, new float[] { 7874.433594f, 86.500465f, 194.182739f }, new float[] { 7881.091797f, 88.818947f, 247.873428f }, new float[] { 7804.406250f, 88.379280f, 365.877625f }, new float[] {7792.430176f, 88.650299f, 383.001404f }, new float[] { 7762.807617f, 83.737671f, 334.874634f }, new float[] { 7760.653809f, 84.683838f, 353.211792f }, new float[] { 7768.957031f, 88.445908f, 421.012054f }, new float[] { 7781.338867f, 89.113235f, 430.440430f }, new float[] { 7769.263672f, 90.858253f, 472.488281f }, new float[] { 7776.356934f, 91.006439f, 493.633820f }, new float[] { 7762.374023f, 91.014488f, 534.747681f }, new float[] { 7739.280762f, 91.134277f, 564.848328f }, new float[] { 7639.000000f, 120.328026f, 769.771545f }, new float[] { 7378.479980f, 94.512924f, 764.417480f }, new float[] { 7325.039063f, 93.792953f, 820.421448f }, new float[] { 7323.554199f, 95.076019f, 869.669067f }, new float[] { 6920.266602f, 84.642204f, 926.835693f }, new float[] { 6942.748047f, 84.112991f, 977.515381f }, new float[] { 6901.287598f, 82.878380f, 1002.580750f }, new float[] { 6848.884766f, 91.492271f, 967.189453f }, new float[] { 6835.840820f, 84.595589f, 942.760620f }, new float[] { 6655.309082f, 92.318848f, 793.134338f }, new float[] { 6639.655273f, 91.830742f, 792.534058f }, new float[] { 6635.730957f, 92.249802f, 780.763000f } }));
            Patrols.Add(new ExpansionAIPatrol(4, "WALK", "SPRINT", "ALTERNATE", "Civilian", "", true, false, (decimal)1.0, -2, -2, -2, -100, new List<float[]>() { new float[] { 9963.346680f, 55.640099f, 10900.844727f }, new float[] { 9965.398438f, 54.729034f, 10969.536133f }, new float[] { 9924.380859f, 57.232151f, 10901.967773f } }));
            Patrols.Add(new ExpansionAIPatrol(4, "WALK", "SPRINT", "ALTERNATE", "West", "", true, false, (decimal)1.0, -2, -2, -2, -100, new List<float[]>() { new float[] { 8796.84f, 59.8781f, 2547 }, new float[] { 8745f, 59.1367f, 2523.2f }, new float[] { 8710.44f, 57.5696f, 2499f }, new float[] {8794.23f, 55.9719f, 2417f },new float[] { 8756.99f, 55.8529f, 2370f },new float[] {8782.76f, 56.1916f, 2345f }, new float[] { 8740.17f, 56.1662f, 2304f }, new float[] {8751.15f, 56.1216f, 2284f }, new float[] { 8828.47f, 56.6495f, 2234f }, new float[] {8764.02f, 53.2922f, 2139f },new float[] { 8656.02f, 47.4257f, 2009f } }));
            Patrols.Add(new ExpansionAIPatrol(4, "WALK", "SPRINT", "ALTERNATE", "East", "", true, false, (decimal)1.0, -2, -2, -2, -100, new List<float[]>() { new float[] { 1583.58f, 292.804f, 2961 }, new float[] { 1600.84f, 294.292f, 3006f },new float[] { 1631.44f, 295.408f, 3029f }, new float[] {1652.89f, 297.99f, 3079.6f }, new float[] { 1685.63f, 297.555f, 3080f } }));
            Patrols.Add(new ExpansionAIPatrol(4, "WALK", "SPRINT", "ALTERNATE", "East", "", true, false, (decimal)1.0, -2, -2, -2, -100, new List<float[]>() { new float[] { 1824.94f, 294.066f, 3075 }, new float[] { 1884.95f, 293.222f, 3047f }, new float[] { 1846.69f, 289.888f, 2996 }, new float[] {1812.78f, 290.333f, 3001f },new float[] { 1796.65f, 287.975f, 2990f } }));
            Patrols.Add(new ExpansionAIPatrol(4, "WALK", "SPRINT", "ALTERNATE", "West", "", true, false, (decimal)1.0, -2, -2, -2, -100,  new List<float[]>() { new float[] { 7160.85f, 84.5561f, 585.604f }, new float[] { 6677.51f, 95.0799f, 770.113f } }));
        }

        private void DefaultCrashPatrols()
        {
            ObjectPatrols.Add(new ExpansionAIObjectPatrol(-3, "WALK", "SPRINT", "HALT_OR_ALTERNATE", "West", "", true, false, (decimal)1.0, -2, -2, "Wreck_UH1Y"));
            ObjectPatrols.Add(new ExpansionAIObjectPatrol(-3, "WALK", "SPRINT", "HALT_OR_ALTERNATE", "East", "", true, false, (decimal)1.0, -2, -2, "Wreck_Mi8_Crashed"));
            ObjectPatrols.Add(new ExpansionAIObjectPatrol(-3, "WALK", "SPRINT", "HALT_OR_ALTERNATE", "East", "PoliceLoadout", true, false, (decimal)1.0, -2, -2, "Land_Wreck_sed01_aban1_police"));
            ObjectPatrols.Add(new ExpansionAIObjectPatrol(-3, "WALK", "SPRINT", "HALT_OR_ALTERNATE", "East", "PoliceLoadout", true, false, (decimal)1.0, -2, -2, "Land_Wreck_sed01_aban2_police"));
            ObjectPatrols.Add(new ExpansionAIObjectPatrol(-3, "JOG", "SPRINT", "HALT_OR_ALTERNATE", "West", "NBCLoadout", true, false, (decimal)1.0, -2, -2, "ContaminatedArea_Static"));
            ObjectPatrols.Add(new ExpansionAIObjectPatrol(-3, "JOG", "SPRINT", "HALT_OR_ALTERNATE", "West", "NBCLoadout", true, false, (decimal)1.0, -2, -2, "ContaminatedArea_Dynamic"));
        }

        public bool checkver()
        {
            if (m_Version != CurrentVersion)
            {
                m_Version = CurrentVersion;
                isDirty = true;
                return true;
            }
            return false;
        }
        public bool SetPatrolNames()
        {
            bool needtosave = false;
            for (int i =0; i < Patrols.Count; i++)
            {
                if (Patrols[i].Name == "" || Patrols[i].Name == null)
                {
                    Patrols[i].Name = "Patrol " + i.ToString();
                    needtosave = true;
                    isDirty = true;
                }
            }
            for (int j = 0; j < ObjectPatrols.Count; j++)
            {
                if (ObjectPatrols[j].Name == "" || ObjectPatrols[j].Name == null)
                {
                    ObjectPatrols[j].Name = "Object Patrol " + j.ToString();
                    needtosave = true;
                    isDirty = true;
                }
            }
            return needtosave;
        }
    }

    public class ExpansionAIObjectPatrol
    {
        public string Name { get; set; }
        public string Faction { get; set; }
        public string Formation { get; set; }
        public decimal FormationLooseness { get; set; }
        public string LoadoutFile { get; set; }
        public int NumberOfAI { get; set; }
        public string Behaviour { get; set; }
        public string Speed { get; set; }
        public string UnderThreatSpeed { get; set; }
        public int CanBeLooted { get; set; }
        public int UnlimitedReload { get; set; }
        public decimal SniperProneDistanceThreshold { get; set; }
        public decimal AccuracyMin { get; set; }
        public decimal AccuracyMax { get; set; }
        public decimal ThreatDistanceLimit { get; set; }
        public decimal DamageMultiplier { get; set; }
        public decimal MinDistRadius { get; set; }
        public decimal MaxDistRadius { get; set; }
        public decimal DespawnRadius { get; set; }
        public decimal MinSpreadRadius { get; set; }
        public decimal MaxSpreadRadius { get; set; }
        public decimal Chance { get; set; }
        public string WaypointInterpolation { get; set; }
        public decimal DespawnTime { get; set; }
        public decimal RespawnTime { get; set; }
        public string ClassName { get; set; }

        public ExpansionAIObjectPatrol() { }
        public ExpansionAIObjectPatrol(int bod = 1, string spd = "JOG", string threatspd = "SPRINT", string beh = "ALTERNATE", string fac = "WEST", string loa = "", bool canbelooted = true, bool unlimitedreload = false, decimal chance = (decimal)1.0, decimal mindistradius = -2, decimal maxdistradius = -2, string classname = "Wreck_UH1Y")
        {
            NumberOfAI = bod;
            Speed = spd;
            UnderThreatSpeed = threatspd;
            Behaviour = beh;
            Faction = fac;
            LoadoutFile = loa;
            CanBeLooted = canbelooted == true ? 1 : 0; ;
            UnlimitedReload = unlimitedreload == true ? 1 : 0;
            AccuracyMin = -1;
            AccuracyMax = -1;
            ThreatDistanceLimit = -1;
            DamageMultiplier = -1;
            ClassName = classname;
            DefaultSpread();
        }
        void DefaultSpread()
        {
            if (Behaviour == "HALT")
            {
                if (ClassName == "ContaminatedArea_Static" || ClassName == "ContaminatedArea_Dynamic")
                {
                    MinSpreadRadius = 0;
                    MaxSpreadRadius = 50;
                }
                else
                {
                    MinSpreadRadius = 5;
                    MaxSpreadRadius = 10;
                }
            }
            else
            {
                if (ClassName == "ContaminatedArea_Static" || ClassName == "ContaminatedArea_Dynamic")
                {
                    MinSpreadRadius = 0;
                    MaxSpreadRadius = 150;
                }
                else
                {
                    MinSpreadRadius = 5;
                    MaxSpreadRadius = 20;
                }
            }
        }
        public override string ToString()
        {
            return Name;
        }
    }

    public class ExpansionAIPatrol
    {
        public string Name { get; set; }
        public string Faction { get; set; }
        public string Formation { get; set; }
        public decimal FormationLooseness { get; set; }
        public string LoadoutFile { get; set; }
        public int NumberOfAI { get; set; }
        public string Behaviour { get; set; }
        public string Speed { get; set; }
        public string UnderThreatSpeed { get; set; }
        public int CanBeLooted { get; set; }
        public int UnlimitedReload { get; set; }
        public decimal SniperProneDistanceThreshold { get; set; }
        public decimal AccuracyMin { get; set; }
        public decimal AccuracyMax { get; set; }
        public decimal ThreatDistanceLimit { get; set; }
        public decimal DamageMultiplier { get; set; }
        public decimal MinDistRadius { get; set; }
        public decimal MaxDistRadius { get; set; }
        public decimal DespawnRadius { get; set;  }
        public decimal MinSpreadRadius { get; set; }
        public decimal MaxSpreadRadius { get; set; }
        public decimal Chance { get; set; }
        public string WaypointInterpolation { get; set; }
        public decimal DespawnTime { get; set; }
        public decimal RespawnTime { get; set; }
        public int UseRandomWaypointAsStartPoint { get; set; }
        public BindingList<float[]> Waypoints { get; set; }

        public ExpansionAIPatrol() { }
        public ExpansionAIPatrol(int bod = 1, string spd = "JOG", string threatspd = "SPRINT", string beh = "ALTERNATE", string fac = "WEST", string loa = "HumanLoadout", bool canbelooted = true, bool unlimitedreload = false, decimal chance = (decimal)1.0, float mindistradius = -2, float maxdistradius = -2, decimal respawntime = -2, decimal wprnd = 0, List<float[]> way = null)
        {
            Name = "";
            NumberOfAI = bod;
            Speed = spd;
            UnderThreatSpeed = threatspd;
            Behaviour = beh;
            Faction = fac;
            LoadoutFile = loa;
            CanBeLooted = canbelooted == true ? 1 : 0;
            UnlimitedReload = unlimitedreload == true ? 1 : 0; ;
            AccuracyMin = -1;
            AccuracyMax = -1;
            ThreatDistanceLimit = -1;
            DamageMultiplier = -1;
            RespawnTime = respawntime;
            MinSpreadRadius = 1;
            MaxSpreadRadius = wprnd;
            UseRandomWaypointAsStartPoint = 1;
            Waypoints = new BindingList<float[]>(way);
            Waypoints = new BindingList<float[]>();
        }
        public override string ToString()
        {
            return Name;
        }
    }

}
