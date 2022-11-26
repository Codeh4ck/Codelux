using System;

namespace Codelux.Common.Extensions
{
    public static class StructExtensions
    {
        public static object Box<T>(this T value) where T : struct => value;

        public static T Unbox<T>(this object value) where T : struct
        {
            if (value.GetType() != typeof(T))
                throw new InvalidCastException(
                    $"Value of type {value.GetType().Name} cannot be unboxed to value of type {typeof(T).Name}");

            try
            {
                return (T)value;
            }
            catch
            {
                return default;
            }
        }
    }
}
