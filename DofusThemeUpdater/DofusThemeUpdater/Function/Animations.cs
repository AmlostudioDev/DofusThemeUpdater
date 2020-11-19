using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DofusThemeUpdater
{
    public partial class Function
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int WM_MouseEnter = 0x02A2;
        private const int WM_MouseLeave = 0x02A3;

        private const int HT_CAPTION = 0x2;
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        public static IntPtr Handle;
        private static void MoveForm(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        public static void InitMovingForm(Control control)
        {
            control.MouseDown += MoveForm;
            foreach (Control c in control.Controls)
            {
                if (c.GetType() != typeof(Button) && c.Text != "X") { c.MouseDown += MoveForm; }
            }
        }
        public static void InitMouseAnimation(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c.GetType() == typeof(Button) || c.Text == "X")
                {
                    c.MouseEnter += (s, e) => { control.Cursor = Cursors.Hand; };
                    c.MouseLeave += (s, e) => { control.Cursor = Cursors.Arrow; };
                }
            }
        }
    }
}
