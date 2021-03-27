using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace AutoPlayLecture
{
    public partial class Form1 : Form
    {
        private string ieilmsurl = "https://ieilms.jbnu.ac.kr/";
        private DateTime startTime;
        private int page = 0;
        public int groupSize = 0;
        private bool idpw = true;
        private Boolean isListening = false;
        private ArrayList groupList = new ArrayList();
        private IWebDriver driver;
        private ArrayList checkList = new ArrayList();
        private Thread playThread;
        private ArrayList threadList = new ArrayList();
        private Dictionary<int, ArrayList> checkDic = new Dictionary<int, ArrayList>();

        public Form1()
        {
            InitializeComponent();
        }

        private void initAll()
        {

            playThread.Abort();
            isListening = false;
            progressBar3.Style = ProgressBarStyle.Continuous;
            progressBar3.Minimum = 0;
            progressBar3.Maximum = 100;
            progressBar3.Value = 0;
            progressBar3.Enabled = true;
            driver.Url = "https://ieilms.jbnu.ac.kr/mypage/group/";
            foreach (ListViewItem item in checkList)
            {
                item.BackColor = Color.White;
            }
            checkList.Clear();
            checkDic.Clear();

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            logAdd("크롬 드라이버 설정...");
            startTime = System.DateTime.Now;
            var options = new ChromeOptions();
            options.AddArguments("headless", "mute-audio");
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            Program.value += 10;
            driver = new ChromeDriver(service, options);
            Program.driverList.Add(driver);
            driver.Url = ieilmsurl;
            Program.driver = driver;
            Program.value += 20;
            logAdd("로그인 시도...");
            Login();
            Program.value += 10;
            Thread.Sleep(2000);
            Program.value += 20;
            try { Thread.Sleep(2000); }
            catch (Exception)
            {
                idpw = false;
                this.Close();
                return;
            }
            Thread thread = new Thread(() => getGroupList());
            threadList.Add(thread);
            thread.IsBackground = true;
            thread.Start();

            listView2.FullRowSelect = true;
            ImageList imageList = new ImageList();
            imageList.ImageSize = new System.Drawing.Size(1, 30);
            listView2.SmallImageList = imageList;
            listView2.HideSelection = false;
        }


/*        class threadPlayLecture
        {
            public threadPlayLecture(Form1 form)
            {
                form.Invoke(new MethodInvoker(delegate ()
                {
                    foreach (ListViewItem item in form.checkList)
                    {
                        int grouid = int.Parse(item.SubItems[0].Text.Split(' ')[0]);
                        int lectureid = int.Parse(item.SubItems[0].Text.Split(' ')[1]);
                        if (!form.checkDic.Keys.Contains(grouid)) { form.checkDic[grouid] = new ArrayList(); }
                        form.checkDic[grouid].Add(lectureid);
                    }

                    foreach (int key in form.checkDic.Keys)
                    {
                        Group group = (Group)form.groupList[key];
                        group.setGroupPageForLecture(form.driver);
                        Thread.Sleep(2000);
                        foreach (int index in form.checkDic[key])
                        {
                            var lec = (Lecture)group.lectureList[index];
                            form.logAdd(lec.name + ": 강의 시작");
                            while (true)
                            {
                                if (lec.playLecture(form.driver)) { break; }
                                form.progressBar3.Maximum = lec.maxProcessValue;
                                form.progressBar3.Value = lec.processValue;
                                form.label1.Text = lec.name;
                                form.label2.Text = lec.processInfo;
                                Thread.Sleep(1000);
                            }
                            Thread.Sleep(2000);
                        }
                        form.driver.FindElement(By.XPath("/html/body/center/div[1]/div[3]/div/ul[1]/li[1]")).Click();
                        Thread.Sleep(2000);
                    }
                }));
            }
        }*/


        private void playLecture()
        {
            foreach (ListViewItem item in checkList)
            {
                int grouid = int.Parse(item.SubItems[0].Text.Split(' ')[0]);
                int lectureid = int.Parse(item.SubItems[0].Text.Split(' ')[1]);
                if (!checkDic.Keys.Contains(grouid)) { checkDic[grouid] = new ArrayList(); }
                checkDic[grouid].Add(lectureid);
            }

            var keylist = checkDic.Keys.ToList();
            foreach (int key in keylist)
            {
                Group group = (Group)groupList[key];
                group.setGroupPageForLecture(driver);
                Thread.Sleep(2000);
                foreach (int index in checkDic[key])
                {
                    var lec = (Lecture)group.lectureList[index];
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                    js.ExecuteScript("javascript:data_load(0," + lec.lecturepage + ",2,'reg_dt','desc');");
                    Thread.Sleep(2000);
                    logAdd(lec.name + ": 강의 시작");
                    while (true)
                    {
                        if (lec.playLecture(driver)) {
                            Program.endLecList.Remove(group.groupId + " " + lec.lecturepage + " " + lec.lectureId);
                            File.WriteAllText(Program.path, String.Empty);
                            using (StreamWriter outputFile = new StreamWriter(Program.path))
                            {
                                foreach (String str in Program.endLecList)
                                {
                                    outputFile.WriteLine(str);
                                }
                                outputFile.Close();
                            }
                            break; 
                        }
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            progressBar3.Maximum = lec.maxProcessValue;
                            progressBar3.Value = lec.processValue;
                            label1.Text = lec.name;
                            label2.Text = lec.processInfo;
                        }));
                        Thread.Sleep(1000);
                    }
                    Thread.Sleep(2000);
                }
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("/html/body/center/div[2]/div[3]/div/ul[1]/li[1]/a")).Click();
                Thread.Sleep(4000);
            }
        }



        private void Login()
        {
            var id = driver.FindElement(By.Id("id"));
            id.SendKeys(Program.id);
            var pass = driver.FindElement(By.Id("passwd"));
            pass.SendKeys(Program.pass);
            driver.FindElement(By.XPath("/html/body/center/div[1]/div[2]/div[2]/form/table/tbody/tr[1]/td[2]/input")).Click();
            Program.value += 20;
            Thread.Sleep(2000);
            try
            {
                driver.FindElement(By.XPath("/html/body/center/div[1]/div[4]/div/div[2]/div/div[4]/div/div/form/table/tbody/tr[1]/td[2]/div/table/tbody/tr/td[2]/div[2]/a/div[1]/b"));
            }
            catch (Exception)
            {
                String path = Application.StartupPath + @"\lecture\login.txt";
                File.Delete(path);
                invokeThreadPlay(Program.form2, Program.form2.thread.Interrupt);
                invokeThreadPlay(Program.form3, Program.form3.thread.Interrupt);
                invokeThreadPlay(Program.form2, Program.form2.Close);
                Program.error = "비번 혹은 아이디가 일치 하지 않습니다.";
                invokeThreadPlay(Program.form3, () => { Program.form3.Visible = true; });
                return;
            }
            Program.value += 20;
            logAdd("로그인 성공!");
        }

        private void invokeThreadPlay(Form form, Action action)
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

        public void logAdd(String text)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                if (textBox1.Lines.Length >= 500) textBox1.Clear();
                textBox1.AppendText(string.Format("{0}\r\n", text));
            }));
        }


        private void getGroupList()
        {
            logAdd("강의 그룹 가져오기");
            var index = 2;
            while (true)
            {
                Boolean boolean = false;
                Thread thread = new Thread(() => boolean = runGetGroupList(index));
                threadList.Add(thread);
                thread.IsBackground = true;
                thread.Start();
                Thread.Sleep(8000);
                if (!boolean)
                {
                    break;
                }
                index++;
            }
            groupSize = groupList.Count;
            logAdd("그룹의 개수 : " + groupSize);
            logAdd("그룹의 강의 가져오기 시작");
            foreach (Group group in groupList)
            {
                Thread thread = new Thread(() => group.setLectureList());
                threadList.Add(thread);
                thread.IsBackground = true;
                thread.Start();
            }
        }

        private void updateSelectListView()
        {

            listView2.BeginUpdate();

            ListViewItem selitem;
            int index = 1;
            listView2.Items.Clear();
            foreach (ListViewItem item in checkList)
            {
                selitem = new ListViewItem(index.ToString());
                selitem.SubItems.Add(item.SubItems[2]);
                listView2.Items.Add(selitem);
                index++;
            }
            listView2.EndUpdate();

        }



        public void setListView()
        {
            logAdd("모든 강의 그룹의 강의 가져오기 성공");
            logAdd("-----------------------------------");
            logAdd("리스트화 시키는중...");
            this.Invoke(new MethodInvoker(delegate ()
            {
                listView1.FullRowSelect = true;
                ImageList imageList = new ImageList();
                imageList.ImageSize = new System.Drawing.Size(1, 30);
                listView1.SmallImageList = imageList;
                listView1.HideSelection = false;

                listView1.BeginUpdate();
                var groupid = 0;
                var lectureid = 0;
                ListViewItem item;
                foreach (Group group in groupList)
                {
                    if(group.lectureList.Count > 0)
                    {
                        logAdd(group.name + "의 그룹 리스트화 시작");
                        foreach (Lecture lecture in group.lectureList)
                        {
                            logAdd(lecture.name + "의 강의 리스트화 시작");
                            item = new ListViewItem(groupid + " " + lectureid);
                            item.SubItems.Add(group.name);
                            item.SubItems.Add(lecture.name);
                            item.SubItems.Add(lecture.processValue.ToString() + "/" + lecture.maxProcessValue.ToString());
                            listView1.Items.Add(item);
                            lectureid++;
                        }
                        item = new ListViewItem(" ");
                        item.SubItems.Add(" ");
                        item.SubItems.Add(" ");
                        item.SubItems.Add(" ");
                        listView1.Items.Add(item);
                    }
                    groupid++;
                    lectureid = 0;
                }
                listView1.EndUpdate();
            }));
            logAdd("리스트화 완료");
            File.WriteAllText(Program.path, String.Empty);
            using (StreamWriter outputFile = new StreamWriter(Program.path))
            {
                foreach (String str in Program.endLecList)
                {
                    outputFile.WriteLine(str);
                }
                outputFile.Close();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isListening) return;
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                if (item.SubItems[0].Text == " ") continue;
                item.Selected = false;
                if (checkList.Contains(item))
                {
                    item.BackColor = Color.White;
                    checkList.Remove(item);
                }
                else
                {
                    item.BackColor = Color.GreenYellow;
                    checkList.Add(item);
                }
                updateSelectListView();
            }
        }

        private Boolean runGetGroupList(int index)
        {
            var group = new Group(index, this);
            if (group.OpenGroupPage())
            {
                groupList.Add(group);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isListening) return;
            if (checkList.Count < 1) return;
            playThread = new Thread(() => playLecture());
            threadList.Add(playThread);
            playThread.IsBackground = true;
            playThread.Start();
            isListening = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            label1.Text = "종료 하는중...";
            foreach (Thread thread in threadList)
            {
                
                if (thread.IsAlive)
                {
                    Console.WriteLine("쓰레드 종료");
                    thread.Abort();
                }
            }
            quitDriver();
        }

        private void quitDriver()
        {
            progressBar3.Maximum = Program.driverList.Count;
            progressBar3.Minimum = 0;
            int value = 0;
            foreach (IWebDriver driver in Program.driverList)
            {
                Console.WriteLine("드라이버 종료");
                driver.Quit();
                value++;
                progressBar3.Value = value;
                label2.Text = value + " / " + progressBar3.Maximum;
            }
            Process.GetCurrentProcess().Kill();
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            initAll();
            label1.Text = "강의를 선택해주세요!";
            label2.Text = "이 곳에서 강의 진행상태가 표시 됩니다!";
            updateSelectListView();
        }
    }
}
