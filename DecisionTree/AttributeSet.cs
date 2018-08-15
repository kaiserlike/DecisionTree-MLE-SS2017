using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionTree
{
    class AttributeSet
    {
        public Dictionary<string, string> attributes { get; private set; }

        public AttributeSet(Dictionary<string, string> att)
        {
            attributes = att;
        }
    }
}
