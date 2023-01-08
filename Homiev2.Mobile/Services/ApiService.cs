using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;
using Homiev2.Mobile.Enum;
using Homiev2.Shared.Dto;
using System.Text.Json;

namespace Homiev2.Mobile.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private string _bearerToken;
        private readonly AuthService _authService;

        public ApiService(IHttpClientFactory httpClient, AuthService authService)
        {
            _httpClient = httpClient.CreateClient("HttpClient");
            _authService = authService;
        }

        public async Task<T> ApiRequestAsync<T>(ApiRequestType requestType, string urlEndpoint, IDto dtoObject = null)
        {
            _bearerToken = _authService.BearerToken;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
            HttpResponseMessage result = new();

            string json;
            StringContent httpContent = null;
            if (dtoObject != null)
            {
                json = JsonSerializer.Serialize(dtoObject, dtoObject.GetType());
                httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            }


            switch (requestType)
            {
                case ApiRequestType.GET:
                    result = await _httpClient.GetAsync($"{urlEndpoint}");
                    break;
                case ApiRequestType.POST:
                    result = await _httpClient.PostAsync($"{urlEndpoint}", httpContent);
                    break;
                case ApiRequestType.PATCH:
                    result = await _httpClient.PatchAsync($"{urlEndpoint}", httpContent);
                    break;
                case ApiRequestType.DELETE:
                    result = await _httpClient.DeleteAsync($"{urlEndpoint}");
                    break;
                default:
                    break;
            }


            if (result.IsSuccessStatusCode)
            {
                var responseString = await result.Content.ReadAsStringAsync();
                if (String.IsNullOrEmpty(responseString))
                {
                    return default(T);
                }
                var deserializedResponse = JsonSerializer.Deserialize<T>(responseString);
                return deserializedResponse;
            }
            else if ((int)result.StatusCode == StatusCodes.Status401Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            else if ((int)result.StatusCode == StatusCodes.Status204NoContent)
            {
                return default(T);
            }
            throw new Exception(result.StatusCode.ToString());
        }



    }
}
