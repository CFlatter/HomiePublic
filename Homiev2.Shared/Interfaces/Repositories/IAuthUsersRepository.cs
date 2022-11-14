using Homiev2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homiev2.Shared.Interfaces.Repositories
{
    public interface IAuthUsersRepository
    {
        Task<AuthUser> GetUserAsync(string username);
        Task<int> UpdateUserAsync(AuthUser user);
    }
}
