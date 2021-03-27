using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPlayLecture
{
    public partial class Form3 : Form
    {

        public Thread thread;

        public Form3()
        {
            InitializeComponent();
        }

        private long number = 0;

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Equals("") || textBox2.Text.Equals(""))
            {
                label6.Text = "아이디혹은 비밀번호를 입력해주세요";
                return;
            }
            bool idChecker = Regex.IsMatch(textBox1.Text, @"[0-9]");
            if (!idChecker)
            {
                label6.Text = "아이디는 숫자만 가능합니다.";
                return;
            }

            Program.id = textBox1.Text;
            Program.pass = textBox2.Text;

            String path = Application.StartupPath + @"\lecture\login.txt";
            FileStream fs = File.Create(path);
            fs.Close();

            using (StreamWriter outputFile = new StreamWriter(path))
            {
                outputFile.WriteLine(textBox1.Text);
                outputFile.WriteLine(textBox2.Text);
            }


            thread = new Thread(openForm);
            thread.Start();
            this.Visible = false;
        }

        private void openForm()
        {
            Program.form2 = new Form2();
            Program.form2.ShowDialog();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Form3_VisibleChanged(object sender, EventArgs e)
        {
            Program.value = 0;
            label6.Text = Program.error;
        }
    }
}
