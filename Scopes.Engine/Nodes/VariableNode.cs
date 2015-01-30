namespace Scopes.Engine.Nodes
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    [DebuggerDisplay("Var{index}")]
    public class VariableNode : ITerminalNode
    {
        private readonly int index;

        public VariableNode(int index)
        {
            Contract.Requires<ArgumentOutOfRangeException>(index >= 0);
            this.index = index;
        }

        public int Arity
        {
            [Pure]
            get
            {
                Contract.Ensures(Contract.Result<int>() == 0);
                return 0;
            }
        }

        public double Evaluate(double[] parameters)
        {
            return parameters[index];
        }
    }
}
