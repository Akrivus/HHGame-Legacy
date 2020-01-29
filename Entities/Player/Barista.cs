using System;
using System.Collections.Generic;
using System.Text;

namespace HHGame.Entities.Player
{
    public class Barista : SkeletonEntity
    {
        public enum Names { Alex, Isaiah, Jordan, Katie, Kayla, Lucas, Riley, Sarah }
        public Names Name;
        public Barista(Names name, Game _game) : base(_game.Assets.GrabImage(string.Format("Characters.{0}.{1}", name, _game.Season)), _game, HeightOf(name))
        {
            Name = name;
        }
        public static int HeightOf(Names name)
        {
            switch (name)
            {
                case Names.Katie:
                    return 15;
                case Names.Isaiah:
                case Names.Lucas:
                    return 19;
                default:
                    return 17;
            }
        }
    }
}
