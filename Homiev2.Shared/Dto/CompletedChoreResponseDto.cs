using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Homiev2.Shared.Dto
{
    public class CompletedChoreResponseDto : IDto
    {
        [JsonPropertyName("choreId")]
        public Guid ChoreId { get; set; }
    }
}
