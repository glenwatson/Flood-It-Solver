using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace View.Input.AI.Logic.Moves
{
    public class SuggestedMoves
    {
        private LinkedList<SuggestedMove> moves = new LinkedList<SuggestedMove>();

        public IEnumerable<Color> BestMoves { get { return moves.Select(move => move.Ordered.First().Color); }}

        public SuggestedMoves() {}

        public SuggestedMoves(Color color)
        {
            moves.AddLast(new SuggestedMove(color));
        }

        public void AddLast(SuggestedMove move)
        {
            moves.AddLast(move);
        }

        public void AddFirst(SuggestedMove move)
        {
            moves.AddFirst(move);
        }

        public SuggestedMove First()
        {
            return moves.First.Value;
        }
    }
}
