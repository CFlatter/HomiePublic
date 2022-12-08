using Homiev2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Homiev2.Shared.Interfaces.Repositories
{
    public interface IChoreRepository
    {
        Task<int> CheckUniqueChoreIdAsync(Guid choreId);
        Task DeleteChoreByChoreIdAsync(Guid choreId);
        Task<BaseChore> GetChoreByIdAsync(Guid choreId);
        Task<IEnumerable<BaseChore>> GetChoresAsync(Household household);
        Task<int> SaveChoreASync<T>(Chore<T> chore) where T : BaseFrequency;
        Task<int> UpdateChoreASync<T>(Chore<T> chore) where T : BaseFrequency;
    }
}
