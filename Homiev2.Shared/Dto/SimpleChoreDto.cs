using Homiev2.Shared.Validation;
using System.ComponentModel.DataAnnotations;

namespace Homiev2.Shared.Dto
{
    public class SimpleChoreDto : IDto
    {
        public Guid? ChoreId { get; set; }
        [Required]
        [MaxLength(50)]
        public string TaskName { get; set; }
        [Required]
        public byte Points { get; set; }
        [Required]
        [RegularExpression("^(1|7|30|365){1}$")]
        public short Timespan { get; set; }
        [Required]
        [Range(1,12,ErrorMessage ="Value must be between 1 & 12")]
        public short Multiplier { get; set; }
        //[FutureDate]
        public DateTime? StartDate { get; set; }
    }
}
