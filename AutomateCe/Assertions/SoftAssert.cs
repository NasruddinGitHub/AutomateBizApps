using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateCe.Assertions
{
    public class SoftAssert
    {
        private readonly List<string> _assertionFailures = new List<string>();

        public void AreEqual<T>(T expected, T actual, string message = "")
        {
            try
            {
                Assert.That(actual, Is.EqualTo(expected), message);
            }
            catch (AssertionException ex)
            {
                _assertionFailures.Add($"{message} - {ex.Message}");
            }
        }

        public void IsTrue(bool condition, string message = "")
        {
            try
            {
                Assert.That(condition, Is.True, message);
            }
            catch (AssertionException ex)
            {
                _assertionFailures.Add($"{message} - {ex.Message}");
            }
        }

        public void IsFalse(bool condition, string message = "")
        {
            try
            {
                Assert.That(condition, Is.False, message);
            }
            catch (AssertionException ex)
            {
                _assertionFailures.Add($"{message} - {ex.Message}");
            }
        }

        public void AssertAll()
        {
            if (_assertionFailures.Count > 0)
            {
                var failureMessage = string.Join(Environment.NewLine, _assertionFailures);
                throw new AssertionException("Soft assertion failures:\n" + failureMessage);
            }
        }
    }
}
