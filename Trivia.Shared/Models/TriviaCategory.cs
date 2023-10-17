using Newtonsoft.Json;

namespace Trivia.Shared.Models
{
    public class TriviaCategory
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }
}
