namespace Scopes.Engine.Mutation
{
    using System.Collections.Generic;
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
            var functionSet = original.FunctionSet;
//            var terminalSet = original.TerminalSet;
            var newNodes = new List<IGepNode>(nodes);
            var index = random.Next(0, nodes.Count);
            if (index > original.HeadLength) {
                // Terminal node only.
                newNodes[index] = terminalFactory.Generate(5, 1, 10);
            } else {
                // Function or Terminal node.  
                var isFunction = this.random.NextDouble() > 0.5d;
                if (isFunction)
                {
                    var setLength = functionSet.Count;
                    newNodes[index] = functionSet.ElementAt(random.Next(0, setLength - 1))();
                } else {
                    newNodes[index] = terminalFactory.Generate(5, 1, 10);
                }
            }

            return new Chromosome(original.HeadLength, original.NumGenes, original.FunctionSet, newNodes);
        }
    }
}
