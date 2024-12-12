
using MathGame;


while(true)
{
    Console.Clear();
    ChooseMathGame();
}

void ChooseMathGame()
{
    Console.WriteLine(@"Welcome to Math Game!
Every round, you will be given with a math question with two integers, and you should provide the correct answer within 15 seconds (Timer running in background).
You only have three lives, so you should answer carefully.
");
    Console.WriteLine("Choose math operation.");
    Console.WriteLine("1 - Addition");
    Console.WriteLine("2 - Subtraction");
    Console.WriteLine("3 - Multiplication");
    Console.WriteLine("4 - Division");
    Console.WriteLine("5 - Random Operation");
    Console.WriteLine("6 - Show previous games");
    GetOperation();
}

void GetDifficulty()
{
    Console.Write("Select from 1 to 5: ");
    string? input= Console.ReadLine();
    if (int.TryParse(input, out int num) && num >= 1 && num <= 5)
    {
        GameProcedure.Level = num;
    }
    else
    {
        Console.CursorLeft = 0;
        Console.CursorTop--;
        Console.Write(new string(' ',Console.WindowWidth));
        Console.CursorLeft = 0;
        GetDifficulty();
    }
}

void GetOperation()
{
    GameProcedure? gameProcedure;
    string? input = Console.ReadLine();
    if (int.TryParse(input, out int num) && num >= 1 && num <= 6)
    {
        if(num==6)
        {
            GameProcedure.ShowPrevData();
            return;
        }
        Console.WriteLine("\nChoose starting level.");
        GetDifficulty();
        Console.Clear();
        gameProcedure = num switch
        {
            1 => new Addition(),
            2 => new Subtraction(),
            3 => new Multiplication(),
            4 => new Division(),
            5 => new RandomOperation(),
            _ => null
        };
    }
    else
    {
        Console.CursorLeft = 0;
        Console.CursorTop--;
        Console.Write(new string(' ', Console.WindowWidth));
        Console.CursorLeft = 0;
        GetOperation();
    }
}