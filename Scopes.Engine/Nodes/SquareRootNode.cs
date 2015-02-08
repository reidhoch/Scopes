namespace Scopes.Engine.Nodes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Linq;

    [DebuggerDisplay("√")]
    public class SquareRootNode : IFunctionNode, IEquatable<SquareRootNode>
    {
        private readonly IList<IGepNode> children;

        public SquareRootNode()
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
                return 1;
            }
        }

        public IGepNode Clone()
        {
            return new SquareRootNode();
        }

        public double Evaluate(double[] parameters)
        {
            return Math.Sqrt(this.Children[0].Evaluate(parameters));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (null == obj) return false;

            var other = obj as SquareRootNode;
            if (null == other) return false;

            return this.children.SequenceEqual(other.children);
        }

        public bool Equals(SquareRootNode obj)
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
