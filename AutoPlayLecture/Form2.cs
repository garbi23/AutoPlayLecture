using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPlayLecture
{
    public partial class Form2 : Form
    {
        public Thread thread;
        public Form1 form1;

        public Form2()
        {
            InitializeComponent();
            timer1.Interval = 100;
            timer1.Tick += new EventHandler(setProgress);
            timer1.Enabled = true;
            timer1.Start();
            progressBar1.Maximum = 100;
            progressBar1.Minimum = 0;
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            thread = new Thread(run);
            thread.Start();
        }
        private void run()
        {
            form1 = new Form1();
            try { form1.Show(); } catch (Exception) { return; }
        }

        private void setProgress(object sender, EventArgs e)
        {
            Console.WriteLine(Program.value);
            if (Program.value > 100) { return; }
            progressBar1.Value = Program.value;
            label3.Text = (Double)progressBar1.Value + "%";
            if (Program.value == 100)
            {
                timer1.Stop();
                this.Opacity = 0;
                this.ShowInTaskbar = false;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
