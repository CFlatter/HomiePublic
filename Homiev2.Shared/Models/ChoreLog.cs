using System;
using System.Collections.Generic;
using System.Text;

namespace Homiev2.Shared.Models
{
    public class ChoreLog
    {
        public Guid ChoreLogId { get; set; }
        public Guid ChoreId { get; set; }
        public Guid HouseholdMemberId { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateCompleted { get; set; }
        public byte Points { get; set; }
        public bool Skipped { get; set; }

    }
}
