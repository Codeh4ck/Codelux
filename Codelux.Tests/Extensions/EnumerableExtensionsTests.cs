using System.Collections.Generic;
using System.Linq;
using Codelux.Common.Extensions;
using NUnit.Framework;

// ReSharper disable PossibleMultipleEnumeration

namespace Codelux.Tests.Extensions;

[TestFixture]
public class EnumerableExtensionsTests
{
    [Test]
    public void GivenIEnumerableListWhenIEnsureListThenListIsReturned()
    {
        IEnumerable<string> list = new List<string>() { "Hello", "World" };

        var result = list.EnsureList();

        Assert.IsNotNull(result);
        Assert.IsNotEmpty(result);

        Assert.AreEqual(typeof(List<string>), result.GetType());
        Assert.AreEqual(typeof(List<string>), result.GetType());

        foreach (string listItem in list)
            Assert.IsTrue(result.Contains(listItem));
    }

    [Test]
    public void GivenIEnumerableNullWhenIEmptyIfNullThenEmptyEnumerableIsReturned()
    {
        IEnumerable<string> list = null;

        var result = list.EmptyIfNull();

        Assert.IsNotNull(result);
        Assert.IsEmpty(result);
    }

    [Test]
    public void GivenSingleObjectWhenIConvertToEnumerableSingletonThenEnumerableIsReturnedAndContainsOnlyOneValue()
    {
        var testString = "This is a test string";

        var result = EnumerableExtensions.CreateSingleton(testString);

        Assert.IsNotNull(result);
        Assert.IsNotEmpty(result);

        Assert.AreEqual(1, result.Count());
        Assert.AreEqual(testString, result.ToList()[0]);
    }

    [Test]
    public void GivenMultipleValuesWhenICreateEnumerableThenEnumerableIsReturnedAndValuesAreCorrect()
    {
        var testString1 = "This is a test string";
        var testString2 = "This is another test string";
        var testString3 = "This is yet another test string";

        var result = EnumerableExtensions.CreateEnumerable(testString1, testString2, testString3);

        Assert.IsNotNull(result);
        Assert.IsNotEmpty(result);

        Assert.AreEqual(3, result.Count());
        Assert.AreEqual(testString1, result.ToList()[0]);
        Assert.AreEqual(testString2, result.ToList()[1]);
        Assert.AreEqual(testString3, result.ToList()[2]);
    }

    [Test]
    public void GivenExistingEnumerableWhenIRemoveAValueThenEnumerableIsReturnedAndValueIsRemoved()
    {
        var testString1 = "This is a test string";
        var testString2 = "REM This is another test string";
        var testString3 = "REM This is yet another test string";

        var result = EnumerableExtensions.CreateEnumerable(testString1, testString2, testString3);

        Assert.IsNotNull(result);
        Assert.IsNotEmpty(result);

        Assert.AreEqual(3, result.Count());
        
        result = result.Remove(x => x.StartsWith("REM"));
        Assert.AreEqual(1, result.Count());
    }
    
    [Test]
    public void GivenExistingEnumerableWhenIReplaceAValueThenEnumerableIsReturnedAndValueIsReplaced()
    {
        var testString1 = "This is a test string";
        var testString2 = "This is another test string";
        var testStringToReplace = "This is yet another test string";

        var result = EnumerableExtensions.CreateEnumerable(testString1, testString2, testStringToReplace);

        Assert.IsNotNull(result);
        Assert.IsNotEmpty(result);

        Assert.AreEqual(3, result.Count());

        result = result.Replace(testStringToReplace, "Replaced");
        Assert.AreEqual(3, result.Count());
        
        Assert.AreEqual("Replaced", result.ToList()[2]);
    }
    
    [Test]
    public void GivenExistingEnumerableWhenIReplaceValuesThenEnumerableIsReturnedAndValuesAreReplaced()
    {
        var testString1 = "This is a test string";
        var testString2 = "REPLACE This is another test string";
        var testStringToReplace = "REPLACE This is yet another test string";

        var result = EnumerableExtensions.CreateEnumerable(testString1, testString2, testStringToReplace);

        Assert.IsNotNull(result);
        Assert.IsNotEmpty(result);

        Assert.AreEqual(3, result.Count());

        result = result.Replace("Replaced", x => x.StartsWith("REPLACE"));
        Assert.AreEqual(3, result.Count());
        
        Assert.AreEqual("Replaced", result.ToList()[1]);
        Assert.AreEqual("Replaced", result.ToList()[2]);
    }
}