using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphCollection;
using System.Collections.Generic;
using System;

namespace DijiktraAlgorithmTest
{
    [TestClass]
    public class MainAlgorithmTest
    {
        [TestMethod]
        public void BasicFunctionTest()
        {
            DijktraPathFinder<string> dijktraPathFinder = new DijktraPathFinder<string>();
            var nodToAdd = new List<string>() { "loc1", "loc2", "loc3", "loc4", "loc5", "loc6", "loc7", "loc8" };
            var edge1 = new Tuple<string, string, int>("loc1", "loc2", 3);
            var edge2 = new Tuple<string, string, int>("loc1", "loc3", 6);
            var edge3 = new Tuple<string, string, int>("loc2", "loc3", 1);
            var edge4 = new Tuple<string, string, int>("loc2", "loc5", 2);
            var edge5 = new Tuple<string, string, int>("loc2", "loc4", 3);
            var edge6 = new Tuple<string, string, int>("loc4", "loc5", 7);
            var edge7 = new Tuple<string, string, int>("loc3", "loc4", 1);
            var edge8 = new Tuple<string, string, int>("loc3", "loc5", 2);
            var edge9 = new Tuple<string, string, int>("loc4", "loc6", 4);
            var edge10 = new Tuple<string, string, int>("loc3", "loc8", 12);
            var edge11 = new Tuple<string, string, int>("loc2", "loc7", 8);
            var edge12 = new Tuple<string, string, int>("loc6", "loc8", 5);
            var edge13 = new Tuple<string, string, int>("loc7", "loc8", 2);
            var edgeToAdd = new List<Tuple<string, string, int>>() { edge1, edge2, edge3, edge4, edge5, edge6, edge7, edge8, edge9, edge10, edge11, edge12, edge13 };
            foreach(var item in nodToAdd)
            {
                dijktraPathFinder.ClearGraph();
                int addednodes = dijktraPathFinder.AddListOfNodes(nodToAdd);
                int addededge = dijktraPathFinder.AddListOfEdge(edgeToAdd);
                Console.WriteLine("Number of Nodes to add: {0}, Number of Nodes added: {1}", nodToAdd.Count, addednodes);
                Console.WriteLine("Number of Edges to add: {0}, Number of Edges added: {1}", edgeToAdd.Count, addededge);
                Console.WriteLine();
                Console.WriteLine("Start from {0}: ", item);
                Console.WriteLine();
                Console.WriteLine();
                foreach (var node in nodToAdd)
                {
                    var pathInfo = dijktraPathFinder.GetPath(item, node);
                    if (pathInfo == null)
                    {
                        Console.WriteLine("Route to {0}: Unreachable", node);
                        Console.WriteLine();
                        continue;
                    }
                    var path = pathInfo.Item1;
                    var cost = pathInfo.Item2;
                    Console.WriteLine("Route to {0}: {1}", node, path);
                    Console.WriteLine("Cost of Route to {0}: {1}", node, cost);
                    Console.WriteLine();
                }
            }
        }
    }
}
