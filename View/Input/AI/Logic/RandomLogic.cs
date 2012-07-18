using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace View.Input.AI.Logic
{
    class RandomLogic : AILogic
    {
        Random rand = new Random();
        public override SuggestedMoves ChooseColor(Color[,] board)
        {
            var randInt = rand.Next(0, Enum.GetValues(typeof(Color)).Length);
            var ans = (Color)randInt;
            return new SuggestedMoves(ans);
        }
    }
}
