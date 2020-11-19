using System.IO;
using System.Windows.Forms;

namespace DofusThemeUpdater
{
    public partial class Function
    {
        public static string GetDofusVersion()
        {
            string version = "";
            RichTextBox richTextBox = new RichTextBox();
            richTextBox.Multiline = true;
            richTextBox.WordWrap = false;
            using (StreamReader reader = new StreamReader(new FileStream(Localisation.DofusVersionFilePath, FileMode.Open)))
            {
                richTextBox.Text = reader.ReadToEnd();
                int wordIndex = richTextBox.Find("\"version\":\""); // "versions":"
                if (wordIndex != -1)
                {
                    int index1 = richTextBox.Text.IndexOf("_", wordIndex) + 1;
                    int index2 = richTextBox.Text.IndexOf("\"", index1);
                    richTextBox.Text = richTextBox.Text.Substring(index1, index2 - index1);
                    version = richTextBox.Text;
                }
                reader.Close();
                reader.Dispose();
            }
            richTextBox.Dispose();
            return version;
        }
    }
}
