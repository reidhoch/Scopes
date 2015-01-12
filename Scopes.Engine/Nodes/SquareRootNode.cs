namespace Scopes.Engine.Nodes
{
    using System;

    public class SquareRootNode : IFunctionNode
    {
        public int Arity
        {
            get
            {
                return 1;
            }
        }

        public IGepNode Child { get; set; }

        public double Evaluate()
        {
            return Math.Sqrt(Child.Evaluate());
        }
    }
}
