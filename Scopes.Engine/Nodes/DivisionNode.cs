namespace Scopes.Engine.Nodes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    [DebuggerDisplay("/")]
    public class DivisionNode : IFunctionNode
    {
        private readonly IList<IGepNode> children;

        public DivisionNode()
        {
            this.children = new List<IGepNode>(this.Arity);
        }

        public IList<IGepNode> Children
        {
            get
            {
                Contract.Ensures(Contract.Result<IList<IGepNode>>() != null);
                return this.children;
            }
        }

        public int Arity
        {
            get
            {
                return 2;
            }
        }

        public double Evaluate(double[] parameters)
        {
            var num = this.Children[0].Evaluate(parameters);
            var den = this.Children[1].Evaluate(parameters);
            if (Math.Abs(den) < Double.Epsilon)
            {
                return Double.NaN;
            }
            return num / den;
        }
    }
}
