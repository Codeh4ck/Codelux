using System;
using System.Collections.Generic;
using Codelux.Common.Extensions;
using NUnit.Framework;

namespace Codelux.Tests.Extensions
{
    [TestFixture]
    public class ExceptionExtensionsTests
    {
        [Test]
        public void GivenExceptionWithInnerExceptionsWhenIGetAllMessagesThenCorrectMessagesAreReturned()
        {
            Exception innermostException = new("Innermost level exception");
            Exception thirdLevelException = new("Third level exception", innermostException);
            Exception secondLevelException = new("Second level exception", thirdLevelException);
            Exception topException = new("Top exception message", secondLevelException);

            List<string> exceptionMessages = topException.GetAllExceptionMessages();

            Assert.AreEqual(4, exceptionMessages.Count);
            Assert.AreEqual(exceptionMessages[0], topException.Message);
            Assert.AreEqual(exceptionMessages[1], secondLevelException.Message);
            Assert.AreEqual(exceptionMessages[2], thirdLevelException.Message);
            Assert.AreEqual(exceptionMessages[3], innermostException.Message);
        }

        [Test]
        public void GivenExceptionWithoutInnerExceptionsWhenIGetAllMessagesThenOneExceptionMessageIsReturned()
        {
            Exception exception = new("Exception message");
            List<string> exceptionMessages = exception.GetAllExceptionMessages();

            Assert.AreEqual(1, exceptionMessages.Count);
            Assert.AreEqual(exceptionMessages[0], exception.Message);
        }

        [Test]
        public void GivenExceptionIsNullWhenIGetAllMessagesThenNullIsReturned()
        {
            Exception exception = null;
            List<string> exceptionMessages = exception.GetAllExceptionMessages();

            Assert.IsNull(exceptionMessages);
        }

        [Test]
        public void GivenExceptionWithInnerExceptionsWhenIGetInnerExceptionsThenInnerExceptionsAreReturned()
        {
            Exception innermostException = new("Innermost level exception");
            Exception thirdLevelException = new("Third level exception", innermostException);
            Exception secondLevelException = new("Second level exception", thirdLevelException);
            Exception topException = new("Top exception message", secondLevelException);

            List<Exception> innerExceptions = topException.GetAllInnerExceptions();

            Assert.AreEqual(3, innerExceptions.Count);

            Assert.AreEqual(innerExceptions[0], secondLevelException);
            Assert.AreEqual(innerExceptions[1], thirdLevelException);
            Assert.AreEqual(innerExceptions[2], innermostException);
        }
    }
}
