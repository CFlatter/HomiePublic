using Homiev2.Data.Contexts;
using Homiev2.Shared.Interfaces.Repositories;
using Homiev2.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Homiev2.Data.Repositories
{
    public class ChoreLogRepository : IChoreLogRepository
    {
        private readonly Homiev2Context _context;

        public ChoreLogRepository(Homiev2Context context)
        {
            _context = context;
        }

        public async Task<int> CheckUniqueChoreLogIdAsync(Guid choreLogId)
        {
            return await _context.ChoreLogs.Where(x => x.ChoreLogId == choreLogId).CountAsync();

        }
        public async Task<int> SaveChoreLogAsync(ChoreLog choreLog)
        {
            await _context.ChoreLogs.AddAsync(choreLog);
            return await _context.SaveChangesAsync();

        }

        public async Task DeleteChoreLogsByHouseholdMemberIdAsync(Guid householdMemberId)
        {
                List<ChoreLog> choreLogs = await _context.ChoreLogs.Where(x => x.HouseholdMemberId == householdMemberId).ToListAsync();
                _context.ChoreLogs.RemoveRange(choreLogs);
                await _context.SaveChangesAsync();
        }

        public async Task DeleteChoreLogsByChoreIdAsync(Guid choreId)
        {

                List<ChoreLog> choreLogs = await _context.ChoreLogs.Where(x => x.ChoreId == choreId).ToListAsync();
                _context.ChoreLogs.RemoveRange(choreLogs);
                await _context.SaveChangesAsync();

        }
    }
}
