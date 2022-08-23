using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.GameObjects.Foods
{
    internal class FoodAsterisk : Food
    {
        private const int foodPoints = 1;
        private const char foodSymbol = '*';
        private const ConsoleColor foodColor = ConsoleColor.DarkYellow;
        public FoodAsterisk(Field field) : base(field, foodPoints, foodSymbol, foodColor)
        {
        }
    }
}
