using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Codelux.Common.Utilities;

namespace Codelux.Tests.Utilities;

[TestFixture]
public class EnsureTests
{
    [Test]
    public static void GivenBoolIsTrueWhenIEnsureItIsTrueThenNoExceptionIsThrown()
    {
        bool value = true;
        Assert.DoesNotThrow(() => Ensure.ItIsTrue(value));
    }
    
    [Test]
    public static void GivenBoolIsFalseWhenIEnsureItIsTrueThenExceptionIsThrownAndValueNameIsCaptured()
    {
        bool value = false;
        ArgumentException ex = Assert.Throws<ArgumentException>(() => Ensure.ItIsTrue(value));
        
        Assert.IsNotNull(ex);
        Assert.AreEqual($"(Parameter '{nameof(value)}')", ex.Message.Trim());
    }
    
    [Test]
    public static void GivenBoolIsFalseWhenIEnsureItIsFalseThenNoExceptionIsThrown()
    {
        bool value = false;
        Assert.DoesNotThrow(() => Ensure.ItIsFalse(value));
    }
    
    [Test]
    public static void GivenBoolIsTrueWhenIEnsureItIsFalseThenExceptionIsThrownAndValueNameIsCaptured()
    {
        bool value = true;
        ArgumentException ex = Assert.Throws<ArgumentException>(() => Ensure.ItIsFalse(value));
        
        Assert.IsNotNull(ex);
        Assert.AreEqual($"(Parameter '{nameof(value)}')", ex.Message.Trim());
    }

    [Test]
    public static void GivenNumericValueWhenIEnsureItIsGreaterThanAndItIsGreaterThanThenNoExceptionIsThrown()
    {
        double doubleValue = 10;
        float floatValue = 10f;
        decimal decimalValue = 10.0m;
        short shortValue = 10;
        int intValue = 10;
        long longValue = 10;        
        ushort ushortValue = 10;
        uint uintValue = 10;
        ulong ulongValue = 10;
        byte byteValue = 10;
        sbyte sbyteValue = 10;
        
        Assert.DoesNotThrow(() => Ensure.ItIsGreaterThan(doubleValue, 5));
        Assert.DoesNotThrow(() => Ensure.ItIsGreaterThan(floatValue, 5));
        Assert.DoesNotThrow(() => Ensure.ItIsGreaterThan(decimalValue, 5));
        Assert.DoesNotThrow(() => Ensure.ItIsGreaterThan(shortValue, 5));
        Assert.DoesNotThrow(() => Ensure.ItIsGreaterThan(intValue, 5));
        Assert.DoesNotThrow(() => Ensure.ItIsGreaterThan(longValue, 5));
        Assert.DoesNotThrow(() => Ensure.ItIsGreaterThan(ushortValue, 5));
        Assert.DoesNotThrow(() => Ensure.ItIsGreaterThan<uint>(uintValue, 5));
        Assert.DoesNotThrow(() => Ensure.ItIsGreaterThan<ulong>(ulongValue, 5));
        Assert.DoesNotThrow(() => Ensure.ItIsGreaterThan(byteValue, 5));
        Assert.DoesNotThrow(() => Ensure.ItIsGreaterThan(sbyteValue, 5));
    }
    
