using Homiev2.Shared.Dto;
using Homiev2.Shared.Enums;
using Homiev2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Homiev2.Shared.Interfaces.Services
{
    public interface IChoreService
    {
        Task<BaseChore> CompleteChoreAsync(string userId, CompletedChoreDto completedChoreDTO);
        Task<BaseChore> CreateChoreAsync(string userId, AdvancedChoreDto advancedChoreDTO);
        Task<BaseChore> CreateChoreAsync(string userId, SimpleChoreDto simpleChoreDTO);
        Task DeleteChoreAsync(string userId, Guid choreId);
        Task<BaseChore> GetChoreByIdAsync(string userId, Guid choreId);
        Task<IEnumerable<BaseChore>> GetChoresAsync(string userId);
    }
}
