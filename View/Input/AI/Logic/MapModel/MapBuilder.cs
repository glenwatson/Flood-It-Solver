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
            MapNode[,] lookup = BuildLookupGrid(board);

            AddMapNeighbors(lookup);

            return lookup.GetAt(0, 0);
        }

        public static TreeNode BuildTree(Color[,] board)
        {
            //Used as lookup for with node a point belongs to.
            MapNode[,] lookup = BuildLookupGrid(board);

            MapNode[,] parents = AddTreeChildren(lookup);

            return root;
        }

        private static TreeNode[,] ToTreeNode(MapNode[,] lookup)
        {
            TreeNode[,] treeLookup = new TreeNode[lookup.Height(), lookup.Width()];
            for (int y = 0; y < lookup.Height(); y++)
            {
                for (int x = 0; x < lookup.Width(); x++)
                {
                    TreeNode treeNode = new TreeNode(lookup.GetAt(x, y).Color);
                    treeLookup.SetAt(x, y, treeNode);
                    throw new NotImplementedException("Doesn't keep the same reference for touching colors");
                }
            }
            return treeLookup;
        }

        private static MapNode[,] BuildLookupGrid(Color[,] board)
        {
            MapNode[,] lookup = new MapNode[board.Height(), board.Width()];

            //build lookup
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
                        lookup.SetAt(x, y, new MapNode(board.GetAt(x, y)));
                    }
                }
            }
            return lookup;
        }

        private static void AddMapNeighbors(MapNode[,] lookup)
        {
            //Add neighbors
            for (int y = 0; y < lookup.Height(); y++)
            {
                for (int x = 0; x < lookup.Width(); x++)
                {
                    bool isLeftNotSame = lookup.CanGetLeft(x) && lookup.GetAt(x, y).Color != lookup.GetLeftOf(x, y).Color;
                    bool isAboveNotSame = lookup.CanGetAbove(y) && lookup.GetAt(x, y).Color != lookup.GetAboveOf(x, y).Color;
                    bool isRightNotSame = lookup.CanGetRight(x) && lookup.GetAt(x, y).Color != lookup.GetRightOf(x, y).Color;
                    bool isBelowNotSame = lookup.CanGetBelow(y) && lookup.GetAt(x, y).Color != lookup.GetBelowOf(x, y).Color;
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
        }


        class WaitingMapNode
        {
            public MapNode MapNode { get; set; }
            public TreeNode Parent { get; set; }
        }
        private static MapNode[,] AddTreeChildren(MapNode[,] lookup)
        {
            MapNode[,] parents = new MapNode[lookup.Height(), lookup.Width()];
            //TreeNode root = new TreeNode(lookup[0, 0].Color);
            ////If it has a parent, it's been claimed by a parent node already
            //bool[,] visited = new bool[lookup.Height(), lookup.Width()];
            //Queue<WaitingMapNode> frontLine = new Queue<WaitingMapNode>();
            //frontLine.Enqueue(new WaitingMapNode() { MapNode = root });

            //while (frontLine.Count > 0)
            //{
            //    WaitingMapNode waiting = frontLine.Dequeue();
            //    TreeNode node = new TreeNode(waiting.Parent, waiting.MapNode.Color);


            //}

            //Add children
            for (int y = 0; y < lookup.Height(); y++)
            {
                for (int x = 0; x < lookup.Width(); x++)
                {
                    //visited.SetAt(x, y, true);
                    bool isLeftNotSame = lookup.CanGetLeft(x) && lookup.GetAt(x, y).Color != lookup.GetLeftOf(x, y).Color;
                    bool isAboveNotSame = lookup.CanGetAbove(y) && lookup.GetAt(x, y).Color != lookup.GetAboveOf(x, y).Color;
                    bool isRightNotSame = lookup.CanGetRight(x) && lookup.GetAt(x, y).Color != lookup.GetRightOf(x, y).Color;
                    bool isBelowNotSame = lookup.CanGetBelow(y) && lookup.GetAt(x, y).Color != lookup.GetBelowOf(x, y).Color;
                    MapNode currentNode = lookup.GetAt(x, y);
                    if (isAboveNotSame && parents.GetAboveOf(x, y) == null)//!visited.GetAboveOf(x, y))
                    {
                        parents.SetAboveOf(x, y, currentNode);
                        currentNode.AddNeighbor(lookup.GetAboveOf(x, y));
                    }
                    if (isLeftNotSame && parents.GetLeftOf(x, y) == null)
                    {
                        parents.SetLeftOf(x, y, currentNode);
                        currentNode.AddNeighbor(lookup.GetLeftOf(x, y));
                    }
                    if (isRightNotSame && parents.GetRightOf(x, y) == null)
                    {
                        parents.SetRightOf(x, y, currentNode);
                        currentNode.AddNeighbor(lookup.GetRightOf(x, y));
                    }
                    if (isBelowNotSame && parents.GetBelowOf(x, y) == null)
                    {
                        parents.SetBelowOf(x, y, currentNode);
                        currentNode.AddNeighbor(lookup.GetBelowOf(x, y));
                    }
                }
            }

            return parents;
        }

    }
}
