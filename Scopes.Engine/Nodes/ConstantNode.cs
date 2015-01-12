namespace Scopes.Engine.Nodes
{
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("{Value}")]
    public class ConstantNode : ITerminalNode
    {
        public IList<IGepNode> Children { get; private set; }

        public int Arity
        {
            get { return 0; }
        }

        public double Value { get; set; }

        public double Evaluate()
        {
            return this.Value;
        }
    }
}
