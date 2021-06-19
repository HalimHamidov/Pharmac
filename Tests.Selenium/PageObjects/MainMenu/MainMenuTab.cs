using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
// using Tests.Selenium.Admin;
// using Tests.Selenium.Elements;
// using Tests.Selenium.Instructions;
// using Tests.Selenium.ModalDialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Tests.Selenium.PageObjects.MainMenu
{
    public class MainMenuTab
    {
        protected readonly IWebDriver _webDriver;

        // private readonly By _createButtonLocator = By.CssSelector("button.button-creator span.mat-button-wrapper");

        // private readonly By _createIssueButtonLocator = By.CssSelector("senat-ui-dropdown-menu-item a[routerlink='/issues/create']");

        // private readonly By _createMeetingButtonLocator = By.CssSelector("i.senat-icon-meeting");

        // private readonly By _createInstructionButtonLocator = By.CssSelector("i.senat-icon-mission");

        // private readonly By _instructionsListMenuTabLocator = By.CssSelector(".mat-tab-list a[href='/instructions']");

        // private readonly By _navigateToAdminConsoleLocator = By.XPath("//a[@routerlink='/admin']");

        // private readonly By _avatarMenuButtonLocator = By.CssSelector("button.mat-button div.avatar");

        // private readonly By _feedbackMenuButtonLocator = By.XPath("//a[@href='/feedback']//span");

        // private readonly By _dashboardMenuButtonLocator = By.XPath("//*[@href='/dashboard']//span");

        // private readonly By _notificationButtonLocator = By.XPath("//senat-ui-tab-link-switch//a[@href='/notifications']");

        // private readonly By _meetingListMenuTabLocator = By.XPath(
        //         "//*[contains(@class,'menu')]//*[@class='mat-tab-links']//a[@href='/meetings']");

        // private readonly By _issueListMenuTabLocator = By.XPath(
        //         "//*[contains(@class,'menu')]//*[@class='mat-tab-links']//a[@href='/issues']");

        // private readonly By _tutorialButtonLocator = By.CssSelector("senat-tutorial-button button");
        
        // private readonly By _exitButtonLocator =
        //    By.XPath("//senat-ui-dropdown-menu-item//*[text()='Выйти']/ancestor::a");

        // private readonly By _confirmButtonLocator = By.CssSelector("senat-mat-dialog-confirm mat-dialog-actions button.mat-raised-button");
        // private readonly By _cancelButtonLocator = By.CssSelector("senat-mat-dialog-confirm mat-dialog-actions button.mat-stroked-button");
        // private readonly By _bannerLocator = By.CssSelector("senat-banner");
        // private readonly By _bannerButtonCloseLocator = By.XPath("//*[contains(@class,'banner-button-close')]");


        public MainMenuTab(IWebDriver driver)
        {
            _webDriver = driver;
        }

        // public FilterElement GetFilterElement()
        // {
        //     return new FilterElement(_webDriver);
        // }

        // public CreateIssueFormPageObject NavigateToCreateIssueForm()
        // {
        //     ClickCreateButton();

        //     ShouldHelper.WaitElement(_webDriver, _createIssueButtonLocator);
        //     ShouldHelper.ClickElement(_webDriver, _createIssueButtonLocator, 1);

        //     return new CreateIssueFormPageObject(_webDriver);
        // }

        // public CreateMeetingFormPageObject NavigateToCreateMeetingForm()
        // {
        //     ClickCreateButton();

        //     ShouldHelper.WaitElement(_webDriver, _createMeetingButtonLocator);
        //     ShouldHelper.ClickElement(_webDriver, _createMeetingButtonLocator, 1);

        //     return new CreateMeetingFormPageObject(_webDriver);
        // }

        // public CreateInstructionFormPageObject NavigateToCreateInstructionsForm()
        // {
        //     ClickCreateButton();

        //     ShouldHelper.WaitElement(_webDriver, _createInstructionButtonLocator);
        //     ShouldHelper.ClickElement(_webDriver, _createInstructionButtonLocator, 1);

        //     return new CreateInstructionFormPageObject(_webDriver);
        // }
        
        // public bool CreateIssueButtonExists()
        // {
        //     return CreateButtonExists(_createIssueButtonLocator);
        // }

        // public bool CreateMeetingButtonExists()
        // {
        //     return CreateButtonExists(_createMeetingButtonLocator);
        // }

        // private bool CreateButtonExists(By locator)
        // {
        //     ClickCreateButton();

        //     var itemExists = ShouldHelper.IsElementExist(_webDriver, locator, 1);

        //     ShouldHelper.JSClickElement(_webDriver, _createButtonLocator);

        //     return itemExists;
        // }

        // private void ClickCreateButton()
        // {
        //     ShouldHelper.WaitElement(_webDriver, _createButtonLocator);
        //     ShouldHelper.ClickElement(_webDriver, _createButtonLocator, 1);
        // }

        // public FeedbackPageObject NavigateToFeedbackPage()
        // {
        //     ShouldHelper.WaitElement(_webDriver, _feedbackMenuButtonLocator);
        //     ShouldHelper.ClickElement(_webDriver, _feedbackMenuButtonLocator, 3);

        //     return new FeedbackPageObject(_webDriver);
        // }

        // public NotificationsPageObject NavigateToNotificationsList()
        // {
        //     ShouldHelper.WaitElement(_webDriver, _notificationButtonLocator);
           //  ShouldHelper.ClickElement(_webDriver, _notificationButtonLocator, 5);

        //     return new NotificationsPageObject(_webDriver);
        // }

        // public MeetingListPageObject NavigateToMeetingList()
        // {
        //     ShouldHelper.WaitElement(_webDriver, _meetingListMenuTabLocator);
        //     ShouldHelper.ClickElement(_webDriver, _meetingListMenuTabLocator, 1);

        //     return new MeetingListPageObject(_webDriver);
        // }

        // public IssueListPageObject NavigateToIssueList()
        // {
        //     ShouldHelper.WaitElement(_webDriver, _issueListMenuTabLocator);
        //     ShouldHelper.ClickElement(_webDriver, _issueListMenuTabLocator, 1);

        //     return new IssueListPageObject(_webDriver);
        // }

        // public DashboardPageObject NavigateToDashboard()
        // {
        //     ShouldHelper.WaitElement(_webDriver, _dashboardMenuButtonLocator);
        //     ShouldHelper.ClickElement(_webDriver, _dashboardMenuButtonLocator, 1);

        //     return new DashboardPageObject(_webDriver);
        // }

        // public AdminConsole NavigateToAdminConsole()
        // {
        //     ShouldHelper.WaitElement(_webDriver, _avatarMenuButtonLocator);
        //     var avatar = _webDriver.FindElement(_avatarMenuButtonLocator);
        //     avatar.Click();
        //     ShouldHelper.WaitElement(_webDriver, _navigateToAdminConsoleLocator);
        //     ShouldHelper.WaitSomeInterval(2);
        //     _webDriver.FindElement(_navigateToAdminConsoleLocator).Click();
        //     return new AdminConsole(_webDriver);
        // }

        // public bool AdminConsoleOptionExists()
        // {
        //     ShouldHelper.WaitElement(_webDriver, _avatarMenuButtonLocator);
        //     ShouldHelper.ClickElement(_webDriver, _avatarMenuButtonLocator);

        //     return ShouldHelper.IsElementExist(_webDriver, _navigateToAdminConsoleLocator, 2);
        // }

        // public InstructionsListPageObject NavigateToInstructionsTab()
        // {
        //     ShouldHelper.WaitElement(_webDriver, _instructionsListMenuTabLocator);
        //     _webDriver.FindElement(_instructionsListMenuTabLocator).Click();

        //     return new InstructionsListPageObject(_webDriver);
        // }

        // public TutorialSlidesPageElement ClickTutorialButton()
        // {
        //     ShouldHelper.ClickElement(_webDriver, _tutorialButtonLocator);

        //     return new TutorialSlidesPageElement(_webDriver);
        // }

        // public MainMenuPageObject LogoutImpersonation()
        // {
        //     PerformLogout();

        //     return new MainMenuPageObject(_webDriver);
        // }

        // public LoginPageObject Logout()
        // {
        //     PerformLogout();

        //     return new LoginPageObject(_webDriver);
        // }

        // protected void PerformLogout()
        // {
        //     ShouldHelper.WaitElement(_webDriver, _avatarMenuButtonLocator);
        //     ShouldHelper.ClickElement(_webDriver, _avatarMenuButtonLocator, 2);

        //     ShouldHelper.WaitElement(_webDriver, _exitButtonLocator);
        //     ShouldHelper.ClickElement(_webDriver, _exitButtonLocator, 1);

        //     ShouldHelper.WaitElement(_webDriver, _confirmButtonLocator);
        //     ShouldHelper.ClickElement(_webDriver, _confirmButtonLocator, 2);
        // }

        // public void CancelLogout()
        // {
        //     ShouldHelper.WaitElement(_webDriver, _avatarMenuButtonLocator);
        //     ShouldHelper.ClickElement(_webDriver, _avatarMenuButtonLocator, 2);

        //     ShouldHelper.WaitElement(_webDriver, _exitButtonLocator);
        //     ShouldHelper.ClickElement(_webDriver, _exitButtonLocator, 1);

        //     ShouldHelper.WaitElement(_webDriver, _cancelButtonLocator);
        //     ShouldHelper.ClickElement(_webDriver, _cancelButtonLocator, 2);
        // }
        
        // public string GetBannerText()
        // {
        //     return _webDriver.FindElement(By.XPath("//span[contains(@class,'banner-text')]")).Text;
        // }

        // public void CloseBanner()
        // {
        //     _webDriver.FindElement(_bannerButtonCloseLocator).Click();

        //     new ConfirmationDialog(_webDriver).Confirm();

        //     ShouldHelper.WaitElementDisapear(_webDriver, _bannerButtonCloseLocator);
        // }

        // public bool BannerExists()
        // {
        //     return ShouldHelper.IsElementExist(_webDriver, _bannerLocator, 2);
        // }

        // public bool CloseBannerButtonExist()
        // {
        //     return ShouldHelper.IsElementExist(_webDriver, _bannerButtonCloseLocator, 2);
        // }

    }
}
