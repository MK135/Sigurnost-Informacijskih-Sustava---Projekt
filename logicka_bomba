using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SIS_prakticni_primjer2
{
    public partial class Form1 : Form
    {
        int brojac = 0;

        PomocnaKlasa PK = new PomocnaKlasa();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.timer.Start();
            this.timer.Tick += new EventHandler(timer_Tick);
            vrijemeLbl.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            vrijemeLbl.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void CloseBttn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void vrijemeLbl_MouseHover(object sender, EventArgs e)
        {
            vrijemeLbl.Font = new Font("Arial", 24);
            vrijemeLbl.BackColor = Color.Coral;
            brojac++;

            if (brojac == (83 % 39))
            {
                this.timer1.Start();
                this.timer1.Tick += new EventHandler(timer1_Tick); 
            }
        }

        private void vrijemeLbl_MouseLeave(object sender, EventArgs e)
        {
            vrijemeLbl.Font = new Font("Microsoft Sans Serif", 18);
            vrijemeLbl.BackColor = Color.PeachPuff;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PK.pokreniTimerOtkucaj();
            if (PK.pokreniTimerOtkucaj() == true)
                this.timer1.Stop();
        }
    }
}
