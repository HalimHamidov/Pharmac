using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
// using Tests.Selenium.ModelHelpers;
// using Tests.Selenium.PageObjects.EducationalMaterial;
// using Tests.Selenium.PageObjects.Instructions;
// using Tests.Selenium.PageObjects.ModalDialog;
using System;
using System.Linq;

namespace Tests.Selenium.PageObjects.MainMenu
{
    public class MainMenuPageObject : MainMenuTab
    {
        private readonly By _dashboardLocator = By.CssSelector("#root > section > section > section > main");
        private readonly By _avatarMenuButtonLocator = By.CssSelector("span.ant-avatar");

        private readonly By _closePopUpNewsWindowButton = By.CssSelector("senat-tutorial-slides-popup .close-button");

        private readonly By _popUpNewsWindow = By.CssSelector("senat-tutorial-slides-popup");

        // // TODO: Upgrade this selector for using text instead of absence of routerlink
        // // private readonly By _exitButtonLocator =
        // // By.XPath( "//div[@class='mat-menu-content']//senat-ui-dropdown-menu-item//a[not(@routerlink)]");

        private readonly By _publicContainerLocator = By.CssSelector("#root > section > aside > div.ant-layout-sider-children > div");
        
        // private readonly By _profiletButtonLocator = By.XPath("//senat-ui-dropdown-menu-item//a[@href='/profile']");
        // private readonly By _senatLogoLocator = By.XPath("//div[@class = 'logo-wrapper']//img");
        // private readonly By _searchPageLocator = By.XPath("//senat-ui-tab-link-switch//a[@href='/search']");

        // private readonly By _newsTutorial = By.CssSelector("body senat-tutorial-slides-popup .new-features-text");

        // private readonly By _instructionsButtonLocator = By.XPath("//*[contains(@class,'menu')]//*[@class='mat-tab-links']/a[@href='/instructions']");

        // private readonly By _EducationalMaterialButtonLocator = By.CssSelector("[href='/edu']");
        
        // private const string TutorialPopupSelector = "senat-tutorial-slides-popup";

        public MainMenuPageObject(IWebDriver webDriver) : base(webDriver)
        {
            ShouldHelper.ShouldLocate(_webDriver, $"{TestSettings.HostPrefix}");
            ShouldHelper.WaitElement(_webDriver, _dashboardLocator);
            CheckLocation();
        }

        private void CheckLocation()
        {
            _webDriver.FindElement(_publicContainerLocator);
            _webDriver.FindElement(_avatarMenuButtonLocator);
        }
            
        // public SearchPageObject ClickOnSearchPage()
        // {
        //     ShouldHelper.WaitElement(_webDriver, _searchPageLocator);
        //     var searchButton = _webDriver.FindElement(_searchPageLocator);
        //     searchButton.Click();
        //     return new SearchPageObject(_webDriver);
        // }


        public MainMenuPageObject CloseTutorialPopup()
        {
            if (ShouldHelper.IsElementExist(_webDriver, _popUpNewsWindow, 5))
            {
                ShouldHelper.WaitElement(_webDriver, _closePopUpNewsWindowButton);
                _webDriver.FindElement(_closePopUpNewsWindowButton).Click();
            }
            return this;
        }

        // public MainMenuPageObject CheckUser(string lastName, string firstName, string middleName)
        // {
        //     var userShortName = PersonNameHelper.ProvideShortPersonName(lastName, firstName, middleName);
        //     return CheckUser(userShortName);
        // }

        // public MainMenuPageObject CheckUser(string pattern)
        // {
        //     _webDriver.FindElement(CreateProfileNameLocator(pattern));
        //     return this;
        // }

        // public ProfilePageObject GoIntoProfile()
        // {
        //     ShouldHelper.WaitElement(_webDriver, _avatarMenuButtonLocator);
        //     var avatar = _webDriver.FindElement(_avatarMenuButtonLocator);

        //     avatar.Click();
        //     ShouldHelper.WaitSomeInterval(1);
        //     ShouldHelper.WaitElement(_webDriver, _profiletButtonLocator);
        //     var profileButton = _webDriver.FindElement(_profiletButtonLocator);

        //     profileButton.Click();
        //     // Must check that was impersonation
        //     return new ProfilePageObject(_webDriver);
        // }

        // private By CreateProfileNameLocator(string pattern)
        // {
        //     return By.XPath(
        //         $"//div[@class='profile-name-wrapper']//span[contains(@class,'profile-name')]//text()[contains(., \"{pattern}\")]/ancestor::span");
        // }

        // public EducationalMaterialPageObject NavigateToEducationalMaterial()
        // {
        //     ShouldHelper.ClickElement(_webDriver, _EducationalMaterialButtonLocator);

        //     return new EducationalMaterialPageObject(_webDriver);
        // }
    }
}