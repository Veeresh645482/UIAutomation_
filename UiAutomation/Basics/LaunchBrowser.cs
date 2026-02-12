using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System;
using System.IO;

namespace UiAutomation.Basics
{
    public class LaunchBrowser
    {
        private IWebDriver? _driver;

        [SetUp]
        public void Initialization()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless=new");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1920,1080");

            // FIX: assign to class-level driver
            _driver = new ChromeDriver(options);
            _driver.Navigate().GoToUrl("https://www.facebook.com/");
        }

        [Test]
        public void Test()
        {
            Console.WriteLine(_driver?.Title);

            // Take screenshot during test
            TakeScreenshot("DuringTest");
        }

        private void TakeScreenshot(string name)
        {
            if (_driver == null) return;

            try
            {
                string folder = Path.Combine(
                    TestContext.CurrentContext.WorkDirectory,
                    "Screenshots");

                Directory.CreateDirectory(folder);

                string fileName = $"{TestContext.CurrentContext.Test.Name}_{name}.png";
                string filePath = Path.Combine(folder, fileName);

                var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                screenshot.SaveAsFile(filePath);

                Console.WriteLine($"Screenshot saved: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Screenshot failed: " + ex.Message);
            }
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                TakeScreenshot("TearDown");
            }
            finally
            {
                _driver?.Quit();
            }
        }
    }
}
