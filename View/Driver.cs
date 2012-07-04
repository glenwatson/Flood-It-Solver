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
            var gui = new HumanGUIPlayer();
            IInput input = gui;
            IView view = gui;
            //give the View and Input to the Controller
            Controller controller = Controller.Instance(input, view);
            view.Display();
        }
    }
}
