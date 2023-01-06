using Homiev2.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Homiev2.Shared.Dto
{
    public class UpdateSimpleChoreDto : IDto
    {
        [JsonPropertyName("choreId")]
        public Guid? ChoreId { get; set; }

        [MaxLength(50)]
        [JsonPropertyName("taskName")]
        public string? TaskName { get; set; }

        [JsonPropertyName("points")]
        public byte? Points { get; set; }

        [RegularExpression("^(Day|Week|Month|Year){1}$")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("timeSpan")]
        public SimpleChoreTimeSpan? TimeSpan { get; set; }

        [Range(1,12,ErrorMessage ="Value must be between 1 & 12")]
        [JsonPropertyName("multiplier")]
        public short? Multiplier { get; set; }
        //[FutureDate]
        [JsonPropertyName("startDate")]
        public DateTime? StartDate { get; set; }
    }
}
