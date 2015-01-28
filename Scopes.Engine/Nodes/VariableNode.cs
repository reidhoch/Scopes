namespace Scopes.Engine.Nodes
{
    using System.Diagnostics;

    [DebuggerDisplay("{Name}({Value})")]
    public class VariableNode : ITerminalNode
    {
        private readonly int index;

        public VariableNode(int index)
        {
            this.index = index;
        }

        public int Arity { get { return 0; } }

        public double Evaluate(double[] parameters)
        {
            return parameters[index];
        }
    }
}
