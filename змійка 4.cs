using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Леонід

namespace GameLoopExemple
{
    #region Program

    internal class Program
    {
        static void Main(string[] args)
        {
            var game = new SnakeGame();
            game.Start();
        }

    }
    #endregion

    #region SnakeGame

    public class SnakeGame : GameLoop
    {
        private Random random;

        int cursorPositionX = Console.WindowWidth / 2;
        int cursorPositionY = Console.WindowHeight / 2;
        int WindowWidth = Console.WindowWidth - 2;
        int WindowHeight = Console.WindowHeight - 2;
        char head = Convert.ToChar(1);
        char body = Convert.ToChar(3);
        char let = Convert.ToChar(4);
        char apple = Convert.ToChar(15);

        private int letPositionX;
        private int letPositionY;

        private int applePositionX;
        private int applePositionY;

        private int barrierPositionX = 0;
        private int barrierPositionY = 0;

        private int _barrierPositionX = Console.WindowWidth - 4;
        private int _barrierPositionY = Console.WindowHeight - 4;


        public SnakeGame()
        {
            random = new Random();
            letPositionX = random.Next(1, Console.WindowWidth);
            letPositionY = random.Next(1, Console.WindowHeight);

            applePositionX = random.Next(1, Console.WindowWidth);
            applePositionY = random.Next(1, Console.WindowHeight);
        }
        ConsoleKey key = ConsoleKey.NoName;


        protected override void Input()
        {
            key = ConsoleKey.NoName;
            //input
            if (Console.KeyAvailable)
            {
                key = Console.ReadKey(true).Key;

            }

        }

        protected override void Update()
        {
            //update
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    letPositionY++;
                    barrierPositionY++;
                    _barrierPositionY++;
                    applePositionY++;
                    break;
                case ConsoleKey.DownArrow:
                    letPositionY--;
                    barrierPositionY--;
                    _barrierPositionY--;
                    applePositionY--;
                    break;
                case ConsoleKey.LeftArrow:
                    letPositionX++;
                    barrierPositionX++;
                    _barrierPositionX++;
                    applePositionX++;
                    break;
                case ConsoleKey.RightArrow:
                    letPositionX--;
                    barrierPositionX--;
                    _barrierPositionX--;
                    applePositionX--;
                    break;
            }
            cursorPositionX = Console.WindowWidth / 2;
            cursorPositionY = Console.WindowHeight / 2;
            Console.SetCursorPosition(cursorPositionX, cursorPositionY);


