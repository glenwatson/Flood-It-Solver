using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace View.Input.AI.Logic.MapModel
{
    class TreeNode : MapNode
    {
        private TreeNode _parent;

        public TreeNode(Color color) : base(color)
        {}

        public TreeNode(TreeNode parent, Color color) : base(color)
        {
            _parent = parent;
        }

        public TreeNode GetParent()
        {
            return _parent;
        }

        public IEnumerable<TreeNode> GetChildern()
        {
            return GetNeighbors().Cast<TreeNode>(); //@OPTIMIZE mass casting
        }

        public void AddChildern(TreeNode node)
        {
            AddNeighbor(node);
        }

        public void Merge(TreeNode node)
        {
            base.Merge(node);
            node._parent = null; //@OPTIMIZE see MapNode.Merge(MapNode)
        }
    }
}
