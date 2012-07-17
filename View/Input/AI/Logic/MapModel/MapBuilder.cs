﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using View.Input.AI.Logic.Extentions;

namespace View.Input.AI.Logic.MapModel
{
    class MapBuilder
    {
        public static MapNode BuildMap(Color[,] board)
        {
            MapNode head = new MapNode(board.GetColorAt(0,0));
            //Used as lookup for with node a point belongs to.
            MapNode[,] lookup = new MapNode[board.Height(), board.Width()];
            lookup[0, 0] = head;

            for (int y = 0; y < board.Height(); y++)
            {
                for (int x = 0; x < board.Width(); x++)
                {
                    //Create MapNode
                    bool isLeftSame = board.CanGetLeft(x) && board.GetColorAt(x, y) == board.GetLeftOf(x, y);
                    bool isAboveSame = board.CanGetAbove(y) && board.GetColorAt(x, y) == board.GetAboveOf(x, y);

                    if (isLeftSame && isAboveSame) //check above & to the left
                    {
                        MapNode left = lookup[x - 1, y];
                        MapNode above = lookup[x, y - 1];
                        lookup[x, y] = left;
                        left.Merge(above);
                    }
                    else if (isLeftSame) // check to the left
                    {
                        lookup[x, y] = lookup[x - 1, y];
                    }
                    else if (isAboveSame) // check above
                    {
                        lookup[x, y] = lookup[x, y - 1];
                    }
                    else
                    {
                        lookup[x, y] = new MapNode(board.GetColorAt(x, y));
                    }

                    //Add neighbors
                    bool isLeftNotSame = board.CanGetLeft(x) && board.GetColorAt(x, y) != board.GetLeftOf(x, y);
                    bool isAboveNotSame = board.CanGetAbove(y) && board.GetColorAt(x, y) != board.GetAboveOf(x, y);
                    bool isRightNotSame = board.CanGetRight(x) && board.GetColorAt(x, y) != board.GetRightOf(x, y);
                    bool isBelowNotSame = board.CanGetBelow(y) && board.GetColorAt(x, y) != board.GetBelowOf(x, y);
                    MapNode currentNode = lookup[x, y];
                    if (isAboveNotSame)
                    {
                        currentNode.AddNeighbor(lookup[x, y - 1]);
                    }
                    if (isLeftNotSame)
                    {
                        currentNode.AddNeighbor(lookup[x - 1, y]);
                    }
                    if (isRightNotSame)
                    {
                        currentNode.AddNeighbor(lookup[x + 1, y]);
                    }
                    if (isBelowNotSame)
                    {
                        currentNode.AddNeighbor(lookup[x, y + 1]);
                    }
                }
            }

            return head;
        }
    }
}
