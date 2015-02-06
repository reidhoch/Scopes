namespace Scopes.Engine.Nodes
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Linq;

    [DebuggerDisplay("-")]
    public class SubtractionNode : IFunctionNode
    {
        private readonly IList<IGepNode> children;

        public SubtractionNode()
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
            return new SubtractionNode();
        }

        public double Evaluate(double[] parameters)
        {
            return this.Children[0].Evaluate(parameters) - this.Children[1].Evaluate(parameters);
        }

        public override bool Equals(object obj)
        {
            if (null == obj) return false;

            var other = obj as SubtractionNode;
            if (null == other) return false;

            return this.children.SequenceEqual(other.children);
        }

        public bool Equals(SubtractionNode obj)
        {
            if (null == obj) return false;

            return this.children.SequenceEqual(obj.children);
        }

        public override int GetHashCode()
        {
            return this.children.GetHashCode();
        }
    }
}
