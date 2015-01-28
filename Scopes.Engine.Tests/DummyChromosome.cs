namespace Scopes.Engine.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

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
        private static int fitness;

        public DummyChromosome()
            : base(10, 1, DummyFunctionSet)
        {
            this.Fitness = fitness;
            Interlocked.Increment(ref fitness);
        }
    }
}
