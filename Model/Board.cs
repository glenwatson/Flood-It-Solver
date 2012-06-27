using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Model
{
    public class FilledEventArgs : EventArgs
    {
        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }
    }
	public delegate void DelBoardFilled(object sender, FilledEventArgs args);
	public class Board
	{
		public event DelBoardFilled BoardFilled;
		public Color[,] Colors { get; private set; }
		public int BoardWidth { get { return Colors.GetLength(1); } }
		public int BoardHeigth { get { return Colors.GetLength(0); } }
		public bool IsFilled 
		{ 
			get
			{
				Color theColor = Colors[0, 0];
				foreach (var color in Colors)
					if (theColor != color)
						return false;
				return true;
			}
		}
		
        public Board(int size) : this(size, size) {}
		
        public Board(int xSize, int ySize)
		{
			if (xSize < 1)
				throw new ArgumentException("xSize must be greater than 0");
			if (ySize < 1)
				throw new ArgumentException("ySize must be greater than 0");
			Colors = new Color[xSize,ySize];
			Randomize();
		}
        
        public Board(Color[,] colors)
        {
            Colors = colors;
        }

        public void Reset()
        {
            Randomize();
        }
		
        private void Randomize()
		{
			var colorValues = Enum.GetValues(typeof(Color));
			Random rand = new Random();
			for (int w = 0; w < BoardWidth; w++)
				for (int h = 0; h < BoardHeigth; h++)
				{
					int arrayLength = colorValues.GetLength(0);
					int randIdx = rand.Next(arrayLength);
					Colors[h, w] = (Color)colorValues.GetValue(randIdx);
				}
		}
		
        private void SetToColor(int x, int y, Color color)
		{
			Colors[y,x] = color;
		}
		
        private Color GetColor(int x, int y)
		{
			return Colors[y, x];
		}

        private bool IsOnBoard(int x, int y)
        {
            return IsWithinWidth(x) && IsWithinHeight(y);
        }
        private bool IsWithinWidth(int x)
        {
            return IsWithinLeftSide(x) && IsWithinRightSide(x);
        }
        private bool IsWithinHeight(int y)
        {
            return IsWithinTopSide(y) && IsWithinBottomSide(y);
        }
        private bool IsWithinRightSide(int x)
        {
            return x < BoardWidth;
        }
        private bool IsWithinLeftSide(int x)
        {
            return x >= 0;
        }
        private bool IsWithinTopSide(int y)
        {
            return y >= 0;
        }
        private bool IsWithinBottomSide(int y)
        {
            return y < BoardHeigth;
        }
		
        public void Pick(Color color)
		{
			Color previousColor = GetColor(0, 0);
            FillFrom(0, 0, previousColor, color);
			if (IsFilled && BoardFilled != null)
                BoardFilled(this, new FilledEventArgs() { BoardWidth = BoardWidth, BoardHeight = BoardHeigth });
		}

        private void FillFrom(int x, int y, Color previousColor, Color chosenColor)
        {
            Color currentColor = GetColor(x, y);
            if (currentColor == previousColor)
            {
                SetToColor(x, y, chosenColor);
                //recurse right
                if (IsWithinRightSide(x + 1))
                    FillFrom(x + 1, y, previousColor, chosenColor);
                //recurse left
                if (IsWithinLeftSide(x - 1))
                    FillFrom(x - 1, y, previousColor, chosenColor);
                //recurse down
                if (IsWithinBottomSide(y + 1))
                    FillFrom(x, y + 1, previousColor, chosenColor);
                //recurse up
                if (IsWithinTopSide(y - 1))
                    FillFrom(x, y - 1, previousColor, chosenColor);
            }
        }
        //private struct Point
        //{
        //    private int _x;
        //    public int X { get { return _x; } }

        //    private int _y;
        //    public int Y { get { return _y; } }

        //    public Point(int x, int y)
        //    {
        //        _x = x;
        //        _y = y;
        //    }
        //}
        //private void FillFrom(int x, int y, Color previousColor, Color chosenColor) //http://en.wikipedia.org/wiki/Flood_fill
        //{
        //    Queue<Point> queue = new Queue<Point>();
        //    if (GetColor(x, y) != previousColor)
        //        return;
        //    queue.Enqueue(new Point(x, y));
        //    while(queue.Count() > 0)
        //    {
        //        Point currentPoint = queue.Dequeue();
        //        int currentX = currentPoint.X;
        //        int currentY = currentPoint.Y;

        //        if (GetColor(currentX, currentY) == previousColor)
        //        {
        //            int west = currentX;
        //            int east = currentX;
        //            while (IsWithinLeftSide(west) && GetColor(west, currentY) == previousColor)
        //                west--;
        //            while (IsWithinRightSide(east) && GetColor(east, currentY) == previousColor)
        //                east++;
        //            for (int i = west; i < east; i++)
        //            {
        //                SetToColor(i, currentY, chosenColor);
        //                if (IsWithinBottomSide(currentY + 1) && GetColor(i, currentY + 1) == previousColor)
        //                    queue.Enqueue(new Point(i, currentY + 1));
        //                if (IsWithinTopSide(currentY - 1) && GetColor(i, currentY - 1) == previousColor)
        //                    queue.Enqueue(new Point(i, currentY - 1));
        //            }
        //        }
        //    }
        //}
		
        public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			for (int w = 0; w < BoardWidth; w++)
			{
				for (int h = 0; h < BoardHeigth; h++)
				{
					sb.Append(' ');
					sb.Append(ColorToLetter(Colors[h,w]));
					//sb.Append(']');
				}
				sb.AppendLine();
			}
			return sb.ToString();
		}

		private char ColorToLetter(Color color)
		{
			char result = 'X';
			switch (color)
			{
				case Color.Red:
					result = 'R';
					break;
				case Color.Orange:
					result = 'O';
					break;
				case Color.Yellow:
					result = 'Y';
					break;
				case Color.Green:
					result = 'G';
					break;
				case Color.Blue:
					result = 'B';
					break;
				case Color.Purple:
					result = 'P';
					break;
				default:
					Debug.Fail("All cases of the enum Color should be taken care of");
					break;
			}
			return result;
		}
	}
}
