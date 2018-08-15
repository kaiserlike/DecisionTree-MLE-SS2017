using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionTree
{
    class AttributePack
    {
        public List<AttributeSet> attributeSets = new List<AttributeSet>();
        public List<string> attributeNames; 
        public string classifierName;

        public AttributePack(){ }

        public void Add(AttributeSet ds)
        {
            attributeSets.Add(ds);
        }

        public void Remove(AttributeSet ds)
        {
            attributeSets.Remove(ds);
        }

        // sort values and get Sets with same value
        public Dictionary<string, AttributePack> sortDatapack(string attName)
        {
            Dictionary<string, AttributePack> sortedDataPacks = new Dictionary<string, AttributePack>();
            foreach (var dataset in attributeSets)
            {
                string attributeValue = dataset.attributes[attName];
                if (!sortedDataPacks.ContainsKey(attributeValue))
                {
                    sortedDataPacks.Add(attributeValue, new AttributePack());
                }
                sortedDataPacks[attributeValue].Add(dataset);
            }

            List<string> newAttributeNames = attributeNames.Where(s => s != attName).ToList();

            foreach (var datapack in sortedDataPacks.Values)
            {
                datapack.attributeNames = newAttributeNames;
                datapack.classifierName = classifierName;
            }

            
            return sortedDataPacks;
        }
    }
}
