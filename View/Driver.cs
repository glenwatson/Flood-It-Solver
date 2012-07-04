using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace View
{
    class Driver
    {
        [STAThread]
        public static void Main(string[] args)
        {
            //create View and Input
            //var gui = new HumanGUIPlayer();
            var ai = AIPlayer.WithRandomLogic();
            IInput input = ai;
            IView view = new GUIDisplay();
            //give the View and Input to the Controller
            Controller controller = Controller.Instance(input, view);
            view.Display();

            ai.Start();
        }
    }
}
