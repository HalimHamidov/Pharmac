using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace Tests.Selenium
{
    /// <summary>
    /// Represents Test with multiply steps or higt-cohesive tests, which must run in some order. Final state of test is the start state of next test
    /// If one of the test - steps failed, then all sequence of tests terminates. Also represents long scenario, so we Start and Release WebDriver only at 
    /// the start and the end of the whole sequence
    /// Pros: Divide test for short steps, no overhead for starting WebDriver before each test, and releasing it after each one. Allow reusing result state of 
    ///       previous test.
    /// Cons: Steps are cohesive, must run in order, cannot run parallely. If test failed, so terminates the whole chain and other cases in chain 
    ///       will not be able to be checked
    /// </summary>
    public class BaseTestSequential : BaseTest
    {
        public IWebDriver Driver;

        /// <summary>
        /// Indicates if the sequence in valid state. If one test fail it will set to False and next tests will know that sequence is not valid
        /// </summary>
        protected bool IsValidState { get; set; }

        protected bool IsDeleteDataForTestFail { get; set; } = false;
        /// <summary>
        /// Start WebDriver only before First Test in sequence
        /// </summary>
        [OneTimeSetUp]
        public void Start()
        {
            InitializeData();
            // if (TestSettings.NeedGenerateSeeds)
            //     DataAutoGenerator.GeneratingService.GenerateSeeds(Seeds);
            Driver = StartDriver();
            IsValidState = true;
        }

        /// <summary>
        /// Release WebDriver only after last test in sequence
        /// </summary>
        [OneTimeTearDown]
        // public void Stop()
        // {
        //     Driver.Quit();
        //     RemoveDownloadDirectory();
        //     if (IsValidState || IsDeleteDataForTestFail)
        //         DataAutoGenerator.DeleteGeneratingObjects();
        // }

        /// <summary>
        /// Check if test failed, take screenshot if so and invalidate the sequence if so.
        /// </summary>
        [TearDown]
        public void DoAfterEach()
        {
            if (IsValidState && TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                IsValidState = false;
                TakeFailedTestScreenshot(Driver);
            }
        }

        /// <summary>
        /// If sequence is invalidated, so terminate each next with Failed State
        /// </summary>
        [SetUp]
        public void DoBeforeEach()
        {
            // If not, we emit special failure message to not confuse this failure with the real reason of sequence failing
            if (!IsValidState)
                Assert.Inconclusive("SequentialTestCaseBroken");
        }
    }
}
