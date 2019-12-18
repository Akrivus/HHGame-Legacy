using Hammerhand.Game;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hammerhand
{
    public static class Program
    {
        public const string Title = "Hammerhand: The Game";
        private static Instance Instance;
        public static void Main()
        {
            Instance = new Instance();
            Instance.Run();
        }
    }
}
