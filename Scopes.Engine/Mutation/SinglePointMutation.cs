namespace Scopes.Engine.Mutation
{
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
            var index = random.Next(0, nodes.Count);
            if (index > (original.HeadLength - 1)) {
                // Terminal node only.
                newNodes[index] = terminalFactory.Generate(parameterCount, 1, 10);
            } else {
                // Function or Terminal node.  
                var isFunction = this.random.NextDouble() > 0.5d;
                if (isFunction) {
                    var setLength = functionSet.Count;
                    newNodes[index] = functionSet.ElementAt(random.Next(0, setLength - 1))();
                } else {
                    newNodes[index] = terminalFactory.Generate(parameterCount, 1, 10);
                }
            }

            return new Chromosome(original.HeadLength, original.NumGenes, original.ParameterCount, original.FunctionSet, newNodes) { LinkingFunction = linkingFunction };
        }

        [ContractInvariantMethod]
        // ReSharper disable once UnusedMember.Local
        private void ObjectInvariant()
        {
            Contract.Invariant(this.random != null);
        }
    }
}
