using Tests.Selenium.PageObjects;
using OpenQA.Selenium;
using System;
using Tests.Selenium.PageObjects.MainMenu;
using Tests.Selenium.TestData.Models;

namespace Tests.Selenium.Services
{
    public class LoginService
    {
        public static MainMenuPageObject Login(IWebDriver driver, User user,
            Action screenshotAction = null, bool closeTutorial = true)
        {
            return Login(driver, user.Login, user.Password, screenshotAction, closeTutorial);
        }

        public static MainMenuPageObject Login(IWebDriver driver, string login, string password, 
            Action screenshotAction = null, bool closeTutorial = true)
        {
            driver.Manage().Window.Maximize();

            var loginPage = new LoginPageObject(driver);

            screenshotAction?.Invoke();

            var mainMenu = loginPage
                .TypeUserName(login)
                .TypePassword(password)
                .Login();

            if (closeTutorial)
                mainMenu.CloseTutorialPopup();

            return mainMenu;
        }
    }
}
