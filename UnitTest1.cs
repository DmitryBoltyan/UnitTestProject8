﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UnitTestProject8
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Random rand = new Random();

            string sender = "test0305";
            string password = "123automation";
            string subject = "Test" + rand.Next(0, 1000);
            string text = "Текст тестового сообщения + " + rand.Next(0, 1000);
            string reciever = "test0305reciever@mailinator.com";

            IWebDriver driver = new ChromeDriver();

            void LoginAs()
            {
                driver.Url = "https://webmail.meta.ua/";
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                driver.FindElement(By.Name("login")).SendKeys(sender);
                driver.FindElement(By.Name("password")).SendKeys(password);
                driver.FindElement(By.Name("subm")).Click();
            }

            void GoToNewLetter()
            {
                driver.FindElement(By.Id("id_send_email")).Click();

            }

            void Writeletter()
            {
                driver.FindElement(By.Name("send_to")).SendKeys(reciever);
                driver.FindElement(By.Name("subject")).SendKeys(subject);
                driver.FindElement(By.Name("body")).SendKeys(text);
                driver.FindElement(By.ClassName("panel_submit")).Click();
            }

            void CheckMail()
            {
                driver.Url = "https://www.mailinator.com/";
                driver.FindElement(By.Id("inboxfield")).SendKeys(reciever);
                driver.FindElement(By.ClassName("input-group-btn")).Click();

                driver.FindElement(By.XPath("//table//*[contains(text(), '"+subject+"')]")).Click();

                driver.SwitchTo().Frame("msg_body");

                var elem = driver.FindElement(By.XPath("//*[contains(.,'"+text+"')]"));
                if (elem == null) Console.WriteLine("Письмо не содержит заданный текст");
            }

            LoginAs();
            GoToNewLetter();
            Writeletter();
            CheckMail();

            driver.Quit();
        }
    }
}
