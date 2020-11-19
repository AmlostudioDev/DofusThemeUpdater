using System.IO;

namespace DofusThemeUpdater
{
    public partial class Function
    {
        private static void CreateSettingFile(string Text)
        {
            if (!Directory.Exists(Localisation.SettingsPath)) { Directory.CreateDirectory(Localisation.SettingsPath); }

            using (StreamWriter writer = new StreamWriter(new FileStream(Localisation.SettingsFilePath, FileMode.OpenOrCreate)))
            {
                writer.Write("DofusPath:" + Text);
                writer.Close();
                writer.Dispose();
            }
        }
    }
}
