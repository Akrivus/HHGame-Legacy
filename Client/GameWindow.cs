using HHGame.Data;
using HHGame.Entities;
using HHGame.Entities.Items;
using HHGame.Entities.Player;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace HHGame.Client
{
    public class GameWindow : RenderWindow
    {
        public static readonly FloatRect FOV = new FloatRect(0, 0, 256, 256);
        public static readonly Vector2f Center = new Vector2f(128, 128);
        private Dictionary<Priority, Queue<QueuedEntity>> Queue = new Dictionary<Priority, Queue<QueuedEntity>>();
        private ConcurrentQueue<QueuedEntity> BackgroundItems = new ConcurrentQueue<QueuedEntity>();
        private ConcurrentQueue<QueuedEntity> MaingroundItems = new ConcurrentQueue<QueuedEntity>();
        private ConcurrentQueue<QueuedEntity> ForegroundItems = new ConcurrentQueue<QueuedEntity>();
        private ConcurrentQueue<QueuedEntity> ConcurrentQueue = new ConcurrentQueue<QueuedEntity>();
        private Clock _frameClock = new Clock();
        public bool ShowHitboxes = false;
        public Game Game;
        public GameWindow(Game game) : base(game.Options.FullscreenEnabled? VideoMode.FullscreenModes[0] : new VideoMode(game.Options.Width, game.Options.Height), Game.Title, game.Options.FullscreenEnabled? Styles.Fullscreen : Styles.Default)
        {
            Game = game;
            SetVerticalSyncEnabled(true);
            SetIcon(192, 192, Game.Assets.GrabImage("Icon").CopyToImage().Pixels);
            SetView(AdjustView());
            Queue.Add(Priority.Background, new Queue<QueuedEntity>());
            Queue.Add(Priority.Mainground, new Queue<QueuedEntity>());
            Queue.Add(Priority.Foreground, new Queue<QueuedEntity>());
            Closed += OnClosed;
            GainedFocus += OnGainedFocus;
            JoystickButtonPressed += OnJoystickButtonPressed;
            JoystickButtonReleased += OnJoystickButtonReleased;
            JoystickConnected += OnJoystickConnected;
            JoystickDisconnected += OnJoystickDisconnected;
            JoystickMoved += OnJoystickMoved;
            KeyPressed += OnKeyPressed;
            KeyReleased += OnKeyReleased;
            LostFocus += OnLostFocus;
            MouseButtonPressed += OnMouseButtonPressed;
            MouseButtonReleased += OnMouseButtonReleased;
            MouseEntered += OnMouseEntered;
            MouseLeft += OnMouseLeft;
            MouseMoved += OnMouseMoved;
            MouseWheelScrolled += OnMouseWheelScrolled;
            Resized += OnResized;
            TextEntered += OnTextEntered;
        }
        public View AdjustView()
        {
            return new View(Center, new Vector2f(255, (float) Size.Y / Size.X * 255.0F));
        }
        public void RunOnce()
        {
            QueuedEntity _item;
            Joystick.Update();
            DispatchEvents();
            Clear(Color.Black);
            Game.Mode.BeginQueue(this);
            while (BackgroundItems.TryDequeue(out _item))
            {
                _item.OnQueue(this, Priority.Background, ConcurrentQueue);
            }
            Swap(BackgroundItems, _item);
            while (MaingroundItems.TryDequeue(out _item))
            {
                _item.OnQueue(this, Priority.Mainground, ConcurrentQueue);
            }
            Swap(MaingroundItems, _item);
            while (ForegroundItems.TryDequeue(out _item))
            {
                _item.OnQueue(this, Priority.Foreground, ConcurrentQueue);
            }
            Swap(ForegroundItems, _item);
            Game.Mode.AfterQueue(this);
            Display();
        }
        private void Swap(ConcurrentQueue<QueuedEntity> queue, QueuedEntity _item)
        {
            while (ConcurrentQueue.TryDequeue(out _item))
            {
                queue.Enqueue(_item);
            }
        }
        public void Watch(Priority priority, QueuedEntity item)
        {
            switch (priority)
            {
                case Priority.Background:
                    BackgroundItems.Enqueue(item);
                    break;
                case Priority.Mainground:
                    MaingroundItems.Enqueue(item);
                    break;
                case Priority.Foreground:
                    ForegroundItems.Enqueue(item);
                    break;
            }
        }
        public override void Display()
        {
            float _fps = 1.0F / _frameClock.ElapsedTime.AsSeconds();
            SetTitle(string.Format("{0} [FPS: {1}]", Game.Title, _fps));
            Game.FrameDelta = 60.0F / _fps;
            _frameClock.Restart();
            base.Display();
        }
        public void DrawText(Text text, bool centered = false)
        {
            text.Origin = new Vector2f(centered? text.GetLocalBounds().Width / 2 : 0, text.GetLocalBounds().Height / 2);
            text.Scale = new Vector2f(0.1F, 0.1F);
            Draw(text);
        }
        private void OnClosed(object sender, EventArgs e)
        {
            Game.Mode.OnClosed(sender, e);
            Close();
        }
        private void OnGainedFocus(object sender, EventArgs e)
        {
            Game.Mode.OnGainedFocus(sender, e);
        }
        private void OnJoystickButtonPressed(object sender, JoystickButtonEventArgs e)
        {
            Game.Mode.OnJoystickButtonPressed(sender, e);
        }
        private void OnJoystickButtonReleased(object sender, JoystickButtonEventArgs e)
        {
            Game.Mode.OnJoystickButtonReleased(sender, e);
        }
        private void OnJoystickConnected(object sender, JoystickConnectEventArgs e)
        {
            Game.Mode.OnJoystickConnected(sender, e);
        }
        private void OnJoystickDisconnected(object sender, JoystickConnectEventArgs e)
        {
            Game.Mode.OnJoystickDisconnected(sender, e);
        }
        private void OnJoystickMoved(object sender, JoystickMoveEventArgs e)
        {
            Game.Mode.OnJoystickMoved(sender, e);
        }
        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            Game.Mode.OnKeyPressed(sender, e);
        }
        private void OnKeyReleased(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.F10:
                    ShowHitboxes = !ShowHitboxes;
                    break;
                case Keyboard.Key.F11:
                    Game.Options.FullscreenEnabled = !Game.Options.FullscreenEnabled;
                    Game.OpenWindow();
                    break;
                default:
                    Game.Mode.OnKeyReleased(sender, e);
                    break;
            }
        }
        private void OnLostFocus(object sender, EventArgs e)
        {
            Game.Mode.OnLostFocus(sender, e);
        }
        private void OnMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            Game.Mode.OnMouseButtonPressed(sender, e);
        }
        private void OnMouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            Game.Mode.OnMouseButtonReleased(sender, e);
        }
        private void OnMouseEntered(object sender, EventArgs e)
        {
            Game.Mode.OnMouseEntered(sender, e);
        }
        private void OnMouseLeft(object sender, EventArgs e)
        {
            Game.Mode.OnMouseLeft(sender, e);
        }
        private void OnMouseMoved(object sender, MouseMoveEventArgs e)
        {
            Game.Mode.OnMouseMoved(sender, e);
        }
        private void OnMouseWheelScrolled(object sender, MouseWheelScrollEventArgs e)
        {
            Game.Mode.OnMouseWheelScrolled(sender, e);
        }
        private void OnResized(object sender, SizeEventArgs e)
        {
            Game.Mode.OnResized(sender, e);
            SetView(AdjustView());
        }
        private void OnTextEntered(object sender, TextEventArgs e)
        {
            Game.Mode.OnTextEntered(sender, e);
        }
    }
}
