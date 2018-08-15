using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionTree
{
    class DecisionTree
    {
        TreeNode Root = new TreeNode();
        public AttributePack data = new AttributePack();

        public DecisionTree()
        {

        }

        public void createDecisionTree(string filename)
        {
            string[] lines = System.IO.File.ReadAllLines(filename);

            string[] attributes = lines[0].Split(';');
            data.classifierName = attributes.Last();

            data.attributeNames = attributes.ToList();


            for (int i = 1; i < lines.Length; i++)
            {
                string[] dataset = lines[i].Split(';');
                Dictionary<string, string> dict = new Dictionary<string, string>();
                for (int j = 0; j < dataset.Length; j++)
                {
                    dict.Add(data.attributeNames[j], dataset[j]);
                }
                data.Add(new AttributeSet(dict));
            }
            //remove classifier
            data.attributeNames.Remove(data.attributeNames.Last());

            buildTree(data, Root);
        }

        public void buildTree(AttributePack dp, TreeNode node)
        {
            double maxGain = 0;
            string maxAttribute = null;

            if (dp.attributeNames.Count < 1)
            {
                //leave node mit mehrheit
                node.value = dp.attributeSets.Max(ds => ds.attributes[dp.classifierName]);
                return;
            }
            if (dp.attributeSets.All(ds => ds.attributes[dp.classifierName] == dp.attributeSets.First().attributes[dp.classifierName]))
            {
                node.value = dp.attributeSets.First().attributes[dp.classifierName];
                return;
            }

            foreach (string attributeName in dp.attributeNames)
            {
                // create Frequency table
                Frequency table = new Frequency(dp, attributeName);
                double gain = table.calcInfoGain();

                if (gain > maxGain)
                {
                    maxGain = gain;
                    maxAttribute = attributeName;
                }
            }
           
            node.value = maxAttribute;

            Dictionary<string, AttributePack> dataPacks = dp.sortDatapack(maxAttribute);
            foreach (var d in dataPacks)
            {
                TreeNode newNode = new TreeNode();
                node.nodes.Add(d.Key, newNode);
                buildTree(d.Value, newNode);
            }
        }

        public string classify(TreeNode node, AttributeSet ds)
        {
            if (node.nodes != null)
            {
                string attributeValue;
                TreeNode nextNode;
                ds.attributes.TryGetValue(node.value, out attributeValue);
                node.nodes.TryGetValue(attributeValue, out nextNode);

                return classify(nextNode, ds);
            }
            return node.value;
        }

        public void Print()
        {
            Print(Root, 0);
        }

        private void Print(TreeNode node, int depth)
        {
            //PrintTabs(depth);
            Console.WriteLine("[" + node.value + "]");
            if (node.nodes.Count > 0)
            {
                foreach (var n in node.nodes)
                {
                    PrintTabs(depth);
                    Console.Write(n.Key + " ->");
                    Print(n.Value, depth + 1);
                }
            }
        }

        private void PrintTabs(int value)
        {
            for (int i = 0; i < value; i++)
            {
                Console.Write("\t");
            }
        }
    }
}
