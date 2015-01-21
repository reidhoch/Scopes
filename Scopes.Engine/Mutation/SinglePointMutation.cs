namespace Scopes.Engine.Mutation
{
    using System.Collections.Generic;
    using System.Linq;

    using MathNet.Numerics.Random;

    using Scopes.Engine.Nodes;

    public class SinglePointMutation : IMutation
    {
        private readonly MersenneTwister random = MersenneTwister.Default;

        public Chromosome Mutate(Chromosome original)
        {
            var nodes = original.Nodes;
            var functionSet = original.FunctionSet;
            var terminalSet = original.TerminalSet;
            var newNodes = new List<IGepNode>(nodes);
            var index = random.Next(0, nodes.Count);
            if (index > original.HeadLength) {
                // Terminal node only.
                var setLength = terminalSet.Count;
                newNodes[index] = terminalSet.ElementAt(random.Next(0, setLength - 1))();
            } else {
                // Function or Terminal node.  
                var isFunction = this.random.NextDouble() > 0.5d;
                if (isFunction)
                {
                    var setLength = functionSet.Count;
                    newNodes[index] = functionSet.ElementAt(random.Next(0, setLength - 1))();
                } else {
                    var setLength = terminalSet.Count;
                    newNodes[index] = terminalSet.ElementAt(random.Next(0, setLength - 1))();
                }
            }

            return new Chromosome(original.HeadLength, original.NumGenes, original.FunctionSet, original.TerminalSet, newNodes);
        }
    }
}
