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

            // IMPORTANT: assign to class field (no redeclaration)
            _driver = new ChromeDriver(options);

            _driver.Navigate().GoToUrl("https://www.facebook.com/");
        }

        [Test]
        public void Test()
        {
            Assert.That(_driver, Is.Not.Null);

            Console.WriteLine("Page Title: " + _driver!.Title);

            // Take screenshot during test execution
            TakeScreenshot("DuringTest");
        }

        private void TakeScreenshot(string stepName)
        {
            if (_driver == null) return;

            try
            {
                string folder = Path.Combine(
                    TestContext.CurrentContext.WorkDirectory,
                    "Screenshots");

                Directory.CreateDirectory(folder);

                string fileName = $"{TestContext.CurrentContext.Test.Name}_{stepName}.png";
                string filePath = Path.Combine(folder, fileName);

                var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                screenshot.SaveAsFile(filePath);

                Console.WriteLine($"Screenshot saved at: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Screenshot capture failed: " + ex.Message);
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
                if (_driver != null)
                {
                    _driver.Quit();
                    _driver.Dispose();   // Required for NUnit1032
                    _driver = null;
                }
            }
        }
    }
}
