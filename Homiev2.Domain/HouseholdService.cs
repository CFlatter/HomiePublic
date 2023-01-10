using Homiev2.Shared.Interfaces.Repositories;
using Homiev2.Shared.Interfaces.Services;
using Homiev2.Shared.Models;
using Homiev2.Shared.Settings;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace Homiev2.Domain
{
    public class HouseholdService : IHouseholdService
    {
        private readonly IHouseholdRepository _householdRepository;
        private readonly IHouseholdMemberRepository _householdMemberRepository;
        private readonly IAuthUsersRepository _authUsersRepository;
        private readonly IOptions<ShareCode> _shareCodeOptions;

        public HouseholdService(IHouseholdRepository householdRepository, IHouseholdMemberRepository householdMemberRepository, IAuthUsersRepository authUsersRepository, IOptions<ShareCode> shareCodeOptions )
        {
            _householdRepository = householdRepository;
            _householdMemberRepository = householdMemberRepository;
            _authUsersRepository = authUsersRepository;
            _shareCodeOptions = shareCodeOptions;

        }

        public async Task<Household> ReturnHouseholdAsync(string username)
        {
            var user = await _authUsersRepository.GetUserAsync(username);
            try
            {
                return await _householdRepository.GetHouseholdAsync(user.HouseholdMember.HouseholdId.Value);
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        public async Task<Household> ReturnHouseholdByShareCodeAsync(string shareCode)
        {
            return await _householdRepository.GetHouseholdByShareCodeAsync(shareCode);
        }

        public async Task<int> CreateHouseholdAsync(Household household)
        {
            return await _householdRepository.CreateHouseholdAsync(household);
        }

        public async Task<int> DeleteHouseholdAsync(Household household)
        {
            return await _householdRepository.DeleteHouseholdAsync(household);
        }

        public string GenerateShareCode()
        {
            byte[] data = new byte[4 * _shareCodeOptions.Value.Size];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(_shareCodeOptions.Value.Size);
            for (int i = 0; i < _shareCodeOptions.Value.Size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % _shareCodeOptions.Value.Characters.Length;


                result.Append(_shareCodeOptions.Value.Characters.ToCharArray()[idx]);
            }

            return result.ToString();
        }

        private async Task<string> GenerateUniqueShareCodeAsync()
        {
            var shareCode = "";
            var isUniqueShareCode = 0;
            var i = 0;
            do
            {
                shareCode = GenerateShareCode();
                isUniqueShareCode = await _householdRepository.CheckUniqueShareCodeAsync(shareCode);
                i++;
            } while (isUniqueShareCode == 0 && i < 10);

            return shareCode;
        }

        public async Task<Guid> GenerateUniqueHouseholdIdAsync()
        {
            Guid householdId;
            var isUniqueHouseholdId = 0;
            var i = 0;
            do
            {
                householdId = Guid.NewGuid();
                isUniqueHouseholdId = await _householdRepository.CheckUniqueHouseholdIdAsync(householdId);
                i++;
            } while (isUniqueHouseholdId == 0 && i < 10);

            return householdId;
        }

    }
}
