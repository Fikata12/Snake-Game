using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.GameObjects
{
    public class Field : Point
    {
        private const char wallSymbol = '\u25A0';
        public Field(int leftX, int topY) : base(leftX, topY)
        {
            this.InitializeFieldBorders();
        }
        public bool IsPointOfWall(Point snakeHead)
        {
            return snakeHead.LeftX == 0 || snakeHead.TopY == 0 ||
                snakeHead.LeftX == this.LeftX - 1 || snakeHead.TopY == this.TopY;
        }
        private void InitializeFieldBorders()
        {
            this.SetHorizontalLine(0);
            this.SetHorizontalLine(this.TopY);

            this.SetVerticalLine(0);
            this.SetVerticalLine(this.LeftX - 1);
        }
        private void SetHorizontalLine(int topY)
        {
            for (int leftX = 0; leftX < this.LeftX; leftX++)
            {
                this.Draw(leftX, topY, wallSymbol);
            }
        }
        private void SetVerticalLine(int leftX)
        {
            for (int topY = 0; topY < this.TopY; topY++)
            {
                this.Draw(leftX, topY, wallSymbol);
            }
        }
    }
}
