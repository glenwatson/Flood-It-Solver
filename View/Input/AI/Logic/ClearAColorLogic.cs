using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using View.Input.AI.Logic.MapModel;
using View.Input.AI.Logic.Extentions;
using View.Input.AI.Logic.Moves;

namespace View.Input.AI.Logic
{
    class ClearAColorLogic : AILogic
    {
        public override SuggestedMoves ChooseColor(Color[,] board)
        {
            MapNode head = MapBuilder.BuildMap(board);
            ISet<MapNode> firstLayer = head.GetNeighbors();
            List<Color> possibleColorsToClear = firstLayer.Select(node => node.Color).ToList();
            
            IEnumerator<MapNode> breathFirstSearch = head.BFS().GetEnumerator();

            while(breathFirstSearch.MoveNext() && possibleColorsToClear.Count > 0)
            {
                MapNode currentNode = breathFirstSearch.Current;
                if (!firstLayer.Contains(currentNode))
                {
                    //can't wipe out color
                    possibleColorsToClear.Remove(currentNode.Color);
                }
            }
            SuggestedMoves moves = new SuggestedMoves();
            possibleColorsToClear.ForEach(color => moves.AddLast(new SuggestedMove(color)));
            return moves;
        }
    }
}
