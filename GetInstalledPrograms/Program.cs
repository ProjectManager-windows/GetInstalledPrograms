using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace GetInstalledPrograms
{
    public class Program
    {
        public class Item
        {
            [XmlAttribute]
            public string DisplayName { get; set; }
            [XmlAttribute]
            public string Publisher { get; set; }
            [XmlAttribute]
            public string InstallLocation { get; set; }

            public Item()
            {
            }
        }
        public static void Main(string[] args)
        {
            using (var stringwriter = new System.IO.StringWriter())
            {
                
                var items = GetInstalledSoftwareList(); 
                XmlSerializer xmlSerializer = new XmlSerializer(items.GetType());
                using (var stream = new StringWriter())
                {
                    xmlSerializer.Serialize(stream, items);
                    Console.WriteLine(stream);
                }
               
                Environment.Exit(0);
            }

            Environment.Exit(1);
        }

        public static string ConvertObjectToXmlString(object classObject)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(classObject.GetType());
            using (var stream = new StringWriter())
            {
                xmlSerializer.Serialize(stream, classObject);
                return stream.ToString();
            }
        }

        public static List<Item> GetInstalledSoftwareList()
        {
            var items = new List<Item>();

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false))
            {
                foreach (String keyName in key.GetSubKeyNames())
                {
                    using (RegistryKey subKey = key.OpenSubKey(keyName))
                    {
                        if (subKey == null) continue;
                        if (subKey.GetValueNames().Length == 0) continue;
                        if (subKey.GetValue("SystemComponent") != null && subKey.GetValue("SystemComponent").ToString() == "1") continue;
                        if (subKey.GetValue("InstallLocation") == null || subKey.GetValue("InstallLocation").ToString() == "") continue;
                        var i = new Item
                        {
                            DisplayName = subKey.GetValue("DisplayName")?.ToString(),
                            Publisher = subKey.GetValue("Publisher")?.ToString(),
                            InstallLocation = subKey.GetValue("InstallLocation")?.ToString()
                        };
                        items.Add(i);
                    }
                }
            }

            //using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false))
            using (var localMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                var key = localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false);
                foreach (String keyName in key.GetSubKeyNames())
                {
                    using (RegistryKey subKey = key.OpenSubKey(keyName))
                    {
                        if (subKey == null) continue;
                        if (subKey.GetValueNames().Length == 0) continue;
                        if (subKey.GetValue("SystemComponent") != null && subKey.GetValue("SystemComponent").ToString() == "1") continue;
                        if (subKey.GetValue("InstallLocation") == null || subKey.GetValue("InstallLocation").ToString() == "") continue;
                        var i = new Item
                        {
                            DisplayName = subKey.GetValue("DisplayName")?.ToString(),
                            Publisher = subKey.GetValue("Publisher")?.ToString(),
                            InstallLocation = subKey.GetValue("InstallLocation")?.ToString()
                        };
                        items.Add(i);
                    }
                }
            }

            using (var localMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
            {
                var key = localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false);
                foreach (String keyName in key.GetSubKeyNames())
                {
                    using (RegistryKey subKey = key.OpenSubKey(keyName))
                    {
                        if (subKey == null) continue;
                        if (subKey.GetValueNames().Length == 0) continue;
                        if (subKey.GetValue("SystemComponent") != null && subKey.GetValue("SystemComponent").ToString() == "1") continue;
                        if (subKey.GetValue("InstallLocation") == null || subKey.GetValue("InstallLocation").ToString() == "") continue;
                        var i = new Item
                        {
                            DisplayName = subKey.GetValue("DisplayName")?.ToString(),
                            Publisher = subKey.GetValue("Publisher")?.ToString(),
                            InstallLocation = subKey.GetValue("InstallLocation")?.ToString()
                        };
                        items.Add(i);
                    }
                }
            }

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall", false))
            {
                foreach (String keyName in key.GetSubKeyNames())
                {
                    using (RegistryKey subKey = key.OpenSubKey(keyName))
                    {
                        if (subKey == null) continue;
                        if (subKey.GetValueNames().Length == 0) continue;
                        if (subKey.GetValue("SystemComponent") != null && subKey.GetValue("SystemComponent").ToString() == "1") continue;
                        if (subKey.GetValue("InstallLocation") == null || subKey.GetValue("InstallLocation").ToString() == "") continue;
                        var i = new Item
                        {
                            DisplayName = subKey.GetValue("DisplayName")?.ToString(),
                            Publisher = subKey.GetValue("Publisher")?.ToString(),
                            InstallLocation = subKey.GetValue("InstallLocation")?.ToString()
                        };
                        items.Add(i);
                    }
                }
            }

            return items;
        }
    }
}