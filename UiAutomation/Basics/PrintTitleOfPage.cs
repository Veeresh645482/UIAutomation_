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
            ChromeOptions options = new ChromeOptions();
options.AddArgument("--headless=new");
options.AddArgument("--no-sandbox");
options.AddArgument("--disable-dev-shm-usage");
options.AddArgument("--disable-gpu");
options.AddArgument("--window-size=1920,1080");

IWebDriver driver = new ChromeDriver(options);
            
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
