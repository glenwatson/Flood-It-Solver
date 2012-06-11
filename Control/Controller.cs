using System;
using System.Threading;
using System.Windows.Threading;
using Model;
using View;

namespace Control
{
    public class Controller : IInputHandler
    {
        private Game game;

        public static void Main(string[] args)
        {
            new HumanGUIPlayer();
        }

        #region singleton

        private static Controller _singleton;

        public static Controller Instance()
        {
            if (_singleton == null)
                _singleton = new Controller();
            return _singleton;
        }

        #endregion

        private Controller()
        {
            game = new Game(2, 3);
            IInput input = new HumanCLIPlayer();
            IView view = new StdOutDisplay();
            Player player = new Player(view, input);
        }

        public void PickColor(Color color)
        {
            game.PickColor(color);
        }

        public Color[,] GetUpdate()
        {
            return game.Board;
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
