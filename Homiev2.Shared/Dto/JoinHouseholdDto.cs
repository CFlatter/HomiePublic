using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Homiev2.Shared.Dto
{
    public class JoinHouseholdDto : IDto
    {
        [Required]
        [JsonPropertyName("shareCode")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "This field must be 10 characters")]
        public string ShareCode { get; set; }
        [Required]
        [JsonPropertyName("memberName")]
        [MaxLength(20)]
        public string MemberName { get; set; }
    }
}
