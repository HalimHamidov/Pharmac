using OpenQA.Selenium;
using Tests.Selenium.PageObjects.MainMenu;

namespace Tests.Selenium.PageObjects
{
    public class ManagersPageObject
    {
        private readonly IWebDriver _webDriver;
        private readonly By _managerButton = By.CssSelector("div.ant-card-extra>button > span + span");
        private readonly By _selfLocator = By.CssSelector("#root > div > div > div > div");// надо исправить
        
        //        Driver.FindElement(By.CssSelector("div.ant-card-extra>button > span + span")).Click();
        // Driver.FindElement(By.CssSelector("input#name.ant-input")).SendKeys("Тест");
        // Driver.FindElement(By.CssSelector("input[type='search']#rc_select_1")).Click();
        // Driver.FindElement(By.XPath("//button[2]/span[contains(text(), 'Добавить')]")).Click();
        // IWebElement managerCreated = Driver.FindElement(By.CssSelector("tr:nth-child(3) > td:nth-child(2)"));
        //             Driver.FindElement(By.CssSelector("div.ant-card-extra>button > span + span")).Click();
        // Driver.FindElement(By.CssSelector("input#name.ant-input")).SendKeys("Тест");
        // Driver.FindElement(By.CssSelector("input[type='search']#rc_select_1")).Click();
        // // IWebElement countryDDown = Driver.FindElement(By.CssSelector("#rc_select_1"));
        //             Driver.FindElement(By.XPath("//button[2]/span[contains(text(), 'Добавить')]")).Click();
        // IWebElement managerCreated = Driver.FindElement(By.CssSelector("tr:nth-child(3) > td:nth-child(2)"));

        // Driver.FindElement(By.CssSelector("div.ant-card-extra>button > span + span")).Click();
        // Driver.FindElement(By.CssSelector("input#name.ant-input")).SendKeys("Тест");
        // Driver.FindElement(By.CssSelector("input[type='search']#rc_select_1")).Click();
        //             IWebElement countryDDown = Driver.FindElement(By.CssSelector("#rc_select_1"));
        // Driver.FindElement(By.XPath("//button[2]/span[contains(text(), 'Добавить')]")).Click();
        // IWebElement managerCreated = Driver.FindElement(By.CssSelector("tr:nth-child(3) > td:nth-child(2)"));
        //             Driver.FindElement(By.XPath("//a[contains(text(), 'Менеджеры')]")).Click();

        // Driver.FindElement(By.XPath("//td[contains(text(), 'Тест')]/ancestor::tr//td[contains(text(), 'Бустон')]/ancestor::tr//span[contains( @class, 'delete' )]")).Click();

        // Driver.FindElement(By.XPath("//span[contains(text(), 'Удалить')]")).Click();

        // Driver.FindElement(By.XPath("//td[contains(text(), 'Тест')]/ancestor::tr//td[contains(text(), 'Истаравшан')]/ancestor::tr//span[contains( @class, 'delete' )]")).Click();

        // Driver.FindElement(By.XPath("//span[contains(text(), 'Удалить')]")).Click();

        // Driver.FindElement(By.XPath("//td[contains(text(), 'Тест')]/ancestor::tr//td[contains(text(), 'Москва')]/ancestor::tr//span[contains( @class, 'delete' )]")).Click();

        // Driver.FindElement(By.XPath("//span[contains(text(), 'Удалить')]")).Click();
        //   Driver.FindElement(By.CssSelector("span>input.ant-input")).SendKeys("Тест");
        //     var alertNoDataFound = Driver.FindElement(By.XPath(".//p[contains(text(), 'Нет данных')]"));


        private readonly By _selfLocator = By.CssSelector("#root > div > div > div > div");// надо исправить

        // signin_container__1ov3t
        private readonly By _userNameLocator = By.CssSelector("input#username");
        private readonly By _passwordLocator = By.CssSelector("input#password");
        private readonly By _loginButtonLocator = By.CssSelector("button.ant-btn");
        private readonly By _ErrorLocator = By.CssSelector("div.ant-message div.ant-message-notice div.ant-message-error");

        public ManagersPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            ShouldHelper.ShouldLocate(_webDriver, $"{TestSettings.HostPrefix}");
            ShouldHelper.WaitElement(_webDriver, _selfLocator, 30);
        }

        public string FindError()
        {
            string ElementError = _webDriver.FindElement(_ErrorLocator).Text;
            return ElementError;
        }

    }
}
