﻿using System.Collections.Generic;

namespace GraphCollection
{
    public class DijiktraGraph<T> : IGraph<T> where T : class
    {
        //All nodes in graph are stored here
        public Dictionary<T, DijiktraNode<T>> NodesDictionary { get; }

        //All processed nodes are stored here
        public Dictionary<T, DijiktraNode<T>> Result { get;  }

        //Constructor, creates empty dictionary for Nodes collection
        public DijiktraGraph()
        {
            NodesDictionary = new Dictionary<T, DijiktraNode<T>>();
            Result = new Dictionary<T, DijiktraNode<T>>();
        }

        //Add nodes to Nodes Dictionary
        //if Nodes already exists return false
        //otherwise, create new node and return true
        public bool AddNodes(T value)
        {
            if (!NodesDictionary.ContainsKey(value))
            {
                NodesDictionary.Add(value, new DijiktraNode<T>(value));
                return true;
            }

            return false;
        }

        //Remove node completely from graph.
        public bool RemoveNodes(T value)
        {
            bool res = PopNodes(value);
            Result.Remove(value);
            return res;
        }

        //pop node with key value from dictionary
        //remove all reference to this node in dictionary
        //keep the node at Result dictionary.
        public bool PopNodes(T value)
        {
            if (NodesDictionary.ContainsKey(value))
            {
                Result.Add(value, NodesDictionary[value]);
                NodesDictionary.Remove(value);
                foreach (var item in NodesDictionary)
                {
                    item.Value.RemoveNeighbor(value);
                }

                return true;
            }

            return false;
        }

        //Add edge to the graph
        //An edge connects two nodes together
        //Also a cost is associated with the edge
        public bool AddEdge(T first, T second, int cost)
        {
            //Make sure both nodes are exist
            if (NodesDictionary.ContainsKey(first) && NodesDictionary.ContainsKey(second))
            {
                //make sure no edge connects both nodes exists already.
                if (!NodesDictionary[first].Neighbors.ContainsKey(second))
                {
                    NodesDictionary[first].AddNeighbor(second, cost);
                    //once edge added return true to comfirm.
                    return true;
                }
            }
            //if anyone of nodes does not exists
            //or an edge connects two nodes already exist
            //no edge will be added and return false
            return false;
        }

        //determine if graph is empty
        public bool IsGraphEmpty()
        {
            return (NodesDictionary.Count == 0);
        }

        //return all nodes in one list sorted by the weight associated with it.
        private List<T> GetSortedListOfNodesValue()
        {
            List<T> res = new List<T>();
            List<DijiktraNode<T>> toSort = new List<DijiktraNode<T>>();
            foreach (var item in NodesDictionary)
            {
                toSort.Add(item.Value);
            }
            toSort.Sort();
            foreach (var item in toSort)
            {
                res.Add(item.Value);
            }
            return res;
        }

        //get the node with smallest weight.
        public T GetSmallestNodes()
        {
           return GetSortedListOfNodesValue()[0];
        }
    }
}