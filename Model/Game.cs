using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
	public class WinEventArgs : EventArgs
	{
		public int Turns { get; set; }
        public DateTime TimeStarted { get; set; }
        public DateTime TimeCompleted { get; set; }
        public FilledEventArgs FilledEvent { get; set; }
	}
	public delegate void DelWin(object sender, WinEventArgs args);
	public class Game
	{
		public event DelWin Winner;
		public int Turns { get; private set; }
        private DateTime _timeOfFirstMove;
		private readonly Board _board;
		public Color[,] Board
		{
			get { return (Color[,])_board.Colors.Clone(); }
		}
		public Game(int size) : this(size, size) { }
		public Game(int xSize, int ySize)
		{
			_board = new Board(xSize, ySize);
            _board.BoardFilled += (s, e) => 
            {
                if (Winner != null)
                {
                    Winner(s, new WinEventArgs
                    {
                        TimeCompleted = DateTime.Now,
                        TimeStarted = _timeOfFirstMove,
                        Turns = Turns,
                        FilledEvent = e
                    });
                }
            };
		}

		public void PickColor(Color color)
		{
            if (Turns == 0)
                _timeOfFirstMove = DateTime.Now;
			Turns++;
			_board.Pick(color);
		}

		public override string ToString()
		{
			return _board.ToString();
		}

        public void Reset()
        {
            _board.Reset();
            Turns = 0;
        }
    }
}
