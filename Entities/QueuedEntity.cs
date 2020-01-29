using HHGame.Client;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace HHGame.Entities
{
    public class QueuedEntity
    {
        private RectangleShape _rect = new RectangleShape();
        private Color _color = Color.Transparent;
        private bool _alive = true;
        protected bool IgnoresAmbientLight = false;
        protected Sprite Sprite;
        protected Game Game;
        public bool AlwaysVisible = false;
        public FloatRect Bounds = Game.EmptyHitbox;
        public float Rotation = 0;
        public float Health = 1;
        public bool IsAlive
        {
            get
            {
                if (_alive)
                {
                    return Game.CanSee(this) && Health > 0;
                }
                else
                {
                    return _alive;
                }
            }
            set { _alive = value; }
        }
        public Color Color
        {
            get
            {
                if (IgnoresAmbientLight || !_color.Equals(Color.Transparent))
                {
                    return _color;
                }
                else
                {
                    byte value = (byte)(255 - Game.Time % Game.LevelLength / Game.LevelLength * 64);
                    byte blue = (byte) Math.Min(255, value / 191.0F * 255);
                    return new Color(value, value, blue);
                }
            }
            set { _color = value; }
        }
        public Vector2f BottomLeft { get { return new Vector2f(Bounds.Left, Bounds.Top + Bounds.Height); } }
        public Vector2f BottomRight { get { return new Vector2f(Bounds.Left + Bounds.Width, Bounds.Top + Bounds.Height); } }
        public Vector2f TopLeft { get { return new Vector2f(Bounds.Left, Bounds.Top); } }
        public Vector2f TopRight { get { return new Vector2f(Bounds.Left + Bounds.Width, Bounds.Top); } }
        public Vector2f Position
        {
            get
            {
                float _x = Bounds.Width / 2 + Bounds.Left;
                float _y = Bounds.Height / 2 + Bounds.Top;
                return new Vector2f(_x, _y);
            }
            set {
                Bounds.Left = value.X - Bounds.Width / 2;
                Bounds.Top = value.Y - Bounds.Height / 2;
            }
        }
        public QueuedEntity(Game _game)
        {
            Game = _game;
        }
        protected virtual void OnEnqueue(GameWindow window, GameWindow.Priority priority, ConcurrentQueue<QueuedEntity> queue) { }
        protected virtual void OnDequeue(GameWindow window, GameWindow.Priority priority, ConcurrentQueue<QueuedEntity> queue) { }
        protected virtual void OnUpdate(GameWindow.Priority priority, ConcurrentQueue<QueuedEntity> queue) { }
        protected virtual void OnDraw(GameWindow window, GameWindow.Priority priority) { }
        public void OnQueue(GameWindow window, GameWindow.Priority priority, ConcurrentQueue<QueuedEntity> queue)
        {
            
            if (IsAlive)
            {
                if (window.ShowHitboxes)
                {
                    window.Draw(GetHitbox());
                }
                OnUpdate(priority, queue);
                OnDraw(window, priority);
                queue.Enqueue(this);
                OnEnqueue(window, priority, queue);
            }
            else
            {
                OnDequeue(window, priority, queue);
            }
        }
        public RectangleShape GetHitbox()
        {
            _rect.Origin = new Vector2f(Bounds.Width / 2, Bounds.Height / 2);
            _rect.Size = new Vector2f(Bounds.Width, Bounds.Height);
            _rect.FillColor = new Color(0, 255, 0, 128);
            _rect.Position = Position; _rect.Rotation = Rotation;
            return _rect;
        }
        public void Move(float x, float y)
        {
            Position = new Vector2f(Position.X + x * Game.FrameDelta, Position.Y + y * Game.FrameDelta);
        }
        public void Rotate(float rotation)
        {
            Rotation += rotation * Game.FrameDelta;
        }
    }
}
