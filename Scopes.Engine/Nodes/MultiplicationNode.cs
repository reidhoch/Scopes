﻿namespace Scopes.Engine.Nodes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Linq;

    [DebuggerDisplay("*")]
    public class MultiplicationNode : IFunctionNode, IEquatable<MultiplicationNode>
    {
        private readonly IList<IGepNode> children;

        public MultiplicationNode()
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
            return new MultiplicationNode();
        }

        public double Evaluate(double[] parameters)
        {
            return this.Children[0].Evaluate(parameters) * this.Children[1].Evaluate(parameters);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (null == obj) return false;

            var other = obj as MultiplicationNode;
            if (null == other) return false;

            return this.children.SequenceEqual(other.children);
        }

        public bool Equals(MultiplicationNode obj)
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
