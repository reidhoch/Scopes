﻿namespace Scopes.Engine.Tests
{
    using System;

    using NUnit.Framework;

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
            var pop = new Population { Limit = Int16.MaxValue };
            pop.Add(new Chromosome(10, 1));

            Assert.That(pop.Size, Is.EqualTo(1));
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void AddTooMany()
        {
            var pop = new Population { Limit = 1 };
            pop.Add(new Chromosome(10, 1));
            pop.Add(new Chromosome(10, 1));
        }
    }
}