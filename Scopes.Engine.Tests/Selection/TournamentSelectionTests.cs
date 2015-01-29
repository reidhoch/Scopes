namespace Scopes.Engine.Tests.Selection
{
    using NUnit.Framework;

    using Scopes.Engine.Selection;

    [TestFixture]
    public class TournamentSelectionTests
    {
        [Test]
        public void DefaultTournamentSize()
        {
            var actual = new TournamentSelection();
            Assert.That(actual.TournamentSize, Is.EqualTo(2));
        }

        [Test]
        public void GetTournamentSize()
        {
            const int Expected = 10;
            var actual = new TournamentSelection(Expected);
            Assert.That(actual.TournamentSize, Is.EqualTo(Expected));
        }

        [Test]
        public void Select()
        {
            var selection = new TournamentSelection();
            var population = new Population { Limit = 10 };
            for (var i = 0; i < 10; i++) {
                population.Add(new DummyChromosome(i));
            }

            for (var i = 0; i < 10; i++)
            {
                var selected = selection.Select(population);
                var first = selected[0];
                var second = selected[1];
                // The least fit chromosome should never be selected.
                Assert.That(first.Fitness < 9.0d, "Fitness = {0}", first.Fitness);
                Assert.That(second.Fitness < 9.0d, "Fitness = {0}", second.Fitness);
            }
        }
    }
}
