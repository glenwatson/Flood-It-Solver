using System;
using System.Threading;
using Model;
using View.Input.AI;
using View.Input.AI.Logic;
using View.Shared;

namespace View.Input
{
    public class AIInput : IInput
    {
        private readonly AILogic _logic;
        private Thread _logicThread;
        private bool _shouldRun = true;

        public static AIInput WithRandomLogic()
        {
            return new AIInput(new RandomLogic());
        }
        public static AIInput WithAnalysisLogic()
        {
            return new AIInput(new AnalysisLogic());
        }

        private AIInput(AILogic logic)
        {
            _logic = logic;
            _logicThread = new Thread(StartQueryingLogic);
        }

        private void StartQueryingLogic()
        {
            while (_shouldRun)
            {
                Color colorChosen = _logic.ChooseColor(GetController().GetUpdate()); //reaches across other thread to get the current Board
                Thread.Sleep(1000);
                ChosenColorQueue.Instance().Enqueue(colorChosen);
            }
        }

        public void Start()
        {
            _shouldRun = true;
            _logicThread.Start();
            WaitForColorChoices();
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
