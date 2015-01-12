namespace Scopes.Engine.Nodes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("/")]
    public class DivisionNode : IFunctionNode
    {
        public DivisionNode()
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
            var num = this.Children[0].Evaluate();
            var den = this.Children[1].Evaluate();
            if (Math.Abs(den) < Double.Epsilon)
            {
                return Double.NaN;
            }
            return num / den;
        }
    }
}
