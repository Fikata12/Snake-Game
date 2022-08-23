using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.GameObjects.Foods
{
    public class FoodDollar : Food
    {
        private const int foodPoints = 2;
        private const char foodSymbol = '$';
        private const ConsoleColor foodColor = ConsoleColor.Green;
        public FoodDollar(Field field) : base(field, foodPoints, foodSymbol, foodColor)
        {
        }
    }
}
