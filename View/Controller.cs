using System;
using System.Windows;
using Control;
using Model;

namespace View
{
    public class Controller : IInputHandler
    {
        private Game game;
        private IInput input;
        private IView view;
        [STAThread]
        public static void Main(string[] args)
        {
            var gui = new HumanGUIPlayer();
            gui.Init(Instance(gui, gui).GetUpdate());
            new Application().Run(gui);
            //Console.ReadLine();
        }

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
            game = new Game(9, 16);
            
            input = i;
            view = v;

            game.Winner += (s, e) => view.GameOver(e);

            Player player = new Player(view, input);
        }

        public void PickColor(Color color)
        {
            game.PickColor(color);
            view.BoardUpdated(GetUpdate());
        }

        public void Reset()
        {
            game.Reset();
            view.BoardUpdated(GetUpdate());
        }

        private Color[,] GetUpdate()
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
