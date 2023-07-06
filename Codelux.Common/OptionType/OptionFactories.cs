using System;

namespace Codelux.Common.OptionType;

public static partial class Option
{
    public static Option<T> From<T>(T valueOrNull) => valueOrNull == null ? None<T>() : Some(valueOrNull);
    public static Option<T> From<T>(T? valueOrNull) where T : struct => valueOrNull == null ? None<T>() : Some(valueOrNull.Value);
    public static Option<T> Some<T>(T instance) => Option<T>.Some(instance);
    public static Option<T> None<T>() => Option<T>.None();
}