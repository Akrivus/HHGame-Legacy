using HHGame.Client;
using HHGame.Client.Modes;
using HHGame.Data;
using HHGame.Entities;
using HHGame.Entities.Player;
using HHGame.Entities.World;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHGame
{
    public class Game
    {
        public const string Title = "Hammerhand: The Game";
        public const float LevelLength = 240;
        public const float Gravity = 1.0F;
        public const float Friction = 0.9F;
        public static Random RNG = new Random();
        public static Game Instance;
        public float FrameDelta;
        public Options Options;
        public Assets Assets;
        private Clock _clock = new Clock();
        private GameWindow _window;
        private Mode _mode;
        public GameWindow Window {
            get { return _window; }
            set {
                if (_window != null)
                {
                    _window.Close();
                }
                _window = value;
                _mode.Load(_window);
            }
        }
        public Mode Mode
        {
            get { return _mode; }
            set
            {
                if (_mode != null)
                {
                    _mode.Exit(_window);
                }
                _mode = value;
            }
        }
        public float Time {
            get { return _clock.ElapsedTime.AsSeconds(); }
        }
        public Season Season
        {
            get
            {
                switch (DateTime.Now.Month)
                {
                    case 12:    // December
                    case 1:     // January
                    case 2:     // February
                        return Season.Winter;
                    case 3:     // March
                    case 4:     // April
                    case 5:     // May
                        return Season.Spring;
                    case 6:     // June
                    case 7:     // July
                    case 8:     // August
                        return Season.Summer;
                    default:
                        return Season.Autumn;
                }
            }
        }
        public Game(Options _options, Assets _assets)
        {
            Options = _options; Assets = _assets;
            Mode = new GameMode(this, Character.Isaiah);
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
            return GameWindow.FOV.Contains(entity.Position.X, entity.Position.Y) || entity.AlwaysVisible;
        }
        public bool CanFall(Vector2f position)
        {
            return position.Y < 192;
        }
        public static void Main(string[] args)
        {
            Instance = new Game(Options.Load(), new Assets());
        }
    }
}
