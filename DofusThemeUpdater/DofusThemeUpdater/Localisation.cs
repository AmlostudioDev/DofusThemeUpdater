using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusThemeUpdater
{
    public class Localisation
    {
        public static string DofusThemesPath { get; set; }
        public static string DofusGameThemePath { get; set; }
        public static string SettingsPath { get; set; }
        public static string SettingsFilePath { get; set; }
        public static string ThemeToUpdate { get; set; }

        public static string DofusVersionFilePath { get; set; }
        public static string DofusVersion
        {
            get { return ""; }
        }
        public static string AppData
        {
            get 
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return path.Replace(@"\Roaming", ""); 
            }
        }
    }
}
