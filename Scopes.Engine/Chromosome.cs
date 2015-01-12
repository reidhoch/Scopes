namespace Scopes.Engine
{
    using Scopes.Engine.Nodes;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Chromosome
    {
        // Not entirely sure what I want to do right here.
        private static readonly Dictionary<char, Func<IFunctionNode>> FunctionSet = new Dictionary<char, Func<IFunctionNode>>
        { 
            { 'Q', () => new SquareRootNode() },
            { '+', () => new AdditionNode() },
            { '-', () => new SubtractionNode() },
            { '*', () => new MultiplicationNode() },
            { '/', () => new DivisionNode() }
        };

        private readonly int headLength;
        private readonly int tailLength;
        private readonly int length;
        private readonly int numGenes;
        private readonly IGepNode[] genes;
        private readonly String karva;

        public Chromosome(int headLength, int numGenes)
        {
            if (headLength < 0) { throw new ArgumentOutOfRangeException("headLength", headLength, "Must be non-negative."); }
            if (numGenes < 1) { throw new ArgumentOutOfRangeException("numGenes", numGenes, "Must be greater than or equal to 1."); }

            this.numGenes = numGenes;
            this.genes = new IGepNode[this.numGenes];
            this.headLength = headLength;
            // Learn maxArity from available nodes.
            const int MaxArity = 2;
            this.tailLength = (this.headLength * (MaxArity - 1)) + 1;
            this.length = this.headLength + this.tailLength;
            this.karva = Generate();
        }

        private string Generate()
        {
            StringBuilder buffer = new StringBuilder(this.length);
            buffer.Append(GenerateRoot()); // Give the root a large chance of being a non-terminal node.
            for (int i = 1; i < this.headLength; i++)
            {
                // Functions and terminals.
            }
            for (int i = this.headLength; i < this.length; i++)
            {
                // Terminals only.
            }
            return buffer.ToString();
        }

        private char GenerateRoot()
        {
            throw new NotImplementedException();
        }

        public string Karva { get { return this.karva; } }
    }
}
