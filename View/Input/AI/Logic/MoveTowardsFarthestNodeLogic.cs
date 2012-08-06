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
    class MoveTowardsFarthestNodeLogic : AILogic
    {
        public override SuggestedMoves ChooseColor(Color[,] board)
        {
            return new SuggestedMoves(GetPath(board).BestMoves.First());
        }

        public SuggestedMoves GetPath(Color[,] board)
        {
            TreeNode head = MapBuilder.BuildTree(board);
            TreeNode farthestNode = head.BFS().Last();

            //Follow the path back to the beginning
            SuggestedMoves suggestedMoves = new SuggestedMoves();

            TreeNode currentNode = farthestNode;
            while (currentNode.Parent != null)
            {
                suggestedMoves.AddFirst(new SuggestedMove(currentNode.Color));
                currentNode = currentNode.Parent;
            }

            return suggestedMoves;
        }


    }
}
