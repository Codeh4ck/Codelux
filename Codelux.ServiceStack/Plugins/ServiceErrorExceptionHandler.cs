using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Codelux.Common.Extensions;
using Codelux.Common.Models;
using Codelux.Common.Responses;
using ServiceStack.Validation;
using ServiceStack.Web;

namespace Codelux.ServiceStack.Plugins
{
    public class ServiceErrorExceptionHandler
    {
        private readonly string _serviceName;

        public ServiceErrorExceptionHandler(string serviceName)
        {
            _serviceName = serviceName;
        }

        public ServiceResponse Handle(IRequest httpReq, object obj, Exception exception)
        {
            if (exception is ServiceErrorException apiErrorException)
            {
                httpReq.Response.StatusCode = (int)apiErrorException.StatusCode;

                if (apiErrorException.InnerException == null)
                    return new ServiceErrorResponse(apiErrorException.Service, apiErrorException.Index, apiErrorException.Message);

                List<string> exceptionMessages = apiErrorException.GetInnerExceptionMessages();

                return new ServiceErrorResponse(apiErrorException.Service, apiErrorException.Index, apiErrorException.Message, exceptionMessages);
            }

            if (exception is NotImplementedException)
            {
                httpReq.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                return new ServiceErrorResponse(_serviceName, ServiceCodes.InternalServerError, "Not Implemented");
            }

            if (exception is TaskCanceledException)
            {
                httpReq.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ServiceErrorResponse(_serviceName, ServiceCodes.InternalServerError, OverridenExceptionMessages.TaskCanceledExceptionMessage);
            }

            if (exception is ValidationError validationError)
            {
                httpReq.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                ServiceValidationResponse validationResponse = new(_serviceName);
                foreach (var violation in validationError.Violations)
                {
                    var apiValidationError = new ServiceValidationError(violation.FieldName, violation.ErrorMessage);
                    validationResponse.ValidationErrors.Add(apiValidationError);
                }
                return validationResponse;
            }

            httpReq.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return new ServiceErrorResponse(_serviceName, ServiceCodes.InternalServerError, exception.Message);
        }
    }
}
