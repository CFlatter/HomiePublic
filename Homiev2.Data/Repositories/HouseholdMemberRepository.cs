using Homiev2.Data.Contexts;
using Homiev2.Shared.Interfaces.Repositories;
using Homiev2.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Homiev2.Data.Repositories
{
    public class HouseholdMemberRepository : IHouseholdMemberRepository
    {
        private readonly Homiev2Context _context;

        public HouseholdMemberRepository(Homiev2Context context)
        {
            _context = context;
        }

        public async Task<HouseholdMember> CreateHouseholdMemberAsync(Household household, string memberName, Guid householdMemberId, string userId = null)
        {
            var householdMember = new HouseholdMember()
            {
                HouseholdId = household.HouseholdId,
                MemberName = memberName,
                HouseholdMemberId = householdMemberId,
            };

            await _context.HouseholdMembers.AddAsync(householdMember);
            var result = await _context.SaveChangesAsync();
            if (result == 1)
            {
                return householdMember;
            }

            return null;
        }

        public async Task<HouseholdMember> GetHouseholdMemberAsync(Guid householdMemberId)
        {
            try
            {
                return await _context.HouseholdMembers.Where(x => x.HouseholdMemberId == householdMemberId).AsNoTracking().FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw new Exception("Household member not found");
            }
            
        }


        public async Task<List<HouseholdMember>> GetAllHouseholdMembersAsync(Guid householdId)
        {
            try
            {
                return await _context.HouseholdMembers.Where(x => x.HouseholdId == householdId).AsNoTracking().ToListAsync();
            }
            catch (Exception)
            {

                throw new Exception("Household not found");
            }
        }

        public async Task<HouseholdMember> FindHouseholdMemberAsync(Household household, string memberName)
        {
            try
            {
                return await _context.HouseholdMembers.Where(x => x.HouseholdId == household.HouseholdId && x.MemberName == memberName).AsNoTracking().FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw new Exception("Household member not found");
            }

        }

        public async Task<int> DeleteHouseholdMemberAsync(HouseholdMember householdMember)
        {
            _context.HouseholdMembers.Remove(householdMember);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> CheckUniqueHouseholdMemberIdAsync(Guid householdMemberId)
        {
            return await _context.HouseholdMembers.Where(x => x.HouseholdMemberId == householdMemberId).CountAsync();

        }
    }
}
