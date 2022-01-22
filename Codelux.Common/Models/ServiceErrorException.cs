using System;
using System.Net;

namespace Codelux.Common.Models
{
    public class ServiceErrorException : Exception
    {
        public ServiceErrorException(string service, int index, HttpStatusCode statusCode)
        {
            Index = index;
            Service = service;
            StatusCode = statusCode;
        }

        public ServiceErrorException(string service, int index, HttpStatusCode statusCode, string message) : base(message)
        {
            Index = index;
            Service = service;
            StatusCode = statusCode;
        }

        public int Index { get; }

        public string Service { get; }

        public HttpStatusCode StatusCode { get; }
    }
}
