using HHGame.Client;
using SFML.Audio;
using SFML.Graphics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace HHGame.Entities.World
{
    public class Stage : QueuedEntity
    {
        private Sprite Background;
        private Sprite Mainground;
        private Sprite Foreground;
        private Sprite Television;
        public Lights Lights;
        public Stage(Game _game) : base(_game)
        {
            Background = new Sprite(Game.Assets.GrabImage("World.Background"));
            Mainground = new Sprite(Game.Assets.GrabImage("World.Mainground"));
            Foreground = new Sprite(Game.Assets.GrabImage("World.Foreground"));
            Television = new Sprite(Game.Assets.GrabImage("World.Television"));
            Lights = new Lights(Game);
            AlwaysVisible = true;
        }
        protected override void OnDraw(GameWindow window, Priority priority)
        {
            switch (priority)
            {
                case Priority.Background:
                    window.Draw(Background);
                    break;
                case Priority.Mainground:
                    //window.Draw(Mainground);
                    window.Draw(Television);
                    break;
                case Priority.Foreground:
                    window.Draw(Foreground);
                    break;
            }
        }
    }
}