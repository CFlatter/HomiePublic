using Homiev2.Shared.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Homiev2.Shared.Dto
{
    public class CompletedChoreDto : IDto
    {
        [Required]
        [JsonPropertyName("choreId")]
        public Guid ChoreId { get; set; }
        [Required]
        [JsonPropertyName("householdMemberId")]
        public Guid HouseholdMemberId { get; set; }
        [Required]
        [JsonPropertyName("skipped")]
        public bool Skipped { get; set; }
        [PastDate]
        [JsonPropertyName("completedDateTime")]
        public DateTime? CompletedDateTime { get; set; }
    }
}
