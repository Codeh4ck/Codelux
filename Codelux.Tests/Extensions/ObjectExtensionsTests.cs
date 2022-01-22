using System;
using Codelux.Common.Extensions;
using NUnit.Framework;

namespace Codelux.Tests.Extensions
{
    [TestFixture]
    public class ObjectExtensionsTests
    {
        [Test]
        public void GivenObjectThatIsNullWhenIGuardThenArgumentNullExceptionIsThrownWithCorrectMessage()
        {
            object obj = null;

            ArgumentNullException exception = 
                Assert.Throws<ArgumentNullException>(delegate() { obj.Guard(nameof(obj)); });

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Message.Contains(nameof(obj)));
        }

        [Test]
        public void GivenObjectThatIsNotNullWhenIGuardNoExceptionIsThrown()
        {
            object obj = new();
            Assert.DoesNotThrow(delegate () { obj.Guard(nameof(obj)); });
        }
    }
}
