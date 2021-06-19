using Tests.Selenium.PageObjects;
using OpenQA.Selenium;
using System;
using Tests.Selenium.PageObjects.MainMenu;
using Tests.Selenium.TestData.Models;

namespace Tests.Selenium.Services
{
    public class NavigationService
    {
        public static MainMenuPageObject NavigateToDictionaries(IWebDriver webDriver)
        {
            webDriver.Url = $"{TestSettings.HostPrefix}/dictionaries/";

            return new MainMenuPageObject(webDriver);
        }
        public static MainMenuPageObject NavigateToDictManagers(IWebDriver webDriver)
        {
            webDriver.Url = $"{TestSettings.HostPrefix}/dictionaries/managers/";

            return new MainMenuPageObject(webDriver);
        }
        public static ManagersPageObject NavigateToDictManagers1(IWebDriver webDriver)
        {
            webDriver.Url = $"{TestSettings.HostPrefix}/dictionaries/managers/";

            return new ManagersPageObject(webDriver);
        }
    }
}
