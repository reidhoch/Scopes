namespace Scopes.Engine.Nodes
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    using MathNet.Numerics;

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

        public IGepNode Clone()
        {
            return new ConstantNode { Value = this.Value };
        }

        public double Evaluate(double[] parameters)
        {
            return this.Value;
        }

        public override bool Equals(object obj)
        {
            if (null == obj) return false;

            var other = obj as ConstantNode;
            if (null == other) return false;

            return this.Value.AlmostEqual(other.Value, Precision.DoublePrecision);
        }

        public bool Equals(ConstantNode obj)
        {
            if (null == obj) return false;

            return this.Value.AlmostEqual(obj.Value, Precision.DoublePrecision);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }
}
