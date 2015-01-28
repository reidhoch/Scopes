namespace Scopes.Engine.Nodes
{
    using System.Diagnostics;

    [DebuggerDisplay("{Value}")]
    public class ConstantNode : ITerminalNode
    {
        public int Arity
        {
            get { return 0; }
        }

        public double Value { get; set; }

        public double Evaluate(double[] parameters)
        {
            return this.Value;
        }
    }
}
