namespace A3___Snake___Angabe
{
    internal class Game
    {
        
        const int WIDTH = 40;
        const int HEIGHT = 20;

        int[] snakeX = new int[3];
        int[] snakeY = new int[3];
        int length = 3;

        int direction = 1; // 0 = up, 1 = right, 2 = down, 3 = left

        int score = 0;//score implementieren 

        int foodX;
        int foodY;

        bool gameOver = false;

        Random rand = new Random();
        /// <summary>
        /// Starts the game
        /// </summary>
        public void Start()
        {
            Console.CursorVisible = false;

            //highscore fehlt

            InitSnake();
            GenerateFood();

            GameLoop();

            //Endgame fehlt

        }
        /// <summary>
        /// Initializes the snake 
        /// </summary>
        void InitSnake()
        {
            int startX = WIDTH / 2;
            int startY = HEIGHT / 2;

            for (int i = 0; i < length; i++)
            {
                snakeX[i] = startX - i;
                snakeY[i] = startY;
            }
        }
        void GameLoop()
        {
            while (!gameOver)
            {
                Input();
                MoveSnake();
                CheckCollision();
                CheckFood();
                Draw();

                Thread.Sleep(120);
            }
        }
        /// <summary>
        /// Keyboard Input for arrows.
        /// If up then you can't move down
        /// </summary>
        void Input()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow && direction != 2) direction = 0;
                if (key == ConsoleKey.RightArrow && direction != 3) direction = 1;
                if (key == ConsoleKey.DownArrow && direction != 0) direction = 2;
                if (key == ConsoleKey.LeftArrow && direction != 1) direction = 3;
            }
        }
        void MoveSnake()
        {
            for (int i = length - 1; i > 0; i--)
            {
                snakeX[i] = snakeX[i - 1];
                snakeY[i] = snakeY[i - 1];
            }

            if (direction == 0) snakeY[0]--;
            if (direction == 1) snakeX[0]++;
            if (direction == 2) snakeY[0]++;
            if (direction == 3) snakeX[0]--;
        }
        /// <summary>
        /// Checks if the snake collides with the wall or itself.
        /// </summary>
        void CheckCollision()
        {
            if (snakeX[0] <= 0 || snakeX[0] >= WIDTH - 1 || snakeY[0] <= 0 || snakeY[0] >= HEIGHT - 1)
            {
                gameOver = true;
            }
            for (int i = 1; i < length; i++)
            {
                if (snakeX[0] == snakeX[i] && snakeY[0] == snakeY[i])
                {
                    gameOver = true;
                }
            }
        }
        /// <summary>
        /// If the head of the snake eats the food it makes a sound
        /// </summary>
        void CheckFood()
        {
            if (snakeX[0] == foodX && snakeY[0] == foodY)
            {
                Console.Beep(); //Wollen wir ein bestimmtes Beep ton?
                score++;              //score++; fehlt
                GrowSnake();
                GenerateFood();
            }
        }
        /// <summary>
        /// The size of the array gets expanded
        /// </summary>
        void GrowSnake()
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
        void GenerateFood()
        {
            bool valid;
            do
            {
                valid = true;
                foodX = rand.Next(1, WIDTH - 1);
                foodY = rand.Next(1, HEIGHT - 1);

                for (int i = 0; i < length; i++)
                {
                    if (snakeX[i] == foodX || snakeY[i] == foodY)
                    {
                        valid = false;
                    }
                }
            } while (!valid);
        }
        /// <summary>
        /// Draws the map
        /// </summary>
        void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Punkte: " + score);
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
            
            bool IsSnakeBody(int x, int y)
            {
                for (int i = 1; i < length; i++)
                {
                    if (snakeX[i] == x && snakeY[i] == y)
                        return true;
                }
                return false;
            }
            //Endgame fehlt
        }
    }
}
