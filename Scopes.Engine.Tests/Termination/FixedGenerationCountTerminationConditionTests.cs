namespace Scopes.Engine.Tests.Termination
{
    using NUnit.Framework;
    using Scopes.Engine.Termination;

    [TestFixture]
    public class FixedGenerationCountTerminationConditionTests
    {
        [Test]
        public void IsSatisfied([Random(1, 500, 10)]int generations)
        {
            var fgc = new FixedGenerationCountTerminationCondition(generations);
            var count = 0;
            while (!fgc.IsSatisfied()) {
                count++;
            }
            Assert.That(count, Is.EqualTo(generations));
        }
    }
}
