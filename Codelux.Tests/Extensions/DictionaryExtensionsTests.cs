using System.Collections.Generic;
using Codelux.Common.Extensions;
using NUnit.Framework;

namespace Codelux.Tests.Extensions
{
    [TestFixture]
    public class DictionaryExtensionsTests
    {
        [Test]
        public void GivenDictionaryWhenIAddARangeOfItemsThenItemsAreAdded()
        {
            List<KeyValuePair<string, int>> list = new()
            {
                new("One", 1),
                new("Two", 2),
                new("Three", 3)
            };

            Dictionary<string, int> dictionary = new();
            dictionary.AddRange(list);

            Assert.AreEqual(list.Count, dictionary.Count);

            foreach (KeyValuePair<string, int> pair in dictionary)
                Assert.IsTrue(list.Contains(pair));
        }
    }
}