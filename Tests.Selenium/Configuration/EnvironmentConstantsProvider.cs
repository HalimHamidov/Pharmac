using System;
using Newtonsoft.Json;
using System.IO;
using Tests.Selenium.TestData;

namespace Tests.Selenium.TestData
{
    public class EnvironmentConstantsProvider
    {
        private const string JsonTemplate = "Constants.{0}.json";
        private const string ConfigurationFolderName = "Configuration";

        public static EnvironmentConstants Provide(string nameEnvironment)
        {
            EnvironmentConstants environmentConstants;
            var fileName = string.Format(JsonTemplate, nameEnvironment);

            var directory = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);

            var settingsPath = Path.Combine(directory, ConfigurationFolderName, fileName);

            var jsonFile = File.ReadAllText(settingsPath);
            environmentConstants = JsonConvert.DeserializeObject<EnvironmentConstants>(jsonFile);
            return environmentConstants;
        }
    }
}
