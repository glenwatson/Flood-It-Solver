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
        public IEnumerable<MoveWeight> Ordered { get { return suggestions.OrderByDescending(move => move.Weight); } }

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
            return suggestions.First(move => move.Color == color).Weight;
        }
    }
    public class MoveWeight
    {
        public Color Color { get; private set; }
        public int Weight { get; private set; }

        public MoveWeight(Color color, int weight)
        {
            Color = color;
            Weight = weight;
        }
    }
}
