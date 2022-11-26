using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Codelux.Common.Models;
using Codelux.Common.Requests;
using Codelux.Common.Responses;
using Newtonsoft.Json;

namespace Codelux.Http
{
    public class ServiceClient
    {
        private readonly string _serviceName;
        private readonly HttpClient _client;

        public ServiceClient(string serviceName, Uri endpoint)
        {
            _serviceName = serviceName;
            _client = new() {BaseAddress = endpoint};
            _client.DefaultRequestHeaders.Add("Accept", "application/json");

            Settings = new() {DateTimeZoneHandling = DateTimeZoneHandling.Utc};
        }

        protected JsonSerializerSettings Settings { get; }

        protected async Task<HttpResponseMessage> MakeRequest(
            Request nonAuthRequest,
            HttpRequestMessage request,
            CancellationToken cancellationToken = default)
        {
            string authUrl =
                $"Id={nonAuthRequest.Id:N}&CallerRequestId={nonAuthRequest.CallerRequestId:N}";

            if (request.RequestUri != null)
            {
                string url = request.RequestUri.ToString();
                if (url.Contains('?')) url += "&" + authUrl;
                else url += "?" + authUrl;

                request.RequestUri = new(url);
            }
            else throw new Exception($"{nameof(request)} param RequestUri property is not set.");

            return await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        protected async Task<HttpResponseMessage> MakeRequest(
            AuthenticatedRequest authRequest,
            HttpRequestMessage request,
            CancellationToken cancellationToken = default)
        {
            string authUrl =
                $"Id={authRequest.Id:N}&UserId={authRequest.UserId:N}&CallerRequestId={authRequest.CallerRequestId:N}" +
                $"&Email={authRequest.Email}&Username={authRequest.Username}";

            if (request.RequestUri != null)
            {
                string url = request.RequestUri.ToString();
                if (url.Contains('?')) url += "&" + authUrl;
                else url += "?" + authUrl;

                request.RequestUri = new(url);
            }
            else throw new Exception($"{nameof(request)} param RequestUri property is not set.");


            return await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }


        protected async Task MakeRequestAndResponseAsync(
            Request nonAuthRequest,
            HttpRequestMessage request,
            CancellationToken cancellationToken = default)
        {
            var response = await MakeRequest(nonAuthRequest, request, cancellationToken).ConfigureAwait(false);
            await EnsureSuccessfulResponse(response).ConfigureAwait(false);
        }

        protected async Task MakeRequestAndResponseAsync(
            AuthenticatedRequest authRequest,
            HttpRequestMessage request,
            CancellationToken cancellationToken = default)
        {
            var response = await MakeRequest(authRequest, request, cancellationToken).ConfigureAwait(false);
            await EnsureSuccessfulResponse(response).ConfigureAwait(false);
        }

        protected async Task<T> MakeRequestAndResponseAsync<T>(
            Request nonAuthRequest,
            HttpRequestMessage request,
            bool throwsOnNotFound = true,
            CancellationToken cancellationToken = default)
        {
            var response = await MakeRequest(nonAuthRequest, request, cancellationToken).ConfigureAwait(false);
            return await GetResponse<T>(response, throwsOnNotFound).ConfigureAwait(false);
        }

        protected async Task<T> MakeRequestAndResponseAsync<T>(
            AuthenticatedRequest authRequest,
            HttpRequestMessage request,
            bool throwsOnNotFound = true,
            CancellationToken cancellationToken = default)
        {
            var response = await MakeRequest(authRequest, request, cancellationToken).ConfigureAwait(false);
            return await GetResponse<T>(response, throwsOnNotFound).ConfigureAwait(false);
        }

        protected async Task<T> GetResponse<T>(HttpResponseMessage response, bool throwsOnNotFound = true)
        {
            string content = await EnsureSuccessfulResponse(response, throwsOnNotFound).ConfigureAwait(false);
            if (content == null && !throwsOnNotFound) return default;

            return JsonConvert.DeserializeObject<T>(content, Settings);
        }

        protected async Task<string> EnsureSuccessfulResponse(HttpResponseMessage response,
            bool throwsOnNotFound = true)
        {
            string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.IsSuccessStatusCode) return content;

            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                {
                    ServiceValidationResponse error = JsonConvert.DeserializeObject<ServiceValidationResponse>(content, Settings);

                    throw new ServiceErrorException(
                        _serviceName, error.Code,
                        response.StatusCode,
                        error.ValidationErrors == null
                            ? null
                            : string.Join(",", error.ValidationErrors.Select(x => string.Join(":", x.Field, x.Message)))
                    );
                }

                case HttpStatusCode.NotFound when !throwsOnNotFound:
                    return null;

                default:
                {
                    ServiceErrorResponse error = JsonConvert.DeserializeObject<ServiceErrorResponse>(content, Settings);
                    throw new ServiceErrorException(_serviceName, error.Code, response.StatusCode, error.Error);
                }
            }
        }
    }
}
