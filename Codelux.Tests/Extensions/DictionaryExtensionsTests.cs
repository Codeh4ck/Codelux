using System.Collections.Generic;
using Codelux.Common.Extensions;
using NUnit.Framework;

namespace Codelux.Tests.Extensions
{
    [TestFixture]
    public class DictionaryExtensionsTests
    {
        [Test]
        public void GivenDictionaryWhenISetItemAndItDoesNotExistThenItemIsAdded()
        {
            Dictionary<string, bool> dictionary = new();
            dictionary.Set("Item", true);

            Assert.AreEqual(1, dictionary.Count);
            Assert.AreEqual(true, dictionary["Item"]);
        }

        [Test]
        public void GivenDictionaryWhenISetItemAndItExistsThenValueIsReplaced()
        {
            Dictionary<string, bool> dictionary = new();
            dictionary.Set("Item", true);

            Assert.AreEqual(1, dictionary.Count);
            Assert.AreEqual(true, dictionary["Item"]);

            dictionary.Set("Item", false);

            Assert.AreEqual(1, dictionary.Count);
            Assert.AreEqual(false, dictionary["Item"]);
        }

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
