namespace Scopes.Engine.Nodes
{
    using System;

    public class SubtractionNode: IGepNode
    {
        public SubtractionNode(IGepNode left, IGepNode right)
        {
            if (null == left) { throw new ArgumentNullException("left"); }
            if (null == right) { throw new ArgumentNullException("right"); }
            this.Left = left;
            this.Right = right;
        }

        public int Arity
        {
            get
            {
                return 2;
            }
        }

        public IGepNode Right { get; private set; }
        public IGepNode Left { get; private set; }

        public double Evaluate()
        {
            return Left.Evaluate() - Right.Evaluate();
        }
    }
}
