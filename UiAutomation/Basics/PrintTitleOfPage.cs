using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace UiAutomation.Basics
{
    public class PrintTitleOfPage
    {
        private IWebDriver _driver;

        [SetUp]
        public void Intialization()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Url = "https://www.google.com/";
        }

        [Test]
        public void Test()
        {
            Console.WriteLine("Title of page is " + _driver.Title);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Close();
        }
    }
}
