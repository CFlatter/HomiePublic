using Homiev2.Data.Contexts;
using Homiev2.Shared.Interfaces.Repositories;
using Homiev2.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Homiev2.Data.Repositories
{
    public class ChoreRepository : IChoreRepository
    {
        private readonly Homiev2Context _context;

        public ChoreRepository(Homiev2Context context)
        {
            _context = context;
        }

        public async Task<int> CheckUniqueChoreIdAsync(Guid choreId)
        {
            return await _context.Chores.Where(x => x.ChoreId == choreId).CountAsync();
        }

        public async Task<BaseChore> GetChoreByIdAsync(Guid choreId)
        {

            var findSimpleSchedule = await _context.ChoreFrequencySimple.Where(s => s.ChoreId == choreId).SingleOrDefaultAsync();
            var findAdvancedSchedule = await _context.ChoreFrequencyAdvanced.Where(a => a.ChoreId == choreId).SingleOrDefaultAsync();

            if (findSimpleSchedule != null)
            {
                var chore = await _context.Chores.Where(x => x.ChoreId == choreId).Join(_context.ChoreFrequencySimple, c => c.ChoreId, s => s.ChoreId, (c, s) => new Chore<ChoreFrequencySimple>
                {
                    ChoreId = c.ChoreId,
                    TaskName = c.TaskName,
                    Points = c.Points,
                    FrequencyTypeId = c.FrequencyTypeId,
                    HouseholdId = c.HouseholdId,
                    CreatedBy = c.CreatedBy,
                    Schedule = new ChoreFrequencySimple {
                        ChoreId = s.ChoreId,
                        TimeSpan = s.TimeSpan,
                        Multiplier = s.Multiplier
                    }
                }).SingleOrDefaultAsync();

                return chore;
            }
            else if (findAdvancedSchedule != null)
            {
                var chore = await _context.Chores.Where(x => x.ChoreId == choreId).Join(_context.ChoreFrequencyAdvanced, c => c.ChoreId, a => a.ChoreId, (c, a) => new Chore<ChoreFrequencyAdvanced>
                {
                    ChoreId = c.ChoreId,
                    TaskName = c.TaskName,
                    Points = c.Points,
                    FrequencyTypeId = c.FrequencyTypeId,
                    HouseholdId = c.HouseholdId,
                    CreatedBy = c.CreatedBy,
                    Schedule = new ChoreFrequencyAdvanced
                    {
                        ChoreId = a.ChoreId,
                        DOfWeek = a.DOfWeek,
                        DOfMonth = a.DOfMonth,
                        FirstDOfMonth = a.FirstDOfMonth,
                        LastDOfMonth = a.LastDOfMonth
                    }
                }).SingleOrDefaultAsync();

                return chore;
            }

            throw new Exception("Can't return chore from database");
            //var chores = _context.Chores.Join(_context.ChoreFrequencySimple, c => c.ChoreId,s => s.ChoreId,);
           
        }

        public async Task<IEnumerable<BaseChore>> GetChoresAsync(Household household)
        {
            if (household == null)
            {
                return new List<BaseChore>();
            }
            return await _context.Chores.Where(x => x.HouseholdId == household.HouseholdId).ToListAsync();
        }

        public async Task<int> SaveChoreASync<T>(Chore<T> chore) where T : BaseFrequency
        {

            if (typeof(T) == typeof(ChoreFrequencySimple))
            {

                var choreDBEntity = await _context.Chores.AddAsync(chore);
                ChoreFrequencySimple schedule = (ChoreFrequencySimple)Convert.ChangeType(chore.Schedule, typeof(ChoreFrequencySimple));
                schedule.ChoreId = choreDBEntity.Entity.ChoreId;
                await _context.ChoreFrequencySimple.AddAsync(schedule);

            }
            else if (typeof(T) == typeof(ChoreFrequencyAdvanced))
            {
                var choreDBEntity = await _context.Chores.AddAsync(chore);
                ChoreFrequencyAdvanced schedule = (ChoreFrequencyAdvanced)Convert.ChangeType(chore.Schedule, typeof(ChoreFrequencyAdvanced));
                schedule.ChoreId = choreDBEntity.Entity.ChoreId;
                await _context.ChoreFrequencyAdvanced.AddAsync(schedule);
            }

                return await _context.SaveChangesAsync();
            
        }

        public async Task<int> UpdateChoreASync<T>(Chore<T> chore) where T : BaseFrequency
        {

            _context.Update(chore);
            if (typeof(T) == typeof(ChoreFrequencySimple))
            {
                _context.ChoreFrequencySimple.Update((ChoreFrequencySimple)Convert.ChangeType(chore.Schedule, typeof(ChoreFrequencySimple)));
            }
            else if (typeof(T) == typeof(ChoreFrequencyAdvanced))
            {
                ChoreFrequencyAdvanced schedule = (ChoreFrequencyAdvanced)Convert.ChangeType(chore.Schedule, typeof(ChoreFrequencyAdvanced));
                _context.ChoreFrequencyAdvanced.Update(schedule);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task DeleteChoreByChoreIdAsync(Guid choreId)
        {
            using(TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var chore = await GetChoreByIdAsync(choreId);
                    if (chore is Chore<ChoreFrequencySimple>)
                    {
                        var schedule = await _context.ChoreFrequencySimple.Where(x => x.ChoreId == choreId).SingleAsync();
                        _context.ChoreFrequencySimple.Remove(schedule);
                    }
                    else if (chore is Chore<ChoreFrequencyAdvanced>)
                    {
                        var schedule = await _context.ChoreFrequencyAdvanced.Where(x => x.ChoreId == choreId).SingleAsync();
                        _context.ChoreFrequencyAdvanced.Remove(schedule);
                    }
                    _context.Chores.Remove(chore);
                    await _context.SaveChangesAsync();
                    transaction.Complete();
                }
                catch (Exception)
                {
                    transaction.Dispose();
                    throw;
                }
            }


        }

    }
}
