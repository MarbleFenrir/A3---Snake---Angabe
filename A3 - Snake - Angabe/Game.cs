using static A3___Snake___Angabe.Directions;

namespace A3___Snake___Angabe;

internal enum Directions {
    up = 0,
    right = 1,
    down = 2,
    left = 3
}

internal class Game
{
    public Game(int seed)
    {
        rand = new(seed);
    }
    #region Properties
    const int WIDTH = 40;
    const int HEIGHT = 20;

    private int[] snakeX = new int[3];
    private int[] snakeY = new int[3];
    private int length = 3;
    private Random rand;
    private Directions direction = up;
    private Leaderboard lb = new();
    
    public ushort score = 0;//score implementieren 

    private int foodX;
    private int foodY;

    private bool gameOver;
    #endregion Properties
    /// <summary>
    /// Starts the game
    /// </summary>
    public bool Start()
    {
        Console.CursorVisible = false;
        do
        {
            
            score = 0;
            direction = up;
            Console.Clear();
            Console.Write("\x1b[3J"); // Clears the console's history
            int sel = StartMenu.Show();
            switch (sel)
            {
                case 0:
                    InitGame();
                    rand = new(Program.seed + rand.Next(score));
                    return true;
                case 1:
                    Console.Clear();
                    Console.WriteLine($"{lb}\nPress any Key to return");
                    Console.ReadKey();
                    break;
                case 2:
                    lb.WriteFile();
                    return false;

            }
        } while (true);
    }
    public static void WriteCenteredText(string s, int line)
    {
        int x = (Console.WindowWidth - s.Length) / 2;
        Console.SetCursorPosition(x, line);
        Console.WriteLine(s);
    }
    internal void InitGame()
    {
        OnScreenKeyboard kb = new();
        Console.Clear();
        InitSnake();
        Draw();
        GenerateFood();

        GameLoop();
        lb.Add(new(kb.Run("Please enter your name: "), score));
        Console.WriteLine(lb);
    }
    private void InitSnake()
    {
        int startX = WIDTH / 2;
        int startY = 3 * HEIGHT / 4;

        for (int i = 0; i < length; i++)
        {
            snakeX[i] = startX - i;
            snakeY[i] = startY;
        }
    }
    private void GameLoop()
    {
        do
        {
            Input();
            MoveSnake();
            gameOver = CheckCollision();
            CheckFood();
            Draw();

            Thread.Sleep(120);
        } while (!gameOver);
    }
    /// <summary>
    /// Keyboard Input for arrows.
    /// If up then you can't move down
    /// </summary>
    private void Input()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.UpArrow && direction != down) direction = up;
            else if (key == ConsoleKey.RightArrow && direction != left) direction = right;
            else if (key == ConsoleKey.DownArrow && direction != up) direction = down;
            else if (key == ConsoleKey.LeftArrow && direction != right) direction = left;
        }
    }
    private void MoveSnake()
    {
        for (int i = length - 1; i > 0; i--)
        {
            snakeX[i] = snakeX[i - 1];
            snakeY[i] = snakeY[i - 1];
        }

        if (direction == up) snakeY[0]--;
        else if (direction == right) snakeX[0]++;
        else if (direction == down) snakeY[0]++;
        else if (direction == left) snakeX[0]--;
    }
    /// <summary>
    /// Checks if the snake collides with the wall or itself.
    /// </summary>
    private bool CheckCollision()
    {
        if (snakeX[0] <= 0 || snakeX[0] >= WIDTH - 1 || snakeY[0] <= 0 || snakeY[0] >= HEIGHT - 1)
        {
            return true;
        }
        for (int i = 1; i < length; i++)
        {
            if (snakeX[0] == snakeX[i] && snakeY[0] == snakeY[i])
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// If the head of the snake eats the food it makes a sound
    /// </summary>
    private void CheckFood()
    {
        if (snakeX[0] == foodX && snakeY[0] == foodY)
        {
            Console.Beep();
            score++;
            ExpandSnakeArray();
            GenerateFood();
        }
    }
    /// <summary>
    /// The size of the array gets expanded
    /// </summary>
    private void ExpandSnakeArray()
    {
        if (length == snakeX.Length)
        {
            int[] newX = new int[snakeX.Length + 3];
            int[] newY = new int[snakeY.Length + 3];

            for (int i = 0; i < snakeX.Length; i++)
            {
                newX[i] = snakeX[i];
                newY[i] = snakeY[i];
            }

            snakeX = newX;
            snakeY = newY;
        }

        length++;
    }
    /// <summary>
    /// Generates a random spawnpoint for food and also checks if food isn't spawned on the snake
    /// </summary>
    private void GenerateFood()
    {
        bool valid;
        int tempX, tempY;
        do
        {
            valid = true;
            tempX = rand.Next(1, WIDTH - 1);
            tempY = rand.Next(1, HEIGHT - 1);

            for (int i = 0; i < length; i++)
            {
                if (snakeX[i] == tempX || snakeY[i] == tempY)
                {
                    valid = false;
                }
            }
        } while (!valid);
        foodX = tempX;
        foodY = tempY;
    }
    /// <summary>
    /// Draws the map
    /// </summary>
    private void Draw()
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Punkte: " + score);
        Console.WriteLine($"Seed: {Program.seed}");
        for (int y = 0; y < HEIGHT; y++)
        {
            for (int x = 0; x < WIDTH; x++)
            {
                if (x == 0 || x == WIDTH - 1 || y == 0 || y == HEIGHT - 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("#");
                    Console.ResetColor();
                }
                else if (x == snakeX[0] && y == snakeY[0])
                {
                    Console.Write("O");
                }
                else if (IsSnakeBody(x, y))
                {
                    Console.Write("o");
                }
                else if (x == foodX && y == foodY)
                {
                    Console.Write("*");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
        
    }
    private bool IsSnakeBody(int x, int y)
    {
        for (int i = 1; i < length; i++)
        {
            if (snakeX[i] == x && snakeY[i] == y)
                return true;
        }
        return false;
    }
}
