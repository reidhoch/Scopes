namespace Scopes.Engine.Tests.Termination
{
    using System;
    using System.Threading;

    using NUnit.Framework;

    using Scopes.Engine.Termination;

    [TestFixture]
    public class ElapsedTimeTerminationConditionTests
    {
        [Test]
        public void IsSatisfied()
        {
            var start = DateTime.Now;
            var duration = TimeSpan.FromSeconds(5);
            var term = new ElapsedTimeTerminationCondition(duration);
            while (!term.IsSatisfied()) {
                Thread.Sleep(25);
            }
            var end = DateTime.Now;
            var actual = end - start;

            Assert.That(actual, Is.EqualTo(duration).Within(TimeSpan.FromMilliseconds(50)));
        }
    }
}
