using Homiev2.Shared.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homiev2.Shared.Dto
{
    public class CompletedChoreDto : BaseDto
    {
        [Required]
        public Guid ChoreId { get; set; }
        [Required]
        public Guid HouseholdMemberId { get; set; }
        [Required]
        public bool Skipped { get; set; }
        [PastDate]
        public DateTime? CompletedDateTime { get; set; }
    }
}
