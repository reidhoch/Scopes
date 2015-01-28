namespace Scopes.Engine.Nodes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    [DebuggerDisplay("√")]
    public class SquareRootNode : IFunctionNode
    {
        private readonly IList<IGepNode> children;

        public SquareRootNode()
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
                return 1;
            }
        }

        public double Evaluate(double[] parameters)
        {
            return Math.Sqrt(this.Children[0].Evaluate(parameters));
        }
    }
}
