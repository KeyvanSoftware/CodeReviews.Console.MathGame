using System;
using System.Diagnostics;
using MathGame.Models;

namespace MathGame;

internal class GameEngine
{
    internal void ArithmeticGame(GameType type, GameDifficulty difficulty, Func<int, int, int> operation, string symbol)
    {
        Console.Clear();
        Helpers.PrintGameHeader(type);
        var score = 0;
        long startTime = Stopwatch.GetTimestamp();

        var max = Helpers.GetMax(symbol, difficulty);

        for (int i = 0; i < 5; i++)
        {
            (int firstNumber, int secondNumber)  = Helpers.GenerateQuestionNumbers(symbol, max);

            var isCorrect = Helpers.AskQuestionAndEvaluate(symbol, firstNumber, secondNumber, operation);

            if (isCorrect) score++;
        }

        var elapsedTime = Helpers.TimeTaken(startTime);

        Helpers.PrintEndMessages(score, elapsedTime);

        Helpers.AddToHistory(score, type, difficulty, elapsedTime);
    }

    internal void RandomGame(GameDifficulty difficulty)
    {
        Console.Clear();
        Helpers.PrintGameHeader(GameType.Random);
        var score = 0;
        long startTime = Stopwatch.GetTimestamp();
        var values = Enum.GetValues(typeof(GameType))
            .Cast<GameType>()
            .Where(g => g!= GameType.Random)
            .ToArray();

        for (int i = 0; i < 5; i++)
        {
            GameType randomGameType = values[Helpers.random.Next(values.Length)];
            string symbol = Helpers.arithmeticOperationsMap[randomGameType].symbol;
            Func<int, int, int> operation = Helpers.arithmeticOperationsMap[randomGameType].operation;

            var max = Helpers.GetMax(symbol, difficulty);

            (int firstNumber, int secondNumber) = Helpers.GenerateQuestionNumbers(symbol, max);

            var isCorrect = Helpers.AskQuestionAndEvaluate(symbol, firstNumber, secondNumber, operation);

            if (isCorrect) score++;
        }

        var elapsedTime = Helpers.TimeTaken(startTime);

        Helpers.PrintEndMessages(score, elapsedTime);

        Helpers.AddToHistory(score, GameType.Random, difficulty, elapsedTime);
    }
}
