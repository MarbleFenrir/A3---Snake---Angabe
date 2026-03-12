namespace A3___Snake___Angabe
{
    internal class Program
    {
        static void Main()
        {
            int spalte = 0;
            int zeile = 0;
            
            while(true)
            {
                ConsoleKeyInfo key = Console.ReadKey(intercept: true);
                switch (key.Key)
                {
                    case ConsoleKey.RightArrow:
                        spalte++;
                        Console.SetCursorPosition(spalte, zeile);
                        break;
                    case ConsoleKey.LeftArrow:
                        spalte--;
                        Console.SetCursorPosition(spalte, zeile);
                        break;
                    case ConsoleKey.UpArrow:
                        zeile--;
                        Console.SetCursorPosition(spalte, zeile);
                        break;
                    case ConsoleKey.DownArrow:
                        zeile++;
                        Console.SetCursorPosition(spalte, zeile);
                        break;
                }
            }
            
        }
    }
}
