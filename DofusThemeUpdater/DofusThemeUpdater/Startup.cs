using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
namespace DofusThemeUpdater
{
    class Startup
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new App());
        }
    }
    public class App : Form
    {
        public App()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Icon = Properties.Resources.Inconnu1;
            Localisation.SettingsPath = Localisation.AppData + @"\Roaming\DofusThemeUpdater";
            Localisation.SettingsFilePath = Localisation.SettingsPath + @"\Settings.File";
            Localisation.DofusThemesPath = Localisation.AppData + @"\Roaming\Dofus\ui\themes";
            Localisation.DofusVersionFilePath = Localisation.AppData + @"\Roaming\zaap\repositories\production\dofus\main\release.json";

            if (Directory.Exists(Localisation.SettingsPath) && File.Exists(Localisation.SettingsFilePath))
            {
                Localisation.DofusGameThemePath = File.ReadAllText(Localisation.SettingsFilePath).Replace("DofusPath:", "");
                UI.Base(this);
            }
            else { UI.Settings(this); }

            Function.Handle = this.Handle;
        }  
    }
}
