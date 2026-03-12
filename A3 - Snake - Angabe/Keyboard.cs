namespace A3___Snake___Angabe;

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

    private string buffer = "";

    public string Run()
    {
        ConsoleKeyInfo key;

        while (true)
        {
            Draw();

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
                buffer = buffer.Substring(0, buffer.Length - 1);

            else if (key.Key == ConsoleKey.Enter)
            {
                if (Select())
                    break;
            }
        }

        return buffer;
    }

    private void Draw()
    {
        Console.Clear();
        Console.WriteLine("Input: " + buffer + "\n");

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
            buffer += value;
        }

        return false;
    }
}
