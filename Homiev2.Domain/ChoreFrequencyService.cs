using Homiev2.Shared.Interfaces.Repositories;
using Homiev2.Shared.Interfaces.Services;
using Homiev2.Shared.Models;

namespace Homiev2.Domain
{
    public class ChoreFrequencyService : IChoreFrequencyService
    {
        private readonly IChoreFrequencyRepository _choreFrequencyRepository;

        public ChoreFrequencyService(IChoreFrequencyRepository choreFrequencyRepository)
        {
            _choreFrequencyRepository = choreFrequencyRepository;
        }

        public async Task<T> GetSchedule<T>(Guid choreId) where T : BaseFrequency
        {
            T schedule = await _choreFrequencyRepository.GetScheduleFromDB<T>(choreId);
            return schedule;
        }
    }


}
