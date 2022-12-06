using System.Text.Json.Serialization;

namespace Homiev2.Shared.Models
{
    public class HouseholdMember 
    {
        [JsonPropertyName("householdId")]
        public Guid? HouseholdId { get; set; }

        [JsonPropertyName("memberName")]
        public string MemberName { get; set; }

        [JsonPropertyName("householdMemberId")]
        public Guid HouseholdMemberId { get; set; }
    }
}
