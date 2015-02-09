namespace Scopes.Engine.Mutation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MathNet.Numerics.Random;

    using Scopes.Engine.Nodes;

    /// <summary>
    /// 
    /// </summary>
    public class Transposition : IMutation
    {
        private readonly MersenneTwister random = MersenneTwister.Default;

        public Chromosome Mutate(Chromosome original)
        {
            var headLength = original.HeadLength;
            var numGenes = original.NumGenes;
            var parameterCount = original.ParameterCount;
            var functionSet = original.FunctionSet;
            var sourcePoint = this.random.Next(original.Length);
            var sourceLen = original.Length - sourcePoint;
            var targetPoint = this.random.Next(1, headLength - 1);
            var targetLen = headLength - targetPoint;
            var transposonLen = this.random.Next(1, Math.Min(targetLen, sourceLen));
            var nodes = original.Nodes;
            var newNodes = new List<IGepNode>();
            newNodes.AddRange(nodes.Select(node => node.Clone()));
            var transposon = new IGepNode[transposonLen];
            for (int i = sourcePoint, j = 0; j < transposonLen; i++, j++) {
                transposon[j] = nodes[i].Clone();
            }
            for (int i = targetPoint, j = 0; j < transposonLen; i++, j++) {
                newNodes[i] = transposon[j];
            }
            return new Chromosome(headLength, numGenes, parameterCount, functionSet, newNodes);
        }
    }
}
