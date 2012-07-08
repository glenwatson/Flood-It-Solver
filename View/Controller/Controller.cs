using Control;
using Model;
using Model.Players;
using View.Shared;

namespace View
{
    public class Controller : IInputHandler
    {
        private Game game;
        private Player player;

        #region singleton

        private static Controller _singleton;

        public static Controller Instance(IInput i, IView v)
        {
            if (_singleton == null)
                _singleton = new Controller(i, v);
            return _singleton;
        }

        #endregion

        private Controller(IInput i, IView v)
        {
            player = new Player(v, i);
            game = new Game(9, 16);
            game.BoardUpdated += player.BoardWasUpdated;
            //Have to manually update the player the first time because we missed the event in the Game ctor()
            player.BoardWasUpdated(GetUpdate());

            game.Winner += (s, e) => { player.GameOver(e); };
        }

        public void PickColor(Color color)
        {
            game.PickColor(color);
        }

        public void Reset()
        {
            game.Reset();
        }

        public Color[,] GetUpdate()
        {
            return game.GetUpdate();
        }
    }

}
