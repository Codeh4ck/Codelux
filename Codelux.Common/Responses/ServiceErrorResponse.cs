using System.Collections.Generic;

namespace Codelux.Common.Responses
{
    public class ServiceErrorResponse : ServiceResponse
    {
        public ServiceErrorResponse() { }
        public ServiceErrorResponse(string service, int code, string message) : base(service, code)
        {
            Error = message;
        }

        public ServiceErrorResponse(string service, int code, string message, List<string> otherMessages) : base(service, code)
        {
            Error = message;
            OtherMessages = otherMessages;
        }

        public ServiceErrorResponse(string service, int code, string message, string[] otherMessages) : base(service, code)
        {
            Error = message;
            OtherMessages = new();
            OtherMessages.AddRange(otherMessages);
        }

        public string Error { get; set; }
        public List<string> OtherMessages { get; set; }
    }
}
