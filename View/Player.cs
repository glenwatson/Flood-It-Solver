using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using View;

namespace Model
{
    /// <summary>
    /// Kind of a second controller. 
    ///     The first controller controls the communication between the Model and View.
    ///     This controller controls the communication between the actual View/Input and the first controller.
    /// </summary>
    public class Player
    {
        private Color[,] boardCache;

        private IView view;
        private IInput input;

        private delegate void BoardUpdatedDel(Color[,] board);
        private event BoardUpdatedDel BoardUpdated;

        public void BoardWasUpdated(Color[,] board)
        {
            BoardUpdatedDel handler = BoardUpdated;
            if (handler != null) handler(board);
        }

        public Player(IInput i)
        {
            input = i;
        }
        public Player(IView v, IInput i) : this(i)
        {
            view = v;
            SubscribeView(v);
        }

        /// <summary>
        /// If anyone else wants to be notified up board updates.
        /// Note: they won't be notified of game overs, 
        ///     only the view used in the constructor will be notified of game overs
        /// </summary>
        /// <param name="view"></param>
        public void SubscribeView(IView view)
        {
            this.BoardUpdated += view.BoardUpdated;
        }

        public void GameOver(WinEventArgs winArgs)
        {
            view.GameOver(winArgs);
        }
    }
}
