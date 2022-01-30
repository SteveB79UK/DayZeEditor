﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DayZeLib
{
    #region Events
    public enum position
    {
        [Description("Fixed")]
        @fixed,
        [Description("Player")]
        player,
        [Description("Uniform")]
        uniform
    };
    public enum limit
    {
        [Description("Mixed")]
        mixed,
        [Description("Custom")]
        custom,
        [Description("Child")]
        child,
        [Description("Parent")]
        parent
    };

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class events
    {
        private BindingList<eventsEvent> eventField;
        [System.Xml.Serialization.XmlElementAttribute("event")]
        public BindingList<eventsEvent> @event
        {
            get
            {
                return this.eventField;
            }
            set
            {
                this.eventField = value;
            }
        }

        public void AddnewEvent(eventsEvent neweventEvent)
        {
            @event.Add(neweventEvent);
        }

        public void RemoveEvent(eventsEvent currentEvent)
        {
            @event.Remove(currentEvent);
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class eventsEvent
    {
        private int nominalField;
        private int minField;
        private int maxField;
        private int lifetimeField;
        private int restockField;
        private int saferadiusField;
        private int distanceradiusField;
        private int cleanupradiusField;
        private eventsEventFlags flagsField;
        private position positionField;
        private limit limitField;
        private int activeField;
        private BindingList<eventsEventChild> childrenField;
        private string nameField;


        public int nominal
        {
            get
            {
                return this.nominalField;
            }
            set
            {
                this.nominalField = value;
            }
        }
        public int min
        {
            get
            {
                return this.minField;
            }
            set
            {
                this.minField = value;
            }
        }
        public int max
        {
            get
            {
                return this.maxField;
            }
            set
            {
                this.maxField = value;
            }
        }
        public int lifetime
        {
            get
            {
                return this.lifetimeField;
            }
            set
            {
                this.lifetimeField = value;
            }
        }
        public int restock
        {
            get
            {
                return this.restockField;
            }
            set
            {
                this.restockField = value;
            }
        }
        public int saferadius
        {
            get
            {
                return this.saferadiusField;
            }
            set
            {
                this.saferadiusField = value;
            }
        }
        public int distanceradius
        {
            get
            {
                return this.distanceradiusField;
            }
            set
            {
                this.distanceradiusField = value;
            }
        }
        public int cleanupradius
        {
            get
            {
                return this.cleanupradiusField;
            }
            set
            {
                this.cleanupradiusField = value;
            }
        }
        public eventsEventFlags flags
        {
            get
            {
                return this.flagsField;
            }
            set
            {
                this.flagsField = value;
            }
        }
        public position position
        {
            get
            {
                return this.positionField;
            }
            set
            {
                this.positionField = value;
            }
        }
        public limit limit
        {
            get
            {
                return this.limitField;
            }
            set
            {
                this.limitField = value;
            }
        }
        public int active
        {
            get
            {
                return this.activeField;
            }
            set
            {
                this.activeField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute("child", IsNullable = false)]
        public BindingList<eventsEventChild> children
        {
            get
            {
                return this.childrenField;
            }
            set
            {
                this.childrenField = value;
            }
        }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        public void SetIntValue(string mytype, int myvalue)
        {
            GetType().GetProperty(mytype).SetValue(this, myvalue, null);
        }

        public override string ToString()
        {
            return name;
        }

        public void Addnechild(eventsEventChild neweventeventschild)
        {
            children.Add(neweventeventschild);
        }

        public void Removechild(eventsEventChild currentChild)
        {
            children.Remove(currentChild);
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class eventsEventFlags
    {
        private int deletableField;
        private int init_randomField;
        private int remove_damagedField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int deletable
        {
            get
            {
                return this.deletableField;
            }
            set
            {
                this.deletableField = value;
            }
        }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int init_random
        {
            get
            {
                return this.init_randomField;
            }
            set
            {
                this.init_randomField = value;
            }
        }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int remove_damaged
        {
            get
            {
                return this.remove_damagedField;
            }
            set
            {
                this.remove_damagedField = value;
            }
        }

    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class eventsEventChild
    {
        private int lootmaxField;
        private int lootminField;
        private int maxField;
        private int minField;
        private string typeField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int lootmax
        {
            get
            {
                return this.lootmaxField;
            }
            set
            {
                this.lootmaxField = value;
            }
        }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int lootmin
        {
            get
            {
                return this.lootminField;
            }
            set
            {
                this.lootminField = value;
            }
        }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int max
        {
            get
            {
                return this.maxField;
            }
            set
            {
                this.maxField = value;
            }
        }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int min
        {
            get
            {
                return this.minField;
            }
            set
            {
                this.minField = value;
            }
        }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        public void SetIntValue(string mytype, int myvalue)
        {
            GetType().GetProperty(mytype).SetValue(this, myvalue, null);
        }
        public override string ToString()
        {
            return type;
        }
    }
    #endregion events
}
