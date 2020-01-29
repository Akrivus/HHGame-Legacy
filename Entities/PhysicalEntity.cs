using HHGame.Client;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace HHGame.Entities
{
    public class PhysicalEntity : QueuedEntity
    {
        protected bool CanBounce;
        protected bool CanRoll;
        public Vector2f Velocity;
        public PhysicalEntity(Game _game, float _x = 0, float _y = 0) : base(_game)
        {
            Velocity = new Vector2f(_x, _y);
        }
        protected virtual void OnLiving(GameWindow.Priority priority, ConcurrentQueue<QueuedEntity> queue) { }
        protected override void OnUpdate(GameWindow.Priority priority, ConcurrentQueue<QueuedEntity> queue)
        {
            Position = new Vector2f(Position.X + Velocity.X, Position.Y + Velocity.Y);
            Velocity = new Vector2f(Velocity.X * Game.Friction, Velocity.Y * Game.Friction);
            if (!Game.CanFall(TopLeft) || !Game.CanFall(BottomLeft))
            {
                if (CanBounce && Velocity.Y > 1)
                {
                    Velocity = new Vector2f(Velocity.X, Velocity.Y * -Game.Friction);
                }
                else
                {
                    Velocity = new Vector2f(Velocity.X, Math.Min(0, Velocity.Y));
                }
            }
            else
            {
                Accelerate(0, Game.Gravity);
            }
            if (CanRoll)
            {
                Rotate(Velocity.X * 2 / (Velocity.Y + 1));
            }
            OnLiving(priority, queue);
        }
        public void Accelerate(float x, float y)
        {
            Velocity = new Vector2f(Velocity.X + x, Velocity.Y + y);
        }
    }
}
