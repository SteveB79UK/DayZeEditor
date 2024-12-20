﻿using System.Diagnostics.Contracts;
using System.Text.Json.Serialization;

namespace DayZeLib
{
    public class ExpansionPartySettings
    {
        const int CurrentVersion = 8;

        public int m_Version { get; set; }
        public int EnableParties { get; set; }
        public int MaxMembersInParty { get; set; }
        public int UseWholeMapForInviteList { get; set; }
        public int ShowPartyMember3DMarkers { get; set; }
        public int ShowDistanceUnderPartyMembersMarkers { get; set; }
        public int ShowNameOnPartyMembersMarkers { get; set; }
        public int EnableQuickMarker { get; set; }
        public int ShowDistanceUnderQuickMarkers { get; set; }
        public int ShowNameOnQuickMarkers { get; set; }
        public int CanCreatePartyMarkers { get; set; }
        //! Added with version 2
        public int ShowPartyMemberHUD { get; set; } //! Show the party hud interface that displays eacht party members name and health
        //! Added with version 3
        public int ShowHUDMemberBlood { get; set; }
        public int ShowHUDMemberStates { get; set; }
        public int ShowHUDMemberStance { get; set; }
        //! Added with version 4
        public int ShowPartyMemberMapMarkers { get; set; }
        //! Added with version 5
        public int ShowHUDMemberDistance { get; set; }
        //! Added with version 6
        public int ForcePartyToHaveTags { get; set; }
        //! Added with version 7
        public int InviteCooldown { get; set; }
        //! Added with Version 8
        public int DisplayPartyTag { get; set; }

        [JsonIgnore]
        public string Filename { get; set; }
        [JsonIgnore]
        public bool isDirty { get; set; }

        public ExpansionPartySettings()
        {
            m_Version = CurrentVersion;
            //! Added with version 1
            EnableParties = 1;
            MaxMembersInParty = 10;
            UseWholeMapForInviteList = 0;

            ShowPartyMember3DMarkers = 1;
            ShowDistanceUnderPartyMembersMarkers = 1;
            ShowNameOnPartyMembersMarkers = 1;
            EnableQuickMarker = 1;
            ShowDistanceUnderQuickMarkers = 1;
            ShowNameOnQuickMarkers = 1;
            CanCreatePartyMarkers = 1;

            //! Added with version 2
            ShowPartyMemberHUD = 1;

            //! Added with version 3
            ShowHUDMemberBlood = 1;
            ShowHUDMemberStates = 1;
            ShowHUDMemberStance = 1;

            //! Added with version 4
            ShowPartyMemberMapMarkers = 1;

            //! Added with version 5
            ShowHUDMemberDistance = 1;

            //! Added with version 6
            ForcePartyToHaveTags = 0;

            //! Added with version 7
            InviteCooldown = 0;

            //! Added with version 8
            DisplayPartyTag = 1;
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

        public void SetIntValue(string mytype, int myvalue)
        {
            GetType().GetProperty(mytype).SetValue(this, myvalue, null);
        }
    }
}
