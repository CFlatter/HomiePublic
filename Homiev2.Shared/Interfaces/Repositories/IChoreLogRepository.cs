using Homiev2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Homiev2.Shared.Interfaces.Repositories
{
    public interface IChoreLogRepository
    {
        Task<int> CheckUniqueChoreLogIdAsync(Guid choreLogId);
        Task DeleteChoreLogsByChoreIdAsync(Guid choreId);
        Task DeleteChoreLogsByHouseholdMemberIdAsync(Guid householdMemberId);
        Task<List<ChoreLog>> GetChoreLogsAsync(DateTime startDate, DateTime endDate);
        Task<int> SaveChoreLogAsync(ChoreLog choreLog);
    }
}
