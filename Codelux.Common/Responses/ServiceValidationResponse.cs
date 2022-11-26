using System.Collections.Generic;
using Codelux.Common.Models;

namespace Codelux.Common.Responses
{
    public class ServiceValidationResponse : ServiceResponse
    {
        public ServiceValidationResponse() : base(string.Empty, ServiceCodes.ValidationError) => ValidationErrors = new();

        public ServiceValidationResponse(string service) : base(service, ServiceCodes.ValidationError) => ValidationErrors = new();

        public List<ServiceValidationError> ValidationErrors { get; }
    }
}
