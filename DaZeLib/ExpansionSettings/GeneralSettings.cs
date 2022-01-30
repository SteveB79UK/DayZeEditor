﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DayZeLib
{
    public enum Lamps
    {
        [Description("The streets lights are off")]
        Disabled = 0,
        [Description("Currently unused. Would require you to fix a generator to make street lights work. - CURRENTLY DOESNT WORK")]
        Generators = 1,
        [Description("Street lights are emitting lights but some of them will stay off intentionnaly")]
        AlwaysOn = 2,
        [Description("Force every lights to be turned on")]
        AlwaysOnEverywhere = 3
    };
    public class GeneralSettings
    {
        public int m_Version { get; set; } //currentversion 4
        public int PlayerLocationNotifier { get; set; }
        public int EnableGlobalChat { get; set; }
        public int EnablePartyChat { get; set; }
        public int EnableTransportChat { get; set; }
        public int DisableShootToUnlock { get; set; }
        public int EnableGravecross { get; set; }
        public int GravecrossDeleteBody { get; set; }
        public int GravecrossTimeThreshold { get; set; }
        public CustomMapping Mapping { get; set; }
        public int EnableLamps { get; set; }
        public int EnableGenerators { get; set; }
        public int EnableLighthouses { get; set; }
        public int EnableHUDNightvisionOverlay { get; set; }
        public int DisableMagicCrosshair { get; set; }
        public int EnableAutoRun { get; set; }
        public int UnlimitedStamina { get; set; }
        public int UseDeathScreen { get; set; }
        public int UseDeathScreenStatistics { get; set; }
        public int UseNewsFeedInGameMenu { get; set; }
        public ChatColors ChatColors { get; set; }

        [JsonIgnore]
        public string Filename { get; set; }
        [JsonIgnore]
        public bool isDirty { get; set; }

        public GeneralSettings()
        {
            m_Version = 4;
            Mapping = new CustomMapping();
            isDirty = true;
        }
        public int getIntValue(string mytype)
        {
            return (int)GetType().GetProperty(mytype).GetValue(this);
        }
        public void SetIntValue(string mytype, int myvalue)
        {
            GetType().GetProperty(mytype).SetValue(this, myvalue, null);
        }
        public string getcolourfromcontrol(string name)
        {
            switch (name)
            {
                case "SystemChatColorPB":
                    return ChatColors.SystemChatColor;
                case "AdminChatColorPB":
                    return ChatColors.AdminChatColor;
                case "GlobalChatColorPB":
                    return ChatColors.GlobalChatColor;
                case "DirectChatColorPB":
                    return ChatColors.DirectChatColor;
                case "TransportChatColorPB":
                    return ChatColors.TransportChatColor;
                case "PartyChatColorPB":
                    return ChatColors.PartyChatColor;
                case "TransmitterChatColorPB":
                    return ChatColors.TransmitterChatColor;
            }
            return "";
        }
        public void setcolour(string name, string Colour)
        {
            Colour = Colour.Substring(2) + Colour.Substring(0, 2);
            switch (name)
            {
                case "SystemChatColorPB":
                    ChatColors.SystemChatColor = Colour;
                    break;
                case "AdminChatColorPB":
                    ChatColors.AdminChatColor = Colour;
                    break;
                case "GlobalChatColorPB":
                    ChatColors.GlobalChatColor = Colour;
                    break;
                case "DirectChatColorPB":
                    ChatColors.DirectChatColor = Colour;
                    break;
                case "TransportChatColorPB":
                    ChatColors.TransportChatColor = Colour;
                    break;
                case "PartyChatColorPB":
                    ChatColors.PartyChatColor = Colour;
                    break;
                case "TransmitterChatColorPB":
                    ChatColors.TransmitterChatColor = Colour;
                    break;
            }
        }

    }
    public class CustomMapping
    {
        public int UseCustomMappingModule { get; set; }
        public BindingList<string> Mapping { get; set; }
        public int BuildingInteriors { get; set; }
        public BindingList<string> Interiors { get; set; }
        public int BuildingIvys { get; set; }

        public CustomMapping()
        {
            Mapping = new BindingList<string>();
            Interiors = new BindingList<string>();
        }
        public void SetIntValue(string mytype, int myvalue)
        {
            GetType().GetProperty(mytype).SetValue(this, myvalue, null);
        }
    }
    public class ChatColors
    {
        public string SystemChatColor { get; set; }
        public string AdminChatColor { get; set; }
        public string GlobalChatColor { get; set; }
        public string DirectChatColor { get; set; }
        public string TransportChatColor { get; set; }
        public string PartyChatColor { get; set; }
        public string TransmitterChatColor { get; set; }

    }
}
