using Homiev2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homiev2.Shared.Interfaces.Services
{
    public interface IHouseholdCreationService
    {
        Task<Household> CreateHouseholdAsync(string userId, string householdName);
    }
}
