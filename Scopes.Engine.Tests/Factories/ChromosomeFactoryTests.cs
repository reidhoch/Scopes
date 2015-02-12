namespace Scopes.Engine.Tests.Factories
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    using Scopes.Engine.Factories;
    using Scopes.Engine.Nodes;

    [TestFixture]
    public class ChromosomeFactoryTests
    {
        [Test]
        public void Generate()
        {
            var factory = ChromosomeFactory.Instance;
            var functionSet = new HashSet<Func<IFunctionNode>>
        { 
            () => new SquareRootNode(),
            () => new AdditionNode(),
            () => new SubtractionNode(),
            () => new MultiplicationNode(),
            () => new DivisionNode() 
        };
            var population = factory.Generate(functionSet, 500, 10, 1, 2, new AdditionNode());
            Assert.That(population.Size, Is.EqualTo(500));
        }

        [Test]
        public void Instance()
        {
            Assert.That(ChromosomeFactory.Instance, Is.Not.Null);
        }
    }
}
