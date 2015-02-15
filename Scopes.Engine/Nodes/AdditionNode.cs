namespace Scopes.Engine.Nodes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>
    /// A <see cref="IFunctionNode"/> that performs Addition.
    /// </summary>
    [DebuggerDisplay("+")]
    public class AdditionNode : IFunctionNode, IEquatable<AdditionNode>
    {
        private readonly IList<IGepNode> children;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionNode" /> class.
        /// </summary>
        public AdditionNode()
        {
            this.children = new List<IGepNode>(this.Arity);
        }

        /// <summary>
        /// Gets the list of children.
        /// </summary>
        public IList<IGepNode> Children
        {
            get
            {
                Contract.Ensures(Contract.Result<IList<IGepNode>>() != null);
                return this.children;
            }
        }

        /// <summary>
        /// Gets the arity of the <see cref="IGepNode"/>.
        /// </summary>
        public int Arity
        {
            [Pure]
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// Clones this <see cref="AdditionNode"/>.
        /// </summary>
        /// <returns>A clone of the <see cref="AdditionNode"/></returns>
        public IGepNode Clone()
        {
            return new AdditionNode();
        }

        /// <summary>
        /// Evaluates the parameters.
        /// </summary>
        /// <param name="parameters">Parameters to be evaluated.</param>
        /// <returns></returns>
        public double Evaluate(double[] parameters)
        {
            return this.Children[0].Evaluate(parameters) + this.Children[1].Evaluate(parameters);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (null == obj)
            {
                return false;
            }

            var other = obj as AdditionNode;
            if (null == other)
            {
                return false;
            }

            return this.children.SequenceEqual(other.children);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(AdditionNode other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (null == other)
            {
                return false;
            }

            return this.children.SequenceEqual(other.children);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
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
