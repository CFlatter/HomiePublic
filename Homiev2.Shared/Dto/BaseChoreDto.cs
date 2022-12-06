using Homiev2.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Homiev2.Shared.Dto
{
    public class BaseChoreDto
    {
        [JsonPropertyName("choreId")]
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
        public DateTime? LastCompletedDate { get; set; }

        [JsonPropertyName("nextDueDate")]
        public DateTime NextDueDate { get; set; }

        [JsonPropertyName("createdBy")]
        public string? CreatedBy { get; set; }
    }
}
