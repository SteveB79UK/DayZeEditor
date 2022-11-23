﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DayZeLib
{
    public class QuestPersistentServerData
    {
        [JsonIgnore]
        public const int m_currentConfigVersion = 0;
        [JsonIgnore]
        public string Filename { get; set; }
        [JsonIgnore]
        public bool isDirty = false;

        public int ConfigVersion { get; set; }
        public int LastWeeklyReset { get; set; }
        public int LastDailyReset { get; set; }

        public QuestPersistentServerData()
        {
            ConfigVersion = m_currentConfigVersion;
            LastDailyReset = 1662883200;
            LastDailyReset = 1662883200;
        }

        public bool checkver()
        {
            if (ConfigVersion != m_currentConfigVersion)
            {
                ConfigVersion = m_currentConfigVersion;
                isDirty = true;
                return true;
            }
            return false;
        }
    }

}
