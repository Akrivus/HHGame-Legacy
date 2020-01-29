using HHGame.Client;
using SFML.Graphics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace HHGame.Entities.World
{
    public class Lights : QueuedEntity
    {
        public bool Flickering = false;
        public Lights(Game _game) : base(_game)
        {
            Sprite = new Sprite(Game.Assets.GrabImage("World.Lights"));
            IgnoresAmbientLight = true;
            AlwaysVisible = true;
        }
        protected override void OnDraw(GameWindow window, GameWindow.Priority priority)
        {
            if (!Flickering || Game.RNG.Next(20) > 2)
            {
                window.Draw(Sprite);
            }
        }
    }
}
