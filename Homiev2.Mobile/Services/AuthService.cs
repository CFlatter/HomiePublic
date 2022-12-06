using Homiev2.Shared.Dto;
using Homiev2.Shared.Settings;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;

namespace Homiev2.Mobile.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public string BearerToken { get; private set; }
        public DateTime TokenExpiry { get; private set; }

        public AuthService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("HttpClient");
        }

        public async Task GetTokenAsync(string username, string password)
        {
            LoginDto loginCreds = new()
            {
                UserName = username,
                Password = password
            };

            var json = JsonSerializer.Serialize(loginCreds);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
 
            var result = await _httpClient.PostAsync($"Account/Login", httpContent);

            if (result.IsSuccessStatusCode)
            {
                var responseString = await result.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<JsonToken>(responseString);
                BearerToken = responseObject.Token;
                TokenExpiry = responseObject.Expiration;
            }
            else
            {
                if ((int)result.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }
                else
                {
                    throw new Exception(result.StatusCode.ToString());
                }
            }
        }

        public bool IsBearerTokenValid()
        {
            if (string.IsNullOrEmpty(BearerToken))
            {
                return false;
            }

            return (TokenExpiry > DateTime.UtcNow);
        }
    }
}
