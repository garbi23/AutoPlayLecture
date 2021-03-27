﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoPlayLecture
{
    class Group
    {
        private int maxPage = 0;
        public ArrayList lectureList = new ArrayList();
        private int groupId = 0;
        private Form1 form = null;
        public String name = "";
        private IWebDriver driver;
        public Group(int groupId, Form1 form)
        {
            this.form = form;
            this.groupId = groupId;
            var options = new ChromeOptions();
            options.AddArguments("headless", "mute-audio");
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            Program.value += 10;
            driver = new ChromeDriver(service, options);
            driver.Url = Program.ieilmsurl;
            Login();
        }

        private void Login()
        {
            var id = driver.FindElement(By.Id("id"));
            id.SendKeys(Program.id);
            var pass = driver.FindElement(By.Id("passwd"));
            pass.SendKeys(Program.pass);
            driver.FindElement(By.XPath("/html/body/center/div[1]/div[2]/div[2]/form/table/tbody/tr[1]/td[2]/input")).Click();
        }


        public Boolean OpenGroupPage()
        {
            try
            {
                Thread.Sleep(3000);
                var grouptd = 0;
                var grouptr = 0;
                if (groupId % 4 == 0)
                {
                    grouptd = 4;
                    grouptr = groupId / 4;
                }
                else
                {
                    grouptd = groupId % 4;
                    grouptr = groupId / 4 + 1;
                }
                Console.WriteLine(groupId.ToString());
                Console.WriteLine(grouptd.ToString());
                Console.WriteLine(grouptr.ToString());
                name = driver.FindElement(By.XPath("/html/body/center/div[1]/div[4]/div/div[2]/div/div[4]/div/div/form/table/tbody/tr[" + grouptr.ToString() + "]/td[" + grouptd.ToString() + "]/div/table/tbody/tr/td[2]/div[2]/a/div[1]/b")).Text;
                driver.FindElement(By.XPath("/html/body/center/div[1]/div[4]/div/div[2]/div/div[4]/div/div/form/table/tbody/tr[" + grouptr.ToString() + "]/td[" + grouptd.ToString() + "]/div/table/tbody/tr/td[2]/div[1]")).Click();
                form.logAdd(name + " | 강의 그룹 추가 완료");
                form.logAdd("-----------------------------");
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public void setGroupPageForLecture(IWebDriver driver)
        {
            try
            {
                var grouptd = 0;
                var grouptr = 0;
                if (groupId % 4 == 0)
                {
                    grouptd = 4;
                    grouptr = groupId / 4;
                }
                else
                {
                    grouptd = groupId % 4;
                    grouptr = groupId / 4 + 1;
                }
                Console.WriteLine(groupId.ToString());
                Console.WriteLine(grouptd.ToString());
                Console.WriteLine(grouptr.ToString());
                name = driver.FindElement(By.XPath("/html/body/center/div[1]/div[4]/div/div[2]/div/div[4]/div/div/form/table/tbody/tr[" + grouptr.ToString() + "]/td[" + grouptd.ToString() + "]/div/table/tbody/tr/td[2]/div[2]/a/div[1]/b")).Text;
                driver.FindElement(By.XPath("/html/body/center/div[1]/div[4]/div/div[2]/div/div[4]/div/div/form/table/tbody/tr[" + grouptr.ToString() + "]/td[" + grouptd.ToString() + "]/div/table/tbody/tr/td[2]/div[1]")).Click();
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("/html/body/center/div[1]/div[4]/div/div[2]/div/div[5]/div[1]/table/thead/tr/th[2]/a/img")).Click();
                form.logAdd(name + " | 강의 목록 준비 완료");
                form.logAdd("-----------------------------");
            }
            catch (Exception)
            {
                return;
            }
        }


        public void setLectureList()
        {
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/center/div[1]/div[4]/div/div[2]/div/div[5]/div[1]/table/thead/tr/th[2]/a/img")).Click();
            Thread.Sleep(2000);
            for (int i = 1; i < 11; i++)
            {
                var tr = "";
                try
                {
                    tr = driver.FindElement(By.XPath("/html/body/center/div[2]/div[4]/div/div[2]/div/div[5]/div[2]/div[3]/form/table/tbody[2]/tr[" + i + "]/td[5]/a/img")).GetAttribute("title");
                }
                catch (Exception) {
                    break;
                }
                var name = tr.Split(' ')[0];
                if (!name.Contains("mp4")) continue;
                var lecture = new Lecture(i, driver);
                if (lecture.getLectureStat()) continue;
                lectureList.Add(lecture);
                form.logAdd(name + " | 강의 추가 완료");
            }
            form.logAdd(name + " | 강의 그룹 모든 강의 추가 완료!");
            form.groupSize--;
            if (form.groupSize <= 0)
            {
                form.setListView();
            }
            form.logAdd("남은 강의 그룹 수 : " + form.groupSize);
            form.logAdd("----------------------------------------");
            driver.Quit();
        }


    }
}
