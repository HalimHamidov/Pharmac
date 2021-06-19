using System;
using System.IO;
using Newtonsoft.Json;

namespace Tests.Selenium.TestData.Models
{
    public static class SeedsProvider
    {
        private static readonly string SeedsFileName = "Seeds.json";

        public static Seeds ProvideSeeds()
        {
            var directory = Path.GetDirectoryName(
                AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
            var seedsPath = Path.Combine(directory, SeedsFileName);
            var seedsFile = File.ReadAllText(seedsPath);
            var seeds = JsonConvert.DeserializeObject<Seeds>(seedsFile);
            return seeds;
        }
    }
}
