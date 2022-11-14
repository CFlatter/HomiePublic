using Homiev2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Homiev2.Shared.Interfaces.Repositories
{
    public interface IChoreFrequencyRepository
    {
        Task<T> GetScheduleFromDB<T>(Guid choreId) where T : BaseFrequency;
    }
}
