namespace Scopes.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using MathNet.Numerics.Random;

    using Scopes.Engine.Factories;
    using Scopes.Engine.Nodes;

    public class Chromosome
    {
        private readonly MersenneTwister random = MersenneTwister.Default;
        private readonly TerminalFactory terminalFactory = TerminalFactory.Instance;
        private readonly IList<IGepNode> nodes;
        private readonly ISet<Func<IFunctionNode>> functionSet;
        private readonly int headLength;
        private readonly int length;
        private readonly int numGenes;
        private readonly int parameterCount;
        private double fitness = Double.MaxValue;
////        private readonly IGepNode[] genes;

        public Chromosome(int headLength, int numGenes, int parameterCount, ISet<Func<IFunctionNode>> functionSet, IEnumerable<IGepNode> nodes) :
            this(headLength, numGenes, parameterCount, functionSet)
        {
            Contract.Requires<ArgumentOutOfRangeException>(headLength > 0);
            Contract.Requires<ArgumentOutOfRangeException>(numGenes >= 1);
            Contract.Requires<ArgumentOutOfRangeException>(parameterCount >= 0);
            Contract.Requires<ArgumentNullException>(functionSet != null);
            Contract.Requires<ArgumentNullException>(nodes != null);
            var nodeArray = nodes.ToArray();
            var count = nodeArray.Length;
            this.nodes = new List<IGepNode>(count);
            for (var i = 0; i < count; i++) {
                this.nodes.Add(nodeArray[i].Clone());
            }
        }

        public Chromosome(int headLength, int numGenes, int parameterCount, ISet<Func<IFunctionNode>> functionSet)
        {
            Contract.Requires<ArgumentOutOfRangeException>(headLength > 0);
            Contract.Requires<ArgumentOutOfRangeException>(numGenes >= 1);
            Contract.Requires<ArgumentOutOfRangeException>(parameterCount >= 0);
            Contract.Requires<ArgumentNullException>(functionSet != null);

            this.numGenes = numGenes;
////            this.genes = new IGepNode[this.numGenes];
            this.headLength = headLength;
            this.parameterCount = parameterCount;
            // Learn maxArity from available nodes.
            var maxArity = functionSet.Select(func => func().Arity).Concat(new[] { Int32.MinValue }).Max();
            var tailLength = (this.headLength * (maxArity - 1)) + 1;
            this.length = this.headLength + tailLength;
            this.nodes = new List<IGepNode>(this.length);
            this.functionSet = functionSet;
//            this.Generate();
        }

        public double Fitness
        {
            get
            {
                return this.fitness;
            }
            set
            {
                this.fitness = value;
            }
        }

        public ISet<Func<IFunctionNode>> FunctionSet
        {
            get
            {
                Contract.Ensures(Contract.Result<ISet<Func<IFunctionNode>>>() != null);
                return this.functionSet;
            }
        }
        public int ParameterCount { get { return this.parameterCount; } }
        public int HeadLength { get { return this.headLength; } }
        public int Length { get { return this.length; } }
        public IList<IGepNode> Nodes
        {
            get
            {
                Contract.Ensures(Contract.Result<IList<IGepNode>>() != null);
                return this.nodes;
            }
        }
        public int NumGenes { get { return this.numGenes; } }

        public void Generate()
        {
            Contract.Requires(this.FunctionSet.Count > 0);
            var setLength = FunctionSet.Count;
            this.nodes.Add(GenerateRoot()); // Give the root a large chance of being a non-terminal node.
            for (var i = 1; i < this.headLength; i++) {
                // Functions and terminals.
                var isFunction = random.NextDouble() < 0.5d;
                if (isFunction) {
                    this.nodes.Add(FunctionSet.ElementAt(random.Next(0, setLength - 1))());
                } else {
                    this.nodes.Add(terminalFactory.Generate(this.ParameterCount, 1, 10));
                }
            }
            for (var i = this.headLength; i < this.length; i++) {
                // Terminals only.
                this.nodes.Add(terminalFactory.Generate(this.ParameterCount, 1, 10));
            }
        }

        public IGepNode Tree
        {
            get
            {
                Contract.Ensures(Contract.Result<IGepNode>() != null);
                var functions = new Queue<IFunctionNode>();
                var root = nodes[0].Clone();
                if (0 == root.Arity) {
                    return root;
                }

                functions.Enqueue(root as IFunctionNode);
                for (var idx = 1; idx < this.length; idx++) {
                    var node = nodes[idx].Clone();
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
            return terminalFactory.Generate(this.ParameterCount, 1, 10);
        }

        [ContractInvariantMethod]
        // ReSharper disable once UnusedMember.Local
        private void ObjectInvariant()
        {
            Contract.Invariant(this.functionSet != null);
            Contract.Invariant(this.nodes != null);
            Contract.Invariant(this.random != null);
        }
    }
}
