using Homiev2.Shared.Models;
using System;
using System.Threading.Tasks;

namespace Homiev2.Shared.Interfaces.Services
{
    public interface IHouseholdService
    {
        Task<int> CreateHouseholdAsync(Household household);
        Task<int> DeleteHouseholdAsync(Household household);
        string GenerateShareCode();
        Task<Guid> GenerateUniqueHouseholdIdAsync();
        Task<Household> ReturnHouseholdAsync(string userId);
        Task<Household> ReturnHouseholdByShareCodeAsync(string shareCode);
    }
}