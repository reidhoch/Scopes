namespace Scopes.Engine.Mutation
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using MathNet.Numerics.Random;

    using Scopes.Engine.Nodes;

    public class RootTransposition : IMutation
    {
        private readonly MersenneTwister random = MersenneTwister.Default;

        public Chromosome Mutate(Chromosome original)
        {
            var headLength = original.HeadLength;
            var nodes = original.Nodes;
            var numGenes = original.NumGenes;
            var parameterCount = original.ParameterCount;
            var functionSet = original.FunctionSet;
            var linkingFunction = original.LinkingFunction;
            var sourcePoint = this.random.Next(headLength);
            while (!(nodes[sourcePoint] is IFunctionNode) && sourcePoint < headLength) {
                sourcePoint++;
            }
            if (sourcePoint == headLength) {
                return original;
            }
            var newNodes = new List<IGepNode>();
            newNodes.AddRange(nodes.Select(node => node.Clone()));
            var maxSourceLen = headLength - sourcePoint;
            var transposonLen = this.random.Next(1, maxSourceLen);
            var transposon = new IGepNode[transposonLen];
            for (int i = sourcePoint, j = 0; j < transposonLen; i++, j++) {
                transposon[j] = newNodes[i].Clone();
            }
            for (var i = headLength - 1; i >= transposonLen; i--) {
                newNodes[i] = newNodes[i - transposonLen];
            }
            for (var i = 0; i < transposonLen; i++) {
                newNodes[i] = transposon[i];
            }
            return new Chromosome(headLength, numGenes, parameterCount, functionSet, newNodes) { LinkingFunction = linkingFunction };
        }

        [ContractInvariantMethod]
        // ReSharper disable once UnusedMember.Local
        private void ObjectInvariant()
        {
            Contract.Invariant(this.random != null);
        }
    }
}
