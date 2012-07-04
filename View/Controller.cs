using Control;
using Model;

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

        //[STAThread]
        //public static void Main(string[] args)
        //{
        //    HumanGUIPlayer displayAndPlayer =  new HumanGUIPlayer();
        //    IPlayer player = displayAndPlayer;
        //    IDisplay display = displayAndPlayer;

        //    displayAndPlayer.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(displayAndPlayer.Show));

        //    Thread t = new Thread(
        //        () => displayAndPlayer.Dispatcher.Invoke(
        //            DispatcherPriority.Normal, new Action(StartGUI(displayAndPlayer))));
        //    t.SetApartmentState(ApartmentState.STA);
        //    t.Start();

        //    var game = new Game(2, 10);
        //    display.Init(game.Board);
        //    bool gameon = true;
        //    game.Winner += (s, e) => { gameon = false; };
        //    while (gameon)
        //    {
        //        game.PickColor(player.MakeMove(game.Board));
        //        display.Update(game.Board);
        //    }
        //    display.GameOver(game.Turns);
        //}

        //private static Action StartGUI(HumanGUIPlayer displayAndPlayer)
        //{
        //    deleteme.App app = new deleteme.App();
        //    app.InitializeComponent();
        //    app.Run();
        //}
    }

}
