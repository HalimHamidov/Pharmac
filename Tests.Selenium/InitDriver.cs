using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using System.Drawing;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.IE;

namespace Tests.Selenium
{
    public enum Browser
    {
        Chrome,
        IE,
        Yandex
    }

    public class InitDriver
    {
        public IWebDriver SetDriver(Browser browser, string downloadDirectory)
        {
            switch (browser)
            {
                case Browser.Chrome:
                    var chromeOptions = BuildChromeOptions(downloadDirectory);

                    if (TestSettings.IsGrid)
                    {
                        return new RemoteWebDriver(
                            new Uri(TestSettings.UrlHub),
                            chromeOptions.ToCapabilities(),
                            TimeSpan.FromMinutes(5))
                        {
                            FileDetector = new LocalFileDetector()
                        };
                    }
                    else
                    {
                        return new ChromeDriver(
                            ChromeDriverService.CreateDefaultService(),
                            chromeOptions,
                            TimeSpan.FromMinutes(1));
                    }

                case Browser.Yandex:
                    var yandexOptions = BuildChromeOptions(downloadDirectory);

                    if (TestSettings.IsGrid)
                    {
                        yandexOptions.BrowserVersion = "yandex";

                        return new RemoteWebDriver(
                            new Uri(TestSettings.UrlHub),
                            yandexOptions.ToCapabilities(),
                            TimeSpan.FromMinutes(5))
                        {
                            FileDetector = new LocalFileDetector()
                        };
                    }
                    else
                    {
                        // расскомментируй и укажи путь до .exe яндекс.браузера
                        // yandexOptions.BinaryLocation = "C:\\Program Files (x86)\\Yandex\\YandexBrowser\\Application\\browser.exe";
                        return new ChromeDriver(
                            ChromeDriverService.CreateDefaultService(),
                            yandexOptions,
                            TimeSpan.FromMinutes(1));
                    }

                case Browser.IE:
                    var IEOptions = BuildIEOptions();

                    if (TestSettings.IsGrid)
                    {
                        return new RemoteWebDriver(
                            new Uri(TestSettings.UrlHub),
                            IEOptions.ToCapabilities(),
                            TimeSpan.FromMinutes(5))
                        {
                            FileDetector = new LocalFileDetector()
                        };
                    }
                    else
                    {
                        return new InternetExplorerDriver(
                           InternetExplorerDriverService.CreateDefaultService(),
                           IEOptions,
                           TimeSpan.FromMinutes(1));
                    }
            }

            return null;
        }

        public ChromeOptions BuildChromeOptions(string downloadDirectory)
        {
            var options = new ChromeOptions();
            // Add Russion locale, without it Front will crash
            options.AddArgument("--lang=ru");
            options.AddArgument("--no-sandbox");
            // Init directiory for downloading files
            if (!Directory.Exists(downloadDirectory))
                Directory.CreateDirectory(downloadDirectory);
            options.AddUserProfilePreference("download.default_directory", downloadDirectory);
            options.AddUserProfilePreference("download.promt_for_download", false);
            options.AddUserProfilePreference("browser.setDownloadBehavior", "allow");

            if (TestSettings.IsHeadless)
                options.AddArgument("--headless");

            return options;
        }

        public InternetExplorerOptions BuildIEOptions()
        {
            var options = new InternetExplorerOptions();

            return options;
        }

    }

}
