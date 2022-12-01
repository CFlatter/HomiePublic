using Homiev2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Homiev2.Shared.Interfaces.Services
{
    public interface IHouseholdMemberService
    {
        Task<HouseholdMember> CreateHouseholdMemberAsync(string username, string memberName);
        Task<HouseholdMember> DeleteHouseholdMemberAsync(string username, string memberName);
        Task<HouseholdMember> GetHouseholdMemberAsync(Guid householdMemberId);
        Task<List<HouseholdMember>> GetHouseholdMembersAsync(string username);
        Task<HouseholdMember> JoinHouseholdAsync(string username, string shareCode, string memberName);
    }
}
