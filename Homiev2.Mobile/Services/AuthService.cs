using Homiev2.Shared.Dto;
using Homiev2.Shared.Models;
using Homiev2.Shared.Settings;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
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

                await SecureStorage.SetAsync("token", BearerToken);
                await SecureStorage.SetAsync("token_expiry", TokenExpiry.ToString());
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

        public async Task RegisterAsync(string email, string password, string friendlyName)
        {


            AuthUser credentials = new()
            {
                Email = email,
                Password = password,
                FriendlyName = friendlyName,
            };

            var json = JsonSerializer.Serialize(credentials);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await _httpClient.PostAsync($"Account/Register", httpContent);

            if (!result.IsSuccessStatusCode)
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

        public async Task<bool> CheckForValidCachedJwtToken()
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                var tokenExpiration = await SecureStorage.GetAsync("token_expiry");

                if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(tokenExpiration) && DateTime.Parse(tokenExpiration) > DateTime.Now)
                {
                    Homiev2.Shared.Settings.JsonToken jwtToken = new();
                    jwtToken.Token = await SecureStorage.GetAsync("token");
                    jwtToken.Expiration = DateTime.Parse(await SecureStorage.GetAsync("token_expiry"));
                    BearerToken = jwtToken.Token;
                    TokenExpiry = jwtToken.Expiration;
                    return true;
                }
            }
            catch (Exception)
            {
                return false;

            }

            return false;
        }
    }
}
