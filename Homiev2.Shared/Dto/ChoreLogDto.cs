using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Homiev2.Shared.Dto
{
    public class ChoreLogDto
    {
        [JsonPropertyName("choreLogId")]
        public Guid ChoreLogId { get; set; }

        [JsonPropertyName("choreId")]
        public Guid ChoreId { get; set; }

        [JsonPropertyName("householdMemberId")]
        public Guid HouseholdMemberId { get; set; }

        [JsonPropertyName("dateAdded")]
        public DateTime DateAdded { get; set; }

        [JsonPropertyName("dateCompleted")]
        public DateTime DateCompleted { get; set; }

        [JsonPropertyName("points")]
        public byte Points { get; set; }

        [JsonPropertyName("skipped")]
        public bool Skipped { get; set; }
    }
}
