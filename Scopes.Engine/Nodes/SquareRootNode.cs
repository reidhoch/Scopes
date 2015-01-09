namespace Scopes.Engine.Nodes
{
    using System;

    public class SquareRootNode : IGepNode
    {
        public SquareRootNode(IGepNode child)
        {
            if (null == child) { throw new ArgumentNullException("child"); }
            this.Child = child;
        }

        public int Arity
        {
            get
            {
                return 1;
            }
        }

        public IGepNode Child { get; private set; }

        public double Evaluate()
        {
            return Math.Sqrt(Child.Evaluate());
        }
    }
}