    [Test]
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public static void GivenNumericValueWhenIEnsureItIsGreaterThanAndItIsLowerOrEqualThanThenExceptionIsThrownAndValueNameIsCaptured()
    {
        double doubleValue = 10;
        float floatValue = 10f;
        decimal decimalValue = 10.0m;
        short shortValue = 10;
        int intValue = 10;
        long longValue = 10;        
        ushort ushortValue = 10;
        uint uintValue = 10;
        ulong ulongValue = 10;
        byte byteValue = 10;
        sbyte sbyteValue = 10;

        Dictionary<string, ArgumentException> lowerThanActualTests = new()
        {
            { doubleValue.GetType()?.FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan(doubleValue, 15)) },
            { floatValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan(floatValue, 15)) },
            { decimalValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan(decimalValue, 15)) },
            { shortValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan(shortValue, 15)) },
            { intValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan(intValue, 15)) },
            { longValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan(longValue, 15)) },
            { ushortValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan(ushortValue, 15)) },
            { uintValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan<uint>(uintValue, 15)) },
            { ulongValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan<ulong>(ulongValue, 15)) },
            { byteValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan(byteValue, 15)) },
            { sbyteValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan(sbyteValue, 15)) }
        };
        
        Dictionary<string, ArgumentException> equalToActualTests = new()
        {
            { doubleValue.GetType()?.FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan(doubleValue, 10)) },
            { floatValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan(floatValue, 10)) },
            { decimalValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan(decimalValue, 10)) },
            { shortValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan(shortValue, 10)) },
            { intValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan(intValue, 10)) },
            { longValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan(longValue, 10)) },
            { ushortValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan(ushortValue, 10)) },
            { uintValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan<uint>(uintValue, 10)) },
            { ulongValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan<ulong>(ulongValue, 10)) },
            { byteValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan(byteValue, 10)) },
            { sbyteValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsGreaterThan(sbyteValue, 10)) }
        };

        foreach (var kvp in lowerThanActualTests)
        {
            Assert.IsNotNull(kvp.Value);
            string variableName = $"{GetTypeFriendlyName(kvp.Key)}Value";
            Assert.That(kvp.Value.Message.Contains(variableName));
        }
        
        foreach (var kvp in equalToActualTests)
        {
            Assert.IsNotNull(kvp.Value);
            string variableName = $"{GetTypeFriendlyName(kvp.Key)}Value";
            Assert.That(kvp.Value.Message.Contains(variableName));
        }
    }
    
    [Test]
    public static void GivenNumericValueWhenIEnsureItIsLowerThanAndItIsLowerThanThanThenNoExceptionIsThrown()
    {
        double doubleValue = 10;
        float floatValue = 10f;
        decimal decimalValue = 10.0m;
        short shortValue = 10;
        int intValue = 10;
        long longValue = 10;        
        ushort ushortValue = 10;
        uint uintValue = 10;
        ulong ulongValue = 10;
        byte byteValue = 10;
        sbyte sbyteValue = 10;
        
        Assert.DoesNotThrow(() => Ensure.ItIsLowerThan(doubleValue, 15));
        Assert.DoesNotThrow(() => Ensure.ItIsLowerThan(floatValue, 15));
        Assert.DoesNotThrow(() => Ensure.ItIsLowerThan(decimalValue, 15));
        Assert.DoesNotThrow(() => Ensure.ItIsLowerThan(shortValue, 15));
        Assert.DoesNotThrow(() => Ensure.ItIsLowerThan(intValue, 15));
        Assert.DoesNotThrow(() => Ensure.ItIsLowerThan(longValue, 15));
        Assert.DoesNotThrow(() => Ensure.ItIsLowerThan(ushortValue, 15));
        Assert.DoesNotThrow(() => Ensure.ItIsLowerThan<uint>(uintValue, 15));
        Assert.DoesNotThrow(() => Ensure.ItIsLowerThan<ulong>(ulongValue, 15));
        Assert.DoesNotThrow(() => Ensure.ItIsLowerThan(byteValue, 15));
        Assert.DoesNotThrow(() => Ensure.ItIsLowerThan(sbyteValue, 15));
    }
    
    [Test]
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public static void GivenNumericValueWhenIEnsureItIsLowerThanAndItIsGreaterOrEqualThanThenExceptionIsThrownAndValueNameIsCaptured()
    {
        double doubleValue = 10;
        float floatValue = 10f;
        decimal decimalValue = 10.0m;
        short shortValue = 10;
        int intValue = 10;
        long longValue = 10;        
        ushort ushortValue = 10;
        uint uintValue = 10;
        ulong ulongValue = 10;
        byte byteValue = 10;
        sbyte sbyteValue = 10;

        Dictionary<string, ArgumentException> greaterThanActualTests = new()
        {
            { doubleValue.GetType()?.FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan(doubleValue, 5)) },
            { floatValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan(floatValue, 5)) },
            { decimalValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan(decimalValue, 5)) },
            { shortValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan(shortValue, 5)) },
            { intValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan(intValue, 5)) },
            { longValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan(longValue, 5)) },
            { ushortValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan(ushortValue, 5)) },
            { uintValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan<uint>(uintValue, 5)) },
            { ulongValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan<ulong>(ulongValue, 5)) },
            { byteValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan(byteValue, 5)) },
            { sbyteValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan(sbyteValue, 5)) }
        };
        
        Dictionary<string, ArgumentException> equalToActualTests = new()
        {
            { doubleValue.GetType()?.FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan(doubleValue, 10)) },
            { floatValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan(floatValue, 10)) },
            { decimalValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan(decimalValue, 10)) },
            { shortValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan(shortValue, 10)) },
            { intValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan(intValue, 10)) },
            { longValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan(longValue, 10)) },
            { ushortValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan(ushortValue, 10)) },
            { uintValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan<uint>(uintValue, 10)) },
            { ulongValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan<ulong>(ulongValue, 10)) },
            { byteValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan(byteValue, 10)) },
            { sbyteValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsLowerThan(sbyteValue, 10)) }
        };

        foreach (var kvp in greaterThanActualTests)
        {
            Assert.IsNotNull(kvp.Value);
            string variableName = $"{GetTypeFriendlyName(kvp.Key)}Value";
            Assert.That(kvp.Value.Message.Contains(variableName));
        }
        
        foreach (var kvp in equalToActualTests)
        {
            Assert.IsNotNull(kvp.Value);
            string variableName = $"{GetTypeFriendlyName(kvp.Key)}Value";
            Assert.That(kvp.Value.Message.Contains(variableName));
        }
    }
    
    [Test]
    public static void GivenNumericValueWhenIEnsureItIsEqualToAndItIsEqualToThenNoExceptionIsThrown()
    {
        double doubleValue = 10;
        float floatValue = 10f;
        decimal decimalValue = 10.0m;
        short shortValue = 10;
        int intValue = 10;
        long longValue = 10;        
        ushort ushortValue = 10;
        uint uintValue = 10;
        ulong ulongValue = 10;
        byte byteValue = 10;
        sbyte sbyteValue = 10;
        
        Assert.DoesNotThrow(() => Ensure.ItIsEqualTo(doubleValue, 10));
        Assert.DoesNotThrow(() => Ensure.ItIsEqualTo(floatValue, 10));
        Assert.DoesNotThrow(() => Ensure.ItIsEqualTo(decimalValue, 10));
        Assert.DoesNotThrow(() => Ensure.ItIsEqualTo(shortValue, 10));
        Assert.DoesNotThrow(() => Ensure.ItIsEqualTo(intValue, 10));
        Assert.DoesNotThrow(() => Ensure.ItIsEqualTo(longValue, 10));
        Assert.DoesNotThrow(() => Ensure.ItIsEqualTo(ushortValue, 10));
        Assert.DoesNotThrow(() => Ensure.ItIsEqualTo<uint>(uintValue, 10));
        Assert.DoesNotThrow(() => Ensure.ItIsEqualTo<ulong>(ulongValue, 10));
        Assert.DoesNotThrow(() => Ensure.ItIsEqualTo(byteValue, 10));
        Assert.DoesNotThrow(() => Ensure.ItIsEqualTo(sbyteValue, 10));
    }
    
    [Test]
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public static void GivenNumericValueWhenIEnsureItIsEqualToAndItIsNotEqualThanThenExceptionIsThrownAndValueNameIsCaptured()
    {
        double doubleValue = 10;
        float floatValue = 10f;
        decimal decimalValue = 10.0m;
        short shortValue = 10;
        int intValue = 10;
        long longValue = 10;        
        ushort ushortValue = 10;
        uint uintValue = 10;
        ulong ulongValue = 10;
        byte byteValue = 10;
        sbyte sbyteValue = 10;

        Dictionary<string, ArgumentException> notEqualToTests = new()
        {
            { doubleValue.GetType()?.FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsEqualTo(doubleValue, 5)) },
            { floatValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsEqualTo(floatValue, 3)) },
            { decimalValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsEqualTo(decimalValue, 15)) },
            { shortValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsEqualTo(shortValue, 4)) },
            { intValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsEqualTo(intValue, 6)) },
            { longValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsEqualTo(longValue, 5)) },
            { ushortValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsEqualTo(ushortValue, 5)) },
            { uintValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsEqualTo<uint>(uintValue, 321)) },
            { ulongValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsEqualTo<ulong>(ulongValue, 32)) },
            { byteValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsEqualTo(byteValue, 244)) },
            { sbyteValue.GetType().FullName, Assert.Throws<ArgumentException>(() => Ensure.ItIsEqualTo(sbyteValue, 53)) }
        };

        foreach (var kvp in notEqualToTests)
        {
            Assert.IsNotNull(kvp.Value);
            string variableName = $"{GetTypeFriendlyName(kvp.Key)}Value";
            Assert.That(kvp.Value.Message.Contains(variableName));
        }
    }

    [Test]
    public void GivenReferenceTypeWhenIEnsureReferenceIsEqualToAndReferenceIsEqualThenNoExceptionIsThrown()
    {
        TestObject obj = new TestObject()
        {
            NumberProperty = 15,
            StringProperty = "A test object"
        };

        TestObject anotherObj = obj;
        
        Assert.DoesNotThrow(() => Ensure.ItIsReferenceEqualTo(anotherObj, obj));
    }
    
    [Test]
    public void GivenReferenceTypeWhenIEnsureReferenceIsEqualToAndReferenceIsNotEqualThenExceptionIsThrownAndValueNameIsCaptured()
    {
        TestObject obj = new TestObject()
        {
            NumberProperty = 15,
            StringProperty = "A test object"
        };

        TestObject anotherObj = new TestObject()
        {
            NumberProperty = 20,
            StringProperty = "Another test object"
        };
        
        var ex = Assert.Throws<ArgumentException>(() => Ensure.ItIsReferenceEqualTo(anotherObj, obj));
        
        Assert.IsNotNull(ex);
        Assert.AreEqual($"(Parameter '{nameof(anotherObj)}')", ex.Message.Trim());
    }

    [Ignore("Not a test")]
    private static string GetTypeFriendlyName(string userType)
    {
        return userType switch
        {
            "System.String" => "string",
            "System.SByte" => "sbyte",
            "System.Byte" => "byte",
            "System.Int16" => "short",
            "System.UInt16" => "ushort",
            "System.Int32" => "int",
            "System.UInt32" => "uint",
            "System.Int64" => "long",
            "System.UInt64" => "ulong",
            "System.Char" => "char",
            "System.Single" => "float",
            "System.Double" => "double",
            "System.Boolean" => "bool",
            "System.Decimal" => "decimal",
            "System.Void" => "void",
            "System.Object" => "object",
            _ => userType
        };
    }

    [Test]
    public void GivenBoolDelegateWhenIEnsureItRequiresAndDelegateReturnsTrueThenNoExceptionIsThrown()
    {
        bool CheckCondition()
        {
            return true;
        }
        
        Assert.DoesNotThrow(() => Ensure.ItRequires(CheckCondition));
    }

    [Test]
    public void GivenBoolDelegateWhenIEnsureItRequiresAndDelegateReturnsFalseThenNoExceptionIsThrown()
    {
        bool CheckCondition()
        {
            return false;
        }

        var ex = Assert.Throws<ArgumentException>(() => Ensure.ItRequires(CheckCondition));
        Assert.AreEqual($"(Parameter '{nameof(CheckCondition)}')", ex.Message.Trim());
    }
    
    [Test]
    public void GivenTargetTypeAndExpressionWhenIEnsureItRequiresAndExpressionIsSatisfiedThenNoExceptionIsThrown()
    {
        string propertyStringValue = "Test Property";
        int propertyIntValue = 24;

        TestObject target = new()
        {
            StringProperty = propertyStringValue,
            NumberProperty = propertyIntValue
        };
        
        Assert.DoesNotThrow(() => Ensure.ItRequires(target, x => x.StringProperty == propertyStringValue && x.NumberProperty == propertyIntValue));
    }

    [Test]
    public void GivenTargetTypeAndExpressionWhenIEnsureItRequiresAndExpressionIsNotSatisfiedThenExceptionIsThrownAndValueIsCaptured()
    {
        string propertyStringValue = "Test Property";
        int propertyIntValue = 24;

        TestObject target = new()
        {
            StringProperty = propertyStringValue,
            NumberProperty = propertyIntValue
        };

        Expression<Func<TestObject, bool>> expression = x =>
            x.StringProperty == "Not the same value" && x.NumberProperty == 53;
        
        var ex = Assert.Throws<ArgumentException>(() => Ensure.ItRequires(target, expression));
        Assert.AreEqual($"(Parameter '{nameof(expression)}')", ex.Message.Trim());
    }

    private class TestObject
    {
        public string StringProperty { get; set; }
        public int NumberProperty { get; set; }
    }
}