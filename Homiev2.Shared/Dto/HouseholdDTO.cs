using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Homiev2.Shared.Dto
{
    public class HouseholdDTO : IDto
    {
        [Required]
        [JsonPropertyName("householdName")]
        [MaxLength(20)]
        public string HouseholdName { get; set; }
    }
}
