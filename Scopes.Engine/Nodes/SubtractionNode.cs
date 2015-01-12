namespace Scopes.Engine.Nodes
{
    using System;

    public class SubtractionNode : IFunctionNode
    {
        public int Arity
        {
            get
            {
                return 2;
            }
        }

        public IGepNode Right { get; set; }
        public IGepNode Left { get; set; }

        public double Evaluate()
        {
            return Left.Evaluate() - Right.Evaluate();
        }
    }
}
