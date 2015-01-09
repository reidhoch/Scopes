﻿namespace Scopes.Engine.Nodes
{
    using System;

    public class DivisionNode : IGepNode
    {
        public DivisionNode(IGepNode left, IGepNode right)
        {
            if (null == left) { throw new ArgumentNullException("left"); }
            if (null == right) { throw new ArgumentNullException("right"); }
            this.Left = left;
            this.Right = right;
        }

        public int Arity
        {
            get
            {
                return 2;
            }
        }

        public IGepNode Right { get; private set; }
        public IGepNode Left { get; private set; }

        public double Evaluate()
        {
            var num = Left.Evaluate();
            var den = Right.Evaluate();
            if (den == 0.0)
            {
                return Double.NaN;
            }
            return num / den;
        }
    }
}
