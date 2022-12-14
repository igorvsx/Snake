using System;
using System.Threading;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Play();
            Console.ReadKey();
        }
    }
    class Game
    {
        public static int Width = (int)Walls.Width;
        public static int Height = (int)Walls.Height;

        ConsoleKeyInfo keyInfo;
        ConsoleKey consoleKey;

        Snake snake;
        Fruit fruit;

        bool IsLost;

        public Game()
        {
            Console.CursorVisible = false;
            snake = new Snake();
            fruit = new Fruit();
        }
        void StartGame()
        {
            Board.Write();
            Console.SetCursorPosition(Height / 5, Width / 2);
            Console.Write("Нажмите кнопку для запуска");
            Console.ReadKey();
            Console.Clear();

            keyInfo = new ConsoleKeyInfo();
            consoleKey = new ConsoleKey();

            IsLost = false;
            
            snake.Restart();
            fruit.Restart();
            Board.Write();
            
        }
        void Control()
        {
            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                consoleKey = keyInfo.Key;
            }
            switch (consoleKey)
            {
                case ConsoleKey.W:
                    if ((snake.Y[0] - snake.Y[1]) == 1) goto case ConsoleKey.S;
                    else snake.Shift(Snake.Direction.Top);
                    break;
                case ConsoleKey.S:
                    if ((snake.Y[0] - snake.Y[1]) == -1) goto case ConsoleKey.W;
                    else snake.Shift(Snake.Direction.Bottom);
                    break;
                case ConsoleKey.A:
                    if ((snake.X[0] - snake.X[1]) == 1) goto case ConsoleKey.D;
                    else snake.Shift(Snake.Direction.Left);
                    break;
                case ConsoleKey.D:
                    if ((snake.X[0] - snake.X[1]) == -1) goto case ConsoleKey.A;
                    else snake.Shift(Snake.Direction.Right);
                    break;
                //case ConsoleKey.Escape:
                //    Console.Clear();
                //    Environment.Exit(0);
                //    break;
                default:
                    if ((snake.Y[0] - snake.Y[1]) == 1) goto case ConsoleKey.S;
                    if ((snake.Y[0] - snake.Y[1]) == -1) goto case ConsoleKey.W;
                    if ((snake.X[0] - snake.X[1]) == 1) goto case ConsoleKey.D;
                    if ((snake.X[0] - snake.X[1]) == -1) goto case ConsoleKey.A;
                    break;
            }
        }
        void Logic()
        {
            Control();
            fruit.Logic(ref snake);
            snake.Logic(ref IsLost);
        }
        public void Play()
        {
            while (true)
            {
                StartGame();
                while (IsLost == false)
                {
                    Logic();
                    Thread.Sleep(100);
                }
                Console.SetCursorPosition(Height / 2, Width / 2);
                Console.Write("Игра закончена.");
                Environment.Exit(0);
            }
        }
    }
}
