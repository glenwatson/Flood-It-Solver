using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Model;
using View.Players;
using View.Input;
using View.Shared;
using View.Views;
using View.Input.AI.Logic;

namespace View
{
    class Driver
    {
        [STAThread]
        public static void Main(string[] args)
        {
            //create View and Input
            HumanGUIPlayer gui = new HumanGUIPlayer();

            /*
            AILogic[] logics = new AILogic[] 
            { 
                new IncreaseSurfaceAreaMapLogic(7),
                new MoveTowardsFarthestNodeLogic(),
                new ClearAColorLogic()
            };
            AIInput ai = new AIInput(logics);
            */
            IInput input = gui;
            //GUIDisplay guiDisplay = new GUIDisplay();
            IView view = gui;

            Player player = new Player(view, gui);
            Game game = new Game(9, 16);
            Controller controller = Controller.Instance(game, player);

            //gui.Start();
            view.Display();

        }
    }
}
