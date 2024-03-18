﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DayZeLib
{
    public class RagBasebuilding
    {
        [JsonIgnore]
        public string Filename { get; set; }
        [JsonIgnore]
        public bool isDirty;

        public BindingList<string> BaseBuildTools { get; set; }
        public BindingList<string> BaseDismantleTools { get; set; }
        public BindingList<string> BaseDestroyToolsWood { get; set; }
        public int BaseBuildTime { get; set; }
        public int BaseDismantleTime { get; set; }
        public int BaseDestroyTimeDefault { get; set; }
        public int BaseDestroyTimeWood { get; set; }
        public int BaseBuildToolDamage { get; set; }
        public int BaseDismantleToolDamage { get; set; }
        public int BaseDestroyToolDamageWood { get; set; }
        public bool BaseCanAttachXmasLights { get; set; }
        public bool BaseInfiniteLifetime { get; set; }
        public bool BaseRaidOnlyDoors { get; set; }
        public bool HideKitWhilePlacing { get; set; }
        public bool BaseBuildAnywhere { get; set; }
        public bool FlagBuildAnywhere { get; set; }
        public bool TentBuildAnywhere { get; set; }
        public bool VanillaBuildAnywhere { get; set; }
        public bool DisableCraftVanillaFence { get; set; }
        public bool DisableCraftVanillaWatchtower { get; set; }
        public bool DisableDestroy { get; set; }
        public bool DisableDismantle { get; set; }
        public bool EnableHideShowInventory { get; set; }
    }

}