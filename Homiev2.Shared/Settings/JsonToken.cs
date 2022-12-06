using System.Text.Json.Serialization;

namespace Homiev2.Shared.Settings
{
    public class JsonToken
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("expiration")]
        public DateTime Expiration { get; set; }
    }
}