            if (cursorPositionX == applePositionX && cursorPositionY == applePositionY)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 1; j <= 7; j++)
                    {
                        switch (j)
                        {
                            case 1:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                break;
                            case 2:
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                break;
                            case 3:
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                break;
                            case 4:
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                break;
                            case 5:
                                Console.ForegroundColor = ConsoleColor.Blue;
                                break;
                            case 6:
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                break;
                            case 7:
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                break;
                        }

                        Console.WriteLine("You Win");                        
                        System.Threading.Thread.Sleep(500);

                        Console.Clear();
                    }
                }
                Environment.Exit(0);
            }

            if (cursorPositionX == letPositionX && cursorPositionY == letPositionY)
            {
                Console.Write($"       you died        ");
                Environment.Exit(0);
            }

            else if (cursorPositionX == barrierPositionX || cursorPositionY == barrierPositionY)
            {
                Console.Write($"       you died        ");
                Environment.Exit(0);
            }

            else if (cursorPositionX == _barrierPositionX || cursorPositionY == _barrierPositionY)
            {
                Console.Write($"       you died        ");
                Environment.Exit(0);
            }
        }
        protected override void Draw()
        {
            //render

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    Console.Clear();
                    if (letPositionY > 0 && letPositionY < WindowWidth)
                    {
                        Console.SetCursorPosition(letPositionX, letPositionY);
                        Console.WriteLine(let);

                    }
                    if (applePositionY > 0 && applePositionY < WindowHeight)
                    {
                        Console.SetCursorPosition(applePositionX, applePositionY);
                        Console.WriteLine(apple);
                    }
                    Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                    Console.WriteLine(head);
                    Console.SetCursorPosition(cursorPositionX, cursorPositionY + 1);
                    Console.WriteLine(body);
                    Console.SetCursorPosition(cursorPositionX, cursorPositionY + 2);
                    Console.Write(body);

                    for (int i = 1; i <= WindowWidth; i++)
                    {
                        if (barrierPositionY <= WindowHeight && barrierPositionY > 0)
                        {
                            Console.SetCursorPosition(i, barrierPositionY);
                            Console.Write('=');
                        }
                        if (_barrierPositionY <= WindowHeight && _barrierPositionY > 0)
                        {
                            Console.SetCursorPosition(i, _barrierPositionY);
                            Console.Write('=');
                        }
                    }
                    for (int i = 1; i <= WindowHeight; i++)
                    {
                        if (barrierPositionX >= 0 && barrierPositionX < WindowHeight)
                        {
                            Console.SetCursorPosition(barrierPositionX, i);
                            Console.Write('=');
                        }
                        if (_barrierPositionX >= 0 && _barrierPositionX < WindowHeight)
                        {
                            Console.SetCursorPosition(_barrierPositionX, i);
                            Console.Write('=');
                        }
                    }
                    break;
                case ConsoleKey.DownArrow:
                    Console.Clear();
                    if (letPositionY > 0 && letPositionY < WindowWidth)
                    {
                        Console.SetCursorPosition(letPositionX, letPositionY);
                        Console.WriteLine(let);

                    }
                    if (applePositionY > 0 && applePositionY < WindowHeight)
                    {
                        Console.SetCursorPosition(applePositionX, applePositionY);
                        Console.WriteLine(apple);
                    }

                    Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                    Console.WriteLine(head);
                    Console.SetCursorPosition(cursorPositionX, cursorPositionY - 1);
                    Console.WriteLine(body);
                    Console.SetCursorPosition(cursorPositionX, cursorPositionY - 2);
                    Console.Write(body);
                    for (int i = 1; i <= WindowWidth; i++)
                    {
                        if (barrierPositionY <= WindowHeight && barrierPositionY > 0)
                        {
                            Console.SetCursorPosition(i, barrierPositionY);
                            Console.Write('=');
                        }
                        if (_barrierPositionY <= WindowHeight && _barrierPositionY > 0)
                        {
                            Console.SetCursorPosition(i, _barrierPositionY);
                            Console.Write('=');
                        }
                    }
                    for (int i = 1; i <= WindowHeight; i++)
                    {
                        if (barrierPositionX <= WindowWidth && barrierPositionX > 0)
                        {
                            Console.SetCursorPosition(barrierPositionX, i);
                            Console.Write('=');
                        }
                        if (_barrierPositionX <= WindowWidth && _barrierPositionX > 0)
                        {
                            Console.SetCursorPosition(_barrierPositionX, i);
                            Console.Write('=');
                        }
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    Console.Clear();
                    if (letPositionX > 0 && letPositionX < WindowWidth && letPositionY > 0)
                    {
                        Console.SetCursorPosition(letPositionX, letPositionY);
                        Console.WriteLine(let);
                    }
                    if (applePositionY > 0 && applePositionY < WindowHeight)
                    {
                        Console.SetCursorPosition(applePositionX, applePositionY);
                        Console.WriteLine(apple);
                    }
                    Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                    Console.Write($"{head}{body}{body}");
                    for (int i = 1; i <= WindowHeight; i++)
                    {
                        if (barrierPositionX <= WindowWidth && barrierPositionX > 0)
                        {
                            Console.SetCursorPosition(barrierPositionX, i);
                            Console.Write('=');
                        }
                        if (_barrierPositionX <= WindowWidth && _barrierPositionX > 0)
                        {
                            Console.SetCursorPosition(_barrierPositionX, i);
                            Console.Write('=');
                        }
                    }
                    break;
                case ConsoleKey.RightArrow:
                    Console.Clear();
                    if (letPositionX > 0 && letPositionX < WindowWidth)
                    {
                        Console.SetCursorPosition(letPositionX, letPositionY);
                        Console.WriteLine(let);
                    }
                    if (applePositionY > 0 && applePositionY < WindowHeight)
                    {
                        Console.SetCursorPosition(applePositionX, applePositionY);
                        Console.WriteLine(apple);
                    }
                    Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                    Console.Write($"{body}{body}{head}");
                    for (int i = 1; i <= WindowHeight; i++)
                    {
                        if (_barrierPositionX <= WindowWidth && _barrierPositionX > 0)
                        {
                            Console.SetCursorPosition(_barrierPositionX, i);
                            Console.Write('=');
                        }

                        if (barrierPositionX <= WindowWidth && barrierPositionX > 0)
                        {
                            Console.SetCursorPosition(barrierPositionX, i);
                            Console.Write('=');
                        }
                    }
                    break;

            }

        }
    }
    #endregion

    #region GameLoop Libray

    public abstract class GameLoop
    {
        public void Start()
        {
            while (true)
            {
                Timing();
                Input();
                Update();
                Draw();
            }
        }

        private void Timing()
        {
            //timing
            System.Threading.Thread.Sleep(50);
        }

        protected abstract void Input();
        protected abstract void Update();
        protected abstract void Draw();

    }

    #endregion
}
