using System;
using NUnit.Framework;

namespace Tests.Selenium
{
    class TestSettingsInitializer
    {
        public static void InitializeSettings()
        {
            if (TestContext.Parameters.Exists(TestSettingsNames.TestDirectoryParameterName))
            {
                TestSettings.WholeTestDirectoryName = TestContext.Parameters[TestSettingsNames.TestDirectoryParameterName];
            }
            if (TestContext.Parameters.Exists(TestSettingsNames.DownloadDirectoryParameterName))
            {
                TestSettings.TestDownloadDirectory = TestContext.Parameters[TestSettingsNames.DownloadDirectoryParameterName];
            }
            if (TestContext.Parameters.Exists(TestSettingsNames.BaseWebSiteAddressParameterName))
            {
                TestSettings.HostPrefix = TestContext.Parameters[TestSettingsNames.BaseWebSiteAddressParameterName];
            }

            //Проверяем существует ли такой параметр и если существует, то записываем в свойство значение этого параметра
            if (TestContext.Parameters.Exists(TestSettingsNames.EnvironmentParameterName))
            {
                TestSettings.NameOfEnvironment = TestContext.Parameters[TestSettingsNames.EnvironmentParameterName];
            }

            if (TestContext.Parameters.Exists(TestSettingsNames.WaitIntervalParameterName))
            {
                TestSettings.WaitIntervalSeconds = Int32.Parse(TestContext.Parameters[TestSettingsNames.WaitIntervalParameterName]);
            }
            if (TestContext.Parameters.Exists(TestSettingsNames.StartAdminLoginParameterName))
            {
                TestSettings.WholeTestDirectoryName = TestContext.Parameters[TestSettingsNames.StartAdminLoginParameterName];
            }
            if (TestContext.Parameters.Exists(TestSettingsNames.StartAdminPasswordParameterName))
            {
                TestSettings.WholeTestDirectoryName = TestContext.Parameters[TestSettingsNames.StartAdminPasswordParameterName];
            }
            if (TestContext.Parameters.Exists(TestSettingsNames.HeadlessParameterName))
            {
                TestSettings.IsHeadless = true;
            }

            if (TestContext.Parameters.Exists(TestSettingsNames.UrlHubParameterName))
            {
                TestSettings.UrlHub = TestContext.Parameters[TestSettingsNames.UrlHubParameterName];
                TestSettings.IsGrid = true;
            }

            if (TestContext.Parameters.Exists(TestSettingsNames.BrowserParameterName))
            {
                TestSettings.Browser = Browser.Chrome;
                if (Enum.TryParse<Browser>(TestContext.Parameters[TestSettingsNames.BrowserParameterName], out var browser))
                {
                    TestSettings.Browser = browser;
                }
            }

            if (TestContext.Parameters.Exists(TestSettingsNames.GenerateSeedsParameterName))
            {
                TestSettings.NeedGenerateSeeds = true;
            }
        }
    }
}
