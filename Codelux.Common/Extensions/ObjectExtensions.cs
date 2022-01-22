using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Codelux.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static void Guard<T>([NotNull] this T o, [CallerArgumentExpression("o")] string message = "") where T : class
        {
            if (o == null) throw new ArgumentNullException(message);
        }

        public static void Guard([NotNull] this string s, [CallerArgumentExpression("s")] string message = "")
        {
            if (string.IsNullOrEmpty(s)) throw new ArgumentNullException(message);
        }
    }
}
