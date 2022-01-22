using System;
using Codelux.Common.Extensions;
using NUnit.Framework;

namespace Codelux.Tests.Extensions
{
    [TestFixture]
    public class StructExtensionsTests
    {
        [Test]
        public void GivenValueThatIsStructWhenIBoxThenValueIsBoxedAndConvertedToObject()
        {
            int structValue = 10;
            object boxed = structValue.Box();

            Assert.NotNull(boxed);
            Assert.AreEqual(structValue, (int)boxed);
        }

        [Test]
        public void GivenValueThatIsBoxedAndIsOfTypeObjectWhenIUnboxThenValueIsUnboxedAndConvertedToStruct()
        {
            int structValue = 20;
            object boxed = structValue.Box();

            Assert.NotNull(boxed);
            Assert.AreEqual(structValue, (int)boxed);

            int unboxed = boxed.Unbox<int>();

            Assert.AreEqual(structValue, unboxed);
        }

        [Test]
        public void GivenValueThatIsBoxedAndIsOfTypeObjectWhenIUnboxToDifferentStructThenItThrows()
        {
            int structValue = 1;
            object boxed = structValue.Box();

            Assert.NotNull(boxed);
            Assert.AreEqual(structValue, (int)boxed);

            Assert.Throws<InvalidCastException>(() => boxed.Unbox<bool>());
        }
    }
}
