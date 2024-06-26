﻿using System;

namespace Codelux.Common.OptionType;

public readonly struct Option<T> : IEquatable<Option<T>>
{
    private static readonly Option<T> NoneOption = new(default, false);

    private readonly T _value;
    private readonly bool _hasValue;

    public T Value
    {
        get
        {
            if (!HasValue) throw new InvalidOperationException("Option has no value");
            return _value;
        }
    }

    public T ValueOr(T defaultValue) => _hasValue ? _value : defaultValue;

    public bool HasValue => _hasValue;
    public bool IsSome => _hasValue;
    public bool IsNone => !_hasValue;

    private Option(T value, bool hasValue)
    {
        if (hasValue && value == null)
            throw new ArgumentNullException(nameof(value), "Option.Some cannot be assigned a null value");

        _value = value;
        _hasValue = hasValue;
    }

    internal static Option<T> Some(T instance) => new(instance, true);
    internal static Option<T> None() => NoneOption;

    public static implicit operator Option<T>(T value) => Option.From(value);

    public bool Equals(Option<T> other)
    {
        return !HasValue && !other.HasValue ||
               HasValue && other.HasValue && Value.Equals(other.Value);
    }

    // ReSharper disable once PossibleNullReferenceException
    public static bool operator ==(Option<T> a, Option<T> b) => a.Equals(b);
    public static bool operator !=(Option<T> a, Option<T> b) => !(a == b);

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        return obj is Option<T> option && Equals(option);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (_value.GetHashCode() * 397) ^ _hasValue.GetHashCode();
        }
    }

    public override string ToString() => HasValue ? $"Some<{typeof(T).Name}>({Value.ToString()})" : $"None<{typeof(T).Name}>";
}