namespace Scopes.Engine.Termination
{
    using System;

    public class ElapsedTimeTerminationCondition : ITerminationCondition
    {
        private readonly DateTime endTime;

        public ElapsedTimeTerminationCondition(TimeSpan duration)
        {
            this.endTime = DateTime.Now.Add(duration);
        }

        public bool IsSatisfied()
        {
            return DateTime.Now >= endTime;
        }
    }
}
