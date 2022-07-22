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
        int cursorPosition;
        char head = Convert.ToChar(1);
        char body = Convert.ToChar(3);
        string frames = " ";
        int WindowWidth = Console.WindowWidth--;
        int _WindowWidth = Console.WindowWidth-2;

        public SnakeGame()
        {
            cursorPosition = cursorPositionX * cursorPositionY;

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
                    frames = "";
                    cursorPosition -= Console.WindowWidth;
                    cursorPosition--;
                    for (int i = 0; i < cursorPosition; i++)
                    {
                        frames += " ";
                    }
                    frames += head;
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < Console.WindowWidth; j++)
                        {
                            frames += " ";
                        }
                        frames += body;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    frames = "";
                    cursorPosition += Console.WindowWidth;
                    cursorPosition++;
                    for (int i = 0; i < cursorPosition; i++)
                    {
                        frames += " ";
                    }
                    frames += body;
                    for (int i = 0; i < Console.WindowWidth; i++)
                    {
                        frames += " ";
                    }
                    frames += body;
                    for (int i = 0; i < Console.WindowWidth; i++)
                    {
                        frames += " ";
                    }
                    frames += head;
                    break;
                case ConsoleKey.LeftArrow:
                    frames = "";
                    for (int i = 0; i < cursorPosition; i++)
                    {
                        frames += " ";
                    }

                    frames += head;
                    cursorPosition--;
                    for (int i = 0; i < 2; i++)
                    {
                        frames += body;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    frames = "";
                    cursorPosition++;
                    for (int i = 0; i < cursorPosition; i++)
                    {
                        frames += " ";
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        frames += body;
                    }
                    frames += head;


                    break;
            }
        }

        protected override void Draw()
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    Console.Clear();
                    Console.Write(frames);
                    break;
                case ConsoleKey.DownArrow:
                    Console.Clear();
                    Console.Write(frames);
                    break;
                case ConsoleKey.LeftArrow:
                    Console.Clear();
                    Console.Write(frames);
                    break;
                case ConsoleKey.RightArrow:
                    Console.Clear();
                    Console.Write(frames);
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
