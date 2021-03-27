using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoPlayLecture
{
    class Lecture
    {
        private int lectureId = 0;
        public int lecturepage = 0;
        public String name = "";
        public int maxProcessValue = 1;
        public int processValue = 0;
        public String processInfo = "";
        public Boolean load = false;
        public Boolean isPlaying = false;
        private IWebDriver driver;
        public Lecture(int lectureId, IWebDriver driver)
        {
            this.lectureId = lectureId;
            this.driver = driver;
        }


        public Boolean playLecture(IWebDriver driver)
        {
            if (!load)
            {
                Thread.Sleep(2000);
                name = driver.FindElement(By.XPath("/html/body/center/div[2]/div[4]/div/div[2]/div/div[5]/div[2]/div[3]/form/table/tbody[2]/tr[" + lectureId.ToString() + "]/td[5]/a/img")).GetAttribute("title");
                driver.FindElement(By.XPath("/html/body/center/div[2]/div[4]/div/div[2]/div/div[5]/div[2]/div[3]/form/table/tbody[2]/tr[" + lectureId.ToString() + "]/td[5]/a/img")).Click();
                Thread.Sleep(2000);
                load = true;
            }
            driver.FindElement(By.XPath("/html/body/center/div/div[4]/div/div[2]/div/div[5]/form[1]/div/div/div[2]/a[3]")).Click();
            Thread.Sleep(500);
            var maxpro = driver.FindElement(By.XPath("/html/body/center/div/div[4]/div/div[2]/div/div[5]/div[5]/div/div[2]/div/div[3]")).Text;
            if (maxpro == "") { maxpro = "0/1(0%)"; }
            processInfo = maxpro;
            var maxprod = maxpro.Split('/')[1].Split('(')[0];
            var minprod = maxpro.Split('/')[0];
            maxProcessValue = int.Parse(maxprod);
            processValue = int.Parse(minprod);
            Thread.Sleep(500);
            driver.FindElement(By.XPath("html/body/center/div/div[4]/div/div[2]/div/div[5]/form[1]/div/div/div[2]/a[3]")).Click();
            Console.WriteLine(processValue + "/" + maxProcessValue);
            if (processValue < maxProcessValue)
            {
                if (!isPlaying)
                {
                    driver.FindElement(By.XPath("/html/body/center/div[1]/div[4]/div/div[2]/div/div[5]/div[2]/video")).Click();
                    isPlaying = true;
                }
            }
            else
            {
                if (isPlaying)
                {
                    driver.FindElement(By.XPath("/html/body/center/div[1]/div[4]/div/div[2]/div/div[5]/div[2]/video")).Click();
                    isPlaying = false;
                }
                maxProcessValue = 1;
                driver.FindElement(By.XPath("/html/body/center/div/div[4]/div/div[2]/div/div[5]/form[1]/div/div/div[2]/a[1]")).Click();
                return true;
            }
            return false;
        }

        public Boolean getLectureStat()
        {
            name = driver.FindElement(By.XPath("/html/body/center/div[2]/div[4]/div/div[2]/div/div[5]/div[2]/div[3]/form/table/tbody[2]/tr[" + lectureId.ToString() + "]/td[5]/a/img")).GetAttribute("title");
            driver.FindElement(By.XPath("/html/body/center/div[2]/div[4]/div/div[2]/div/div[5]/div[2]/div[3]/form/table/tbody[2]/tr[" + lectureId.ToString() + "]/td[5]/a/img")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/center/div/div[4]/div/div[2]/div/div[5]/form[1]/div/div/div[2]/a[3]")).Click();
            Thread.Sleep(200);
            var maxpro = driver.FindElement(By.XPath("/html/body/center/div/div[4]/div/div[2]/div/div[5]/div[5]/div/div[2]/div/div[3]")).Text;
            if (maxpro == "") { maxpro = "0/1(0%)"; }
            var maxprod = maxpro.Split('/')[1].Split('(')[0];
            var minprod = maxpro.Split('/')[0];
            maxProcessValue = int.Parse(maxprod);
            processValue = int.Parse(minprod);
            driver.FindElement(By.XPath("/html/body/center/div/div[4]/div/div[2]/div/div[5]/form[1]/div/div/div[2]/a[1]")).Click();
            Console.WriteLine("이름 : " + name + processValue + "/" + maxProcessValue);
            Thread.Sleep(3000);
            if (processValue >= maxProcessValue){return true;}
            return false;
        }
    }
}
