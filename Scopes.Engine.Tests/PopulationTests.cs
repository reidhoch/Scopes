namespace Scopes.Engine.Tests
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    using Scopes.Engine.Nodes;

    [TestFixture]
    // ReSharper disable ObjectCreationAsStatement
    public class PopulationTests
    {
        [Test]
        public void ElitismRate([Random(0.0, 1.0, 5)] double rate)
        {
            var pop = new Population { ElitismRate = rate };
            Assert.That(pop.ElitismRate, Is.EqualTo(rate));
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ElitismRateTooLow([Random(-5.0, -1.0, 5)] double rate)
        {
            new Population { ElitismRate = rate };
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ElitismRateTooHigh([Random(1.01, 5.0, 5)] double rate)
        {
            new Population { ElitismRate = rate };
        }

        [Test]
        public void Limit([Random(0, Int32.MaxValue, 5)]int limit)
        {
            var pop = new Population { Limit = limit };
            Assert.That(pop.Limit, Is.EqualTo(limit));
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LimitTooLow([Random(Int32.MinValue, 0, 5)] int limit)
        {
            new Population { Limit = limit };
        }

        [Test]
        public void Add()
        {
            var functionSet = new HashSet<Func<IFunctionNode>>
                                  {
                                      () => new SquareRootNode(),
                                      () => new AdditionNode(),
                                      () => new SubtractionNode(),
                                      () => new MultiplicationNode(),
                                      () => new DivisionNode()
                                  };
            var pop = new Population { Limit = Int16.MaxValue };
            pop.Add(new Chromosome(10, 1, 2, functionSet));

            Assert.That(pop.Size, Is.EqualTo(1));
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void AddTooMany()
        {
            var functionSet = new HashSet<Func<IFunctionNode>>
                                  {
                                      () => new SquareRootNode(),
                                      () => new AdditionNode(),
                                      () => new SubtractionNode(),
                                      () => new MultiplicationNode(),
                                      () => new DivisionNode()
                                  };
            var pop = new Population { Limit = 1 };
            pop.Add(new Chromosome(10, 1, 2, functionSet));
            pop.Add(new Chromosome(10, 1, 2, functionSet));
        }

        [Test]
        public void NextGeneration()
        {
            var pop = new Population { ElitismRate = .5, Limit = 20 };
            for (var i = 0; i < 20; i++) {
                pop.Add(new DummyChromosome(i));
            }

            var actual = pop.NextGeneration();
            Assert.That(actual, Is.Not.Null);
            Assert.That((actual as Population).ElitismRate, Is.EqualTo(0.5));
            Assert.That(actual.Limit, Is.EqualTo(20));
            Assert.That(actual.Chromosomes[0].Fitness, Is.EqualTo(0));
            Assert.That(actual.Chromosomes[1].Fitness, Is.EqualTo(1));
            Assert.That(actual.Chromosomes[2].Fitness, Is.EqualTo(2));
            Assert.That(actual.Chromosomes[3].Fitness, Is.EqualTo(3));
            Assert.That(actual.Chromosomes[4].Fitness, Is.EqualTo(4));
        }
    }
}
