using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.GameObjects
{
    public abstract class Food : Point
    {
        private readonly Random random;
        private readonly Field field;
        private readonly ConsoleColor foodColor;
        protected Food(Field field, int foodPoints, char foodSymbol, ConsoleColor foodColor) : base(field.LeftX, field.TopY)
        {
            this.random = new Random();
            this.field = field;
            this.FoodPoints = foodPoints;
            this.FoodSymbol = foodSymbol;
            this.foodColor = foodColor;
        }
        public int FoodPoints { get; }
        public char FoodSymbol { get; } 
        public void SetRandomPosition(Queue<Point> snakeElements)
        {
            do
            {
                this.LeftX = random.Next(2, this.field.LeftX - 2);
                this.TopY = random.Next(2, this.field.TopY - 2);
            } while (snakeElements.Any(p => p.LeftX == this.LeftX && p.TopY == this.TopY));

            Console.BackgroundColor = this.foodColor;
            this.Draw(this.FoodSymbol);
            Console.BackgroundColor = ConsoleColor.White;
        }
        public bool IsFoodPoint(Point snakeHead)
        {
            return snakeHead.TopY == this.TopY && snakeHead.LeftX == this.LeftX;
        }
    }
}
