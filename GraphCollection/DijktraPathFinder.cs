﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GraphCollection
{
    public class DijktraPathFinder<T> where T : class
    {
        public DijiktraGraph<T> MainGraph { get; }

        private bool FINISHED;

        public DijktraPathFinder()
        {
            MainGraph = new DijiktraGraph<T>();
            FINISHED = false;
        }

        //Add one node into graph
        //returns a boolean value indicate if the edge has been added successfully
        public bool AddNodes(T nodes)
        {
            return MainGraph.AddNodes(nodes);
        }

        //Add list of nodes into graph
        //returns an integer value indicate if the edge has been added successfully
        public int AddListOfNodes(List<T> nodes)
        {
            int res = 0;
            foreach (var item in nodes)
            {
                var addRes = MainGraph.AddNodes(item);
                if (addRes)
                {
                    res++;
                }
            }

            return res;
        }

        //Add one edge into graph
        //returns a boolean value indicate if the edge has been added successfully
        public bool AddEdge(Tuple<T, T, int> edge)
        {
            return MainGraph.AddEdge(edge.Item1, edge.Item2, edge.Item3);
        }

        //Add list of edges into graph
        //Returns an integer value indicate how many deges have been added to graph sucessfully
        public int AddListOfEdge(List<Tuple<T, T, int>> edges)
        {
            int res = 0;
            foreach (var item in edges)
            {
                var addRes = MainGraph.AddEdge(item.Item1, item.Item2, item.Item3);
                if (addRes)
                {
                    res++;
                }
            }

            return res;
        }

        public void ClearGraph()
        {
            MainGraph.ClearGraph();
            FINISHED = false;
        }

        //Find shortest path to every reachable node(from start point)
        //using Dijktra's Algorithm
        public bool FindPaths(T startPoint)
        {
            if (!MainGraph.NodesDictionary.ContainsKey(startPoint))
            {
                return false;
            }

            //Special case
            //handle the start point node first
            var neighbors = MainGraph.NodesDictionary[startPoint].Neighbors;
            foreach (var item in neighbors)
            {
                //Set the weight of neighbor node
                MainGraph.NodesDictionary[item.Key].Weight = item.Value;
                //set the neighbor node's previous node point to start point.
                MainGraph.NodesDictionary[item.Key].PreviousNode = startPoint;
            }
            //once visited all neighbors, remove it from graph
            //however the node is kept at result dictionary
            //Refer to DijktraGraph class.
            MainGraph.PopNodes(startPoint);

            //handle all the reachable nodes in graph
            while (!MainGraph.IsGraphEmpty())
            {
                //get the node with smallest weight
                var nodeToVisit = MainGraph.GetSmallestNodes();
                //if the minimum value in all nodes in graph is infinite means
                //that graph contains nodes that not reachable from given start point
                //So algorithm reaches the end before entire graph is traversed.
                if (!MainGraph.NodesDictionary[nodeToVisit].WeightSet)
                {
                    return true;
                }
                //get node's weight, used to calculate the new weight
                int weight = MainGraph.NodesDictionary[nodeToVisit].Weight;
                var allNeighbors = MainGraph.NodesDictionary[nodeToVisit].Neighbors;
                //traverse through all neighbor nodes
                foreach (var item in allNeighbors)
                {
                    var neighborNode = MainGraph.NodesDictionary[item.Key];
                    //new weight proposed to neighbor node is 
                    //current node's weight + cost of edge connect these two nodes
                    int newWeight = item.Value + weight;
                    //if weight is set to infinity
                    //this node is unvisited, set the weight
                    //and previous node.
                    if (!neighborNode.WeightSet)
                    {
                        neighborNode.Weight = newWeight;
                        neighborNode.PreviousNode = nodeToVisit;
                    }
                    else
                    {
                        //if the cumulative weight is smaller then 
                        //neighbor's current weight, means this route is shorter
                        //set the new weight and new previous node
                        if (neighborNode.Weight > newWeight)
                        {
                            neighborNode.Weight = newWeight;
                            neighborNode.PreviousNode = nodeToVisit;
                        }
                    }
                }

                MainGraph.PopNodes(nodeToVisit);
            }
            return true;
        }

        //Reconstruct the path from result dictionary.
        public Tuple<String, int> GetPath(T startPoint, T endPoint)
        {
            if (endPoint.Equals(startPoint))
            {
                return new Tuple<string, int>((startPoint + " -> " + startPoint), 0);
            }
            if (!FINISHED)
            {
                FINISHED = FindPaths(startPoint);
            }
            //Check if find path method executed successfully
            if (FINISHED)
            {
                var resultDictionary = MainGraph.Result;
                //Check if endPoint does exists
                if (!resultDictionary.ContainsKey(endPoint) || resultDictionary[endPoint].PreviousNode == null)
                {
                    return null;
                }

                T current = resultDictionary[endPoint].Value;
                //find the total cost of the path
                int finalCost = resultDictionary[endPoint].Weight;
                StringBuilder pathBuilder = new StringBuilder(current.ToString());
                //Traverse through the path and construct path in string type
                while (resultDictionary[current].PreviousNode != null)
                {
                    pathBuilder.Insert(0, " -> ");
                    current = resultDictionary[current].PreviousNode;
                    pathBuilder.Insert(0, current.ToString());
                }
                //construct the result in tuple type
                //item 1 is the path in string type
                //item 2 is the cost of this path
                var result = new Tuple<String, int>(pathBuilder.ToString(), finalCost);
                return result;
            }
            return null;
        }
    }
}
