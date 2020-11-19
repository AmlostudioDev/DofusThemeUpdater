using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DofusThemeUpdater
{
    public partial class Function
    {
        public static string GetThemeVersion(string dt_file)
        {
            string version = "";
            RichTextBox richTextBox = new RichTextBox();
            richTextBox.Multiline = true;
            richTextBox.WordWrap = false;
            using (StreamReader reader = new StreamReader(new FileStream(dt_file, FileMode.Open)))
            {
                richTextBox.Text = reader.ReadToEnd();
                int wordIndex = richTextBox.Find("<dofusVersion>"); // "versions":"
                if (wordIndex != -1)
                {
                    int index1 = richTextBox.Text.IndexOf(">", wordIndex) + 1;
                    int index2 = richTextBox.Text.IndexOf("<", index1);
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
