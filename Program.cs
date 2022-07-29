using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


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

        private GameWorld _gameWorld;
        private Camera _camera;
        int k = 1;
        int k2 = 2;
        int g = 0;
        int positionX = 0;
        int positionY = 0;
        int positionX_ = 0;
        int positionY_ = 0;
        Random rnd = new Random();
        int random = 1; 
        int random2 = 1;

        int Weigth = Console.WindowWidth - 5;
        int Height = Console.WindowHeight - 5;
        int positionX2 = 0;
        int positionY2 = 0;
        public SnakeGame()
        {
            positionX = rnd.Next(5, Weigth);
            positionY = rnd.Next(5, Height);
            positionX2 = rnd.Next(5, Weigth);
            positionY2 = rnd.Next(5, Height);
            positionX_ = positionX + 10;
            positionY_ = positionY + 10;


            _gameWorld = new GameWorld();
            _camera = new Camera(_gameWorld, new Rectangle(0, 0, Console.WindowWidth, Console.WindowHeight));
        }

        protected override void Input()
        {

        }

        protected override void Update()
        {
            _gameWorld.GetGameObjects().Clear();
            if (positionX_ == positionX2 && positionY_ == positionY2)
            {
                random = 3;
            }
            if (positionX == Weigth)
            {
                k = 1;
                random = rnd.Next(1, 3);
            }
            else if (positionX == 1)
            {
                k = 2;
                random = rnd.Next(1, 3);
            }
            if (positionY == Height)
            {
                k = 3;
                random = rnd.Next(1, 3);
            }
            else if (positionY == 3)
            {
                k = 4;
                random = rnd.Next(1, 3);
            }
            if (positionX2 == Weigth)
            {
                k2 = 1;
                random = rnd.Next(1, 3);
            }
            else if (positionX2 == 1)
            {
                k2 = 2;
                random = rnd.Next(1, 3);
            }
            if (positionY2 == Height)
            {
                k2 = 3;
                random = rnd.Next(1, 3);
            }
            else if (positionY2 == 3)
            {
                k2 = 4;
                random = rnd.Next(1, 3);
            }
            Console.Write(random);
            switch (random)
            {
                case 1:
                    switch (k)
                    {
                        case 1:
                            positionX--;
                            positionY--;
                            break;
                        case 2:
                            positionX++;
                            positionY++;
                            break;
                        case 3:
                            positionX++;
                            positionY--;
                            break;
                        case 4:
                            positionX--;
                            positionY++;
                            break;
                    }
                    switch (k2)
                    {
                        case 1:
                            positionX2--;
                            positionY2--;
                            break;
                        case 2:
                            positionX2++;
                            positionY2++;
                            break;
                        case 3:
                            positionX2++;
                            positionY2--;
                            break;
                        case 4:
                            positionX2--;
                            positionY2++;
                            break;
                    }
                    break;
                case 2:
                    switch (k)
                    {
                        case 1:
                            positionX--;
                            positionY++;
                            break;
                        case 2:
                            positionX++;
                            positionY--;
                            break;
                        case 3:
                            positionX--;
                            positionY--;
                            break;
                        case 4:
                            positionX++;
                            positionY++;
                            break;
                    }
                    switch (k2)
                    {
                        case 1:
                            positionX2--;
                            positionY2++;
                            break;
                        case 2:
                            positionX2++;
                            positionY2--;
                            break;
                        case 3:
                            positionX2--;
                            positionY2--;
                            break;
                        case 4:
                            positionX2++;
                            positionY2++;
                            break;
                    }
                    break;
                case 3:
                    positionX2++;
                    positionY2++;
                    positionX--;
                    positionY--;
                    break;
            }

        }

        protected override void Draw()
        {
            _gameWorld.AddGameObject(new Circle(5, new Point(positionX, positionY))); 
            _gameWorld.AddGameObject(new Circle(5, new Point(positionX2, positionY2)));

            Console.CursorVisible = false;
            string template = new string(' ', Console.WindowWidth);
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < Console.WindowHeight; i++)
            {
                sb.Append(template);
            }

            var shotResult = _camera.GetShot();
            var viewPort = _camera.GetViewPort();

            for (var y = 0; y < viewPort.Height; y++)
            {
                for (var x = 0; x < viewPort.Width; x++)
                {
                 Console.BackgroundColor = shotResult[x, y] ? ConsoleColor.DarkRed : ConsoleColor.Black;

                        random2 = rnd.Next(0, 4);
                        switch (random2)
                        {
                            case 0:
                                Console.BackgroundColor = shotResult[x, y] ? ConsoleColor.DarkRed : ConsoleColor.Black;
                                break;
                            case 1:
                                Console.BackgroundColor = shotResult[x, y] ? ConsoleColor.DarkGreen : ConsoleColor.Black;
                                break;
                            case 2:
                                Console.BackgroundColor = shotResult[x, y] ? ConsoleColor.DarkBlue : ConsoleColor.Black;
                                break;
                        }
                    
                    Console.Write(" ");
                    if (shotResult[x, y])
                    {
                        sb[y * (Console.WindowWidth) + x] = '0';
                    }
                }
            }

            //render
            Console.SetCursorPosition(0, 0);
            //            Console.Write(sb);
        }




    }

    public class Circle : GameObject
    {
        private int _radius;
        private Point _position;

        public Circle(int radius, Point position)
        {
            _radius = radius;
            _position = position;
        }

        public override bool GetHitTestResult(Point pos)
        {
            return Math.Pow(_radius, 2) >= (Math.Pow(_position.X - pos.X, 2) + Math.Pow(_position.Y - pos.Y, 2));
        }
    }
    #endregion

    #region GameWorlds Libray
    public class GameWorld
    {
        private List<GameObject> _gameObjects;

        public GameWorld()
        {
            _gameObjects = new List<GameObject>();
        }

        public void AddGameObject(GameObject gameObject)
        {
            if (!_gameObjects.Contains(gameObject))
            {
                _gameObjects.Add(gameObject);
            }
        }

        public List<GameObject> GetGameObjects()
        {
            return _gameObjects;
        }
    }


    public abstract class GameObject
    {
        public abstract bool GetHitTestResult(Point pos);

    }

    public class Camera
    {
        private GameWorld _world;
        private Rectangle _viewPort;

        public Camera(GameWorld gameWorld, Rectangle viewPort)
        {
            _world = gameWorld;
            _viewPort = viewPort;
        }

        public bool[,] GetShot()
        {
            bool[,] data = new bool[_viewPort.Width, _viewPort.Height];
            for (int y = _viewPort.Y; y < _viewPort.Bottom; y++)
            {
                for (int x = _viewPort.X; x < _viewPort.Right; x++)
                {
                    var gameObjects = _world.GetGameObjects();
                    for (int i = 0; i < gameObjects.Count; i++)
                    {
                        var gameObject = gameObjects[i];
                        if (gameObject.GetHitTestResult(new Point(x, y)))
                        {
                            data[x, y] = true;
                            break;
                        }

                    }

                }
            }

            return data;

        }

        public Rectangle GetViewPort()
        {
            return _viewPort;
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
        }

        protected abstract void Input();
        protected abstract void Update();
        protected abstract void Draw();

    }

    #endregion

}
