namespace Scopes.Engine.Mutation
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using MathNet.Numerics.Random;

    using Scopes.Engine.Factories;
    using Scopes.Engine.Nodes;

    public class SinglePointMutation : IMutation
    {
        private readonly MersenneTwister random = MersenneTwister.Default;
        private readonly TerminalFactory terminalFactory = TerminalFactory.Instance;

        public Chromosome Mutate(Chromosome original)
        {
            var nodes = original.Nodes;
            var parameterCount = original.ParameterCount;
            var functionSet = original.FunctionSet;
            var linkingFunction = original.LinkingFunction;
            var newNodes = new List<IGepNode>();
            newNodes.AddRange(nodes.Select(node => node.Clone()));
            var mutationPoint = random.Next(0, nodes.Count);
            if (!IsHead(mutationPoint, original.NumGenes, original.HeadLength, original.TailLength)) {
                // Terminal node only.
                newNodes[mutationPoint] = terminalFactory.Generate(parameterCount, 1, 10);
            } else {
                // Function or Terminal node.  
                var isFunction = this.random.NextDouble() > 0.5d;
                if (isFunction) {
                    var setLength = functionSet.Count;
                    newNodes[mutationPoint] = functionSet.ElementAt(random.Next(0, setLength - 1))();
                } else {
                    newNodes[mutationPoint] = terminalFactory.Generate(parameterCount, 1, 10);
                }
            }

            return new Chromosome(original.HeadLength, original.NumGenes, original.ParameterCount, original.FunctionSet, newNodes) { LinkingFunction = linkingFunction };
        }

        private static bool IsHead(int mutationPoint, int numGenes, int headLength, int tailLength)
        {
            Contract.Requires<ArgumentOutOfRangeException>(numGenes > 0);
            var start = new int[numGenes];
            var end = new int[numGenes];
            start[0] = 0;
            end[0] = headLength;
            for (var i = 1; i < numGenes; i++) {
                start[i] = i * (headLength + tailLength);
                end[i] = start[i] + headLength;
            }
            for (var j = 0; j < numGenes; j++)
            {
                if (mutationPoint >= start[j] && mutationPoint < end[j]) return true;
            }

            return false;
        }

        [ContractInvariantMethod]
        // ReSharper disable once UnusedMember.Local
        private void ObjectInvariant()
        {
            Contract.Invariant(this.random != null);
            Contract.Invariant(this.terminalFactory != null);
        }
    }
}
