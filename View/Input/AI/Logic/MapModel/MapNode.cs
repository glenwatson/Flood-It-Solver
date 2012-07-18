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
        private List<MapNode> _neighbors = new List<MapNode>();

        public MapNode(Color color)
        {
            Color = color;
        }

        public void AddNeighbor(MapNode node)
        {
            _neighbors.Add(node);
        }

        public List<MapNode> GetNeighbors()
        {
            return _neighbors;
        }

        public void Merge(MapNode node)
        {
            this._neighbors.AddRange(node._neighbors);
            node._neighbors.Clear(); //@OPTIMIZE We don't have to remove them, it should be understood that the node is invalid
        }
    }
}
