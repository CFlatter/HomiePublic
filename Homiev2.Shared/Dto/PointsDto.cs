using Homiev2.Shared.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Homiev2.Shared.Dto
{
    public class PointsDto : IDto
    {
        [Required]
        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }
        [Required]
        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }
      
    }
}
