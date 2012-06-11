using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using View;

namespace Model
{
    public class Player
    {
        private Color[,] boardCache;

        private IView view;
        private IInput input;

        public delegate void ColorSelectedDel(Color color);
        public event ColorSelectedDel ColorSelected;

        public void OnColorSelected(Color color)
        {
            ColorSelectedDel handler = ColorSelected;
            if (handler != null) handler(color);
        }

        public delegate void BoardUpdatedDel(Color[,] board);
        public event BoardUpdatedDel BoardUpdated;

        public void OnBoardUpdated(Color[,] board)
        {
            BoardUpdatedDel handler = BoardUpdated;
            if (handler != null) handler(board);
        }

        public Player(IView v, IInput i)
        {
            view = v;
            input = i;
            this.BoardUpdated += view.BoardUpdated;
            input.ColorSelected += this.ColorSelected;
            input.SetPlayer(this);
        }

    }
}
