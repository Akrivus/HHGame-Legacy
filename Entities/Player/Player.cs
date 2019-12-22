using System;
using System.Collections.Generic;
using System.Text;

namespace Hammerhand.Entities.Player
{
    public class Player : Barista
    {
        public int JoystickID;
        public Player(int joystick, Names _name, Game _game) : base(_name, _game)
        {
            JoystickID = joystick;
        }
    }
}
