namespace Scopes.Engine.Termination
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Threading;

    public class FixedGenerationCountTerminationCondition : ITerminationCondition
    {
        private readonly int numGenerations;
        private int generation = 0;

        public FixedGenerationCountTerminationCondition(int numGenerations)
        {
            Contract.Requires<ArgumentOutOfRangeException>(numGenerations > 0);
            this.numGenerations = numGenerations;
        }

        public bool IsSatisfied()
        {
            if (this.generation < this.numGenerations) {
                Interlocked.Increment(ref generation);
                return false;
            }
            return true;
        }
    }
}
