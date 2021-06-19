using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;

namespace Tests.Selenium
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class PropertyTestId : NUnitAttribute, IApplyToTest
    {
        private string TestId { get; }
        public PropertyTestId(string testId)
        {
            TestId = testId;
        }

        public string GetTestId()
        {
            return TestId;
        }

        public void ApplyToTest(Test test)
        {
            test.Properties.Add("TestKey", this.TestId);
        }
    }
}
