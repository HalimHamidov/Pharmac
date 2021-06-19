using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Selenium
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CoveredSteps : NUnitAttribute, IApplyToTest
    {
        private int[] Steps { get; }
        public CoveredSteps(params int[] steps)
        {
            this.Steps = steps;
        }
        public void ApplyToTest(Test test)
        {
            test.Properties.Add("CoveredSteps", string.Join(", ", Steps));
        }
    }
}
