using System;

namespace Homiev2.Shared.Models
{
    public class Household
    {
        public Guid HouseholdId { get; set; }

        public string HouseholdName { get; set; }

        public string UserId { get; set; }

        public string ShareCode { get; set; }

    }
}
