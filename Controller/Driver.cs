using System;
using View.Input;
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
            IView view = new StdOutDisplay();
            //give the View and Input to the Controller
            Controller controller = Controller.Instance(input, view);
            view.Display();

            ai.Start();
        }
    }
}
