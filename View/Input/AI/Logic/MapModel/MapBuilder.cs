using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using View.Extentions;

namespace View.Input.AI.Logic.MapModel
{
    class MapBuilder
    {
        public static MapNode BuildMap(Color[,] board)
        {
            //Used as lookup for with node a point belongs to.
            MapNode[,] lookup = new MapNode[board.Height(), board.Width()];

            for (int y = 0; y < board.Height(); y++)
            {
                for (int x = 0; x < board.Width(); x++)
                {
                    //Create MapNode
                    bool isLeftSame = board.CanGetLeft(x) && board.GetAt(x, y) == board.GetLeftOf(x, y);
                    bool isAboveSame = board.CanGetAbove(y) && board.GetAt(x, y) == board.GetAboveOf(x, y);

                    if (isLeftSame && isAboveSame) //check above & to the left
                    {
                        MapNode left = lookup.GetLeftOf(x, y);
                        MapNode above = lookup.GetAboveOf(x, y);
                        lookup.SetAt(x, y, left);
                        left.Merge(above);
                    }
                    else if (isLeftSame) // check to the left
                    {
                        lookup.SetAt(x, y, lookup.GetLeftOf(x, y));
                    }
                    else if (isAboveSame) // check above
                    {
                        lookup.SetAt(x, y, lookup.GetAboveOf(x, y));
                    }
                    else
                    {
                        lookup.SetAt(x, y, new MapNode(board.GetAt(x, y)) );
                    }

                    //Add neighbors
                    bool isLeftNotSame = board.CanGetLeft(x) && board.GetAt(x, y) != board.GetLeftOf(x, y);
                    bool isAboveNotSame = board.CanGetAbove(y) && board.GetAt(x, y) != board.GetAboveOf(x, y);
                    bool isRightNotSame = board.CanGetRight(x) && board.GetAt(x, y) != board.GetRightOf(x, y);
                    bool isBelowNotSame = board.CanGetBelow(y) && board.GetAt(x, y) != board.GetBelowOf(x, y);
                    MapNode currentNode = lookup.GetAt(x, y);
                    if (isAboveNotSame)
                    {
                        currentNode.AddNeighbor(lookup.GetAboveOf(x, y));
                    }
                    if (isLeftNotSame)
                    {
                        currentNode.AddNeighbor(lookup.GetLeftOf(x, y));
                    }
                    if (isRightNotSame)
                    {
                        currentNode.AddNeighbor(lookup.GetRightOf(x, y));
                    }
                    if (isBelowNotSame)
                    {
                        currentNode.AddNeighbor(lookup.GetBelowOf(x, y));
                    }
                }
            }

            return lookup.GetAt(0, 0);
        }
    }
}
