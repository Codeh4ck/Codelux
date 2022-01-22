using System;
using System.ComponentModel;
using System.Reflection;

namespace Codelux.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum) throw new ArgumentException($"{nameof(GetDescription)} is only valid for enum types.");

            FieldInfo fieldInfo = value.GetType().GetField(value.ToString() ?? string.Empty);
            
            if (fieldInfo == null) return string.Empty;

            DescriptionAttribute[] descriptionAttributes =
                (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Description : value.ToString();
        }
    }
}
