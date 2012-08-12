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
        public LinkedList<SuggestedMove> Moves { get; private set; }
        public IEnumerable<Color> BestMoves { get { return Moves.Select(move => move.OrderedBest.First().Color); }}

        public SuggestedMoves() 
        {
            Moves = new LinkedList<SuggestedMove>();
        }

        public SuggestedMoves(Color color) : this()
        {
            Moves.AddLast(new SuggestedMove(color));
        }

        public void AddLast(SuggestedMove move)
        {
            Moves.AddLast(move);
        }

        public void AddFirst(SuggestedMove move)
        {
            Moves.AddFirst(move);
        }

        public SuggestedMove First()
        {
            return Moves.First.Value;
        }

        public void Intersect(SuggestedMoves moves)
        {
            IEnumerator<SuggestedMove> thisEnumerator = Moves.GetEnumerator();
            IEnumerator<SuggestedMove> otherEnumerator = moves.Moves.GetEnumerator();
            while (thisEnumerator.MoveNext() && otherEnumerator.MoveNext())
            {
                SuggestedMove thisSuggestedMove = thisEnumerator.Current;
                SuggestedMove otherSuggestedMove = otherEnumerator.Current;
                thisSuggestedMove.Intersect(otherSuggestedMove);
            }
            moves.Moves = null; //trash the other data
        }
    }
}
