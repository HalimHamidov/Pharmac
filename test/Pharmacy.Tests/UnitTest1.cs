using NUnit.Framework;
using Pharmacy.Models;

namespace Pharmacy.TestProjects
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
        [Test]
        public void ShouldExceedMaxLengthName()
        {
            //Arrange
            var testAgent = new Agent
            {
                // ManagerId = 1,
                // CityId = 1,
                // Supplier = true,
                // Client = true,
                Name = "Tree"
            };
            //Act
            testAgent.Name =
                "ItisalongestablishedfactthatareaderwillbedistractedbythereadablecontentofapagewhenlookingatitslayoutThepointofusingLoremIpsumisthatithasamoreorlessnormaldistributionoflettersasopposedtousingContentherecontentheremakingitlooklikereadableEnglish";
            var result = testAgent.Name;
            
            //Assert
            Assert.That(result.Length,Is.Not.InRange(0,150));
            // Assert.That(result.Length,Is.Not.InRange(0,150)); //gives error
        }
    }
}