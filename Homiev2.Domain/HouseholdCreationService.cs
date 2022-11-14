using Homiev2.Shared.Interfaces.Repositories;
using Homiev2.Shared.Interfaces.Services;
using Homiev2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Homiev2.Domain
{
    public class HouseholdCreationService : IHouseholdCreationService
    {
        private readonly IHouseholdService _householdService;
        private readonly IHouseholdMemberService _householdMemberService;
        private readonly IAuthUsersRepository _authUsersRepository;

        public HouseholdCreationService(IHouseholdService householdService, IHouseholdMemberService householdMemberService, IAuthUsersRepository authUsersRepository)
        {
            _householdService = householdService;
            _householdMemberService = householdMemberService;
            _authUsersRepository = authUsersRepository;
        }

        public async Task<Household> CreateHouseholdAsync(string username, string householdName)
        {
            //Check user doesn't already have a household created by them
            var existingHouseholdForUser = await _householdService.ReturnHouseholdAsync(username);
            if (existingHouseholdForUser == null)
            {
                var newHousehold = new Household();
                newHousehold.HouseholdName = householdName;
                newHousehold.HouseholdId = await _householdService.GenerateUniqueHouseholdIdAsync();
                newHousehold.ShareCode = _householdService.GenerateShareCode();
                newHousehold.UserId = username;

                using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Check save to DB worked
                    var writeToDB = await _householdService.CreateHouseholdAsync(newHousehold);
                    if (writeToDB == 1)
                    {
                        var user = await _authUsersRepository.GetUserAsync(username);
                        try
                        {
                            var result = await _householdMemberService.JoinHouseholdAsync(user.UserName, newHousehold.ShareCode, user.FriendlyName);
                            transaction.Complete();
                            return newHousehold;
                        }
                        catch (Exception)
                        {

                            transaction.Dispose();
                            throw new ApplicationException("Household failed to save to the database");
                        }
                    }
                    else
                    {
                        transaction.Dispose();
                        throw new ApplicationException("Household failed to save to the database");
                    }
                };

            

            }
            else
            {
                throw new ArgumentException("User already has a household");
            }

        }
    }
}
