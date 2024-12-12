using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGame
{
    public abstract class GameProcedure
    {
        public static int Level { get; set; }
        public static int UpperLimit { get; set; }
        public static List<(string roundInfo, string roundQuestion, string result)> History { get; private set; } = new List<(string, string, string)>();
        public static List<(string selectedOperation, List<string> levelsCompleted, string outcome)> PrevData { get; private set; } = new List<(string, List<string>, string)>();
        protected string? SelectedOperation { get; set; }
        protected List<string> LevelsCompleted { get; set; } = new List<string>();
        protected string? Outcome { get; set; }
        protected int FirstNum { get; set; }
        protected int SecondNum { get; set; }
        protected string? RoundInfo { get; set; }
        protected int CorrectAnswer { get; set; }
        protected string? Result { get; set; }
        protected string? RoundQuestion { get; set; }
        protected string? OperationSymbol { get; set; }
        protected int Round { get; set; } = 1;
        public GameProcedure()
        {
            UpperLimit = Level switch
            {
                1 => 100,
                2 => 250,
                3 => 500,
                4 => 750,
                5 => 100,
                _ => 0
            };
        }
        protected void ShowQuestion()
        {
            RoundInfo = $"Level {Level}\tRound: {Round}/10\tLives: {Player.Lives}/3\n";
            Console.WriteLine(RoundInfo);
            Console.Write($" {FirstNum} {OperationSymbol} {SecondNum} = ");
            string? input = Console.ReadLine();
            RoundQuestion = $" {FirstNum} {OperationSymbol} {SecondNum} = {input}";
            if (int.TryParse(input, out int num) && num == CorrectAnswer)
            {
                Round++;
                Player.Wins++;
                Result = "Correct!";
                Console.WriteLine(Result);
                Console.ReadKey();
                History.Add((RoundInfo, RoundQuestion, Result));
                Console.Clear();
            }
            else
            {
                Round++;
                Player.Lives--;
                Result = $"Wrong! Correct answer is {CorrectAnswer}";
                Console.WriteLine(Result);
                Console.ReadKey();
                History.Add((RoundInfo, RoundQuestion, Result));
                Console.Clear();
            }
        }
        protected static void ShowHistory()
        {
            if (History.Count == 0)
            {
                Console.WriteLine("No history data found.");
                return;
            }
            foreach (var data in History)
            {
            }
        }
        protected virtual void Procedure() { }
        protected void EndGameOptions()
        {
            if (Round > 10)
            {
                string levelCompleted = $"You have completed Level {Level}!";
                LevelsCompleted.Add(levelCompleted);
                Console.WriteLine($"{levelCompleted}\n");
                if(Level<=5)
                {
                    Console.WriteLine("Press ENTER to continue one level up.");
                }
                else 
                {
                    Outcome = "You have completed the game!";
                    Console.WriteLine(Outcome);
                    PrevData.Add((SelectedOperation!, LevelsCompleted, Outcome));
                }
            }
            if (Player.Lives == 0)
            {
                Outcome = "You lost";
                Console.WriteLine("Outcome");
                PrevData.Add((SelectedOperation!, LevelsCompleted, Outcome));
            }
            Console.WriteLine("Press H to view rounds history or press other keys to choose new game.");
            ConsoleKey key=Console.ReadKey().Key;
            if (key == ConsoleKey.Enter && Level <= 5 && Player.Lives>0)
            {
                Level++;
                UpperLimit = Level switch
                {
                    1 => 100,
                    2 => 250,
                    3 => 500,
                    4 => 750,
                    5 => 100,
                    _ => 0
                };
                Round = 1;
                Console.Clear();
                Procedure();
            }
            else if(key==ConsoleKey.H)
            {
                Console.Clear();
                ShowHistory();
            }
        }
    }

    class Addition : GameProcedure
    {
        public Addition()
        {
            SelectedOperation = "Addition game has been selected.";
            OperationSymbol = "+";
            Procedure();
        }
        protected override void Procedure()
        {
            Random random = new Random();
            while (Round <= 10 && Player.Lives > 0)
            {
                FirstNum = random.Next(0, UpperLimit);
                SecondNum = random.Next(0, UpperLimit);
                CorrectAnswer = FirstNum + SecondNum;
                ShowQuestion();
            }

            EndGameOptions();
        }
    }
}
   