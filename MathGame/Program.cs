
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

    string? input=Console.ReadLine();
    Console.Clear();
    if (input == "1")
    {
        MathGame.IntroMessage = "Addition game has been selected.\n";
        mathGame = new Addition();
    }
    else if (input == "2")
    {
        MathGame.IntroMessage = "Subtraction game has been selected.\n";
        mathGame = new Subtraction();
    }
    else if (input == "3")
    {
        MathGame.IntroMessage = "Multiplication game has been selected.\n";
        mathGame = new Multiplication();
    }
    else if (input == "4")
    {
        MathGame.IntroMessage = "Division game has been selected.\n";
        mathGame = new Division();
    }
    else
    {
        ChooseMathGame();
    }
}

abstract class MathGame
{
    public static string? IntroMessage {  get; set; }
    public int[] Number { get; set; } = new int[2];
    public int Result { get; set; } = 0;
    public MathGame()
    {
        Console.WriteLine(IntroMessage);
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
        Console.WriteLine($"\nThe result is {Result}.");
        Console.ReadKey();
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
            Result = Number[0] / Number[1];
        }
        catch (Exception e) { Console.WriteLine(e.Message); }
        ShowResult();
    }
}