﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using View.Input.AI.Logic.MapModel;
using View.Input.AI.Logic.Extentions;

namespace View.Input.AI.Logic
{
    class MoveTowardsFarthestNodeLogic : AILogic
    {
        public override SuggestedMoves ChooseColor(Color[,] board)
        {
            MapNode head = MapBuilder.BuildMap(board);
            MapNode farthestNode = head.BFS().Last();
            return new SuggestedMoves(farthestNode.Color);
        }
    }
}
