using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace View.Input.AI.Logic
{
    public class SuggestedMoves
    {
        private List<SuggestedMove> moves = new List<SuggestedMove>();

        public IEnumerable<Color> BestMoves { get { return moves.Select(move => move.Ordered.First().Color); }}

        public SuggestedMoves() {}

        public SuggestedMoves(Color color)
        {
            moves.Add(new SuggestedMove(color));
        }

        public void Add(SuggestedMove move)
        {
            moves.Add(move);
        }
    }
}
