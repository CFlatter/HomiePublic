using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;
using Homiev2.Mobile.Enum;
using Newtonsoft.Json.Linq;
using Homiev2.Shared.Dto;
using System.Collections;

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

        public async Task<T> ApiRequestAsync<T>(ApiRequestType requestType, string urlEndpoint, BaseDto dtoObject = null)
        {
            _bearerToken = _authService.BearerToken;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
            HttpResponseMessage result = new();

            switch (requestType)
            {
                case ApiRequestType.GET:
                    result = await _httpClient.GetAsync($"{urlEndpoint}");
                    break;
                case ApiRequestType.POST:
                    var json = JsonConvert.SerializeObject(dtoObject);
                    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                    result = await _httpClient.PostAsync($"{urlEndpoint}", httpContent);
                    break;
                case ApiRequestType.DELETE:
                    //TODO
                    break;
                default:
                    break;
            }


            if (result.IsSuccessStatusCode)
            {
                var responseString = await result.Content.ReadAsStringAsync();
                var deserializedResponse = JsonConvert.DeserializeObject<T>(responseString);
                return deserializedResponse;
            }
            else if ((int)result.StatusCode == StatusCodes.Status401Unauthorized)
            {
                throw new UnauthorizedAccessException();   
            }
            throw new Exception(result.StatusCode.ToString());
        }



    }
}
