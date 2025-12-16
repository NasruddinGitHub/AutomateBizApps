using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateCe.Assertions
{
    public static class AssertHelper
    {
        public static void AreEqual<T>(T expected, T actual, string message = "")
        {
            Assert.That(actual, Is.EqualTo(expected), message);
        }

        public static void IsTrue(bool condition, string message = "")
        {
            Assert.That(condition, Is.True, message);
        }

        public static void IsFalse(bool condition, string message = "")
        {
            Assert.That(condition, Is.False, message);
        }

    }
}
