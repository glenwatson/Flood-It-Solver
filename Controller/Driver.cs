﻿using System;
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
            //create Input & View
            //HumanGUIPlayer gui = new HumanGUIPlayer(); //IInput & IView

            AILogicWeight[] logics = new AILogicWeight[] 
            {
                //new AILogicWeight(new RandomLogic(), 1),
                new AILogicWeight(new IncreaseSurfaceAreaMapLogic(4), 1000),
                //new AILogicWeight(new IncreaseSurfaceAreaGridLogic(), 1),
                new AILogicWeight(new MoveTowardsFarthestNodeLogic(), 100),
                new AILogicWeight(new ClearAColorLogic(), 10000),
                //new AILogicWeight(new HighestCount(), 1),
                new AILogicWeight(new AvoidColor(), 100),
            };
            AIInput ai = new AIInput(logics); //IInput
            GUIDisplay guiDisplay = new GUIDisplay(); //IView

            Player player = new Player(guiDisplay, ai);
            Game game = new Game(3,3);//9, 16);
            Controller controller = Controller.Instance(game, player);

            //gui.Display();
            ai.Start();
            guiDisplay.Display();

        }
    }
}
