using Trivia.Shared.Models;
using Trivia.Shared.Services;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

OpenTriviaDbService client = new();
TriviaCategory[] categories = await client.GetCategoriesAsync();

app.MapGet("/categories", () => categories)
    .WithName("GetCategories")
    .WithOpenApi();

app.MapGet("/difficulties", () =>
    {
        //Array difficulties = Enum.GetValues(typeof(OpenTriviaDbEnums.Difficulty));

        Dictionary<string, int> difficulties = new();
        foreach (OpenTriviaDbEnums.Difficulty difficulty in Enum.GetValues(typeof(OpenTriviaDbEnums.Difficulty)))
        {
            difficulties.Add(difficulty.ToString(), Convert.ToInt16(difficulty));
        }

        return difficulties;
    })
    .WithName("GetDifficulties")
    .WithOpenApi();

app.MapGet("/types", () =>
    {
        //Array questionTypes = Enum.GetValues(typeof(OpenTriviaDbEnums.QuestionType));
        Dictionary<string, int> questionTypes = new();
        foreach (OpenTriviaDbEnums.QuestionType questionType in Enum.GetValues(typeof(OpenTriviaDbEnums.QuestionType)))
        {
            questionTypes.Add(questionType.ToString(), Convert.ToInt16(questionType));
        }
        return questionTypes;
    })
    .WithName("GetQuestionTypes")
    .WithOpenApi();

app.MapGet("/questions/{numOfQuestions}/{categoryId}/{questionType}/{difficulty}", 
        async (
            int numOfQuestions, 
            int categoryId, 
            OpenTriviaDbEnums.QuestionType questionType, 
            OpenTriviaDbEnums.Difficulty difficulty) =>
    {
        TriviaCategory category = categories.First();
        Question[] questions = await client.GetQuestionsAsync(
            numOfQuestions, 
            categoryId, 
            questionType, 
            difficulty);
        return questions;
    })
    .WithName("GetTriviaQuestions")
    .WithOpenApi();

app.Run();
