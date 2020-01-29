using HHGame.Entities;
using SFML.Window;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace HHGame.Client
{
    public class Mode
    {
        public Game Game;
        public Mode(Game game)
        {
            Game = game;
        }
        public virtual void Load(GameWindow window) { }
        public virtual void BeginQueue(GameWindow window) { }
        public virtual void AfterQueue(GameWindow window) { }
        public virtual void Exit(GameWindow window) { }
        public virtual void OnClosed(object sender, EventArgs e) { }
        public virtual void OnGainedFocus(object sender, EventArgs e) { }
        public virtual void OnJoystickButtonPressed(object sender, JoystickButtonEventArgs e) { }
        public virtual void OnJoystickButtonReleased(object sender, JoystickButtonEventArgs e) { }
        public virtual void OnJoystickConnected(object sender, JoystickConnectEventArgs e) { }
        public virtual void OnJoystickDisconnected(object sender, JoystickConnectEventArgs e) { }
        public virtual void OnJoystickMoved(object sender, JoystickMoveEventArgs e) { }
        public virtual void OnKeyPressed(object sender, KeyEventArgs e) { }
        public virtual void OnKeyReleased(object sender, KeyEventArgs e) { }
        public virtual void OnLostFocus(object sender, EventArgs e) { }
        public virtual void OnMouseButtonPressed(object sender, MouseButtonEventArgs e) { }
        public virtual void OnMouseButtonReleased(object sender, MouseButtonEventArgs e) { }
        public virtual void OnMouseEntered(object sender, EventArgs e) { }
        public virtual void OnMouseLeft(object sender, EventArgs e) { }
        public virtual void OnMouseMoved(object sender, MouseMoveEventArgs e) { }
        public virtual void OnMouseWheelScrolled(object sender, MouseWheelScrollEventArgs e) { }
        public virtual void OnResized(object sender, SizeEventArgs e) { }
        public virtual void OnTextEntered(object sender, TextEventArgs e) { }
    }
}
