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
        protected static List<string> LevelsCompleted { get; set; } = new List<string>();
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
                1 => 101,
                2 => 251,
                3 => 501,
                4 => 751,
                5 => 1001,
                _ => 0
            };
            Player.Lives = 3;
            History = new List<(string, string, string)>();
            LevelsCompleted = new List<string>();
        }
        protected void ShowQuestion()
        {
            RoundInfo = $"Level {Level}\tRound: {Round}/10\tLives: {Player.Lives}/3";
            Console.WriteLine(RoundInfo);
            Console.WriteLine();
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
                Console.WriteLine(data.roundInfo);
                Console.WriteLine(data.roundQuestion);
                Console.WriteLine(data.result);
                Console.WriteLine();
            }
        }
        public static void ShowPrevData()
        {
            Console.Clear();
            Console.WriteLine("Showing previous game data: \n");
            if (PrevData.Count == 0)
            {
                Console.WriteLine("No previous game data found.");
                Console.ReadKey();
                return;
            }
            foreach (var data in PrevData)
            {
                Console.WriteLine(data.selectedOperation);
                foreach (string levelCompleted in data.levelsCompleted)
                {
                    Console.WriteLine(levelCompleted);
                }
                Console.WriteLine(data.outcome);
                Console.WriteLine();
            }
            Console.ReadKey();
        }
        protected virtual void Procedure() { }
        protected void EndGameOptions()
        {
            if (Round > 10)
            {
                string levelCompleted = $"You have completed Level {Level}!";
                if(LevelsCompleted.Count==0 || LevelsCompleted.Last()!=levelCompleted) LevelsCompleted.Add(levelCompleted);
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
                Outcome = "You lost.";
                Console.WriteLine(Outcome);
                PrevData.Add((SelectedOperation!, LevelsCompleted, Outcome));
            }
            Console.WriteLine("Press H to view rounds history or press other keys to choose new game.");
            ConsoleKey key=Console.ReadKey().Key;
            if (key == ConsoleKey.Enter && Level <= 5 && Player.Lives>0)
            {
                Level++;
                UpperLimit = Level switch
                {
                    1 => 101,
                    2 => 251,
                    3 => 501,
                    4 => 751,
                    5 => 1001,
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
                EndGameOptions();
            }
            if(Player.Lives>0 && Level<=5)
            {
                Console.WriteLine();
                Outcome = "You have exited the game.";
                Console.WriteLine(Outcome);
                Console.ReadKey();
                PrevData.Add((SelectedOperation!, LevelsCompleted, Outcome));
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

    class Subtraction : GameProcedure
    {
        public Subtraction()
        {
            SelectedOperation = "Subtraction game has been selected.";
            OperationSymbol = "-";
            Procedure();
        }
        protected override void Procedure()
        {
            Random random = new Random();
            while (Round <= 10 && Player.Lives > 0)
            {
                FirstNum = random.Next(0, UpperLimit);
                SecondNum = random.Next(0, UpperLimit);
                CorrectAnswer = FirstNum - SecondNum;
                ShowQuestion();
            }

            EndGameOptions();
        }
    }

    class Multiplication : GameProcedure
    {
        public Multiplication()
        {
            SelectedOperation = "Multiplication game has been selected.";
            OperationSymbol = "*";
            Procedure();
        }
        protected override void Procedure()
        {
            Random random = new Random();
            while (Round <= 10 && Player.Lives > 0)
            {
                FirstNum = random.Next(0, UpperLimit);
                SecondNum = random.Next(0, UpperLimit);
                CorrectAnswer = FirstNum * SecondNum;
                ShowQuestion();
            }

            EndGameOptions();
        }
    }
    class Division : GameProcedure
    {
        public Division()
        {
            SelectedOperation = "Division game has been selected.";
            OperationSymbol = "/";
            Procedure();
        }
        protected override void Procedure()
        {
            Random random = new Random();
            while (Round <= 10 && Player.Lives > 0)
            {
                FirstNum = random.Next(0, UpperLimit);
                GenerateDivisor();
                CorrectAnswer = FirstNum / SecondNum;
                ShowQuestion();
            }

            EndGameOptions();
        }

        private void GenerateDivisor()
        {
            Random random = new Random();
            SecondNum=random.Next(0, UpperLimit);
            if(SecondNum==0 || FirstNum%SecondNum!=0 || FirstNum<SecondNum)
            {
                GenerateDivisor();
            }
        }
    }

    class RandomOperation : GameProcedure
    {
        public RandomOperation()
        {
            SelectedOperation = "Random operation game has been selected.";
            Procedure();
        }
        protected override void Procedure()
        {
            int operation;
            Random random = new Random();
            while (Round <= 10 && Player.Lives > 0)
            {
                operation = random.Next(1, 5);
                FirstNum = random.Next(0, UpperLimit);
                GenerateSecondNum(operation);
                if (operation == 1)
                {
                    OperationSymbol = "+";
                    CorrectAnswer = FirstNum + SecondNum;
                }
                else if (operation == 2)
                {
                    OperationSymbol = "-";
                    CorrectAnswer = FirstNum - SecondNum;
                }
                else if (operation == 3)
                {
                    OperationSymbol = "*";
                    CorrectAnswer = FirstNum * SecondNum;
                }
                else if (operation == 4)
                {
                    OperationSymbol = "/";
                    CorrectAnswer = FirstNum / SecondNum;
                }
                ShowQuestion();
            }
            EndGameOptions();
        }
        private void GenerateSecondNum(int _operation)
        {
            Random random = new Random();
            SecondNum = random.Next(0, UpperLimit);
            if (_operation == 4)
            {
                while(SecondNum == 0 || FirstNum % SecondNum != 0 || FirstNum < SecondNum)
                {
                    SecondNum = random.Next(0, UpperLimit);
                }
            }
        }
    }
}
   