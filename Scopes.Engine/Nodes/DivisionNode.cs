namespace Scopes.Engine.Nodes
{
    using System;

    public class DivisionNode : IFunctionNode
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
            var num = Left.Evaluate();
            var den = Right.Evaluate();
            if (Math.Abs(den) < Double.Epsilon)
            {
                return Double.NaN;
            }
            return num / den;
        }
    }
}
