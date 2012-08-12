using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using View.Input.AI.Logic.MapModel;
using View.Input.AI.Logic.Moves;

namespace View.Input.AI.Logic
{
    public class IncreaseSurfaceAreaMapLogic : AILogic
    {
        private int _lookAheadLevel;
        public IncreaseSurfaceAreaMapLogic(int lookAheadLevel)
        {
            _lookAheadLevel = lookAheadLevel;
        }
        public override SuggestedMoves ChooseColor(Color[,] board)
        {
            MapNode head = Builder.BuildMap(board);
            ISet<MapNode> neighbors = head.GetNeighbors();

            int highestSurfaceArea = int.MinValue;
            Color highestSurfaceAreaColor = head.Color;

            MapNode headClone = head.Clone();
            foreach (MapNode neighbor in neighbors)
            {
                int neighborsIncreaseSurrfaceArea = neighbor.GetNeighbors().Count;
                if (neighborsIncreaseSurrfaceArea > highestSurfaceArea)
                {
                    highestSurfaceArea = neighborsIncreaseSurrfaceArea;
                    highestSurfaceAreaColor = neighbor.Color;
                }
            }

            return new SuggestedMoves(highestSurfaceAreaColor);
        }


        class MapNodeDecisionTree
        {
            public MapNode CurrentMap { get; private set; }
            public Color Color { get { return CurrentMap.Color; } }
            public List<MapNodeDecisionTree> Children { get; private set; }
            public MapNodeDecisionTree(MapNode currentMap)
            {
                CurrentMap = currentMap;
                Children = new List<MapNodeDecisionTree>();
            }
        }
        private MapNodeDecisionTree BuildDecisionTree(MapNode head, int recurseLevel)
        {
            MapNodeDecisionTree decisionNode = new MapNodeDecisionTree(head);
            if (recurseLevel > 0)
            {
                foreach (MapNode neighbor in head.GetNeighbors()) //@OPTIMIZE duplicate work if two neighbors are the same color
                {
                    MapNode headClone = head.Clone();
                    headClone.PickColor(neighbor.Color); //make the move
                    MapNodeDecisionTree childNode = BuildDecisionTree(headClone, recurseLevel - 1);
                    decisionNode.Children.Add(childNode);
                }
            }
            return decisionNode;
        }


        private int GetSurfaceArea(MapNode head)
        {
            return head.GetNeighbors().Count;
        }
    }
}
