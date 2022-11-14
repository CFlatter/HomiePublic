using Homiev2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homiev2.Shared.Interfaces.Repositories
{
    public interface IHouseholdRepository
    {
        Task<int> CheckUniqueHouseholdIdAsync(Guid householdId);
        Task<int> CheckUniqueShareCodeAsync(string shareCode);
        Task<int> CreateHouseholdAsync(Household household);
        Task<int> DeleteHouseholdAsync(Household household);
        Task<Household> GetHouseholdAsync(string username);
        Task<Household> GetHouseholdByShareCodeAsync(string shareCode);
    }
}
