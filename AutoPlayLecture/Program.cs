using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPlayLecture
{

    static class Program
    {

        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        /// 
        public static int value = 0;
        public static Form2 form2;
        public static Form3 form3;
        public static String error = "";
        public static String id = "";
        public static String pass = "";
        public static IWebDriver driver;
        public static ArrayList driverList = new ArrayList();
        public static ArrayList endLecList = new ArrayList();
        public static String ieilmsurl = "https://ieilms.jbnu.ac.kr/";
        public static String path = Application.StartupPath + @"\lecture\endLec.txt";

        public static void invokeThreadPlay(Form form, Action action)
        {
            if (form.InvokeRequired)
            {
                form.Invoke(new MethodInvoker(delegate ()
                {
                    action();

                }));
            }
            else
            {
                action();
            }
        }




        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!File.Exists(path))
            {
                FileStream fs = File.Create(path);
                fs.Close();
            }
            else
            {
                String[] lines = File.ReadAllLines(Program.path);
                foreach (String str in lines)
                {
                    Program.endLecList.Add(str);
                }
            }


            if (File.Exists(Application.StartupPath + @"\lecture\login.txt"))
            {
                String[] lines = File.ReadAllLines(Application.StartupPath + @"\lecture\login.txt");
                id = lines[0];
                pass = lines[1];
                form2 = new Form2();
                form3 = new Form3();
                Application.Run(form2);
            }
            else
            {
                form3 = new Form3();
                Application.Run(form3);
            }
        }
    }
}
