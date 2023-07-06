using System;
using System.Runtime.CompilerServices;

namespace Codelux.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static void Guard<T>(this T o, string message = "", [CallerArgumentExpression("o")] string paramName = "") where T : class
        {
            if (o == null) throw new ArgumentNullException(message, paramName);
        }

        public static void Guard(this string s, string message = "", [CallerArgumentExpression("s")] string paramName = "")
        {
            if (string.IsNullOrEmpty(s)) throw new ArgumentNullException(message, paramName);
        }
    }
}