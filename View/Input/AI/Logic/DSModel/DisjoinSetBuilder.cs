using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace View.Input.AI.Logic.DSModel
{
    class DisjoinSetBuilder
    {
        
        public static DisjoinSetCollection FromBoard(Color[,] board)
        {
            DisjoinSetCollection coll = new DisjoinSetCollection();
            //awful
            for (int y = 0; y < board.GetLength(0); y++)
            {
                for (int x = 0; x < board.GetLength(1); x++)
                {
                    bool isLeftSame = x != 0 && board[x, y] == board[x - 1, y];
                    bool isAboveSame = y != 0 && board[x, y] == board[x, y - 1];

                    if (isLeftSame && isAboveSame)//check above & to the left
                    {
                        /* These SetRepresentatives can really be inferred. We don't have to Find() them, 
                         * we know what they are. It's a less encapsulated design, but it removes the need 
                         * for a 'search' and an object's exsistence.
                         * @OPTIMIZE
                         */
                        //Merge the two sets
                        SetRepresentative aboveRep = coll.Find(new Point(x, y - 1)); 
                        SetRepresentative leftRep = coll.Find(new Point(x - 1, y));
                        coll.AddPointToSet(new Point(x, y), leftRep);
                        coll.Union(leftRep, aboveRep);
                    }
                    else if (isLeftSame)//check to the left
                    {
                        SetRepresentative rep = coll.Find(new Point(x - 1, y));
                        coll.AddPointToSet(new Point(x, y), rep);
                    }
                    else if (isAboveSame)//check above
                    {
                        SetRepresentative rep = coll.Find(new Point(x, y - 1));
                        coll.AddPointToSet(new Point(x, y), rep);
                    }
                    else
                    {
                        coll.MakeSet(new Point(x, y));
                    }
                }
            }

            return coll;
        }
    }
}
