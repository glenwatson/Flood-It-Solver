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
            this.Color = node.Color;
            node._neighbors.Clear(); //@OPTIMIZE We don't have to remove them, it should be understood that the node is invalid
        }
        public void PickColor(Color color)
        {
            foreach (MapNode mapNode in _neighbors.Where(mn => mn.Color == color))
            {
                this.Merge(mapNode);
            }
        }
        /// <summary>
        /// Copies the Color over to a new MapNode & .Clone()s neighbors
        /// </summary>
        /// <returns>A new MapNode</returns>
        public MapNode Clone()
        {
            MapNode clone = new MapNode(Color);
            foreach (MapNode neighbor in _neighbors)
            {
                clone.AddNeighbor(neighbor.Clone());
            }
            return clone;
        }
    }
}
