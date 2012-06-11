using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Model
{
	public delegate void DelBoardFilled(object sender, EventArgs args);
	public class Board
	{
		public event DelBoardFilled BoardFilled;
		public Color[,] Colors { get; private set; }
		public int BoardWidth { get { return Colors.GetLength(0); } }
		public int BoardHeigth { get { return Colors.GetLength(1); } }
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
		
        private void Randomize()
		{
			var colorValues = Enum.GetValues(typeof(Color));
			Random rand = new Random();
			for (int i = 0; i < BoardWidth; i++)
				for (int j = 0; j < BoardHeigth; j++)
				{
					int arrayLength = colorValues.GetLength(0);
					int randIdx = rand.Next(arrayLength);
					Colors[i, j] = (Color)colorValues.GetValue(randIdx);
				}
		}
		
        private void SetToColor(int x, int y, Color color)
		{
			Colors[x,y] = color;
		}
		
        private Color GetColor(int x, int y)
		{
			return Colors[x,y];
		}
		
        public void Pick(Color color)
		{
			Color previousColor = GetColor(0, 0);
			var traveled = new bool[BoardWidth,BoardHeigth];
			FillFrom(0, 0, previousColor, color, traveled);
			if (IsFilled && BoardFilled != null)
				BoardFilled(this, new EventArgs());
            //print traveled
            //for (int i = 0; i < traveled.GetLength(0); i++)
            //{
            //    for (int j = 0; j < traveled.GetLength(1); j++)
            //    {
            //        Console.Write(" ");
            //        Console.Write(traveled[i, j] ? " " : "X");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine();
		}
		
        private void FillFrom(int x, int y, Color previousColor, Color chosenColor, bool[,] traveled)
		{
		    Color currentColor = GetColor(x, y);
			if (currentColor == previousColor && !traveled[x,y])
			{
				SetToColor(x, y, chosenColor);
				traveled[x, y] = true;
				//recurse right
				if (x < BoardWidth-1)
					FillFrom(x + 1, y, previousColor, chosenColor, traveled);
				//recurse left
				if (x > 0)
					FillFrom(x - 1, y, previousColor, chosenColor, traveled);
				//recurse down
				if (y < BoardHeigth-1)
					FillFrom(x, y + 1, previousColor, chosenColor, traveled);
				//recurse up
				if (y > 0)
					FillFrom(x, y - 1, previousColor, chosenColor, traveled);
			}
		}
		
        public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < BoardWidth; i++)
			{
				for (int j = 0; j < BoardHeigth; j++)
				{
					sb.Append(' ');
					sb.Append(ColorToLetter(Colors[i,j]));
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
