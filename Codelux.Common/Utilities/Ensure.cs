using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Codelux.Common.Utilities;

public static class Ensure
{
    public static void ItIsTrue(bool expressionOrValue, string message = "", [CallerArgumentExpression("expressionOrValue")] string paramName = "")
    {
        if (!expressionOrValue) throw new ArgumentException(message, paramName);
    }

    public static void ItIsFalse(bool expressionOrValue, string message = "", [CallerArgumentExpression("expressionOrValue")] string paramName = "")
    {
        if (expressionOrValue) throw new ArgumentException(message, paramName);
    }

    public static void ItIsGreaterThan<T>(T expressionOrValue, T target, string message = "", [CallerArgumentExpression("expressionOrValue")] string paramName = "")
        where T : struct, IComparable<T>
    {
        if (expressionOrValue.CompareTo(target) <= 0) throw new ArgumentException(message, paramName);
    }

    public static void ItIsLowerThan<T>(T expressionOrValue, T target, string message = "", [CallerArgumentExpression("expressionOrValue")] string paramName = "")
        where T : struct, IComparable<T>
    {
        if (expressionOrValue.CompareTo(target) >= 0) throw new ArgumentException(message, paramName);
    }

    public static void ItIsEqualTo<T>(T expressionOrValue, T target, string message = "", [CallerArgumentExpression("expressionOrValue")] string paramName = "")
        where T : struct, IComparable<T>
    {
        if (expressionOrValue.CompareTo(target) != 0) throw new ArgumentException(message, paramName);
    }

    public static void ItIsReferenceEqualTo<T>(T expressionOrValue, T target, string message = "", [CallerArgumentExpression("expressionOrValue")]
        string paramName = "") where T : class
    {
        if (expressionOrValue != target) throw new ArgumentException(message, paramName);
    }

    public static void ItRequires(Func<bool> func, string message = "", [CallerArgumentExpression("func")] string paramName = "")
    {
        if (!func()) throw new ArgumentException(message, paramName);
    }
    
    public static void ItRequires<T>(T target, Expression<Func<T,bool>> expression, string message = "", [CallerArgumentExpression("expression")] string paramName = "")
    {
        var func = expression.Compile();
        if (!func(target)) throw new ArgumentException(message, paramName);
    }
}