using System;

namespace GraphCollection
{
    public class DijiktraNode<T> : Node<T>, IComparable<DijiktraNode<T>> where T : class
    {
        //when WeightSet == false
        //the filed _weight is considered to be equal to infinity.
        public bool WeightSet { get; private set; }

        //Previous Node (for route back trace)
        public T PreviousNode { get; set; }

        //backing field for property weight
        private int _weight;
        //The weight of this node
        public int Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                //once _weight is set,
                //flip WeightSet property to true.
                WeightSet = true;
            }
        }

        public DijiktraNode(T value) : base(value)
        {
            WeightSet = false;
        }

        //IComparable interface implementation
        public int CompareTo(DijiktraNode<T> value)
        {
            //if both nodes' weight set to infinity
            //then the weight is the same
            if (!WeightSet && !value.WeightSet)
            {
                return 0;
            }
            //if one node's weight set to infinity
            //then that node is bigger
            if (WeightSet && !value.WeightSet)
            {
                return -1;
            }
            if (!WeightSet && value.WeightSet)
            {
                return 1;
            }
            //if both nodes' value is set
            //then compare the weight as usual
            return (Weight - value.Weight);
        }
    }
}
