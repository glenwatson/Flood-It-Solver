using System;
using System.ComponentModel;
using System.Threading;
using Model;
using View.Input.AI;
using View.Input.AI.Logic;
using View.Input.AI.Logic.Moves;
using View.Shared;

namespace View.Input
{
    public class AIInput : IInput
    {
        private readonly AILogic _logic;
        private BackgroundWorker _logicThread;
        private bool _shouldRun = true;

        public static AIInput WithRandomLogic()
        {
            return new AIInput(new RandomLogic());
        }
        public static AIInput WithMoveTowardsFarthestNodeLogic()
        {
            return new AIInput(new MoveTowardsFarthestNodeLogic());
        }
        public static AIInput IncreaseSurfaceAreaMapLogic()
        {
            return new AIInput(new IncreaseSurfaceAreaMapLogic(-1));
        }
        public static AIInput WithIncreaseSurfaceAreaGridLogic()
        {
            return new AIInput(new IncreaseSurfaceAreaGridLogic());
        }
        public static AIInput WithClearAColorLogic()
        {
            return new AIInput(new ClearAColorLogic());
        }

        private AIInput(AILogic logic)
        {
            _logic = logic;
            _logicThread = new BackgroundWorker();
            _logicThread.DoWork += StartQueryingLogic;
        }

        private void StartQueryingLogic(object sender, DoWorkEventArgs e)
        {
            while (_shouldRun)
            {
                //Thread.Sleep(1000);
                Console.ReadLine();
                SuggestedMoves colorsChosen = _logic.ChooseColor(GetController().GetUpdate()); //reaches across other thread to get the current Board
                Controller controller = GetController();
                foreach (Color colorChosen in colorsChosen.BestMoves)
                    controller.PickColor(colorChosen);
                //ChosenColorQueue.Instance().Enqueue(colorChosen);
            }
        }

        public void Start()
        {
            _shouldRun = true;
            _logicThread.RunWorkerAsync();
            //WaitForColorChoices();
        }
        public void Stop()
        {
            _shouldRun = false;
        }

        private void WaitForColorChoices()
        {
            while (_shouldRun)
            {
                Color colorChosen = ChosenColorQueue.Instance().Dequeue();
                GetController().PickColor(colorChosen);
            }
        }

        public void GameOver(WinEventArgs winArgs)
        {
            GetController().Reset();
        }

        private Controller GetController()
        {
            return Controller.Instance(this, null);
        }
    }
}
