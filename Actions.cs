using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace AutomationChallenge_MoisesRivas
{
    public class Actions
    {
        #region Variables
        public IWebDriver Driver { get; set; }
        public string url = "https://www.saucedemo.com/";
        #endregion

        #region SetupDriver
        //Method to setupDriver
        public void SetupDriver(string projectPath, string driver)
        {
            if (driver.ToLower().Equals("chrome"))
            {
                Driver = new ChromeDriver(projectPath + @"\Drivers");
                Driver.Manage().Cookies.DeleteAllCookies();
                Driver.Manage().Window.Maximize();
                Driver.Navigate().GoToUrl(url);
            }
            else if (driver.ToLower().Equals("firefox"))
            {
                Driver = new FirefoxDriver(projectPath + @"\Drivers");
                Driver.Manage().Cookies.DeleteAllCookies();
                Driver.Manage().Window.Maximize();
                Driver.Navigate().GoToUrl(url);
            }
        }

        public void Quit()
        {
            Driver.Quit();
            Driver.Dispose();
        }

        #endregion

        #region Actions

        public void TakeSnapshot(bool passed, string message)
        {
            Screenshot ss = ((ITakesScreenshot)Driver).GetScreenshot();
            var byteArray = ss.AsByteArray;
            File.WriteAllBytes(CreateSnapshotsDirectory(passed, message), byteArray);
        }

        public string CreateSnapshotsDirectory(bool passed, string message)
        {
            try
            {
                StackTrace stackTrace = new StackTrace();
                MethodBase methodBase = stackTrace.GetFrames().FirstOrDefault(x => x.GetMethod().Name.Contains("TC")).GetMethod();
                var baseFileName = passed ? "Snap_" + message + "_" : "Error_" + message + "_";
                var fileName = baseFileName + GetPlainDate() + ".png";
                string directoryInjected = methodBase.Name;
                var pathDirectory = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Snapshots", directoryInjected);

                if (!Directory.Exists(pathDirectory))
                {
                    Directory.CreateDirectory(pathDirectory);
                }
                return Path.Combine(pathDirectory, fileName);
            }
            catch (Exception e)
            {
                Quit();
                throw e;
            }
        }

        private string GetPlainDate()
        {
            return DateTime.Now.ToString("s").Replace("-", string.Empty).Replace(":", string.Empty).Replace(".", string.Empty);
        }

        public MediaEntityModelProvider CaptureScreenShotAndReturnModel(string name)
        {
            var screenshot = ((ITakesScreenshot)Driver).GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, name).Build();
        }
        #endregion

    }
}