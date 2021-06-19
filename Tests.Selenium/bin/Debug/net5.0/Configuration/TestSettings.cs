using System.IO;

namespace Tests.Selenium
{
    public class TestSettings
    {

        public static string HostPrefix { get; set; }

        //указан адрес хаба для локальной машины 
        public static string UrlHub { get; set; } = "http://192.168.181.145:54089/index.html#";
        public static Browser Browser { get; set; } = Browser.Chrome;

        //Название среды
        public static string NameOfEnvironment { get; set; } = "staging";

        public static string TestDownloadDirectory { get; set; } = $"{Directory.GetCurrentDirectory()}\\Tests\\Downloads";
        public static string WholeTestDirectoryName { get; set; } = $"{Directory.GetCurrentDirectory()}\\Tests\\Results";
        public static string TestFilesDirectory { get; set; } = "TestFiles";
        public static int WaitIntervalSeconds { get; set; } = 30;
        public static bool IsHeadless { get; set; } = false;
        public static bool IsGrid { get; set; } = false;
        public static bool NeedGenerateSeeds { get; set; } = false;

    }
}
