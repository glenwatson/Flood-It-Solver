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
            //HumanGUIPlayer gui = new HumanGUIPlayer();
            AIInput ai = new AIInput(new IncreaseSurfaceAreaMapLogic(1));
            IInput input = ai;
            GUIDisplay guiDisplay = new GUIDisplay();
            IView view = guiDisplay;

            Player player = new Player(view, ai);
            Game game = new Game(9, 16);
            Controller controller = Controller.Instance(game, player);

            ai.Start();
            view.Display();

        }
    }
}
