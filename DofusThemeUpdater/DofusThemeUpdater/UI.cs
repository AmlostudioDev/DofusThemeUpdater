using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
namespace DofusThemeUpdater
{
    public class UI
    {
        public static void Base(Form app)
        {
            app.Size = new Size(600, 200);
            app.Paint += (s, e) =>
            {
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 3f), 1f, 1f, app.Width-3f, app.Height-3f);
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.DimGray), 3f), 4f, 4f, app.Width-9f, app.Height-9f);
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 3f), 7f, 7f, app.Width-15f, app.Height-15f);
            };
            Label lbl_Title = new Label();
            lbl_Title.AutoSize = true;
            lbl_Title.Font = new Font("Arial", 12f, FontStyle.Bold);
            lbl_Title.Text = "Dofus Theme Updater";
            lbl_Title.ForeColor = Color.Black;
            lbl_Title.Location = new Point((int)(app.Size.Width - app.CreateGraphics().MeasureString(lbl_Title.Text, lbl_Title.Font).Width) / 2, 10);
            app.Controls.Add(lbl_Title);

            Label lbl_Close = new Label();
            lbl_Close.AutoSize = true;
            lbl_Close.Font = new Font("Arial", 12f, FontStyle.Bold);
            lbl_Close.Text = "X";
            lbl_Close.ForeColor = Color.Black;
            lbl_Close.Location = new Point((int)(app.Size.Width - app.CreateGraphics().MeasureString(lbl_Close.Text, lbl_Close.Font).Width) - 15, 10);
            lbl_Close.Click += (s, e) => { Application.Exit(); };
            app.Controls.Add(lbl_Close);

            Panel pnl_Themes = new Panel();
            pnl_Themes.BackColor = app.BackColor;
            pnl_Themes.Location = new Point(0, (int)app.CreateGraphics().MeasureString(lbl_Title.Text, lbl_Title.Font).Height + 10);
            pnl_Themes.Size = new Size(app.Size.Width,app.Size.Height-29);
            pnl_Themes.Paint += (s, e) =>
            {
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 3f), 1f, 1f, pnl_Themes.Size.Width - 3f, pnl_Themes.Size.Height - 3f);
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.DimGray), 3f), 4f, 4f, pnl_Themes.Size.Width - 9f, pnl_Themes.Size.Height - 9f);
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 3f), 7f, 7f, pnl_Themes.Size.Width - 15f, pnl_Themes.Size.Height - 15f);
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.DimGray), 2f), 4f, 1f, 1f, 3f);
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.DimGray), 2f), pnl_Themes.Size.Width - 5f, 1f, 1f, 3f);
            };
            app.Controls.Add(pnl_Themes);

            Panel pnl_Themes2 = new Panel();
            pnl_Themes2.AutoScroll = true;
            pnl_Themes2.BackColor = app.BackColor;
            pnl_Themes2.Location = new Point(10,10);
            pnl_Themes2.Size = new Size(app.Size.Width-19, app.Size.Height - 48);         
            pnl_Themes.Controls.Add(pnl_Themes2);

            List<string> Themes = Directory.GetDirectories(Localisation.DofusThemesPath).ToList();
            int Y = 2;
            string dofusversion = Function.GetDofusVersion();
            foreach (string Theme in Themes)
            {
                string dt_file = Directory.GetFiles(Theme, "*.dt")[0];
                string version = Function.GetThemeVersion(dt_file);

                Label lbl_theme = new Label();
                lbl_theme.AutoSize = true;
                lbl_theme.Font = new Font("Arial", 12f, FontStyle.Bold);
                lbl_theme.Text = Theme.Replace(Localisation.DofusThemesPath + @"\","");
                lbl_theme.ForeColor = Color.Black;
                lbl_theme.Location = new Point(4, Y);
                lbl_theme.Click += (s, e) => { Application.Exit(); };
                pnl_Themes2.Controls.Add(lbl_theme);

                Label lbl_themeVersion = new Label();
                lbl_themeVersion.AutoSize = true;
                lbl_themeVersion.Font = new Font("Arial", 12f, FontStyle.Bold);
                lbl_themeVersion.Text = version;
                if (version != dofusversion)
                {
                    lbl_themeVersion.ForeColor = Color.Red;
                }
                else { lbl_themeVersion.ForeColor = Color.Green; }
                lbl_themeVersion.Location = new Point(lbl_theme.Location.X + (int)pnl_Themes2.CreateGraphics().MeasureString(lbl_theme.Text, lbl_theme.Font).Width + 4, Y);
                lbl_themeVersion.Click += (s, e) => { Application.Exit(); };
                pnl_Themes2.Controls.Add(lbl_themeVersion);

                Button btn_update = new Button();
                btn_update.Size = new Size(100, 22);
                btn_update.TabStop = false;
                btn_update.Font = new Font("Arial", 9f, FontStyle.Bold);
                btn_update.Text = "Mettre à jour";
                btn_update.Location = new Point(lbl_themeVersion.Location.X + (int)pnl_Themes2.CreateGraphics().MeasureString(lbl_themeVersion.Text,lbl_themeVersion.Font).Width + 4,Y-2);
                if (version != dofusversion) { btn_update.Enabled = true; } else { btn_update.Enabled = false; }
                btn_update.Click += (s, e) =>
                {
                    Localisation.ThemeToUpdate = lbl_theme.Text;
                    UI.ProgressBar(app);

                    Function.UpdateTheme(app);
                    //if(Function.GetThemeVersion(dt_file) == dofusversion)
                    //{ 
                        btn_update.Enabled = false;
                        lbl_themeVersion.Text = Function.GetThemeVersion(dt_file);
                        lbl_themeVersion.ForeColor = Color.Green;
                        btn_update.Location = new Point(lbl_themeVersion.Location.X + (int)pnl_Themes2.CreateGraphics().MeasureString(lbl_themeVersion.Text, lbl_themeVersion.Font).Width + 4, btn_update.Location.Y);
                    //};
                    
                };
                pnl_Themes2.Controls.Add(btn_update);

                Y += (int)pnl_Themes2.CreateGraphics().MeasureString(lbl_theme.Text, lbl_theme.Font).Height + 10;
            }

            Function.InitMovingForm(app);
            Function.InitMouseAnimation(app);
            Function.InitMovingForm(pnl_Themes);
            Function.InitMouseAnimation(pnl_Themes);
            Function.InitMovingForm(pnl_Themes2);
            Function.InitMouseAnimation(pnl_Themes2);
        }
        public static void Settings(Form app)
        {
            app.Size = new Size(600, 200);
            app.Paint += (s, e) =>
            {
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 3f), 1f, 1f, app.Size.Width - 3f, app.Size.Height - 3f);
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.DimGray), 3f), 4f, 4f, app.Size.Width - 9f, app.Size.Height - 9f);
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 3f), 7f, 7f, app.Size.Width - 15f, app.Size.Height - 15f);
            };

            Label lbl_Title = new Label();
            lbl_Title.AutoSize = true;
            lbl_Title.Font = new Font("Arial", 12f, FontStyle.Bold);
            lbl_Title.Text = "Dofus Theme Updater";
            lbl_Title.ForeColor = Color.Black;
            lbl_Title.Location = new Point((int)(app.Size.Width - app.CreateGraphics().MeasureString(lbl_Title.Text, lbl_Title.Font).Width) /2,10);
            app.Controls.Add(lbl_Title);

            Label lbl_Close = new Label();
            lbl_Close.AutoSize = true;
            lbl_Close.Font = new Font("Arial", 12f, FontStyle.Bold);
            lbl_Close.Text = "X";
            lbl_Close.ForeColor = Color.Black;
            lbl_Close.Location = new Point((int)(app.Size.Width-app.CreateGraphics().MeasureString(lbl_Close.Text,lbl_Close.Font).Width)-15,10);
            lbl_Close.Click += (s, e) => { Application.Exit(); };
            app.Controls.Add(lbl_Close);

            Panel pnl_Themes = new Panel();
            pnl_Themes.BackColor = app.BackColor;
            pnl_Themes.AutoScroll = true;
            pnl_Themes.Location = new Point(0, (int)app.CreateGraphics().MeasureString(lbl_Title.Text, lbl_Title.Font).Height + 10);
            pnl_Themes.Size = new Size(app.Size.Width, app.Size.Height - 29);
            pnl_Themes.Paint += (s, e) =>
            {
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 3f), 1f, 1f, pnl_Themes.Width - 3f, pnl_Themes.Height - 3f);
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.DimGray), 3f), 4f, 4f, pnl_Themes.Width - 9f, pnl_Themes.Height - 9f);
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 3f), 7f, 7f, pnl_Themes.Width - 15f, pnl_Themes.Height - 15f);
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.DimGray), 2f), 4f, 1f, 1f, 3f);
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.DimGray), 2f), pnl_Themes.Size.Width - 5f, 1f, 1f, 3f);
            };
            app.Controls.Add(pnl_Themes);

            Label lbl_DofusPath = new Label();
            lbl_DofusPath.AutoSize = true;
            lbl_DofusPath.Font = new Font("Arial", 12f, FontStyle.Bold);
            lbl_DofusPath.ForeColor = Color.Black;
            lbl_DofusPath.Text = "Répertoire du jeu :";
            lbl_DofusPath.Location = new Point(10,20);
            pnl_Themes.Controls.Add(lbl_DofusPath);

            Button btn_ChoosePath = new Button();
            btn_ChoosePath.TabStop = false;
            btn_ChoosePath.FlatStyle = FlatStyle.Flat;
            btn_ChoosePath.Font = new Font("Arial", 9f, FontStyle.Bold);
            btn_ChoosePath.Text = "Choisir";
            btn_ChoosePath.Size = new Size(100,22);
            btn_ChoosePath.Location = new Point(
                lbl_DofusPath.Location.X + (int)pnl_Themes.CreateGraphics().MeasureString(lbl_DofusPath.Text, lbl_DofusPath.Font).Width + 4,
                lbl_DofusPath.Location.Y - 2);
            btn_ChoosePath.Click += (s, e) => { Function.FolderSelecter(app); };
            pnl_Themes.Controls.Add(btn_ChoosePath);

            Function.InitMovingForm(app);
            Function.InitMouseAnimation(app);
            Function.InitMovingForm(pnl_Themes);
            Function.InitMouseAnimation(pnl_Themes);
        }

        public static Panel panel_progress = new Panel();
        public static ProgressBar progbar = new ProgressBar();
        public static void ProgressBar(Form app)
        {
            panel_progress = new Panel();
            panel_progress.Size = new Size(200,60);
            panel_progress.Location = new Point((int)(app.Size.Width - panel_progress.Size.Width)/2, (int)(app.Size.Height - panel_progress.Size.Height) / 2);
            panel_progress.Paint += (s, e) =>
            {
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 3f), 1f, 1f, panel_progress.Size.Width - 3f, panel_progress.Size.Height - 3f);
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.DimGray), 3f), 4f, 4f, panel_progress.Size.Width - 9f, panel_progress.Size.Height - 9f);
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 3f), 7f, 7f, panel_progress.Size.Width - 15f, panel_progress.Size.Height - 15f);
            };
            app.Controls.Add(panel_progress);
            panel_progress.BringToFront();

            progbar = new ProgressBar();
            progbar.Maximum = 0; progbar.Maximum = 100;
            progbar.Size = new Size(140,20);
            progbar.Location = new Point((int)(panel_progress.Size.Width - progbar.Size.Width) / 2, (int)(panel_progress.Size.Height - progbar.Size.Height)/2);
            panel_progress.Controls.Add(progbar);
        }
    }
}
