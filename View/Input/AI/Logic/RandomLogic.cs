using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using View.Input.AI.Logic.Moves;

namespace View.Input.AI.Logic
{
    public class RandomLogic : AILogic
    {
        private Random rand;
        public RandomLogic()
        {
            rand = new Random();
        }

        public RandomLogic(int seed)
        {
            rand = new Random(seed);
        }

        public override SuggestedMoves ChooseColor(Color[,] board)
        {
            var randInt = rand.Next(0, Enum.GetValues(typeof(Color)).Length);
            var ans = (Color)randInt;
            return new SuggestedMoves(ans);
        }
    }
}
