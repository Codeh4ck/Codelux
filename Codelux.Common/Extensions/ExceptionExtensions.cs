using System;
using System.Collections.Generic;

namespace Codelux.Common.Extensions
{
    public static class ExceptionExtensions
    {
        public static List<string> GetAllExceptionMessages(this Exception exception)
        {
            if (exception == null) return null;
            List<string> exceptionMessages = new() { exception.Message };

            List<string> innerMessages = exception.GetInnerExceptionMessages();
            if (innerMessages != null) exceptionMessages.AddRange(innerMessages);

            return exceptionMessages;
        }

        public static List<string> GetInnerExceptionMessages(this Exception exception)
        {
            if (exception.InnerException == null) return null;

            List<string> exceptionMessages = new();

            Exception current = exception.InnerException;
            exceptionMessages.Add(current.Message);

            while (current.InnerException != null)
            {
                current = current.InnerException;
                exceptionMessages.Add(current.Message);
            }

            return exceptionMessages;
        }

        public static List<Exception> GetAllInnerExceptions(this Exception exception)
        {
            if (exception.InnerException == null) return null;

            List<Exception> exceptions = new();

            Exception current = exception.InnerException;
            exceptions.Add(current);

            while (current.InnerException != null)
            {
                current = current.InnerException;
                exceptions.Add(current);
            }

            return exceptions;
        }
    }
}
