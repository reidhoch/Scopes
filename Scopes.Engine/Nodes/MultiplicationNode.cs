namespace Scopes.Engine.Nodes
{
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("*")]
    public class MultiplicationNode : IFunctionNode
    {
        public MultiplicationNode()
        {
            this.Children = new List<IGepNode>(this.Arity);
        }

        public IList<IGepNode> Children { get; private set; }

        public int Arity
        {
            get
            {
                return 2;
            }
        }

        public double Evaluate()
        {
            return this.Children[0].Evaluate() * this.Children[1].Evaluate();
        }
    }
}
