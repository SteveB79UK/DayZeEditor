﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DayZeLib
{
    public class DamageSystemSettings
    {
        static int CurrentVersion = 1;

        public int m_Version { get; set; }
        public int Enabled { get; set; }
        public int CheckForBlockingObjects { get; set; }
        public BindingList<string> ExplosionTargets { get; set; }
        public Dictionary<string,string> ExplosiveProjectiles { get; set; }

        [JsonIgnore]
        public BindingList<ExplosiveProjectiles> explosinvesList;
        [JsonIgnore]
        public string Filename { get; set; }
        [JsonIgnore]
        public bool isDirty;


        public void ConvertDicttolist()
        {
            explosinvesList = new BindingList<ExplosiveProjectiles>();
            foreach(KeyValuePair<string,string> e in ExplosiveProjectiles)
            {
                ExplosiveProjectiles newe = new ExplosiveProjectiles()
                {
                    explosion = e.Key,
                    ammo = e.Value
                };
                explosinvesList.Add(newe);
            }
        }
        public void ConvertListtodict()
        {
            ExplosiveProjectiles = new Dictionary<string, string>();
            foreach (ExplosiveProjectiles ep in explosinvesList)
            {
                ExplosiveProjectiles.Add(ep.explosion, ep.ammo);
            }
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
    }
    public class ExplosiveProjectiles
    {
        public string explosion { get; set; }
        public string ammo { get; set; }

        public override string ToString()
        {
            return explosion;
        }
    }
}
