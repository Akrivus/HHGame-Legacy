using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using HHGame.Client;
using HHGame.Data;
using SFML.Graphics;
using SFML.System;

namespace HHGame.Entities.Player
{
    public class Barista : SkeletonEntity
    {
        public enum Stations { PourOver, Register, Espresso }
        public Stations Station = Stations.Register;
        public Character Character;
        public bool LockedInStation;
        public Barista(Character character, Game _game) : base(_game.Assets.GrabImage(string.Format("Characters.{0}.{1}", character, _game.Season)), _game, character.GetHeight())
        {
            Character = character;
        }
        protected override void OnUpdate(Priority priority, ConcurrentQueue<QueuedEntity> queue)
        {
            base.OnUpdate(priority, queue);
            switch (Station)
            {
                case Stations.PourOver:
                    Velocity = new Vector2f((192 - Position.X) * GetWalkingSpeed() * Game.FrameDelta, Velocity.Y);
                    break;
                case Stations.Register:
                    Velocity = new Vector2f((208 - Position.X) * GetWalkingSpeed() * Game.FrameDelta, Velocity.Y);
                    break;
                case Stations.Espresso:
                    Velocity = new Vector2f((232 - Position.X) * GetWalkingSpeed() * Game.FrameDelta, Velocity.Y);
                    break;
            }
        }
    }
}
