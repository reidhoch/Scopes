namespace Scopes.Engine.Nodes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("√")]
    public class SquareRootNode : IFunctionNode
    {
        public SquareRootNode()
        {
            this.Children = new List<IGepNode>(this.Arity);
        }

        public IList<IGepNode> Children { get; private set; }

        public int Arity
        {
            get
            {
                return 1;
            }
        }

        public double Evaluate()
        {
            return Math.Sqrt(this.Children[0].Evaluate());
        }
    }
}
