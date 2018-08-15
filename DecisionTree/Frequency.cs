using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionTree
{
    class Frequency
    {
        Dictionary<string, Dictionary<string, int>> _freqTable = new Dictionary<string, Dictionary<string, int>>();
        private AttributePack attributePack;

        public Dictionary<string, Dictionary<string, int>> FreqTable
        {
            get
            {
                return _freqTable;
            }

            set
            {
                _freqTable = value;
            }
        }

        public Frequency(AttributePack ap, string attributeName)
        {
            string classifierName = ap.classifierName;
            attributePack = ap;

            foreach (AttributeSet ds in ap.attributeSets)
            {
                //Values durchgehen
                string attributeValue;
                ds.attributes.TryGetValue(attributeName, out attributeValue);
                // check ob bereits existiert
                if (!_freqTable.ContainsKey(attributeValue))
                {
                    _freqTable.Add(attributeValue, new Dictionary<string, int>());
                }
                // classifier bestimmen
                string classifierValue;
                ds.attributes.TryGetValue(classifierName, out classifierValue);

                // classifier value checken und hochzählen
                Dictionary<string, int> a;
                if (_freqTable.TryGetValue(attributeValue, out a))
                {
                    if (!a.ContainsKey(classifierValue))
                    {
                        a.Add(classifierValue, 0);
                    }
                }

                _freqTable[attributeValue][classifierValue]++;
            }
        }

        public double calcInfoGain()
        {
            Dictionary<string, int> classifierValues = new Dictionary<string, int>();
            foreach (var row in _freqTable.Keys)
            {
                foreach (var column in _freqTable[row])
                {
                    if(!classifierValues.ContainsKey(column.Key))
                    {
                        classifierValues.Add(column.Key, 0);
                    }
                    classifierValues[column.Key] += column.Value;
                }
            }
            // all classifierValues / Anzahl Daten
            double[] temp = classifierValues.Values.Select(i => (double) i / attributePack.attributeSets.Count).ToArray();

            double entropyFrequencyTable = calcEntropy(temp);


            //substract  all attributes from entropyFrequencyTable
            foreach (var row in _freqTable.Keys)
            {
                int attributeSum = 0;
                foreach (var value in _freqTable[row].Values)
                {
                    attributeSum += value;
                }
                temp = _freqTable[row].Values.Select(i => (double) i / attributeSum).ToArray();

                entropyFrequencyTable -= (double) attributeSum / attributePack.attributeSets.Count * calcEntropy(temp);
            }

            return entropyFrequencyTable;
        }

        public double calcEntropy(double[] values)
        {
            double entropy = 0;
            foreach (double value in values)
            {
                entropy += -value * Log2(value);
            }
            return entropy;
        }

        private double Log2(double value)
        {
            return Math.Log10(value) / Math.Log10(2);
        }
    }
}
