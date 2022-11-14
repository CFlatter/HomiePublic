using Homiev2.Data.Contexts;
using Homiev2.Shared.Interfaces.Repositories;
using Homiev2.Shared.Models;
using Homiev2.Shared.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.X509.Qualified;

namespace Homiev2.Data.Repositories
{
    public class ChoreFrequencyRepository : IChoreFrequencyRepository
    {
        private readonly Homiev2Context _context;

        public ChoreFrequencyRepository(Homiev2Context context)
        {
            _context = context;
        }

        public async Task<T> GetScheduleFromDB<T>(Guid choreId) where T : BaseFrequency
        {

            try
            {
                return await _context.FindAsync<T>(choreId);
            }
            catch (Exception)
            {

                throw new Exception("Invalid schedule type");
            }              
        }
    }

}

