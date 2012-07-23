﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using View.Input.AI.Logic.MapModel;

namespace View.Input.AI.Logic
{
    class IncreaseSurfaceAreaLogic : AILogic
    {
        private int _lookAheadLevel;
        public IncreaseSurfaceAreaLogic(int lookAheadLevel)
        {
            _lookAheadLevel = lookAheadLevel;
        }
        public override SuggestedMoves ChooseColor(Color[,] board)
        {
            MapNode head = MapBuilder.BuildMap(board);
            List<MapNode> neighbors = head.GetNeighbors();

            int currentSurrfaceArea = neighbors.Count();
            int highestSurrfaceArea = currentSurrfaceArea;
            Color highestSurrfaceAreaColor = head.Color;

            foreach (MapNode neighbor in neighbors)
            {
                int neighborsIncreaseSurrfaceArea = neighbor.GetNeighbors().Count();
                if (neighborsIncreaseSurrfaceArea > highestSurrfaceArea)
                {
                    highestSurrfaceArea = neighborsIncreaseSurrfaceArea;
                    highestSurrfaceAreaColor = neighbor.Color;
                }
            }

            return new SuggestedMoves(highestSurrfaceAreaColor);
        }
    }
}
