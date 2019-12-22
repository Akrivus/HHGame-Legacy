using Hammerhand.Client;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hammerhand.Entities.Items
{
    public class Cup : PhysicalEntity
    {
        public Cup(Game _game) : base(_game)
        {
            Sprite = new Sprite(Game.Assets.GrabImage("Items.Cup"));
            Sprite.Origin = new Vector2f(4, 4);
            Bounds = new FloatRect(0, 0, 8, 8);
            CanBounce = true; CanRoll = true;
        }
        protected override void OnDraw(GameWindow window, GameWindow.Priority priority)
        {
            Sprite.Color = Game.Stage.Color;
            Sprite.Position = Position;
            Sprite.Rotation = Rotation;
            window.Draw(Sprite);
        }
    }
}
