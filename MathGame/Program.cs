
MathGame mathGame;

while(true)
{
    Console.Clear();
    ChooseMathGame();
}

void ChooseMathGame()
{
    Console.WriteLine("Choose a Math Game: ");
    Console.WriteLine("1 - Addition");
    Console.WriteLine("2 - Subtraction");
    Console.WriteLine("3 - Multiplication");
    Console.WriteLine("4 - Division");
    Console.WriteLine("5 - Show history");

    string? input=Console.ReadLine();
    Console.Clear();
    if (input == "1")
    {
        MathGame.IntroMessage = "Addition game has been selected.";
        mathGame = new Addition();
    }
    else if (input == "2")
    {
        MathGame.IntroMessage = "Subtraction game has been selected.";
        mathGame = new Subtraction();
    }
    else if (input == "3")
    {
        MathGame.IntroMessage = "Multiplication game has been selected.";
        mathGame = new Multiplication();
    }
    else if (input == "4")
    {
        MathGame.IntroMessage = "Division game has been selected.";
        mathGame = new Division();
    }
    else if (input == "5")
    {
        Console.WriteLine("Showing history: \n");
        MathGame.ShowHistory();
        Console.ReadKey();
    }
    else
    {
        ChooseMathGame();
    }
}

abstract class MathGame
{
    public static List<(string message, int firstNum, int secondNum, int result)> History { get; private set; }=new List<(string, int, int , int)> ();
    public static string? IntroMessage {  get; set; }
    public int[] Number { get; set; } = new int[2];
    public int Result { get; set; } = 0;
    public MathGame()
    {
        Console.WriteLine(IntroMessage);
        Console.WriteLine();
        for (int i = 0; i < 2; i++)
        {
            string index = i switch
            {
                0 => "first",
                1 => "second",
                _ => ""
            };
            Console.Write($"Enter {index} integer: ");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int num)) Number[i] = num;
            else
            {
                i--;
                Console.CursorLeft = 0;
                Console.CursorTop--;
                Console.Write(new string(' ',Console.WindowWidth));
                Console.CursorLeft=0;
            }
        }
    }
    protected void ShowResult()
    {
        History.Add((IntroMessage!, Number[0], Number[1], Result));
        Console.WriteLine($"\nThe result is {Result}.");
        Console.ReadKey();
    }
    public static void ShowHistory()
    {
        if(History.Count==0)
        {
            Console.WriteLine("No history data found.");
            return;
        }
        foreach(var data in History)
        {
            Console.WriteLine($"{History.IndexOf(data)+1}. {data.message}");
            Console.WriteLine($"   First integer: {data.firstNum}");
            Console.WriteLine($"   Second integer: {data.secondNum}");
            Console.WriteLine($"   Result: {data.result}\n");
        }
    }
}

class Addition:MathGame
{
    public Addition()
    {
        Result = Number[0] + Number[1];
        ShowResult();
    }
}

class Subtraction : MathGame
{
    public Subtraction()
    {
        Result = Number[0] - Number[1];
        ShowResult();
    }
}

class Multiplication : MathGame
{
    public Multiplication()
    {
        Result = Number[0] * Number[1];
        ShowResult();
    }
}

class Division : MathGame
{
    public Division()
    {
        try
        {
            if (Number[0]<0 || Number[0]>100)
            {
                Console.WriteLine("\nDividend (first integer) should be between 0 and 100.");
                Console.ReadKey();
                return;
            }

            if (Number[0] % Number[1] == 0)
            {
                Result = Number[0] / Number[1];
                ShowResult(); ;
            }
            else
            {
                Console.WriteLine("\nInvalid operation. Result does not show valid integer.");
                Console.ReadKey();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

