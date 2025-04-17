using System.Diagnostics;
using MathGame.Models;

namespace MathGame;

internal class Helpers
{
    internal static readonly Random random = new();
    internal static List<Game> games = new List<Game>
    {
        //new Game { Date = DateTime.Now.AddDays(1), Type = GameType.Addition, Score = 5 },
        //new Game { Date = DateTime.Now.AddDays(2), Type = GameType.Multiplication, Score = 4 },
        //new Game { Date = DateTime.Now.AddDays(3), Type = GameType.Division, Score = 4 },
        //new Game { Date = DateTime.Now.AddDays(4), Type = GameType.Subtraction, Score = 3 },
        //new Game { Date = DateTime.Now.AddDays(5), Type = GameType.Addition, Score = 1 },
        //new Game { Date = DateTime.Now.AddDays(6), Type = GameType.Multiplication, Score = 2 },
        //new Game { Date = DateTime.Now.AddDays(7), Type = GameType.Division, Score = 3 },
        //new Game { Date = DateTime.Now.AddDays(8), Type = GameType.Subtraction, Score = 4 },
        //new Game { Date = DateTime.Now.AddDays(9), Type = GameType.Addition, Score = 4 },
        //new Game { Date = DateTime.Now.AddDays(10), Type = GameType.Multiplication, Score = 1 },
        //new Game { Date = DateTime.Now.AddDays(11), Type = GameType.Subtraction, Score = 0 },
        //new Game { Date = DateTime.Now.AddDays(12), Type = GameType.Division, Score = 2 },
        //new Game { Date = DateTime.Now.AddDays(13), Type = GameType.Subtraction, Score = 5 },
    };

    internal static readonly Dictionary<GameType, (Func<int, int, int> operation, string symbol)> arithmeticOperationsMap = new()
        {
            { GameType.Addition, ((x, y) => x + y, "+") },
            { GameType.Subtraction, ((x, y) => x - y, "-") },
            { GameType.Multiplication, ((x, y) => x * y, "*") },
            { GameType.Division, ((x, y) => x / y, "/") }
        };


    internal static void PrintGames()
    {
        Console.Clear();
        Console.WriteLine("Games History");
        Console.WriteLine("---------------------------------------------------");
        foreach (var game in games)
        {
            Console.WriteLine($"{game.Date} - {game.Difficulty} {game.Type}: {game.Score}pts - {game.TimeTaken} time taken to complete");
        }
        Console.WriteLine("---------------------------------------------------\n");
        Console.WriteLine("Press any key to return to Main Menu");
        Console.ReadLine();
    }
    internal static void AddToHistory(int gameScore, GameType gameType, GameDifficulty difficulty, string timeTaken)
    {
        games.Add(new Game
        {
            Date = DateTime.Now,
            Score = gameScore,
            Type = gameType,
            Difficulty = difficulty,
            TimeTaken = timeTaken
        });
    }


    internal static int[] GetDivisionNumbers(int max)
    {
        var random = new Random();
        var firstNumber = random.Next(0, max);
        var secondNumber = random.Next(1, max);

        var result = new int[2];

        while (firstNumber % secondNumber != 0)
        {
            firstNumber = random.Next(0, max);
            secondNumber = random.Next(1, max);
        }

        result[0] = firstNumber;
        result[1] = secondNumber;

        return result;
    }

    internal static string? ValidateResult(string result)
    {
        while (string.IsNullOrEmpty(result) || !Int32.TryParse(result, out _))
        {
            Console.WriteLine("Your answer needs to be an integer. Try again.");
            result = Console.ReadLine();
        }
        return result;
    }

    internal static string GetName()
    {
        Console.WriteLine("Please type your name:");
        var name = Console.ReadLine();

        while (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Name can't be empty");
            name = Console.ReadLine();
        }
        return name;
    }

    internal static (int first, int second) GenerateQuestionNumbers(string symbol, int max)
    {
        int firstNumber;
        int secondNumber;

        if (symbol == "/")
        {
            var divisionNumbers = Helpers.GetDivisionNumbers(max);
            firstNumber = divisionNumbers[0];
            secondNumber = divisionNumbers[1];
        }
        else
        {
            firstNumber = random.Next(1, max);
            secondNumber = random.Next(1, max);
        }
        return (firstNumber, secondNumber);
    }

    internal static bool AskQuestionAndEvaluate(string symbol, int firstNumber, int secondNumber, Func<int, int, int> operation)
    {
        Console.WriteLine($"{firstNumber} {symbol} {secondNumber}");
        var isCorrect = false;
        var userInput = Console.ReadLine();
        userInput = Helpers.ValidateResult(userInput);

        if (int.Parse(userInput) == operation(firstNumber, secondNumber))
        {
            Console.WriteLine("\nYour answer was correct! Type any key for the next question");
            isCorrect = true;
        }
        else
        {
            Console.WriteLine("\nYour answer was incorrect. Type any key for the next question");
        }
        Console.ReadLine();
        return isCorrect;
    }

    internal static void PrintGameHeader(GameType type)
    {
        Console.WriteLine($"Welcome to the {type} Game!\n");
    }

    internal static int GetMax(string symbol, GameDifficulty difficulty)
    {
        int max = (symbol, difficulty) switch
        {
            ("/", GameDifficulty.Easy) => 99,
            ("/", GameDifficulty.Medium) => 999,
            ("/", GameDifficulty.Hard) => 9999,
            (_, GameDifficulty.Medium) => 99,
            (_, GameDifficulty.Hard) => 999,
            _ => 9
        };
        return max;
    }

    internal static string TimeTaken(long startTime)
    {
        TimeSpan elapsedTime = Stopwatch.GetElapsedTime(startTime);
        return elapsedTime.ToString(@"m\:ss\:ff");
    }

    internal static void PrintEndMessages(int score, string elapsedTime)
    {
        Console.Clear();
        Console.WriteLine($"Time taken to complete: {elapsedTime}");
        Console.WriteLine($"Game over. Your final score is {score}. Press any key to go back to the main menu.");
        Console.ReadLine();
    }
}
