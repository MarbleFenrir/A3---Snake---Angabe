namespace A3___Snake___Angabe;

internal class StartMenu
{
    private static int selectedIndex = 0;

    private static readonly string[] options =
    {
        "Start Game",
        "Leaderboard",
        "Quit"
    };

    public static int Show()
    {
        ConsoleKey key;
        do
        {
            DrawMenu();

            var keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex + 1) % options.Length;
                    break;

                case ConsoleKey.Enter:
                    return selectedIndex;

                case ConsoleKey.Escape:
                    return 2;
            }
        } while (true);
    }

    private static void DrawMenu()
    {
        Console.Clear();
        Console.CursorVisible = false;
        if (Console.WindowHeight > 15)
            Console.WriteLine(@"  /$$$$$$  /$$   /$$  /$$$$$$  /$$   /$$ /$$$$$$$$
 /$$__  $$| $$$ | $$ /$$__  $$| $$  /$$/| $$_____/
| $$  \__/| $$$$| $$| $$  \ $$| $$ /$$/ | $$      
|  $$$$$$ | $$ $$ $$| $$$$$$$$| $$$$$/  | $$$$$   
 \____  $$| $$  $$$$| $$__  $$| $$  $$  | $$__/   
 /$$  \ $$| $$\  $$$| $$  | $$| $$\  $$ | $$      
|  $$$$$$/| $$ \  $$| $$  | $$| $$ \  $$| $$$$$$$$
 \______/ |__/  \__/|__/  |__/|__/  \__/|________/
                                                  
                                                  
                                                  ");

        int windowWidth = Console.WindowWidth;
        int windowHeight = Console.WindowHeight;

        for (int i = 0; i < options.Length; i++)
        {
            string text = options[i];
            int x = (windowWidth - text.Length) / 2;
            int y = (windowHeight / 2 - options.Length / 2) + i;

            Console.SetCursorPosition(x, y);

            if (i == selectedIndex)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine(text);
                Console.ResetColor();
            }
            else Console.WriteLine(text);
        }
    }
}
