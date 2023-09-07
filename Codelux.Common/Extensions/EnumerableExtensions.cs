using System;
using System.Collections.Generic;
using System.Linq;

namespace Codelux.Common.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> enumerable) => enumerable ?? Enumerable.Empty<T>();

    public static IEnumerable<T> EnsureList<T>(this IEnumerable<T> enumerable)
    {
        return enumerable switch
        {
            null => new List<T>(),
            IList<T> list => list,
            _ => enumerable.ToList()
        };
    }

    public static IEnumerable<T> CreateSingleton<T>(T value)
    {
        yield return value;
    }

    public static IEnumerable<T> CreateEnumerable<T>(params T[] values) => values;

    public static IEnumerable<T> Remove<T>(this IEnumerable<T> enumerable, T value)
    {
        if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
        return enumerable.Remove(x => x.Equals(value));
    }
    
    public static IEnumerable<T> Remove<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
    {
        if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
        return enumerable.Where(x => !predicate(x));
    }
    
    public static IEnumerable<T> Replace<T>(this IEnumerable<T> enumerable, T value, Func<T, bool> predicate)
    {
        if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
        return enumerable.Select(x => predicate(x) ? value : x);
    }
    
    public static IEnumerable<T> Replace<T>(this IEnumerable<T> enumerable, T currentValue, T newValue)
    {
        if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
        return enumerable.Replace(newValue, x => x.Equals(currentValue));
    }
}