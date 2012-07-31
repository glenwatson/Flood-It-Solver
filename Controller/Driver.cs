using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using View.Input;
using View.Players;
using View.Shared;
using View.Views;

namespace View
{
    class Driver
    {
        [STAThread]
        public static void Main(string[] args)
        {
            //create View and Input
            //HumanGUIPlayer gui = new HumanGUIPlayer();
            AIInput ai = AIInput.WithMoveTowardsFarthestNodeLogic();
            IInput input = ai;
            GUIDisplay guiDisplay = new GUIDisplay();
            IView view = guiDisplay;
            //give the View and Input to the Controller
            Controller controller = Controller.Instance(input, view);

            ai.Start();
            view.Display();

        }
    }
}
