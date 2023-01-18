using Homiev2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Homiev2.Shared.Interfaces.Services
{
    public interface IChoreLogService
    {
        Task DeleteChoreLogsAsync(Guid choreId);
        Task<List<ChoreLog>> GetChoreLogsAsync(DateTime startDate, DateTime endDate);
        Task<ChoreLog> LogChoreAsync(Guid choreId, Guid householdMemberId, DateTime dateCompleted, byte points, bool skipped);
    }
}
