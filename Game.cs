using Hammerhand.Client;
using Hammerhand.Data;
using Hammerhand.Entities;
using Hammerhand.Entities.World;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hammerhand
{
    public class Game
    {
        private GameWindow _window;
        public Clock Clock = new Clock();
        public Random RNG = new Random();
        public float FrameDelta;
        public Stage Stage;
        public Options Options;
        public Assets Assets;
        public GameWindow Window {
            set {
                if (_window != null)
                {
                    _window.Close();
                }
                _window = value;
            }
            get { return _window; }
        }
        public float Time {
            get { return Clock.ElapsedTime.AsSeconds(); }
        }
        public Game(Options _options, Assets _assets)
        {
            Options = _options; Assets = _assets;
            Stage = new Stage(this);
            OpenWindow();
        }
        public void OpenWindow()
        {
            Window = new GameWindow(this);
            while (Window.IsOpen)
            {
                Window.RunOnce();
            }
        }
        public bool CanSee(QueuedEntity entity)
        {
            return entity.AlwaysVisible || GameWindow.FOV.Contains(entity.Position.X, entity.Position.Y);
        }
        public bool CanFall(Vector2f position)
        {
            if (position.Y > 172 && position.X > 170)
            {
                return position.Y < 172;
            }
            return position.Y < 192;
        }
        public const string Title = "Hammerhand: The Game";
        public const float Gravity = 1.0F;
        public const float Friction = 0.9F;
        public static readonly FloatRect EmptyHitbox = new FloatRect(0, 0, 0, 0);
        public static Game Instance;
        public static void Main(string[] args)
        {
            Instance = new Game(Options.Load(), new Assets());
        }
    }
}
