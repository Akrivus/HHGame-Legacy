using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hammerhand.Game
{
    class Instance
    {
        public GameWindow Window;
        public Instance()
        {
            Window = new GameWindow(false);
        }
        public void Run()
        {

        }
    }
}
