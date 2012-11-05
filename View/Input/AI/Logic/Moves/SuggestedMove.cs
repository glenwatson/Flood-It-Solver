using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace View.Input.AI.Logic.Moves
{
    public class SuggestedMove
    {
        private HashSet<MoveWeight> suggestions = new HashSet<MoveWeight>();

        /// <summary>
        /// Orders the possible moves from best to worse
        /// </summary>
        public IEnumerable<MoveWeight> OrderedBest { get { return suggestions.OrderByDescending(move => move.Weight); } }

        public SuggestedMove(IEnumerable<Color> colors)
        {
            foreach(Color color in colors)
            {
                AddSuggestion(color, 1);
            }
        }
        public SuggestedMove(Color color) : this(color, 1) {}
        public SuggestedMove(Color color, int weight)
        {
            AddSuggestion(color, weight);
        }

        public void AddSuggestion(Color color, int weight)
        {
            suggestions.Add(new MoveWeight(color, weight));
        }

        public int GetWeight(Color color)
        {
            return suggestions.Single(move => move.Color == color).Weight;
        }

        internal void Intersect(SuggestedMove otherSuggestedMove)
        {
            suggestions.Intersect(otherSuggestedMove.suggestions);
        }
    }
}
