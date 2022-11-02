﻿using System;
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
            var items = GetInstalledSoftwareList();
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            Console.WriteLine(JsonSerializer.Serialize(items, options));
        }

        public static List<Item> GetInstalledSoftwareList()
        {
            string displayName;
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
                        items.Add(new Item(
                            subKey.GetValue("DisplayName")?.ToString(),
                            subKey.GetValue("Publisher")?.ToString(),
                            subKey.GetValue("InstallLocation")?.ToString()));
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
                        items.Add(new Item(
                            subKey.GetValue("DisplayName")?.ToString(),
                            subKey.GetValue("Publisher")?.ToString(),
                            subKey.GetValue("InstallLocation")?.ToString()));
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
                        items.Add(new Item(
                            subKey.GetValue("DisplayName")?.ToString(),
                            subKey.GetValue("Publisher")?.ToString(),
                            subKey.GetValue("InstallLocation")?.ToString()));
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
                        items.Add(new Item(
                            subKey.GetValue("DisplayName")?.ToString(),
                            subKey.GetValue("Publisher")?.ToString(),
                            subKey.GetValue("InstallLocation")?.ToString()));
                    }
                }
            }

            return items;
        }
    }
}