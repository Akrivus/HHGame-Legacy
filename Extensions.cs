using HHGame.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace HHGame
{
    public static class Extensions
    {
        public static int GetHeight(this Character character)
        {
            switch (character)
            {
                case Character.Shepherd:
                    return 9;
                case Character.Katie:
                    return 15;
                default:
                    return 17;
                case Character.Isaiah:
                case Character.Lucas:
                    return 19;
            }
        }
    }
}
