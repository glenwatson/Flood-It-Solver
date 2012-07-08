using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            AIInput ai = AIInput.WithRandomLogic();
            IInput input = ai;
            View.Shared.IView view = new GUIDisplay();
            //give the View and Input to the Controller
            Controller controller = Controller.Instance(input, view);
            view.Display();

            ai.Start();
        }
    }
}
