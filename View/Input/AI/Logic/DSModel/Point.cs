using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Input.AI.Logic.DSModel
{
    struct Point
    {
        private int _x;
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        private int _y;
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }


        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public bool IsTouching(Point point)
        {
            if (this._x == point._x)
            {
                return Math.Abs(this._y - point._y) == 1;
            }
            if (this._y == point._y)
            {
                return Math.Abs(this._x - point._x) == 1;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is Point)
            {
                Point p = (Point)obj;
                return this._x == p._x && this._y == p._y;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return _x * _y;
        }
    }
}
