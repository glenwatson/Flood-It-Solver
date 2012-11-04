using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        private readonly List<AILogic> _logics;
        private BackgroundWorker _logicThread;
        private bool _shouldRun = true;

        /// <summary>
        /// Uses multiple AILogics in the choice of color
        /// (Order does not matter)
        /// </summary>
        /// <param name="logics">The AILogics to use</param>
        public AIInput(params AILogic[] logics)
        {
            _logics = logics.ToList();
            _logicThread = new BackgroundWorker();
            _logicThread.DoWork += StartQueryingLogic;
        }
        public AIInput(AILogic logic) : 
            this(new AILogic[]{logic})
        {}

        public void BoardUpdated(Color[,] board)
        {
            //Allow query logic thread to allow the AI to calc the next Color
        }

        private void StartQueryingLogic(object sender, DoWorkEventArgs e)
        {
            while (_shouldRun)
            {
                QueryLogic();
            }
        }

        private void QueryLogic()
        {
            if (_logics.Count == 1)
            {
                QuerySingleLogic();
            }
            else
            {
                QueryMultipleLogics();
            }
        }

        private void QuerySingleLogic()
        {
            //Lets the one logic class choose it's color and makes the best moves out of it
            Controller controller = GetController();
            foreach (Color colorChosen in _logics.Single().ChooseColor(controller.GetUpdate()).BestMoves)
            {
                Thread.Sleep(1000);
                Console.WriteLine(colorChosen);
                controller.PickColor(colorChosen);
            }
        }

        /// <summary>
        /// Asks each AILogic for it's vote of color for the next move and chooses the highest vote
        /// </summary>
        private void QueryMultipleLogics()
        {
            Controller controller = GetController();
            Dictionary<Color, int> colorVote = new Dictionary<Color, int> { {Color.Red, 0}, {Color.Blue, 0}, {Color.Yellow, 0}, {Color.Green, 0}, {Color.Orange, 0}, {Color.Purple, 0} };
            foreach (AILogic logic in _logics)
            {
                SuggestedMoves colorsChosen = logic.ChooseColor(controller.GetUpdate()); //reaches across other thread to get the current Board

                Color color = colorsChosen.BestMoves.First();
                colorVote[color]++;
            }

            Color highestVote = colorVote.OrderBy(keyValuePair => keyValuePair.Value).First().Key;
            controller.PickColor(highestVote);
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
            return Controller.Instance(null, null);
        }
    }
}
