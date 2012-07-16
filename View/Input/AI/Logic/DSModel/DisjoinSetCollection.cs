using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Input.AI.Logic.DSModel
{
    class DisjoinSetCollection
    {
        List<DisjointSet> sets = new List<DisjointSet>();

        public void MakeSet(Point point)
        {
            DisjointSet set = new DisjointSet();
            set.Add(point);
            sets.Add(set);
        }
        private DisjointSet FindSetWithRep(Point rep)
        {
            foreach (DisjointSet set in sets)
            {
                if (set.Contains(rep))
                    return set;
            }
            return null;
        }
        public SetRepresentative Find(Point point)
        {
            DisjointSet set = FindSetWithRep(point);

            return set == null ? default(SetRepresentative) : set.Representative;
        }

        public void Union(SetRepresentative rep1, SetRepresentative rep2)
        {
            DisjointSet ds1 = FindSetWithRep(rep1.GetRep());
            DisjointSet ds2 = FindSetWithRep(rep2.GetRep());
            ds1.Union(ds2);
        }

        internal void AddPointToSet(Point point, SetRepresentative rep)
        {
            FindSetWithRep(rep.GetRep()).Add(point);
        }
    }
}
