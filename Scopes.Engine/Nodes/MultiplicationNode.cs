namespace Scopes.Engine.Nodes
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    [DebuggerDisplay("*")]
    public class MultiplicationNode : IFunctionNode
    {
        private readonly IList<IGepNode> children;

        public MultiplicationNode()
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
            return this.Children[0].Evaluate(parameters) * this.Children[1].Evaluate(parameters);
        }
    }
}
