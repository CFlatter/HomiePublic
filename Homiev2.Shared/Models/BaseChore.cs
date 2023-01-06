using Homiev2.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Homiev2.Shared.Models
{
    public class BaseChore
    {
        [JsonPropertyName("choreId")]
        [Key]
        public Guid ChoreId { get; set; }

        [JsonPropertyName("taskName")]
        public string TaskName { get; set; }

        [JsonPropertyName("points")]
        public byte Points { get; set; }

        [JsonPropertyName("frequencyTypeId")]
        public FrequencyType FrequencyTypeId { get; set; }

        [JsonPropertyName("householdId")]
        public Guid HouseholdId { get; set; }

        [JsonPropertyName("lastCompletedDate")]
        public DateTime? LastCompletedDate { get; protected set; }

        [JsonPropertyName("nextDueDate")]
        public DateTime NextDueDate { get; protected set; }

        [JsonPropertyName("createdBy")]
        public string? CreatedBy { get; set; }



    }
}
