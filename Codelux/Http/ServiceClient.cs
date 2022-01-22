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

            string url = request.RequestUri.ToString();
            if (url.Contains('?')) url += "&" + authUrl;
            else url += "?" + authUrl;

            request.RequestUri = new(url);

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

            string url = request.RequestUri.ToString();
            if (url.Contains('?')) url += "&" + authUrl;
            else url += "?" + authUrl;

            request.RequestUri = new(url);

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
            string content = string.Empty;
            if (response.Content != null)
            {
                content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            if (response.IsSuccessStatusCode) return content;

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var error = JsonConvert.DeserializeObject<ServiceValidationResponse>(content, Settings);
                throw new ServiceErrorException(
                    _serviceName, error.Code,
                    response.StatusCode,
                    error.ValidationErrors == null
                        ? null
                        : string.Join(",", error.ValidationErrors.Select(x => string.Join(":", x.Field, x.Message)))
                );
            }
            else if (response.StatusCode == HttpStatusCode.NotFound && !throwsOnNotFound) return null;
            else
            {
                var error = JsonConvert.DeserializeObject<ServiceErrorResponse>(content, Settings);
                throw new ServiceErrorException(
                    _serviceName,
                    error.Code,
                    response.StatusCode,
                    error.Error);
            }
        }
    }
}
