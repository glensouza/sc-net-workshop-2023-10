using Newtonsoft.Json;

namespace Trivia.Shared.Models
{
    internal class TokenResponse
    {
        [JsonProperty("response_code")]
        public long ResponseCode { get; set; }

        [JsonProperty("response_message")]
        public string ResponseMessage { get; set; } = string.Empty;

        [JsonProperty("token")]
        public string Token { get; set; } = string.Empty;
    }
}
