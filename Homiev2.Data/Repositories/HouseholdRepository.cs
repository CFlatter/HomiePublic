using Homiev2.Data.Contexts;
using Homiev2.Shared.Interfaces.Repositories;
using Homiev2.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Homiev2.Data.Repositories
{
    public class HouseholdRepository : IHouseholdRepository
    {
        private readonly Homiev2Context _context;

        public HouseholdRepository(Homiev2Context context)
        {
            _context = context;
        }

        public async Task<Household> GetHouseholdAsync(string userId)
        {
            try
            {
                return await _context.Households.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw new Exception($"Household not found with user ID {userId}");
            }
            
        }

        public async Task<Household> GetHouseholdByShareCodeAsync(string shareCode)
        {
            try
            {
                return await _context.Households.Where(x => x.ShareCode == shareCode).FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw new Exception($"Household not found with share code {shareCode}");
            }
        }

        public async Task<int> CreateHouseholdAsync(Household household)
        {
            await _context.Households.AddAsync(household);
            return await _context.SaveChangesAsync();
        }


        public async Task<int> CheckUniqueShareCodeAsync(string shareCode)
        {

            return await _context.Households.Where(x => x.ShareCode == shareCode).CountAsync();

        }

        public async Task<int> CheckUniqueHouseholdIdAsync(Guid householdId)
        {
            return await _context.Households.Where(x => x.HouseholdId == householdId).CountAsync();

        }

        public async Task<int> DeleteHouseholdAsync(Household household)
        {
            _context.Households.Remove(household);
            return await _context.SaveChangesAsync();
        }




    }
}
