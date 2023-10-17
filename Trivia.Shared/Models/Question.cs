using Newtonsoft.Json;

namespace Trivia.Shared.Models
{
    public class Question
    {
        [JsonProperty("category")]
        public string Category { get; set; } = string.Empty;

        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty("difficulty")]
        public string Difficulty { get; set; } = string.Empty;

        [JsonProperty("question")]
        public string QuestionText { get; set; } = string.Empty;

        [JsonProperty("correct_answer")]
        public string CorrectAnswer { get; set; } = string.Empty;

        [JsonProperty("incorrect_answers")]
        public string[] IncorrectAnswers { get; set; } = Array.Empty<string>();
    }
}
