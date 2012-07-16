using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Input.AI.Logic.DSModel
{
    struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsTouching(Point point)
        {
            if (this.X == point.X)
            {
                return Math.Abs(this.Y - point.Y) == 1;
            }
            if (this.Y == point.Y)
            {
                return Math.Abs(this.X - point.X) == 1;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is Point)
            {
                Point p = (Point)obj;
                return this.X == p.X && this.Y == p.Y;
            }
            return false;
        }

    }
}
