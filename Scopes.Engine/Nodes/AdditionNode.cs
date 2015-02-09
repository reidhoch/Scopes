namespace Scopes.Engine.Nodes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Linq;

    [DebuggerDisplay("+")]
    public class AdditionNode : IFunctionNode, IEquatable<AdditionNode>
    {
        private readonly IList<IGepNode> children;

        public AdditionNode()
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
            [Pure]
            get
            {
                return 2;
            }
        }

        public IGepNode Clone()
        {
            return new AdditionNode();
        }

        public double Evaluate(double[] parameters)
        {
            return this.Children[0].Evaluate(parameters) + this.Children[1].Evaluate(parameters);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (null == obj) return false;

            var other = obj as AdditionNode;
            if (null == other) return false;

            return this.children.SequenceEqual(other.children);
        }

        public bool Equals(AdditionNode obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (null == obj) return false;

            return this.children.SequenceEqual(obj.children);
        }

        public override int GetHashCode()
        {
            return this.children.GetHashCode();
        }

        [ContractInvariantMethod]
        // ReSharper disable once UnusedMember.Local
        private void ObjectInvariant()
        {
            Contract.Invariant(this.children != null);
        }
    }
}
