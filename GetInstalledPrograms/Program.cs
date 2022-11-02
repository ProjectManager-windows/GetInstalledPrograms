using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using Microsoft.Win32;
using System.Text.Json;
using System.Text.Unicode;

namespace GetInstalledPrograms
{
    class Item
    {
        public string DisplayName { get; }
        public string Publisher { get; }
        public string InstallLocation { get; }

        public Item(string displayName, string publisher, string installLocation)
        {
            DisplayName = displayName;
            Publisher = publisher;
            InstallLocation = installLocation;
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            var registryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKey))
            {
                if (key == null) return;
                var items = new List<Item>();
                foreach (var subKeyName in key.GetSubKeyNames())
                {
                    using (RegistryKey subKey = key.OpenSubKey(subKeyName))
                    {
                        if (subKey == null) continue;
                        if (subKey.GetValueNames().Length < 3) continue;
                        if (subKey.GetValue("SystemComponent") != null && subKey.GetValue("SystemComponent").ToString() == "1") continue;
                        if (subKey.GetValue("InstallLocation") == null || subKey.GetValue("InstallLocation").ToString() == "") continue;
                        items.Add(new Item(
                            subKey.GetValue("DisplayName")?.ToString(),
                            subKey.GetValue("Publisher")?.ToString(),
                            subKey.GetValue("InstallLocation")?.ToString()));
                    }
                }

                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                    WriteIndented = true
                };
                Console.WriteLine(JsonSerializer.Serialize(items, options));
            }
        }
    }
}