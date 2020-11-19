using System;
using System.IO;
using System.Windows.Forms;

namespace DofusThemeUpdater
{
    public partial class Function
    {
        public static void FolderSelecter(Form app)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.RootFolder = Environment.SpecialFolder.MyComputer;
            FBD.Description = "Veillez choisir le dossier de votre jeu \" Dofus \"";
            DialogResult result = FBD.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (FBD.SelectedPath.Contains("Dofus") && File.Exists(FBD.SelectedPath + @"\Dofus.exe"))
                {
                    Localisation.DofusGameThemePath = FBD.SelectedPath + @"\content\themes";
                    Console.WriteLine(Localisation.DofusGameThemePath);
                    CreateSettingFile(FBD.SelectedPath);
                    foreach (Control c in app.Controls) { c.Dispose(); }
                    app.Controls.Clear();
                    UI.Base(app);
                }
                else { MessageBox.Show("Erreur, ce n'est pas le dossier du jeu \" Dofus \""); }
            }
        }
    }
}
