using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.GameObjects
{
    public class Snake
    {
        private const int defaultSleepTime = 250;
        private const int SleepTimeDecrementPerFood = 10;
        private const int MinSleepTime = 70;
        private const char snakeSymbol = '\u25CF';
        private const char emptySpace = ' ';

        private readonly Queue<Point> snakeElements;
        private readonly IList<Food> food;
        private readonly Field field;

        private int nextLeftX;
        private int nextTopY;
        private int foodIndex;
        private Snake()
        {
            this.SleepTime = defaultSleepTime;
            snakeElements = new Queue<Point>();
            this.food = new List<Food>();
            this.foodIndex = this.RandomFoodNumber;
        }
        public Snake(Field field) : this()
        {
            this.field = field;
            this.GetFoods();
            this.CreateSnake();
        }
        private void CreateSnake()
        {
            for (int topY = 1; topY  <= 6; topY ++)
            {
                Point newPoint = new Point(2, topY);
                this.snakeElements.Enqueue(newPoint);
            }
            this.foodIndex = this.RandomFoodNumber;
            this.food[this.foodIndex].SetRandomPosition(this.snakeElements);
        }
        private void GetFoods()
        {
            Type[] foodTypes = Assembly.GetExecutingAssembly()
                .GetTypes().Where(t => t.Name.StartsWith("Food") 
                && t.IsAbstract == false).ToArray();
            foreach (var foodType in foodTypes)
            {
                Food currfood = (Food)Activator.CreateInstance(foodType, new object[] { field });
                this.food.Add(currfood);
            }
        }
        private void GetNextPoint(Point direction, Point snakeHead)
        {
            this.nextLeftX = snakeHead.LeftX + direction.LeftX;
            this.nextTopY = snakeHead.TopY + direction.TopY;
        }
        public bool CanMove(Point direction)
        {
            Point currSnakeHead = this.snakeElements.Last();
            this.GetNextPoint(direction, currSnakeHead);
            bool isNextPointOfSnake = this.snakeElements.Any(p => p.LeftX == this.nextLeftX && p.TopY == this.nextTopY);
            if (isNextPointOfSnake)
            {
                return false;
            }
            Point newSnakeHead = new Point(this.nextLeftX, this.nextTopY);
            if (this.field.IsPointOfWall(newSnakeHead))
            {
                return false;
            }
            this.snakeElements.Enqueue(newSnakeHead);
            newSnakeHead.Draw(snakeSymbol);
            if (this.food[foodIndex].IsFoodPoint(newSnakeHead))
            {
                this.Eat(direction, currSnakeHead);
            }
            Point snakeTail = this.snakeElements.Dequeue();
            snakeTail.Draw(emptySpace);

            return true;
        }
        private void Eat(Point direction, Point currSnakeHead)
        {
            int points = this.food[this.foodIndex].FoodPoints;
            for (int i = 0; i < points; i++)
            {
                Point newPoint = new Point(this.nextLeftX, this.nextTopY);
                this.snakeElements.Enqueue(newPoint);
                newPoint.Draw(snakeSymbol);
                this.GetNextPoint(direction, currSnakeHead);
            }
            if (this.SleepTime - SleepTimeDecrementPerFood >= MinSleepTime)
            {
                this.SleepTime -= SleepTimeDecrementPerFood;
            }
            this.foodIndex = this.RandomFoodNumber;
            this.food[this.foodIndex].SetRandomPosition(this.snakeElements);
        }
        public int SleepTime { get; private set; }
        public int RandomFoodNumber => new Random().Next(0, this.food.Count);
    }
}
