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

        public IGepNode Clone()
        {
            return new VariableNode(this.index);
        }

        public double Evaluate(double[] parameters)
        {
            return parameters[index];
        }

        public override bool Equals(object obj)
        {
            if (null == obj) return false;

            var other = obj as VariableNode;
            if (null == other) return false;

            return this.index == other.index;
        }

        public bool Equals(VariableNode obj)
        {
            if (null == obj) return false;

            return this.index == obj.index;
        }

        public override int GetHashCode()
        {
            return this.index.GetHashCode();
        }
    }
}
