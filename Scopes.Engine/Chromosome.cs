namespace Scopes.Engine
{
    using Scopes.Engine.Nodes;
    using System;
using System.Collections.Generic;

    public class Chromosome
    {
        //private static readonly Dictionary<char, Type> 

        private readonly int headLength;
        private readonly int tailLength;
        private readonly int length;
        private readonly int numGenes;
        private readonly IGepNode[] genes;

        public Chromosome(int headLength, int numGenes)
        {
            if (headLength < 0)
            {
                throw new ArgumentOutOfRangeException("headLength", headLength, "Must be non-negative.");
            }
            if (numGenes < 1)
            {
                throw new ArgumentOutOfRangeException("numGenes", numGenes, "Must be greater than or equal to 1.");
            }
            this.numGenes = numGenes;
            this.genes = new IGepNode[this.numGenes];
            this.headLength = headLength;
            // Learn maxArity from available nodes.
            var maxArity = 2;
            this.tailLength = this.headLength * (maxArity - 1) + 1;
            this.length = this.headLength + this.tailLength;
        }
    }
}
