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
        private readonly List<AILogicWeight> _logics;
        private BackgroundWorker _logicThread;
        private bool _shouldRun = true;

        /// <summary>
        /// Uses multiple AILogics with their choice of color weighted<br/>
        /// (Order does not matter)
        /// </summary>
        /// <param name="weightedLogics">The AILogics to use with their weight</param>
        public AIInput(IEnumerable<AILogicWeight> weightedLogics)
        {
            _logics = weightedLogics.ToList();
            _logicThread = new BackgroundWorker();
            _logicThread.DoWork += StartQueryingLogic;
        }
        /// <summary>
        /// Uses multiple AILogics in the choice of color<br/>
        /// (Order does not matter)
        /// </summary>
        /// <param name="logics">The AILogics to use</param>
        public AIInput(params AILogic[] logics) : 
            this(logics.Select(logic => new AILogicWeight(logic, 1))) {}
        public AIInput(AILogic logic) : 
            this(new AILogic[]{logic}) {}

        public void BoardUpdated(Color[,] board)
        {
            //huh?
            //Allow query logic thread to allow the AI to calc the next Color
        }

        private void StartQueryingLogic(object sender, DoWorkEventArgs e)
        {
            while (_shouldRun)
            {
                QueryLogic();
            }
        }

        /// <summary>
        /// Asks the AI for it's suggestions and picks the best move(s)
        /// </summary>
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

        /// <summary>
        /// Queries the only AILogic and plays those moves
        /// </summary>
        private void QuerySingleLogic()
        {
            //Lets the one logic class choose it's color and makes the best moves out of it
            Controller controller = GetController();
            foreach (Color colorChosen in _logics.Single().Logic.ChooseColor(controller.GetUpdate()).BestMoves)
            {
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
            Dictionary<Color, int> colorVote = new Dictionary<Color, int>();
            foreach (AILogicWeight logic in _logics)
            {
                SuggestedMoves colorsChosen = logic.Logic.ChooseColor(controller.GetUpdate()); //reaches across other thread to get the current Board

                if (colorsChosen.BestMoves.Any()) //if there are any moves returned
                {
                    Color color = colorsChosen.BestMoves.First();
                    if (!colorVote.ContainsKey(color))
                    {
                        colorVote.Add(color, 0);
                    }
                    colorVote[color] += logic.Weight;
                }
            }

            if (colorVote.Count > 0)
            {
                Color highestVote = colorVote.OrderByDescending(keyValuePair => keyValuePair.Value).First().Key;
                Console.WriteLine(highestVote);
                controller.PickColor(highestVote);
            }
            else
            {
                Console.WriteLine("No colors were suggested!");
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
            return Controller.Instance(null, null);
        }
    }
    public class AILogicWeight
    {
        public AILogic Logic { get; private set; }
        public int Weight { get; private set; }
        public AILogicWeight(AILogic logic, int weight)
        {
            Logic = logic;
            Weight = weight;
        }
    }
}
