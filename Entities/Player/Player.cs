using SFML.System;
using SFML.Window;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using HHGame.Client;
using HHGame.Data;
using HHGame.Entities.Items;

namespace HHGame.Entities.Player
{
    public class Player : Barista
    {
        public const uint XboxA = 0;
        public const uint XboxB = 1;
        public const uint XboxX = 2;
        public const uint XboxY = 3;
        public const uint XboxLeftBumper = 4;
        public const uint XboxRightBumper = 5;
        public const uint XboxMenu = 6;
        public const uint XboxStart = 7;
        public const uint XboxLeftStick = 8;
        public const uint XboxRightStick = 9;
        protected uint JoystickID;
        public Player(uint joystick, Character _character, Game _game) : base(_character, _game)
        {
            JoystickID = joystick;
            RightHeldItem = new Cup(Game);
            AlwaysVisible = true;
        }
        protected override void OnLiving(Priority priority, ConcurrentQueue<QueuedEntity> queue)
        {
            Facing = new Vector2f(Position.X + GetAxisPosition(Joystick.Axis.X) * 100, Position.Y);
            HeadRotation = -GetAxisPosition(Joystick.Axis.Y) * 15;
            LockedInStation = GetAxisPosition(Joystick.Axis.Z) > 0.9;
            RightArmRotation = GetAxisPosition(Joystick.Axis.U) * 360;
            LeftArmRotation = GetAxisPosition(Joystick.Axis.V) * 360;
            if (UpdateStations())
            {
                
            }
        }
        private bool UpdateStations()
        {
            if (Math.Abs(GetAxisPosition(Joystick.Axis.X)) > 0.9 && !LockedInStation)
            {
                switch (Station)
                {
                    case Stations.Espresso:
                        if (GetAxisPosition(Joystick.Axis.X) < 0.0)
                        {
                            Station = Stations.PourOver;
                            return false;
                        }
                        break;
                    case Stations.Register:
                        if (GetAxisPosition(Joystick.Axis.X) < 0.0)
                        {
                            Station = Stations.PourOver;
                            return false;
                        }
                        if (GetAxisPosition(Joystick.Axis.X) > 0.0)
                        {
                            Station = Stations.Espresso;
                            return false;
                        }
                        break;
                    case Stations.PourOver:
                        if (GetAxisPosition(Joystick.Axis.X) > 0.0)
                        {
                            Station = Stations.Espresso;
                            return false;
                        }
                        break;
                };
            }
            return true;
        }
        public float GetAxisPosition(Joystick.Axis axis)
        {
            return Joystick.GetAxisPosition(JoystickID, axis) / 100.0F;
        }
        public bool IsButtonPressed(uint button)
        {
            return Joystick.IsButtonPressed(JoystickID, button);
        }
    }
}
