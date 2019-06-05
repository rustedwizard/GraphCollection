using System;

namespace GraphCollection
{
    public class DijiktraNode<T> : Node<T>
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
    }
}
