using MathGame.Models;

namespace MathGame;

internal class Menu
{
    GameEngine engine = new();
    internal void ShowMenu(string name, DateTime date)
    {
        Console.Clear();
        Console.WriteLine($"Hello {name}. It's {date}. This is your math's game.");
        Console.WriteLine("Press any key to show menu");
        Console.ReadLine();
        Console.WriteLine("\n");

        var isGameOn = true;
        var validDifficulty = false;
        var gameDifficulty = GameDifficulty.Easy;

        do
        {
            Console.Clear();
            Console.WriteLine("""
                What difficulty would you like to play today? Choose from the options below: 
                E - Easy
                M - Medium
                H - Hard
                """);

            var difficultySelected = Console.ReadLine();

            if (difficultySelected != null)
            {
                switch (difficultySelected.Trim().ToLower())
                {
                    case "e":
                        gameDifficulty = GameDifficulty.Easy;
                        validDifficulty = true;
                        break;
                    case "m":
                        gameDifficulty = GameDifficulty.Medium;
                        validDifficulty = true;
                        break;
                    case "h":
                        gameDifficulty = GameDifficulty.Hard;
                        validDifficulty = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }
            }
        } while (!validDifficulty);

        do
        {
            Console.Clear();
            Console.WriteLine("""
                What game would you like to play today? Choose from the options below:
                V - View Previous Games
                A - Addition
                S - Subtraction
                M - Multiplication
                D - Division
                R - Random
                Q - Quit the program
                """);
            Console.WriteLine("---------------------------------------------------");

            var gameSelected = Console.ReadLine();

            if (gameSelected != null)
            {

                switch (gameSelected.Trim().ToLower())
                {
                    case "v":
                        Helpers.PrintGames();
                        break;
                    case "a":
                        engine.ArithmeticGame(GameType.Addition, gameDifficulty, (x, y) => x + y, "+");
                        break;
                    case "s":
                        engine.ArithmeticGame(GameType.Subtraction, gameDifficulty, (x, y) => x - y, "-");
                        break;
                    case "m":
                        engine.ArithmeticGame(GameType.Multiplication, gameDifficulty, (x, y) => x * y, "*");
                        break;
                    case "d":
                        engine.ArithmeticGame(GameType.Division, gameDifficulty, (x, y) => x / y, "/");
                        break;
                    case "r":
                        engine.RandomGame(gameDifficulty);
                        break;
                    case "q":
                        Console.WriteLine("Goodbye");
                        isGameOn = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid Input");
                        Console.WriteLine("Press any key to show menu");
                        Console.ReadLine();
                        break;
                }
            }
        } while (isGameOn);
    }
}
