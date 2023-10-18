using Trivia.Shared.Models;
using Trivia.Shared.Services;

OpenTriviaDbService client = new();
TriviaCategory[] categories = await client.GetCategoriesAsync();
TriviaCategory category = categories.First();
Question[] questions = await client.GetQuestionsAsync(10, Convert.ToInt16(category.Id), OpenTriviaDbEnums.QuestionType.MultiChoice, OpenTriviaDbEnums.Difficulty.Easy);
Question question = questions.First();
Console.WriteLine(question.QuestionText);
Console.WriteLine(question.CorrectAnswer);
Console.WriteLine(question.IncorrectAnswers[0]);
Console.WriteLine(question.IncorrectAnswers[1]);
Console.WriteLine(question.IncorrectAnswers[2]);

ConsoleColor backgroundColor = Console.BackgroundColor;
ConsoleColor foregroundColor = Console.ForegroundColor;
Console.BackgroundColor = ConsoleColor.DarkRed;
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine($"Session Token: {client.SessionToken}");
Console.BackgroundColor = backgroundColor;
Console.ForegroundColor = foregroundColor;
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
