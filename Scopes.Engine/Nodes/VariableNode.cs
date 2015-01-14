namespace Scopes.Engine.Nodes
{
    using System.Diagnostics;

    [DebuggerDisplay("{Name}({Value})")]
    public class VariableNode : ITerminalNode
    {
        public int Arity { get { return 0; } }

        public string Name { get; set; }
        public double Value { get; set; }

        public double Evaluate()
        {
            return this.Value;
        }
    }
}
