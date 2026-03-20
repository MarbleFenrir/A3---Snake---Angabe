namespace A3___Snake___Angabe;
/// <summary>
/// Creates an <c>OnScreenKeyboard</c> object.<br/><br/>Startable with <paramref name="obj"></paramref>.Run()
/// </summary>
/// <param name="obj"></param>
public class OnScreenKeyboard
{
    private string[,] chars =
    {
        { "A","B","C","D","E","F","G","H","I","J","K","L","M"},
        { "N","O","P","Q","R","S","T","U","V","W","X","Y","Z"},
        { "a","b","c","d","e","f","g","h","i","j","k","l","m"},
        { "n","o","p","q","r","s","t","u","v","w","x","y","z"},
        { "0","1","2","3","4","5","6","7","8","9","+","-","="},
        { ".",",","!","?","@",":",";","[","]","(",")","_","/"},
        { "{","}","|","~","^","#","$","%","&","*"," ","BS","END"}
    };

    private int rows = 7;
    private int cols = 13;

    private int cursorRow = 0;
    private int cursorCol = 0;
    /// <summary>
    /// Gets the last input of the Keyboard object
    /// </summary>
    public string Input
    {
        get { return buffer; }
    }
    private string buffer = "";
    /// <summary>
    /// Starts the OnScreenKeyboard. Returns a non-nullable string of the Users input.<br/><br/>Note that this also clears the screen.
    /// </summary>
    /// <returns></returns>
    public string Run(string title = "Input: ")
    {
        ConsoleKeyInfo key;
        Console.CursorVisible = false;
        while (true)
        {
            Draw(title);

            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.LeftArrow)
                MoveLeft();

            else if (key.Key == ConsoleKey.RightArrow)
                MoveRight();

            else if (key.Key == ConsoleKey.UpArrow)
                MoveUp();

            else if (key.Key == ConsoleKey.DownArrow)
                MoveDown();

            else if (key.Key == ConsoleKey.Backspace && buffer.Length > 0)
            {
                if (key.Modifiers == ConsoleModifiers.None)
                    buffer = buffer.Substring(0, buffer.Length - 1);
                else if (key.Modifiers == ConsoleModifiers.Control)
                {
                    if (buffer.IndexOf(' ') != -1)
                        buffer = buffer.Split(" ", 2)[0];
                    else buffer = "";
                }

            }
            else if (key.Key == ConsoleKey.Escape)
            {
                cursorCol = cols - 1;
                cursorRow = rows - 1;
            }

            else if (key.Key == ConsoleKey.Enter)
            {
                if (Select())
                    break;
            }
            else
            {
                if (buffer.Length <= 10)
                    buffer += key.KeyChar;
            }
        }
        Console.CursorVisible = true;
        Console.Clear();
        return buffer;
    }
    public string RunAsync(string title = "Input: ")
    {
        ConsoleKeyInfo key;
        Console.CursorVisible = false;
        while (true)
        {
            DrawAsync(title);

            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.LeftArrow)
                MoveLeft();

            else if (key.Key == ConsoleKey.RightArrow)
                MoveRight();

            else if (key.Key == ConsoleKey.UpArrow)
                MoveUp();

            else if (key.Key == ConsoleKey.DownArrow)
                MoveDown();

            else if (key.Key == ConsoleKey.Backspace && buffer.Length > 0)
            {
                if (key.Modifiers == ConsoleModifiers.None)
                    buffer = buffer.Substring(0, buffer.Length - 1);
                else if (key.Modifiers == ConsoleModifiers.Control)
                {
                    if (buffer.IndexOf(' ') != -1)
                        buffer = buffer.Split(" ", 2)[0];
                    else buffer = "";
                }

            }
            else if (key.Key == ConsoleKey.Escape)
            {
                cursorCol = cols - 1;
                cursorRow = rows - 1;
            }

            else if (key.Key == ConsoleKey.Enter)
            {
                if (Select())
                    break;
            }
            else
            {
                if (buffer.Length <= 10)
                    buffer += key.KeyChar;
            }
        }
        Console.CursorVisible = true;
        Console.Clear();
        return buffer;
    }

    private void Draw(string title)
    {
        Console.Clear();
        Console.WriteLine(title + buffer + "\n");
        string output = "";
        int r;
        int c;

        for (r = 0; r < rows; r++)
        {
            for (c = 0; c < cols; c++)
            {
                if (r == cursorRow && c == cursorCol)
                {
                    output += "[";
                    output +=chars[r, c];
                    output += "]";
                }
                else
                {
                    output += " ";
                    output += chars[r, c];
                    output += " ";
                }
            }

            output += "\n";
        }
        Console.WriteLine(output);
    }
    private void DrawAsync(string title)
    {
        Console.Clear();
        Console.WriteLine(title + buffer + "\n");
        int r;
        int c;

        for (r = 0; r < rows; r++)
        {
            for (c = 0; c < cols; c++)
            {
                if (r == cursorRow && c == cursorCol)
                {
                    Console.Write("[");
                    Console.Write(chars[r, c]);
                    Console.Write("]");
                }
                else
                {
                    Console.Write(" ");
                    Console.Write(chars[r, c]);
                    Console.Write(" ");
                }
            }

            Console.WriteLine();
        }
    }

    private void MoveLeft()
    {
        cursorCol--;
        if (cursorCol < 0)
            cursorCol = cols - 1;
    }

    private void MoveRight()
    {
        cursorCol++;
        if (cursorCol >= cols)
            cursorCol = 0;
    }

    private void MoveUp()
    {
        cursorRow--;
        if (cursorRow < 0)
            cursorRow = rows - 1;
    }

    private void MoveDown()
    {
        cursorRow++;
        if (cursorRow >= rows)
            cursorRow = 0;
    }

    private bool Select()
    {
        string value = chars[cursorRow, cursorCol];

        if (value == "BS")
        {
            if (buffer.Length > 0)
                buffer = buffer.Substring(0, buffer.Length - 1);
        }
        else if (value == "END")
        {
            return true;
        }
        else
        {
            if (buffer.Length <= 10)
                buffer += value;
        }

        return false;
    }
}
