namespace Scopes.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MathNet.Numerics.Random;

    using Scopes.Engine.Nodes;
    using System.Diagnostics.Contracts;

    public class Chromosome
    {
        private readonly MersenneTwister random = MersenneTwister.Default;
        private readonly IList<IGepNode> nodes;
        private readonly ISet<Func<IFunctionNode>> functionSet;
        private readonly ISet<Func<ITerminalNode>> terminalSet;
        private readonly int headLength;
////        private readonly int tailLength;
        private readonly int length;
        private readonly int numGenes;
////        private readonly IGepNode[] genes;

        public Chromosome(int headLength, int numGenes, ISet<Func<IFunctionNode>> functionSet, ISet<Func<ITerminalNode>> terminalSet, IEnumerable<IGepNode> nodes) :
            this(headLength, numGenes, functionSet, terminalSet)
        {
            Contract.Requires<ArgumentOutOfRangeException>(headLength > 0);
            Contract.Requires<ArgumentOutOfRangeException>(numGenes >= 1);
            Contract.Requires<ArgumentNullException>(functionSet != null);
            Contract.Requires<ArgumentNullException>(terminalSet != null);
            Contract.Requires<ArgumentNullException>(nodes != null);
            this.nodes = new List<IGepNode>(nodes);
        }

        public Chromosome(int headLength, int numGenes, ISet<Func<IFunctionNode>> functionSet, ISet<Func<ITerminalNode>> terminalSet)
        {
            Contract.Requires<ArgumentOutOfRangeException>(headLength > 0);
            Contract.Requires<ArgumentOutOfRangeException>(numGenes >= 1);
            Contract.Requires<ArgumentNullException>(functionSet != null);
            Contract.Requires<ArgumentNullException>(terminalSet != null);

            this.numGenes = numGenes;
////            this.genes = new IGepNode[this.numGenes];
            this.headLength = headLength;
            // Learn maxArity from available nodes.
            var maxArity = functionSet.Select(func => func().Arity).Concat(new[] { Int32.MinValue }).Max();
            var tailLength = (this.headLength * (maxArity - 1)) + 1;
            this.length = this.headLength + tailLength;
            this.nodes = new List<IGepNode>(this.length);
            this.functionSet = functionSet;
            this.terminalSet = terminalSet;
            this.Generate();
        }

        public double Fitness { get; protected set; }
        public ISet<Func<IFunctionNode>> FunctionSet { get { return this.functionSet; } }
        public int HeadLength { get { return this.headLength; } }
        public int Length { get { return this.length; } }
        public IList<IGepNode> Nodes { get { return this.nodes; } }
        public int NumGenes { get { return this.numGenes; } }
        public ISet<Func<ITerminalNode>> TerminalSet { get { return this.terminalSet; } }

        public void Generate()
        {
            var setLength = FunctionSet.Count;
            this.nodes.Add(GenerateRoot()); // Give the root a large chance of being a non-terminal node.
            for (var i = 1; i < this.headLength; i++) {
                // Functions and terminals.
                var isFunction = random.NextDouble() < 0.5d;
                if (isFunction) {
                    this.nodes.Add(FunctionSet.ElementAt(random.Next(0, setLength - 1))());
                } else {
                    this.nodes.Add(new ConstantNode());
                }
            }
            for (var i = this.headLength; i < this.length; i++) {
                // Terminals only.
                this.nodes.Add(new ConstantNode());
            }
        }

        public IGepNode Tree
        {
            get
            {
                var functions = new Queue<IFunctionNode>();
                var root = nodes[0];
                if (0 == root.Arity) {
                    return root;
                }

                functions.Enqueue(root as IFunctionNode);
                for (var idx = 1; idx < this.length; idx++) {
                    var node = nodes[idx];
                    if (0 != node.Arity) {
                        functions.Enqueue(node as IFunctionNode);
                    }
                    var parent = functions.Peek();
                    parent.Children.Add(node);
                    if (parent.Children.Count != parent.Arity) {
                        continue;
                    }
                    functions.Dequeue();
                    if (functions.Count == 0) {
                        break;
                    }
                }

                return root;
            }
        }

        private IGepNode GenerateRoot()
        {
            var setLength = FunctionSet.Count;
            var isFunction = random.NextDouble() < 0.9d;
            if (isFunction) {
                return FunctionSet.ElementAt(random.Next(0, setLength - 1))();
            }
            // Return a terminal node.
            return new ConstantNode();
        }
    }
}
