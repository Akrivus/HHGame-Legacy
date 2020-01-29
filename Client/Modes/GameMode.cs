using HHGame.Data;
using HHGame.Entities.Player;
using HHGame.Entities.World;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;

namespace HHGame.Client.Modes
{
    public class GameMode : Mode
    {
        public Player Player;
        public Stage Stage;
        public GameMode(Game _game, Character _character) : base(_game)
        {
            Player = new Player(0, _character, _game);
            Stage = new Stage(_game);
        }
        public override void Load(GameWindow window)
        {
            window.Watch(Priority.Background, Stage);
            window.Watch(Priority.Background, Player);
            window.Watch(Priority.Mainground, Stage);
            window.Watch(Priority.Foreground, Stage);
            window.Watch(Priority.Foreground, Stage.Lights);
        }
    }
}
