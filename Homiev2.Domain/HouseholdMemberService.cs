using Homiev2.Shared.Exceptions;
using Homiev2.Shared.Interfaces.Repositories;
using Homiev2.Shared.Interfaces.Services;
using Homiev2.Shared.Models;
using System.Transactions;

namespace Homiev2.Domain
{
    public class HouseholdMemberService : IHouseholdMemberService
    {
        private readonly IHouseholdService _householdService;
        private readonly IHouseholdMemberRepository _householdMemberRepository;
        private readonly IAuthUsersRepository _authUsersRepository;
        private readonly IChoreLogRepository _choreLogRepository;

        public HouseholdMemberService(IHouseholdService householdService, IHouseholdMemberRepository householdMemberRepository, IAuthUsersRepository authUsersRepository, IChoreLogRepository choreLogRepository)
        {
            _householdService = householdService;
            _householdMemberRepository = householdMemberRepository;
            _authUsersRepository = authUsersRepository;
            _choreLogRepository = choreLogRepository;
        }
        private async Task<Guid> GenerateUniqueHouseholdMemberIdAsync()
        {
            Guid householdMemberId;
            var isUniqueHouseholdMemberId = 0;
            var i = 0;
            do
            {
                householdMemberId = Guid.NewGuid();
                isUniqueHouseholdMemberId = await _householdMemberRepository.CheckUniqueHouseholdMemberIdAsync(householdMemberId);
                i++;
            } while (isUniqueHouseholdMemberId == 0 && i < 10);

            return householdMemberId;
        }
        public async Task<HouseholdMember> CreateHouseholdMemberAsync(string username, string memberName)
        {
            var household = await _householdService.ReturnHouseholdAsync(username);
            var householdMemberId = await GenerateUniqueHouseholdMemberIdAsync();
            var householdMember = await _householdMemberRepository.CreateHouseholdMemberAsync(household, memberName, householdMemberId);
            if (householdMember != null) //Save successful
            {
                return await _householdMemberRepository.GetHouseholdMemberAsync(householdMemberId);
            }
            else
            {
                throw new Exception("New Member failed to save to the database");
            }
        }

        public async Task<HouseholdMember> GetHouseholdMemberAsync(Guid householdMemberId)
        {
            return await _householdMemberRepository.GetHouseholdMemberAsync(householdMemberId);
        }

        public async Task<List<HouseholdMember>> GetHouseholdMembersAsync(string username)
        {

            var household = await _householdService.ReturnHouseholdAsync(username);

            if (household != null)
            {
                return await _householdMemberRepository.GetAllHouseholdMembersAsync(household.HouseholdId);
            }
            return null;

        }

        public async Task<HouseholdMember> JoinHouseholdAsync(string username, string shareCode, string memberName)
        {
            var household = await _householdService.ReturnHouseholdByShareCodeAsync(shareCode);
            var householdMemberId = await GenerateUniqueHouseholdMemberIdAsync();

            var householdMember = await _householdMemberRepository.CreateHouseholdMemberAsync(household, memberName, householdMemberId, username);
            if (householdMember != null) //Save successful
            {
                var user = await _authUsersRepository.GetUserAsync(username);
                user.HouseholdMember = householdMember;
                await _authUsersRepository.UpdateUserAsync(user);
                return householdMember;
            }
            else
            {
                throw new Exception("New Member failed to save to the database");
            }
        }

        public async Task<HouseholdMember> DeleteHouseholdMemberAsync(string username, string memberName)
        {
            var household = await _householdService.ReturnHouseholdAsync(username);
            var userToDelete = await _householdMemberRepository.FindHouseholdMemberAsync(household, memberName) ?? throw new Exception("User not found in database");

            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _choreLogRepository.DeleteChoreLogsByHouseholdMemberIdAsync(userToDelete.HouseholdMemberId);
                    var deleteFromDB = await _householdMemberRepository.DeleteHouseholdMemberAsync(userToDelete);
                    transaction.Complete();

                    if (deleteFromDB == 1) //Save successful
                    {
                        return userToDelete;
                    }
                    else
                    {
                        throw new Exception("Failed to delete user");
                    }
                }
                catch (Exception e)
                {
                    transaction.Dispose();
                    throw new Exception(e.Message);
                }



            }



        }
    }
}
