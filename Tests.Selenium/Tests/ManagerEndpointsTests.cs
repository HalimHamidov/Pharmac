using System;
using System.Threading;
using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Tests.Selenium.PageObjects;
using Tests.Selenium.PageObjects.MainMenu;
using Tests.Selenium.Services;

namespace Tests.Selenium
{
    [TestFixture(Author = "Halim", Description = "Auto-tests")]
    [Category("PHAR-T1")]
    [AllureDisplayIgnored]
    [PropertyTestId("PHAR-T1")]
    [AllureIssue("PHAR-T1")]
    [AllureEpic("Вопрос")]
    [AllureFeature("Менеджер")]
    [AllureStory("Добавление менеджеров в справочник")]
    [Category("Regression")]
    [CoveredSteps(1, 2, 3, 4, 5)]
    [AllureNUnit]

    class ManagerEndpointsTests : BaseTestSequential
    {
        private MainMenuPageObject _mainMenu;
        private MainMenuPageObject _managersMenu;
        private MainMenuPageObject _dictMenu;
        private ManagersPageObject _managers1Menu;

        [Test]
        [AllureSuite("Логин и пароль при входе")]
        [Description("Заходим в приложение")]
        public void LoginPage_LoginAsAdmin_IsLogined()
        {
            _mainMenu = LoginService.Login(Driver,
                EnvironmentConstants.StartLogin,
                EnvironmentConstants.StartLoginPassword);
            Assert.IsNotNull(_mainMenu);
        }

        [Test]
        [Description("Переход в Менеджера")]
        [AllureSubSuite("Нажимаем на справочники и менеджер. Перешли на страничку выбора менеджера")]
        public void MainMenu_NavigateToDictManagersMenu_IsOpen()
        {
            _dictMenu = NavigationService.NavigateToDictionaries(Driver);
            Assert.IsNotNull(_dictMenu);
            _managersMenu = NavigationService.NavigateToDictManagers(Driver);
            Assert.IsNotNull(_managersMenu);
        }

        [Test]
        [Description("Добавляем нового Менеджера Москва")]
        [AllureSubSuite("Нажимаем на добавить знак +, набираем название и выбираем страну или город, кликаем ")]
        public void ManagerPageObject_CreateNewManager_IsCreated()
        {
            _managers1Menu = NavigationService.NavigateToDictManagers1(Driver);

            Driver.FindElement(By.CssSelector("div.ant-card-extra>button > span + span")).Click();
            Driver.FindElement(By.CssSelector("input#name.ant-input")).SendKeys("Тест");
            Driver.FindElement(By.CssSelector("input[type='search']#rc_select_1")).Click();

            IWebElement countryDDown = Driver.FindElement(By.CssSelector("#rc_select_1"));
            Thread.Sleep(2000);
            countryDDown.SendKeys("Москва");
            Thread.Sleep(2000);
            countryDDown.SendKeys(Keys.Return);
            Driver.FindElement(By.XPath("//button[2]/span[contains(text(), 'Добавить')]")).Click();
            Thread.Sleep(2000);
            IWebElement managerCreated = Driver.FindElement(By.CssSelector("tr:nth-child(3) > td:nth-child(2)"));
            Assert.That(managerCreated.Text, Does.Contain("Тест"));
        }

