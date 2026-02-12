using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace UiAutomation.Basics
{
    public class LaunchBrowser
    {
        private IWebDriver? _driver;

        [SetUp]
        public void Intialization()
        {
            ChromeOptions options = new ChromeOptions();
options.AddArgument("--headless=new");
options.AddArgument("--no-sandbox");
options.AddArgument("--disable-dev-shm-usage");
options.AddArgument("--disable-gpu");
options.AddArgument("--window-size=1920,1080");

IWebDriver _driver = new ChromeDriver(options);
            _driver.Url = "https://www.facebook.com/";
        }

        [Test]
        public void Test()
        {
            Console.WriteLine(_driver?.Title);
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                if (_driver != null)
                {
                    // Create screenshot folder
                    string folder = Path.Combine(
                        TestContext.CurrentContext.WorkDirectory,
                        "Screenshots");

                    Directory.CreateDirectory(folder);

                    // File name based on test
                    string fileName = $"{TestContext.CurrentContext.Test.Name}.png";
                    string filePath = Path.Combine(folder, fileName);

                    // Take screenshot
                    var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                    screenshot.SaveAsFile(filePath);

                    Console.WriteLine($"Screenshot saved: {filePath}");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Screenshot failed: " + ex.Message);
            }
            finally
            {
                _driver?.Quit();
            }
        }

    }
}
