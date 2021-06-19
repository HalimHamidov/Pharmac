using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System.Drawing;
using System.Linq;
using Allure.Commons;
using Tests.Selenium.TestData;
using Tests.Selenium.TestData.Models;
using Tests.Selenium.TestData.Generator;

namespace Tests.Selenium
{
    /// <summary>
    /// Base class for tests, must be inhereted, provide some methods for start ChromeWebDriver,
    /// taking screenshots. 
    /// <see cref="BaseTestIndependent"/> and <see cref="BaseTestSequential"/> as two main cases of use.
    /// </summary>
    public class BaseTest
    {
        protected EnvironmentConstants EnvironmentConstants;

        protected DataAutoGenerator DataAutoGenerator;

        protected Seeds Seeds;

        // protected IWebDriver  Driver;

        // The main Directory for all test environment, where stores screenshots and HTML files
        protected readonly string TestDownloadDirectory;
        private readonly string WholeTestDirectoryName;
        private readonly int _waitIntervalSeconds;

        protected BaseTest()
        {
            // Populate test parameters
            TestSettingsInitializer.InitializeSettings();

            TestDownloadDirectory = Path.Combine(TestSettings.TestDownloadDirectory, TestContext.CurrentContext.Test.FullName);
            WholeTestDirectoryName = TestSettings.WholeTestDirectoryName;
            _waitIntervalSeconds = TestSettings.WaitIntervalSeconds;
            
            if (!Directory.Exists(WholeTestDirectoryName))
                Directory.CreateDirectory(WholeTestDirectoryName);
        }
        /// <summary>
        /// Build WebDriver, start it and init test environment, such as directories
        /// </summary>
        protected IWebDriver StartDriver()
        {
            var driver = new InitDriver().SetDriver(TestSettings.Browser, TestDownloadDirectory);

            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(_waitIntervalSeconds);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_waitIntervalSeconds);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(_waitIntervalSeconds);
            // Set to standard window size
            driver.Manage().Window.Size = new Size(1440, 800);
            //StartPage
            driver.Url = $"{TestSettings.HostPrefix}";

            return driver;
        }

        protected void InitializeData()
        {
            EnvironmentConstants = EnvironmentConstantsProvider.Provide(TestSettings.NameOfEnvironment);
            TestSettings.HostPrefix = TestSettings.HostPrefix ?? EnvironmentConstants.HostPrefix;
            
            Seeds = SeedsProvider.ProvideSeeds();
            DataAutoGenerator = new DataAutoGenerator(TestSettings.HostPrefix, Seeds, TakeTestId());
            DataAutoGenerator.Login(EnvironmentConstants.StartLogin, EnvironmentConstants.StartLoginPassword);
        }

        private string TakeTestId()
        {
            var attrs = Attribute.GetCustomAttributes(this.GetType());
            foreach (var attr in attrs)
            {
                if (attr is PropertyTestId)
                {
                    var testId = (PropertyTestId)attr;
                    return testId.GetTestId();
                }
            }
            return "";
        }
        
        /// <summary>
        /// Check and take screenshot if test failed
        /// </summary>
        protected void TakeFailedTestScreenshot(IWebDriver driver)
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                // If test failed we need HTML and want not internal exception to reThrow, because it may override reason of test failure
                TakeScreenshot(driver, null, withHTML: false, allowReThrow: false);
            }
        }

        /// <summary>
        /// Take screenshot of browser actual state
        /// </summary>
        /// <param name="stepName">Name of step of test, for identify for which part of the test screenshot belongs to
        /// It also take part in generating screenshot fileName</param>
        /// <param name="withHTML">Include HTML or not. This needs for fail cases, where HTML of failed browser state can help find the reason of failure</param>
        /// <param name="allowReThrow">If true, than exception will be reThrow</param>
        protected void TakeScreenshot(IWebDriver driver, string stepName, bool withHTML = false, bool allowReThrow = true)
        {
            try
            {
                TakeScreenshotImpl(driver, stepName, withHTML);
            }
            catch (Exception)
            {
                if (allowReThrow)
                    throw;
            }
        }

        private void TakeScreenshotImpl(IWebDriver driver, string stepName = null, bool withHTML = false)
        {
            // If null, test is for Failed State
            var fileName = String.IsNullOrEmpty(stepName)
                ? TestContext.CurrentContext.Test.FullName
                : stepName;
            var testDirectory = Path.Combine(WholeTestDirectoryName, "Screenshots");

            if (!Directory.Exists(testDirectory))
                Directory.CreateDirectory(testDirectory);

            var fullFileName = Path.Combine(testDirectory, $"{fileName}.jpeg");
            driver.TakeScreenshot().SaveAsFile(fullFileName, ScreenshotImageFormat.Jpeg);

            AllureLifecycle.Instance.AddAttachment("Failed screen", "image/jpeg", fullFileName);

            if (withHTML)
            {
                var htmlText = driver.FindElement(By.TagName("html")).GetAttribute("innerHTML");
                var htmlTextFullFileName = Path.Combine(testDirectory, $"{fileName}.html");
                File.WriteAllText(htmlTextFullFileName, htmlText);
            }
        }

        protected void RemoveDownloadDirectory()
        {
            if (Directory.Exists(TestDownloadDirectory))
            {
                Directory.Delete(TestDownloadDirectory, true);
            }
        }
}
}
