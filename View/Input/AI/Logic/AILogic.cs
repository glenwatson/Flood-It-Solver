using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using View.Input.AI.Logic.Moves;

namespace View.Input.AI.Logic
{
    public abstract class AILogic
    {
        //protected AILogic() { }
        public abstract SuggestedMoves ChooseColor(Color[,] board);
        public abstract void ChoseColor(Color color);
    }
}
