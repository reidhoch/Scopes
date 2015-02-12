namespace Scopes.Engine.Crossover
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using MathNet.Numerics.Random;

    using Scopes.Engine.Nodes;

    public class NPointCrossover : ICrossover
    {
        private readonly MersenneTwister random = MersenneTwister.Default;
        private readonly int crossoverPoints;

        public NPointCrossover(int crossoverPoints)
        {
            Contract.Requires<ArgumentOutOfRangeException>(crossoverPoints > 0);

            this.crossoverPoints = crossoverPoints;
        }

        public int CrossoverPoints { get { return this.crossoverPoints; } }

        public List<Chromosome> Crossover(Chromosome father, Chromosome mother)
        {
            var headLength = father.HeadLength;
            var numGenes = father.NumGenes;
            var parameterCount = father.ParameterCount;
            var functionSet = father.FunctionSet;
            var linkingFunction = father.LinkingFunction;
            var length = father.Length;
            var fatherNodes = father.Nodes;
            var motherNodes = mother.Nodes;

            var sonNodes = new List<IGepNode>(length);
            var daughterNodes = new List<IGepNode>(length);

            var pointsLeft = this.crossoverPoints;
            var lastIdx = 0;
            for (var idx = 0; idx < this.crossoverPoints; idx++, pointsLeft--) {
                var cxIdx = 1 + lastIdx + random.Next(length - lastIdx - pointsLeft);
                for (var j = lastIdx; j < cxIdx; j++) {
                    sonNodes.Add(fatherNodes[j]);
                    daughterNodes.Add(motherNodes[j]);
                }
                var tmp = sonNodes;
                sonNodes = daughterNodes;
                daughterNodes = tmp;

                lastIdx = cxIdx;
            }
            for (var j = lastIdx; j < length; j++) {
                sonNodes.Add(fatherNodes[j]);
                daughterNodes.Add(motherNodes[j]);
            }

            var son = new Chromosome(headLength, numGenes, parameterCount, functionSet, sonNodes) { LinkingFunction = linkingFunction };
            var daughter = new Chromosome(headLength, numGenes, parameterCount, functionSet, daughterNodes) { LinkingFunction = linkingFunction };
            return new List<Chromosome> { son, daughter };
        }

        [ContractInvariantMethod]
        // ReSharper disable once UnusedMember.Local
        private void ObjectInvariant()
        {
            Contract.Invariant(this.crossoverPoints > 0);
            Contract.Invariant(this.random != null);
        }
    }
}
