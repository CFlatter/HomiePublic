using Homiev2.Shared.Dto;
using Homiev2.Shared.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Homiev2.Mobile.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public string BearerToken { get; private set; }

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

            var json = JsonConvert.SerializeObject(loginCreds);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await _httpClient.PostAsync($"Account/Login", httpContent);

            if (result.IsSuccessStatusCode)
            {
                var responseString = await result.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<dynamic>(responseString);
                BearerToken = (string)responseObject.token;
            }
        }
    }
}
