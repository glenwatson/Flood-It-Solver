using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace View.Input.AI.Logic.MapModel
{
    class MapNode
    {
        public Color Color { get; private set; }
        private ISet<MapNode> _neighbors = new HashSet<MapNode>();

        public MapNode(Color color)
        {
            Color = color;
        }

        public void AddNeighbor(MapNode node)
        {
            _neighbors.Add(node);
        }

        public ISet<MapNode> GetNeighbors()
        {
            return _neighbors;
        }

        public void Merge(MapNode node)
        {
            this._neighbors.Concat(node._neighbors);
            node._neighbors.Clear(); //@OPTIMIZE We don't have to remove them, it should be understood that the node is invalid
        }
    }
}
