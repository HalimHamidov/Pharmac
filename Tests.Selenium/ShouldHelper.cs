using System;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Tests.Selenium
{
    /// <summary>
    /// Helper class for checking if browser current page in valid equals test required location
    /// </summary>
    public static class ShouldHelper
    {
        /// <summary>
        /// Check if location is valid
        /// </summary>
        /// <param name="webDriver">Current instance of WebDriver</param>
        /// <param name="location">Test required location</param>
        public static void ShouldLocate(IWebDriver webDriver, string location)
        {
            try
            {
                // Wait for page loading
                new WebDriverWait(webDriver, TimeSpan.FromSeconds(60)).Until(ExpectedConditions.UrlContains(location));
            }
            catch (WebDriverTimeoutException ex)
            {
                throw new NotFoundException($"Cannot find out that app in specific location: {location}", ex);
            }
        }

        public static void WaitSomeInterval(int seconds = 10, int milliseconds = 0)
        {
            Task.Delay(TimeSpan.FromSeconds(seconds) + TimeSpan.FromMilliseconds(milliseconds)).Wait();
        }

        public static bool IsElementDisapeared(IWebDriver webDriver, By locator, int waitTime = 10)
        {
            try
            {
                WaitElementDisapear(webDriver, locator, waitTime);
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }

            return true;
        }

        public static void WaitElementDisapear(IWebDriver webDriver, By locator, int waitTime = 10)
        {
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            new WebDriverWait(webDriver, TimeSpan.FromSeconds(waitTime))
                .Until(x => x.FindElements(locator).Count == 0);

            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TestSettings.WaitIntervalSeconds);
        }

        public static void WaitElement(IWebDriver webDriver, By locator, int seconds = 20)
        {
            var waitOne = new WebDriverWait(webDriver, TimeSpan.FromSeconds(seconds));
            waitOne.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            var waitTwo = new WebDriverWait(webDriver, TimeSpan.FromSeconds(seconds));
            waitTwo.IgnoreExceptionTypes(typeof(StaleElementReferenceException));

            waitOne.Until(ExpectedConditions.ElementIsVisible(locator));
            waitTwo.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        public static bool IsElementExist(IWebDriver _webDriver, By elementToFind, int waitTime = 0)
        {
            // var timeLeft = _webDriver.Manage().Timeouts().ImplicitWait;
            if (waitTime != 0)
                _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(waitTime);
            var isElementExist = _webDriver.FindElements(elementToFind).Count > 0;
            if (waitTime != 0)
                _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TestSettings.WaitIntervalSeconds);
            return isElementExist;
        }

        public static void ScrollIntoView(IWebDriver webDriver, By locator)
        {
            ScrollIntoView(webDriver, webDriver.FindElement(locator));
        }

        public static void ScrollIntoView(IWebDriver webDriver, IWebElement element)
        {
            var js = (IJavaScriptExecutor)webDriver;
            js.ExecuteScript("arguments[0].scrollIntoView()", element);
            WaitSomeInterval(1);
        }

        public static IEnumerable<string> GetElementsText(IWebDriver _webDriver, By locator)
        {
            return _webDriver.FindElements(locator).Select(x => x.Text).ToList();
        }

        public static string GetJSValue(IWebDriver _webDriver, By locator)
        {
            var element = _webDriver.FindElement(locator);

            return GetJSValue(_webDriver, element);
        }

        public static string GetJSValue(IWebDriver _webDriver, IWebElement element)
        {
            var js = (IJavaScriptExecutor)_webDriver;

            return (string)js.ExecuteScript("return arguments[0].value;", element);
        }

        public static void ClickElement(IWebDriver webDriver, By locator, int waitTime = 0)
        {
            var element = webDriver.FindElement(locator);
            element.Click();
            WaitSomeInterval(waitTime);
        }

        public static void JSClickElement(IWebDriver webDriver, By locator, int waitTime = 0)
        {
            var js = (IJavaScriptExecutor)webDriver;
            var element = webDriver.FindElement(locator);

            js.ExecuteScript("return arguments[0].click();", element);
            WaitSomeInterval(waitTime);
        }

        public static void SendTextElement(IWebDriver webDriver, By locator, string text)
        {
            var element = webDriver.FindElement(locator);
            element.Clear();
            element.SendKeys(text);
        }

        public static bool IsFileDownloaded(IWebDriver webDriver, string fileFullPath, int seconds = 5)
        {
            try
            {
                new WebDriverWait(webDriver, TimeSpan.FromSeconds(seconds))
                .Until(x => File.Exists(fileFullPath));
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool ElementContainsText(IWebDriver webDriver, By locator, string text, int seconds = 20)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(seconds));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            wait.Message = $"Not found text '{text}' after { wait.Timeout}";

            wait.Until(d => d.FindElement(locator).Text.Contains(text));

            return true;
        }

        public static bool WaitFunc(IWebDriver webDriver, Func<IWebDriver, bool> waitFunc, int seconds = 10)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(seconds));
            wait.IgnoreExceptionTypes(typeof(WebDriverException));

            try
            {
                wait.Until(waitFunc);

                return true;
            }
            catch(WebDriverTimeoutException)
            {
                return false;
            }
        }

        public static void ClickElementUntilFail(IWebDriver webDriver, By locator, int times = 10)
        {
            for (var i = 0; i < times; i++)
            {
                try
                {
                    webDriver.FindElement(locator).Click();
                    WaitSomeInterval(1);
                }
                catch (Exception) { break; }
            }
        }
    }
}