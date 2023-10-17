using Newtonsoft.Json;

namespace Trivia.Shared.Models
{
    internal class CategoryResponse
    {
        [JsonProperty("trivia_categories")]
        public TriviaCategory[] TriviaCategories { get; set; } = Array.Empty<TriviaCategory>();
    }
}
