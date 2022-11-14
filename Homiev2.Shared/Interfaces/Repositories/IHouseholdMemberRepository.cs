using Homiev2.Shared.Models;
using System;
using System.Threading.Tasks;

namespace Homiev2.Shared.Interfaces.Repositories
{
    public interface IHouseholdMemberRepository
    {
        Task<int> CheckUniqueHouseholdMemberIdAsync(Guid householdMemberId);
        Task<HouseholdMember> CreateHouseholdMemberAsync(Household household, string memberName, Guid householdMemberId, string username = null);
        Task<int> DeleteHouseholdMemberAsync(HouseholdMember householdMember);
        Task<HouseholdMember> FindHouseholdMemberAsync(Household household, string memberName);
        Task<List<HouseholdMember>> GetAllHouseholdMembersAsync(Guid householdId);
        Task<HouseholdMember> GetHouseholdMemberAsync(Guid householdMemberId);
    }
}