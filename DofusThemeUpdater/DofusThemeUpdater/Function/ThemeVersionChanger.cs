using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace DofusThemeUpdater
{
    public partial class Function
    {
        private static bool ChangeVersion(string dt_file)
        {
            try
            {
                RichTextBox richTextBox = new RichTextBox();
                richTextBox.Multiline = true;
                richTextBox.WordWrap = false;
                using (StreamReader reader = new StreamReader(new FileStream(dt_file, FileMode.Open)))
                {
                    richTextBox.Text = reader.ReadToEnd();
                    int wordIndex = richTextBox.Find("<dofusVersion>");
                    if (wordIndex != -1)
                    {
                        int index1 = richTextBox.Text.IndexOf(">", wordIndex) + 1;
                        int index2 = richTextBox.Text.IndexOf("<", index1);
                        richTextBox.Text = richTextBox.Text.Remove(index1, index2 - index1).Insert(index1, GetDofusVersion());
                        //<dofusVersion>2.57.9.12</dofusVersion>
                        
                    }
                    wordIndex = richTextBox.Find("<modificationDate>");
                    if (wordIndex != -1)
                    {
                        int index1 = richTextBox.Text.IndexOf(">", wordIndex) + 1;
                        int index2 = richTextBox.Text.IndexOf("<", index1);
                        richTextBox.Text = richTextBox.Text.Remove(index1, index2 - index1).Insert(index1, DateTime.UtcNow.ToString("dd/MM/yyyy"));
                        //<modificationDate>15/11/2020</modificationDate>
                    }

                    reader.Close();
                    reader.Dispose();
                }
                File.Delete(dt_file);
                System.Threading.Thread.Sleep(1000);
                if (!File.Exists(dt_file))
                {
                    using (StreamWriter writer = new StreamWriter(new FileStream(dt_file, FileMode.OpenOrCreate)))
                    {
                        writer.Write("");
                        writer.Write(richTextBox.Text);
                    }
                }
                return true;
            }
            catch (Exception error) { return false; }
        }
    }
}
