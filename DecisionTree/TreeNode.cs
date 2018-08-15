using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionTree
{
    class TreeNode
    {
        public Dictionary<string, TreeNode> nodes = null;
        public string value;

        public TreeNode()
        {
            nodes = new Dictionary<string, TreeNode>();
        }
    }
}
