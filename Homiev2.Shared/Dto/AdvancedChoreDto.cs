using Homiev2.Shared.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homiev2.Shared.Dto
{
    public class AdvancedChoreDto : BaseDto
    {
        public Guid? ChoreId { get; set; }
        [Required]
        [MaxLength(50)]
        public string TaskName { get; set; }
        [Required]
        public byte Points { get; set; }
        [Required]
        [RegularExpression("^(1|2|3|4){1}$")]
        public byte AdvancedType { get; set; }
        [Range(1, 7, ErrorMessage = "Value must be between 1-7")]
        public int? DOfWeek { get; set; }
        [Range(1, 7, ErrorMessage = "Value must be between 1-31")]
        public byte? DOfMonth { get; set; }
        public bool? FirstDOfMonth { get; set; }
        public bool? LastDOfMonth { get; set; }
        //[FutureDate]
        public DateTime? StartDate { get; set; }
    }
}
