using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace DofusThemeUpdater
{
    public partial class Function
    {
        private static Form form;
        private static BackgroundWorker worker = new BackgroundWorker();
        public static void UpdateTheme(Form app)
        {
            form = app;
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync();
        }
        private static void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            form.Controls.Remove(UI.panel_progress);
            UI.panel_progress.Dispose();
            UI.progbar.Dispose();
            worker.Dispose();
        }

        private static void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string SourceFolder = Localisation.DofusGameThemePath + @"\content\themes\darkStone";
            string DestFolder = Localisation.DofusThemesPath + @"\" + Localisation.ThemeToUpdate;
            string dt_file = Directory.GetFiles(DestFolder, "*.dt")[0];

            if (GetThemeVersion(dt_file) != GetDofusVersion())
            {
                List<string> GameThemeDirectory = GetDirectories(SourceFolder);
                int max = 0;
                int counter = 0;
                foreach (string dir in GameThemeDirectory)
                {
                    List<string> Files = GetFiles(dir);
                    max += Files.Count;
                }

                foreach (string dir in GameThemeDirectory)
                {
                    List<string> Files = GetFiles(dir);
                    foreach (string file in Files)
                    {
                        string newPath = file.Replace(SourceFolder, "");
                        newPath = DestFolder + newPath;
                        if (!File.Exists(newPath))
                        {
                            int index = newPath.LastIndexOf(@"\") + 1;
                            string directory = newPath.Remove(index, newPath.Length - index);
                            if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }
                            if (!file.Contains(".dt")) { File.Copy(file, newPath, false); }
                        }
                        counter += 1;
                        if (counter * 100 / max > UI.progbar.Value) {
                            BackgroundWorker bgwWorker = sender as BackgroundWorker;
                            bgwWorker.ReportProgress(counter * 100 / max,null);
                        }
                    }
                }
                ChangeVersion(dt_file);
            }
        }
        private static void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UI.progbar.Value = e.ProgressPercentage;
        }
    }
}
