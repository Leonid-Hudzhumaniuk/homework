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

        int cursorPositionX = Console.WindowWidth / 2;
        int cursorPositionY = Console.WindowHeight / 2;

        public SnakeGame()
        {
            _gameWorld = new GameWorld();
            _camera = new Camera(_gameWorld, new Rectangle(0, 0, Console.WindowWidth, Console.WindowHeight));
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
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    _gameWorld.GetGameObjects().Clear();
                    cursorPositionY--;
                    break;
                case ConsoleKey.DownArrow:
                    _gameWorld.GetGameObjects().Clear();
                    cursorPositionY++;
                    break;
                case ConsoleKey.LeftArrow:
                    _gameWorld.GetGameObjects().Clear();
                    cursorPositionX--;
                    break;
                case ConsoleKey.RightArrow:
                    _gameWorld.GetGameObjects().Clear();
                    cursorPositionX++;
                    break;
            }
        

        }

        protected override void Draw()
        {
            
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
                    Console.BackgroundColor = shotResult[x, y] ? ConsoleColor.White : ConsoleColor.Black;
                    Console.Write(" ");
                    if (shotResult[x, y])
                    {
                        sb[y * (Console.WindowWidth) + x] = '0';
                    }
                }
            }

            _gameWorld.AddGameObject(new Circle(0, new Point(cursorPositionX, cursorPositionY))); 

            //render
            Console.SetCursorPosition(0, 0);

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
        public void RemoveGameObject(GameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
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
            System.Threading.Thread.Sleep(50);
        }

        protected abstract void Input();
        protected abstract void Update();
        protected abstract void Draw();

    }

    #endregion

}
