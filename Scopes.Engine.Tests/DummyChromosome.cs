namespace Scopes.Engine.Tests
{
    using System;
    using System.Collections.Generic;

    using Scopes.Engine.Nodes;

    public class DummyChromosome : Chromosome
    {
        private static readonly ISet<Func<IFunctionNode>> DummyFunctionSet = new HashSet<Func<IFunctionNode>>
                                  {
                                      () => new SquareRootNode(),
                                      () => new AdditionNode(),
                                      () => new SubtractionNode(),
                                      () => new MultiplicationNode(),
                                      () => new DivisionNode()
                                  };

        public DummyChromosome(int fitness)
            : base(10, 1, DummyFunctionSet)
        {
            this.Fitness = fitness;
        }
    }
}
