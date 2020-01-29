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
        public bool AlwaysVisible = false;
        public FloatRect Bounds = new FloatRect(0, 0, 0, 0);
        public float Rotation = 0;
        public float Health = 1;
        protected bool IgnoresAmbientLight = false;
        protected Sprite Sprite;
        protected Game Game;
        private bool _alive = true;
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
        protected virtual void OnEnqueue(GameWindow window, Priority priority, ConcurrentQueue<QueuedEntity> queue) { }
        protected virtual void OnDequeue(GameWindow window, Priority priority, ConcurrentQueue<QueuedEntity> queue) { }
        protected virtual void OnUpdate(Priority priority, ConcurrentQueue<QueuedEntity> queue) { }
        protected virtual void OnDraw(GameWindow window, Priority priority) { }
        public void OnQueue(GameWindow window, Priority priority, ConcurrentQueue<QueuedEntity> queue)
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
            RectangleShape rect = new RectangleShape();
            rect.Origin = new Vector2f(Bounds.Width / 2, Bounds.Height / 2);
            rect.Size = new Vector2f(Bounds.Width, Bounds.Height);
            rect.FillColor = new Color(0, 255, 0, 128);
            rect.Position = Position; rect.Rotation = Rotation;
            return rect;
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
