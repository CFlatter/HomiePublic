using Homiev2.Mobile.Interfaces;
using Homiev2.Shared.Models;
using System.Net.Http.Json;

namespace Homiev2.Mobile.Services
{
    public class HouseholdService : IHouseholdService
    {
        private readonly HttpService _httpClient;

        public HouseholdService(HttpService httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<HouseholdMember>> GetAllHouseholdMembersAsync()
        {
            var response = await _httpClient.Client.GetAsync($"{_httpClient.Client.BaseAddress}/HouseholdMember/HouseholdMembers");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<HouseholdMember>>();
            }

            return new List<HouseholdMember>();
        }
    }
}
