using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using View.Input.AI.Logic.Moves;
using Model;
using View.Input.AI.Logic.MapModel;

namespace View.Input.AI.Logic
{
    /// <summary>
    /// Picks the Color with the highest count along the current edge
    /// </summary>
    public class HighestCount : AILogic
    {
        public override SuggestedMoves ChooseColor(Color[,] board)
        {
            IDictionary<Color, int> count = new Dictionary<Color, int>();
            foreach(MapNode edgeNode in MapBuilder.BuildMap(board).GetNeighbors())
            {
                if (!count.ContainsKey(edgeNode.Color))
                {
                    count[edgeNode.Color] = 1;
                }
                else
                {
                    count[edgeNode.Color] = count[edgeNode.Color] + 1;
                }
            }
            return new SuggestedMoves(count.OrderByDescending(keyValuePair => keyValuePair.Value).First().Key);
        }
    }
}
