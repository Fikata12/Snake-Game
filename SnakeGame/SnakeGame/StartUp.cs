namespace SnakeGame
{
    using GameObjects;
    using Utilities;
    using System;
    using SnakeGame.Core;

    public class StartUp
    {
        public static void Main()
        {
            ConsoleWindow.CustomizeConsole();

            Field field = new Field(60, 20);
            Snake snake = new Snake(field);

            IEngine engine = new Engine(snake, field);
            engine.Run();
        }
    }
}
