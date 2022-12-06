using Homiev2.Data.Contexts;
using Homiev2.Shared.Interfaces.Repositories;
using Homiev2.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Homiev2.Data.Repositories
{
    public class AuthUsersRepository : IAuthUsersRepository
    {
        private readonly Homiev2Context _context;

        public AuthUsersRepository(Homiev2Context context)
        {
            _context = context;
        }

        public async Task<AuthUser> GetUserAsync(string username)
        {
            return await _context.Users.Where(x => x.UserName == username).AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task<int> UpdateUserAsync(AuthUser user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync();
        }
    }
}
