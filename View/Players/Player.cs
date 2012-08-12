using Model;
using View.Shared;

namespace View.Players
{
    /// <summary>
    /// Kind of a 2nd controller. 
    ///     The first controller controls the communication between the Model and View.
    ///     This controller controls the communication between the actual View/Input and the first controller.
    /// </summary>
    public class Player
    {
        private IView view;
        private IInput input;

        protected delegate void BoardUpdatedDel(Color[,] board);
        protected event BoardUpdatedDel BoardUpdated;

        public void BoardWasUpdated(Color[,] board)
        {
            BoardUpdatedDel handler = BoardUpdated;
            if (handler != null) handler(board);
        }

        public Player(IView v, IInput i)
        {
            view = v;
            SubscribeView(v);
            input = i;
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
            input.GameOver(winArgs);
        }
    }
}
