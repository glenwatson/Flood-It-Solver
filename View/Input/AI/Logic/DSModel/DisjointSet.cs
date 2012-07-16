using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Input.AI.Logic.DSModel
{
    class DisjointSet
    {
        private LinkedList<Point> set = new LinkedList<Point>();

        public SetRepresentative Representative { get { return new SetRepresentative(set.First.Value); } }

        public bool Contains(Point point)
        {
            return set.Contains(point);
        }

        internal void Add(Point point)
        {
            set.AddLast(point);
        }

        internal void Union(DisjointSet ds2)
        {
            set.Concat(ds2.set);
        }
    }
}
