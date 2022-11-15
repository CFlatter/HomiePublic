using Homiev2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homiev2.Mobile.Interfaces
{
    public interface IHouseholdService
    {
        Task<List<HouseholdMember>> GetAllHouseholdMembersAsync();
    }
}
