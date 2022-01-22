using Codelux.Common.Extensions;
using NUnit.Framework;

namespace Codelux.Tests.Extensions
{
    [TestFixture]
    public class EnumExtensionsTests
    {
        enum TestEnum
        {
            // Using full namespace due to collision with NUnit's DescriptionAttribute
            [System.ComponentModel.Description("First enum value")]
            FirstValue = 0,
            [System.ComponentModel.Description("Second enum value")]
            SecondValue = 1
        }


        [Test]
        public void GivenEnumWithDescriptionAttributesWhenIGetDescriptionThenDescriptionIsReturned()
        {
            TestEnum firstValue = TestEnum.FirstValue;
            TestEnum secondValue = TestEnum.SecondValue;

            string firstDescription = firstValue.GetDescription();
            string secondDescription = secondValue.GetDescription();

            Assert.AreEqual("First enum value", firstDescription);
            Assert.AreEqual("Second enum value", secondDescription);
        }
    }
}
