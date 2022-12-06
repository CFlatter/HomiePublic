using Homiev2.Shared.Dto;
using Homiev2.Shared.Enums;
using Homiev2.Shared.Interfaces.Repositories;
using Homiev2.Shared.Interfaces.Services;
using Homiev2.Shared.Models;
using System.Transactions;

namespace Homiev2.Domain
{
    public class ChoreService : IChoreService
    {
        private readonly IChoreRepository _choreRepository;
        private readonly IHouseholdService _householdService;
        private readonly IChoreLogService _choreLogService;
        private readonly IChoreFrequencyService _choreFrequencyService;


        public ChoreService(IChoreRepository choreRepository, IHouseholdService householdService, IChoreLogService choreLogService, IChoreFrequencyService choreFrequencyService)
        {
            _choreRepository = choreRepository;
            _householdService = householdService;
            _choreLogService = choreLogService;
            _choreFrequencyService = choreFrequencyService;

        }

        public async Task<IEnumerable<BaseChore>> GetChoresAsync(string userId)
        {
            var household = await _householdService.ReturnHouseholdAsync(userId);
            var chores = await _choreRepository.GetChoresAsync(household);

            return chores;
        }

        public async Task<BaseChore> CreateChoreAsync(string userId, SimpleChoreDto simpleChoreDTO)
        {
            var household = await _householdService.ReturnHouseholdAsync(userId);
            var chore = new Chore<ChoreFrequencySimple>
            {
                ChoreId = await GenerateChoreIdAsync(),
                TaskName = simpleChoreDTO.TaskName,
                Points = simpleChoreDTO.Points,
                FrequencyTypeId = FrequencyType.Simple,
                HouseholdId = household.HouseholdId,
                CreatedBy = userId,
                Schedule = new ChoreFrequencySimple()
                {
                    TimeSpan = simpleChoreDTO.Timespan,
                    Multiplier = simpleChoreDTO.Multiplier
                }
            };
            chore.InitNextDueDate(simpleChoreDTO.StartDate);

            var result = await _choreRepository.SaveChoreASync<ChoreFrequencySimple>(chore);
            if (result != 0)
            {
                return await _choreRepository.GetChoreByIDAsync(chore.ChoreId);
            }
            else
            {
                throw new Exception("Chore did not save to database!");
            }
        }

        public async Task<BaseChore> CreateChoreAsync(string userId, AdvancedChoreDto advancedChoreDTO)
        {
            var household = await _householdService.ReturnHouseholdAsync(userId);
            var chore = new Chore<ChoreFrequencyAdvanced>
            {
                ChoreId = await GenerateChoreIdAsync(),
                TaskName = advancedChoreDTO.TaskName,
                Points = advancedChoreDTO.Points,
                FrequencyTypeId = FrequencyType.Advanced,
                HouseholdId = household.HouseholdId,
                CreatedBy = userId,
                Schedule = new ChoreFrequencyAdvanced()
                {
                    AdvancedType = advancedChoreDTO.AdvancedType,
                    DOfWeek = advancedChoreDTO.DOfWeek,
                    DOfMonth = advancedChoreDTO.DOfMonth,
                    FirstDOfMonth = advancedChoreDTO.FirstDOfMonth,
                    LastDOfMonth = advancedChoreDTO.LastDOfMonth
                }
            };
            chore.InitNextDueDate(advancedChoreDTO.StartDate);

            var result = await _choreRepository.SaveChoreASync<ChoreFrequencyAdvanced>(chore);
            if (result != 0)
            {
                return await _choreRepository.GetChoreByIDAsync(chore.ChoreId);
            }
            else
            {
                throw new Exception("Chore did not save to database!");
            }
        }



        private async Task<Guid> GenerateChoreIdAsync()
        {
            Guid choreId;
            var isUniqueChoreId = 0;
            do
            {
                choreId = Guid.NewGuid();
                isUniqueChoreId = await _choreRepository.CheckUniqueChoreIdAsync(choreId);
            } while (isUniqueChoreId != 0);

            return choreId;
        }

        public async Task<BaseChore> CompleteChoreAsync(CompletedChoreDto completedChoreDTO)
        {

            //Get Chore
            var completedChore = await _choreRepository.GetChoreByIDAsync(completedChoreDTO.ChoreId); //Get chore from DB

            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //Log Chore
                await _choreLogService.LogChoreAsync(completedChore.ChoreId, completedChoreDTO.HouseholdMemberId, completedChoreDTO.CompletedDateTime ?? DateTime.Now, completedChore.Points, completedChoreDTO.Skipped); //Log chore            

                if (completedChore.FrequencyTypeId == FrequencyType.Simple)
                {
                    Chore<ChoreFrequencySimple> nextChore = (Chore<ChoreFrequencySimple>)completedChore;
                    //Calculate next date
                    nextChore.GenerateNewChore(completedChoreDTO.CompletedDateTime ?? DateTime.Now);
                    //Save new chore
                    var result = await _choreRepository.UpdateChoreASync<ChoreFrequencySimple>(nextChore);
                    if (result != 0)
                    {
                        transaction.Complete();
                        return nextChore;
                    }
                    else
                    {
                        transaction.Dispose();
                        throw new Exception("Chore did not save to database!");
                    }
                }
                else if (completedChore.FrequencyTypeId == FrequencyType.Advanced)
                {
                    //Create new chore object   
                    Chore<ChoreFrequencyAdvanced> nextChore = (Chore<ChoreFrequencyAdvanced>)completedChore;
                    //Calculate next date
                    nextChore.GenerateNewChore(completedChoreDTO.CompletedDateTime ?? DateTime.Now);
                    //Save new chore
                    var result = await _choreRepository.UpdateChoreASync<ChoreFrequencyAdvanced>(nextChore);
                    if (result != 0)
                    {
                        transaction.Complete();
                        return nextChore;
                    }
                    else
                    {
                        transaction.Dispose();
                        throw new Exception("Chore did not save to database!");
                    }
                }
                else
                {
                    throw new Exception("Frequency not found!");
                }
            }

        }

        public async Task DeleteChoreAsync(Guid choreId)
        {
            using (TransactionScope transaction = new (TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _choreLogService.DeleteChoreLogsAsync(choreId);
                    await _choreRepository.DeleteChoreByChoreIdAsync(choreId);
                    transaction.Complete();
                }
                catch (Exception e )
                {
                    transaction.Dispose();
                    throw new Exception(e.Message);
                }

            }

        }
    }

}