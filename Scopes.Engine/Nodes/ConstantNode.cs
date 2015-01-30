namespace Scopes.Engine.Nodes
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    [DebuggerDisplay("{Value}")]
    public class ConstantNode : ITerminalNode
    {
        public int Arity
        {
            [Pure]
            get
            {
                Contract.Ensures(Contract.Result<int>() == 0);
                return 0;
            }
        }

        public double Value { get; set; }

        public double Evaluate(double[] parameters)
        {
            return this.Value;
        }
    }
}
