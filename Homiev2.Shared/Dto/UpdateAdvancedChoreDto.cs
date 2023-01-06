using Homiev2.Shared.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Homiev2.Shared.Dto
{
    public class UpdateAdvancedChoreDto : BaseAdvancedChoreDto, IDto
    {
        [JsonPropertyName("choreId")]
        public Guid? ChoreId { get; set; }


        [JsonPropertyName("taskName")]
        [MaxLength(50)]
        public string? TaskName { get; set; }


        [JsonPropertyName("points")]
        public byte? Points { get; set; }

        [JsonPropertyName("dOfWeek")]
        [Range(1, 7, ErrorMessage = "Value must be between 1-7")]
        public override DayOfWeek? DOfWeek { get; set; }

        [JsonPropertyName("dOfMonth")]
        [Range(1, 31, ErrorMessage = "Value must be between 1-31")]
        public override byte? DOfMonth { get; set; }

        [JsonPropertyName("firstDOfMonth")]
        public override bool? FirstDOfMonth { get; set; }

        [JsonPropertyName("lastDOfMonth")]
        public override bool? LastDOfMonth { get; set; }

        [JsonPropertyName("startDate")]
        //[FutureDate] commented out as it made this field required TODO fix
        public DateTime? StartDate { get; set; }

   

    }
}
