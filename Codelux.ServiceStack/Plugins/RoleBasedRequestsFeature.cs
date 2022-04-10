using Funq;
using System;
using System.Net;
using ServiceStack;
using Codelux.Common.Models;
using Codelux.Common.Requests;
using Codelux.ServiceStack.Roles;

namespace Codelux.ServiceStack.Plugins
{
    public class RoleBasedRequestsFeature : IPlugin
    {

        private IRoleValidator _roleValidator;
        private IProtectedRouteCollection _protectedRouteCollection;

        public string UnauthorizedRequestErrorMessage { get; set; } = "Unauthorized endpoint reached.";
        public HttpStatusCode UnauthorizedRequestStatusCode { get; set; } = HttpStatusCode.Forbidden;

        public void Register(IAppHost appHost)
        {
            Console.WriteLine("Registered plugin");
            Container container = appHost.GetContainer();

            IRoleValidator roleValidator = container.Resolve<IRoleValidator>();

            if (roleValidator == null)
                throw new NotImplementedException("Role validator is not registered in the IoC");

            _roleValidator = roleValidator;

            if (_protectedRouteCollection == null)
            {
                IProtectedRouteCollection protectedRouteCollection = container.Resolve<IProtectedRouteCollection>();

                if (protectedRouteCollection == null)
                    throw new NotImplementedException("Protected request container is not registered in the IoC");

                _protectedRouteCollection = protectedRouteCollection;
            }

            appHost.GlobalRequestFiltersAsync.Add(async (request, response, dto) =>
            {
                Type dtoType = dto.GetType();
                
                if (_protectedRouteCollection.IsProtectedRequestType(dtoType))
                {
                    AuthenticatedRequest authenticatedRequest = dto as AuthenticatedRequest;
                    if (authenticatedRequest == null) return;

                    IHasRole initiator = await _roleValidator.GetModelAsync(authenticatedRequest.UserId);

                    bool canExecute = _protectedRouteCollection.CanExecute(dtoType, initiator);

                    if (!canExecute)
                    {
                        response.StatusCode = (int)UnauthorizedRequestStatusCode;
                        response.StatusDescription = UnauthorizedRequestErrorMessage;
                        response.ContentType = request.ContentType;

                        ServiceErrorException exception = new ServiceErrorException(nameof(RoleBasedRequestsFeature), 0,
                            UnauthorizedRequestStatusCode, UnauthorizedRequestErrorMessage);
                        
                        response.Dto = DtoUtils.CreateErrorResponse(request, exception,
                            new ResponseStatus(response.StatusCode.ToString()));

                        await response.WriteAsync(response.Dto.ToJson());
                        await response.EndRequestAsync();
                    }
                }
            });
        }
    }
}
