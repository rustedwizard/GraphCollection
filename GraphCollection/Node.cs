using System.Collections.Generic;

namespace GraphCollection
{
    public abstract class Node<T> : INode<T>
    {
        //Property Value: this is the business value of the node
        public T Value { get; set; }

        //Property Neighbors: this dictionary used to keep track all of its neighbors
        //It uses Value as the key
        //the cost(the value of the edge) as its value
        public Dictionary<T, int> Neighbors { get; }

        protected Node(T value)
        {
            Value = value;
            Neighbors = new Dictionary<T, int>();
        }

        //Add Neighbor to current node
        public virtual void AddNeighbor(T value, int cost)
        {
            Neighbors.Add(value, cost);
        }

        //Remove neighbor from current node
        //if neighbor exists, return true;
        //otherwise return false;
        public virtual bool RemoveNeighbor(T value)
        {
            if (Neighbors.ContainsKey(value))
            {
                Neighbors.Remove(value);
                return true;
            }
            return false;
        }

        //Get list of neighbors
        //List only contains the value of nodes
        public virtual List<T> GetNeighborsList()
        {
            List<T> res = new List<T>();
            foreach (var item in Neighbors)
            {
                res.Add(item.Key);
            }

            return res;
        }
    }
}
