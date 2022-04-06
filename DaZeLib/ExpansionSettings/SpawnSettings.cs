﻿using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace DayZeLib
{
    public class SpawnSettings
    {
        public int m_Version { get; set; } //current version 3
        public StartingClothing StartingClothing { get; set; }
        public int EnableSpawnSelection { get; set; }
        //public int SpawnSelectionScreenMenuID { get; set; } //removed in version 3
        public int SpawnOnTerritory { get; set; }
        public BindingList<SpawnLocations> SpawnLocations { get; set; }
        public StartingGear StartingGear { get; set; }
        public float SpawnHealthValue { get; set; }
        public float SpawnEnergyValue { get; set; }
        public float SpawnWaterValue { get; set; }


        [JsonIgnore]
        public string Filename { get; set; }
        [JsonIgnore]
        public bool isDirty
        {
            get { return _isdirty; }
            set
            {
                _isdirty = value;
            }
        }
        [JsonIgnore]
        private bool _isdirty = false;

        public SpawnSettings()
        {
            m_Version = 3;
            StartingClothing = new StartingClothing();
            SpawnLocations = new BindingList<SpawnLocations>();
            StartingGear = new StartingGear();
            isDirty = true;
        }

        public void SetStartingWeapons()
        {

            if (StartingGear.PrimaryWeapon != null && StartingGear.PrimaryWeapon.ClassName == null)
                StartingGear.PrimaryWeapon = null;
            if (StartingGear.SecondaryWeapon != null && StartingGear.SecondaryWeapon.ClassName == null)
                StartingGear.SecondaryWeapon = null;
        }
    }
    public class StartingClothing
    {
        public int EnableCustomClothing { get; set; }
        public int SetRandomHealth { get; set; }
        public BindingList<string> Headgear { get; set; }
        public BindingList<string> Glasses { get; set; }
        public BindingList<string> Masks { get; set; }
        public BindingList<string> Tops { get; set; }
        public BindingList<string> Vests { get; set; }
        public BindingList<string> Gloves { get; set; }
        public BindingList<string> Pants { get; set; }
        public BindingList<string> Belts { get; set; }
        public BindingList<string> Shoes { get; set; }
        public BindingList<string> Armbands { get; set; }
        public BindingList<string> Backpacks { get; set; }

        public StartingClothing()
        {
            EnableCustomClothing = 0;
            SetRandomHealth = 0;
            Headgear = new BindingList<string>();
            Glasses = new BindingList<string>();
            Masks = new BindingList<string>();
            Tops = new BindingList<string>();
            Vests = new BindingList<string>();
            Gloves = new BindingList<string>();
            Pants = new BindingList<string>();
            Belts = new BindingList<string>();
            Shoes = new BindingList<string>();
            Armbands = new BindingList<string>();
            Backpacks = new BindingList<string>();
        }
    }
    public class SpawnLocations
    {
        public string Name { get; set; }
        public BindingList<float[]> Positions { get; set; }

        public SpawnLocations()
        {
            Name = "New SpawnLocation";
            Positions = new BindingList<float[]>();

        }
        public override string ToString()
        {
            return Name;
        }
    }
    public class StartingGear
    {
        public int EnableStartingGear { get; set; }
        public int UseUpperGear { get; set; }
        public int UsePantsGear { get; set; }
        public int UseBackpackGear { get; set; }
        public int UseVestGear { get; set; }
        public int UsePrimaryWeapon { get; set; }
        public int UseSecondaryWeapon { get; set; }
        public int ApplyEnergySources { get; set; }
        public int SetRandomHealth { get; set; }
        public BindingList<Gear> UpperGear { get; set; }
        public BindingList<Gear> PantsGear { get; set; }
        public BindingList<Gear> BackpackGear { get; set; }
        public BindingList<Gear> VestGear { get; set; }
        public Gear PrimaryWeapon { get; set; }
        public Gear SecondaryWeapon { get; set; }

        public StartingGear()
        {
            UpperGear = new BindingList<Gear>();
            PantsGear = new BindingList<Gear>();
            BackpackGear = new BindingList<Gear>();
            VestGear = new BindingList<Gear>();
            PrimaryWeapon = null;
            SecondaryWeapon = null;
        }
    }
    public class Gear : EmptyGear
    {
        public string ClassName { get; set; }
        public int Quantity { get; set; }
        public BindingList<string> Attachments { get; set; }

        public override string ToString()
        {
            return ClassName;
        }
    }
    public class EmptyGear
    {

    }
}
