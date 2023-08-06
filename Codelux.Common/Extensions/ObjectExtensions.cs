using System;
using System.Runtime.CompilerServices;

namespace Codelux.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static void Guard<T>(this T o, [CallerArgumentExpression("o")] string paramName = "") where T : class
        {
            if (o == null) throw new ArgumentNullException(paramName);
        }

        public static void Guard(this string s, [CallerArgumentExpression("s")] string paramName = "")
        {
            if (string.IsNullOrEmpty(s)) throw new ArgumentNullException(paramName);
        }
    }
}