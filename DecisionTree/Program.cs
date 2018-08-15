using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionTree
{
    class Program
    {
        static void Main(string[] args)
        {

            DecisionTree dt = new DecisionTree();

            dt.createDecisionTree("..\\..\\ToPlayOrNotToPlay.csv");
            //dt.createDecisionTree("..\\..\\PartyOrNot.csv");

            dt.Print();

            Console.ReadLine();
        }
    }
}
