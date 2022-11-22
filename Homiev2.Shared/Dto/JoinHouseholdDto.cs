using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Homiev2.Shared.Dto
{
    public class JoinHouseholdDto : BaseDto
    {
        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "This field must be 10 characters")]
        public string ShareCode { get; set; }
        [Required]
        [MaxLength(20)]
        public string MemberName { get; set; }
    }
}
