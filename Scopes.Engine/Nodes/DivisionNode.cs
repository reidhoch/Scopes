namespace Scopes.Engine.Nodes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Linq;

    [DebuggerDisplay("/")]
    public class DivisionNode : IFunctionNode, IEquatable<DivisionNode>
    {
        private readonly IList<IGepNode> children;

        public DivisionNode()
        {
            this.children = new List<IGepNode>(this.Arity);
        }

        public IList<IGepNode> Children
        {
            get
            {
                Contract.Ensures(Contract.Result<IList<IGepNode>>() != null);
                return this.children;
            }
        }

        public int Arity
        {
            get
            {
                return 2;
            }
        }

        public IGepNode Clone()
        {
            return new DivisionNode();
        }

        public double Evaluate(double[] parameters)
        {
            var num = this.Children[0].Evaluate(parameters);
            var den = this.Children[1].Evaluate(parameters);
            if (Math.Abs(den) < Double.Epsilon) {
                return Double.NaN;
            }
            return num / den;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (null == obj) return false;

            var other = obj as DivisionNode;
            if (null == other) return false;

            return this.children.SequenceEqual(other.children);
        }

        public bool Equals(DivisionNode obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (null == obj) return false;

            return this.children.SequenceEqual(obj.children);
        }

        public override int GetHashCode()
        {
            return this.children.GetHashCode();
        }
    }
}
