using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hammerhand.Game
{
    public class GameWindow
    {
        public bool IsFullScreen = false;
        private RenderWindow _window;
        public GameWindow(bool _fullscreen)
        {
            _window = new RenderWindow(_fullscreen ? VideoMode.FullscreenModes[0] : new VideoMode(960, 720), Program.Title, _fullscreen ? Styles.Fullscreen : Styles.Default);
            AttachEventsTo(_window);
        }
        public void AttachEventsTo(RenderWindow _window)
        {
            _window.Closed += OnClosed;
            _window.GainedFocus += OnGainedFocus;
            _window.JoystickButtonPressed += OnJoystickButtonPressed;
            _window.JoystickButtonReleased += OnJoystickButtonReleased;
            _window.JoystickConnected += OnJoystickConnected;
            _window.JoystickDisconnected += OnJoystickDisconnected;
            _window.JoystickMoved += OnJoystickMoved;
            _window.KeyPressed += OnKeyPressed;
            _window.KeyReleased += OnKeyReleased;
            _window.LostFocus += OnLostFocus;
            _window.MouseButtonPressed += OnMouseButtonPressed;
            _window.MouseButtonReleased += OnMouseButtonReleased;
            _window.MouseEntered += OnMouseEntered;
            _window.MouseLeft += OnMouseLeft;
            _window.MouseMoved += OnMouseMoved;
            _window.MouseWheelScrolled += OnMouseWheelScrolled;
            _window.Resized += OnResized;
            _window.TextEntered += OnTextEntered;
        }
        private void OnClosed(object sender, EventArgs e)
        {
            _window.Close();
        }
        private void OnGainedFocus(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void OnJoystickButtonPressed(object sender, JoystickButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void OnJoystickButtonReleased(object sender, JoystickButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void OnJoystickConnected(object sender, JoystickConnectEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void OnJoystickDisconnected(object sender, JoystickConnectEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void OnJoystickMoved(object sender, JoystickMoveEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void OnKeyReleased(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void OnLostFocus(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void OnMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void OnMouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void OnMouseEntered(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void OnMouseLeft(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void OnMouseMoved(object sender, MouseMoveEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void OnMouseWheelScrolled(object sender, MouseWheelScrollEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void OnResized(object sender, SizeEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void OnTextEntered(object sender, TextEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
