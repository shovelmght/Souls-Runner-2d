using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesserKnown.Public
{
    public static class PublicVariables
    {
        public static int health = 10;

        public static void Lose_Health(int amount)
        {
            health -= amount;
        }

        public static void Reset_Level(int amount)
        {
            health = amount;
        }
    }
}
