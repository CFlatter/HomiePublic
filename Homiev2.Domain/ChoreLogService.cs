using Homiev2.Shared.Interfaces.Repositories;
using Homiev2.Shared.Interfaces.Services;
using Homiev2.Shared.Models;

namespace Homiev2.Domain
{
    public class ChoreLogService : IChoreLogService
    {
        private readonly IChoreLogRepository _choreLogRepository;

        public ChoreLogService(IChoreLogRepository choreLogRepository)
        {
            _choreLogRepository = choreLogRepository;
        }

        public async Task<List<ChoreLog>> GetChoreLogsAsync(DateTime startDate, DateTime endDate)
        {
            return await _choreLogRepository.GetChoreLogsAsync(startDate,endDate);
            
        }

        public async Task<ChoreLog> LogChoreAsync(Guid choreId, Guid householdMemberId, DateTime dateCompleted, byte points, bool skipped)
        {
            ChoreLog choreLog = new ChoreLog
            {
                ChoreLogId = GenerateChoreLogIdAsync().Result,
                ChoreId = choreId,
                HouseholdMemberId = householdMemberId,
                DateAdded = DateTime.Now,
                DateCompleted = dateCompleted,
                Points = points,
                Skipped = skipped
            };

            await _choreLogRepository.SaveChoreLogAsync(choreLog);
            return choreLog;
        }

        private async Task<Guid> GenerateChoreLogIdAsync()
        {
            Guid choreLogId;
            var isUniqueChoreLogId = 0;
            var i = 0;
            do
            {
                choreLogId = Guid.NewGuid();
                isUniqueChoreLogId = await _choreLogRepository.CheckUniqueChoreLogIdAsync(choreLogId);
                i++;
            } while (isUniqueChoreLogId == 0 && i < 10);

            return choreLogId;
        }

        public async Task DeleteChoreLogsAsync(Guid choreId)
        {
            try
            {
                await _choreLogRepository.DeleteChoreLogsByChoreIdAsync(choreId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            } 
        }
    }
}
