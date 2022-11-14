using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homiev2.Shared.Models
{
    public class HouseholdMember 
    {
        public Guid? HouseholdId { get; set; }

        public string MemberName { get; set; }

        public Guid HouseholdMemberId { get; set; }
    }
}
