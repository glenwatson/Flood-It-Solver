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

        public static Controller Instance(Game g, Player p)
        {
            if (_singleton == null)
                _singleton = new Controller(g, p);
            return _singleton;
        }

        #endregion

        private Controller(Game g, Player p)
        {
            player = p;
            game = g;
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
