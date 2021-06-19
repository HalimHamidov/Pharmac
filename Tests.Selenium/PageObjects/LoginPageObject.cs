using OpenQA.Selenium;
using Tests.Selenium.PageObjects.MainMenu;

namespace Tests.Selenium.PageObjects
{
    public class LoginPageObject
    {
        private readonly IWebDriver _webDriver;
        private readonly By _selfLocator = By.CssSelector("#root > div > div > div > div");// надо исправить
        
       // signin_container__1ov3t
        private readonly By _userNameLocator = By.CssSelector("input#username");
        private readonly By _passwordLocator = By.CssSelector("input#password");
        private readonly By _loginButtonLocator = By.CssSelector("button.ant-btn");
        private readonly  By _ErrorLocator = By.CssSelector("div.ant-message div.ant-message-notice div.ant-message-error");

        public LoginPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            ShouldHelper.ShouldLocate(_webDriver, $"{TestSettings.HostPrefix}");
            ShouldHelper.WaitElement(_webDriver, _selfLocator, 30);
        }

        public LoginPageObject TypeUserName(string userName)
        {
            var element = _webDriver.FindElement(_userNameLocator);
            element.Clear();
            element.SendKeys(userName);
            return this;
        }

        public LoginPageObject TypePassword(string password)
        {
            var element = _webDriver.FindElement(_passwordLocator);
             element.Clear();
            element.SendKeys(password);
            return this;
        }
        public LoginPageObject ErrorLogin()
        {
            var element = _webDriver.FindElement(_loginButtonLocator);
            element.Submit();
            return new LoginPageObject(_webDriver);
        }
        public MainMenuPageObject Login()
        {
            var element = _webDriver.FindElement(_loginButtonLocator);
            element.Submit();
            return new MainMenuPageObject(_webDriver);
        }

        public string FindError()
        {
            string ElementError = _webDriver.FindElement(_ErrorLocator).Text;
            return ElementError; 
         }

    }
}