        [Test]
        [Description("Добавляем нового Менеджера Истаравшан")]
        [AllureSubSuite("Нажимаем на добавить знак +, набираем название и выбираем страну или город, кликаем ")]
        public void ManagerPageObject_CreateNewManager_IsCreatedInCity2()
        {
            _managersMenu = NavigationService.NavigateToDictManagers(Driver);

            Driver.FindElement(By.CssSelector("div.ant-card-extra>button > span + span")).Click();
            Driver.FindElement(By.CssSelector("input#name.ant-input")).SendKeys("Тест");
            Driver.FindElement(By.CssSelector("input[type='search']#rc_select_1")).Click();

            IWebElement countryDDown = Driver.FindElement(By.CssSelector("#rc_select_1"));
            Thread.Sleep(2000);
            countryDDown.SendKeys("Истаравшан");
            Thread.Sleep(2000);
            countryDDown.SendKeys(Keys.Return);
            Driver.FindElement(By.XPath("//button[2]/span[contains(text(), 'Добавить')]")).Click();
            Thread.Sleep(2000);
            IWebElement managerCreated = Driver.FindElement(By.CssSelector("tr:nth-child(3) > td:nth-child(2)"));
            Assert.That(managerCreated.Text, Does.Contain("Тест"));
        }
        [Test]
        [Description("Добавляем нового Менеджера Бустон")]
        [AllureSubSuite("Нажимаем на добавить знак +, набираем название и выбираем страну или город, кликаем ")]
        public void ManagerPageObject_CreateNewManager_IsCreatedInCity3()
        {
            _managersMenu = NavigationService.NavigateToDictManagers(Driver);

            Driver.FindElement(By.CssSelector("div.ant-card-extra>button > span + span")).Click();
            Driver.FindElement(By.CssSelector("input#name.ant-input")).SendKeys("Тест");
            Driver.FindElement(By.CssSelector("input[type='search']#rc_select_1")).Click();

            IWebElement countryDDown = Driver.FindElement(By.CssSelector("#rc_select_1"));
            Thread.Sleep(2000);
            countryDDown.SendKeys("Бустон");
            Thread.Sleep(2000);
            countryDDown.SendKeys(Keys.Return);
            Driver.FindElement(By.XPath("//button[2]/span[contains(text(), 'Добавить')]")).Click();
            Thread.Sleep(2000);
            IWebElement managerCreated = Driver.FindElement(By.CssSelector("tr:nth-child(3) > td:nth-child(2)"));
            Assert.That(managerCreated.Text, Does.Contain("Тест"));
        }

        [Test]
        [Description("Удаляем созданного Менеджера")]
        [AllureSubSuite("Нажимаем на добавить знак Удалить, кликаем, подтверждаем ")]
        public void ManagerPageObject_DeleteCreatedManager_ManagerIsDeleted()
        {
            _managersMenu = NavigationService.NavigateToDictManagers(Driver);
            Driver.FindElement(By.XPath("//a[contains(text(), 'Менеджеры')]")).Click();
            Thread.Sleep(2000);
            Driver.FindElement(By.XPath("//td[contains(text(), 'Тест')]/ancestor::tr//td[contains(text(), 'Бустон')]/ancestor::tr//span[contains( @class, 'delete' )]")).Click();
            Thread.Sleep(2000);
            Driver.FindElement(By.XPath("//span[contains(text(), 'Удалить')]")).Click();
            Thread.Sleep(2000);
            Driver.FindElement(By.XPath("//td[contains(text(), 'Тест')]/ancestor::tr//td[contains(text(), 'Истаравшан')]/ancestor::tr//span[contains( @class, 'delete' )]")).Click();
            Thread.Sleep(2000);
            Driver.FindElement(By.XPath("//span[contains(text(), 'Удалить')]")).Click();
            Thread.Sleep(2000);
            Driver.FindElement(By.XPath("//td[contains(text(), 'Тест')]/ancestor::tr//td[contains(text(), 'Москва')]/ancestor::tr//span[contains( @class, 'delete' )]")).Click();
            Thread.Sleep(2000);
            Driver.FindElement(By.XPath("//span[contains(text(), 'Удалить')]")).Click();
            Thread.Sleep(2000);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }

        [Test]
        [Description("Ищем Менеджера Тест")]
        [AllureSubSuite("Нажимаем на посик поле набираем слово Тест и ждем что нет данных ")]
        public void ManagerPageObjectMenu_FindAnyManager_NoDataFound()
        {
            _managersMenu = NavigationService.NavigateToDictManagers(Driver);

            Driver.FindElement(By.CssSelector("span>input.ant-input")).SendKeys("Тест");
            var alertNoDataFound = Driver.FindElement(By.XPath(".//p[contains(text(), 'Нет данных')]"));
            Thread.Sleep(2000);
            Assert.That(alertNoDataFound.Text, Does.Contain("Нет данных"));
            Thread.Sleep(2000);
        }
    }
}
