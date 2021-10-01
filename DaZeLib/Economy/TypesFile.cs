﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace DayZeLib
{
    public class LootPart
    {
        public string name, tag;
        public int Index, nominal, lifetime, restock, min, quantmin, quantmax, cost;
        public bool count_in_cargo, count_in_hoarder, count_in_map, count_in_player, crafted, deloot, Disabled;
        public List<string> usage;
        public List<string> TierValue;
        public string category;

        public LootPart()
        {
            Index = -12345;
            nominal = -12345;
            lifetime = -12345;
            restock = -12345;
            min = -12345;
            quantmin = -12345;
            quantmax = -12345;
            cost = -12345;

        }
        public override string ToString()
        {
            return name.ToString();
        }
    }
    public class TypesFile
    {
        public string Filename { get; set; }
        public types types { get; set; }
        public bool isDirty { get; set; }
        public string modname { get; set; }

        public TypesFile (string filename)
        {
            Filename = filename;
            var mySerializer = new XmlSerializer(typeof(types));
            // To read the file, create a FileStream.
            using (var myFileStream = new FileStream(filename, FileMode.Open))
            {
                // Call the Deserialize method and cast to the object type.
                try
                {
                    types = (types)mySerializer.Deserialize(myFileStream);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + filename + "\n" + ex.InnerException.Message);
                }
            }
            foreach(type type in types.type)
            {

                if(type.usage == null || type.usage.Count == 0 || type.usage[0].name == null)
                    type.usage = null;
                
                if (type.value == null || type.value.Count == 0 || (type.value[0].name == null && type.value[0].user == null))
                    type.value = null;

                else if (type.value[0].name == null && type.value[0].user != null)
                    type.Usinguserdifinitions = true;
                else
                    type.Usinguserdifinitions = false;
            }
            types.type = new BindingList<type>(types.type.OrderBy(x => x.name).ToList());
        }
        public bool SaveTyes(string saveTime)
        {
            foreach(type t in types.type)
            {
                if(t.value != null)
                    t.value = new BindingList<value>(new BindingList<value>(t.value.OrderBy(x => x.name).ToList()));
                if (t.usage != null)
                    t.usage = new BindingList<usage>(new BindingList<usage>(t.usage.OrderBy(x => x.name).ToList()));
                if (t.tag != null)
                    t.tag = new BindingList<tag>(new BindingList<tag>(t.tag.OrderBy(x => x.name).ToList()));
            }

            Directory.CreateDirectory(Path.GetDirectoryName(Filename) + "\\Backup\\" + saveTime);
            File.Copy(Filename, Path.GetDirectoryName(Filename) + "\\Backup\\" + saveTime + "\\" + Path.GetFileName(Filename), true);
            var serializer = new XmlSerializer(typeof(types));
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var sw = new StringWriter();
            sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
            var xmlWriter = XmlWriter.Create(sw, new XmlWriterSettings() { OmitXmlDeclaration = true, Indent = true ,});
            serializer.Serialize(xmlWriter, types, ns);
            Console.WriteLine(sw.ToString());
            File.WriteAllText(Filename, sw.ToString());
            return true;
        }
        public bool SaveTyes()
        {
            foreach (type t in types.type)
            {
                if (t.value != null)
                    t.value = new BindingList<value>(new BindingList<value>(t.value.OrderBy(x => x.name).ToList()));
                if (t.usage != null)
                    t.usage = new BindingList<usage>(new BindingList<usage>(t.usage.OrderBy(x => x.name).ToList()));
                if (t.tag != null)
                    t.tag = new BindingList<tag>(new BindingList<tag>(t.tag.OrderBy(x => x.name).ToList()));
            }

            var serializer = new XmlSerializer(typeof(types));
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var sw = new StringWriter();
            sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
            var xmlWriter = XmlWriter.Create(sw, new XmlWriterSettings() { OmitXmlDeclaration = true, Indent = true });
            serializer.Serialize(xmlWriter, types, ns);
            Console.WriteLine(sw.ToString());
            File.WriteAllText(Filename, sw.ToString());
            return true;
        }
        public List<type> SerachTypes(string Searchterm, bool exact = false)
        {
            if (exact)
                return types.type.Where(x => x.name == Searchterm).ToList();
            else
                return types.type.Where(x => x.name.ToLower().Contains(Searchterm)).ToList();
            
        }
        public List<type> SerachTypes(string[] Searchterm, bool exact = false)
        {
            List<type> list = new List<type>();
            foreach(string s in Searchterm)
            {
                list.AddRange(types.type.Where(x => x.name.ToLower().Contains(s)).ToList());
            }
            return list;

        }
    }
}
