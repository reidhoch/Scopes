namespace Scopes.Engine.Fitness
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    public class FitnessEvaluator : IFitnessEvaluator
    {
        // Mapping of parameters to expected value.
        private readonly Dictionary<double[], double> dataSet;

        public FitnessEvaluator(Dictionary<double[], double> dataSet)
        {
            Contract.Requires<ArgumentNullException>(dataSet != null);
            this.dataSet = dataSet;
        }

        public double Calculate(Chromosome chromosome)
        {
            var root = chromosome.Tree;
            var error = 0.0d;
            foreach (var kvp in dataSet)
            {
                var parameters = kvp.Key;
                var expected = kvp.Value;
                var actual = root.Evaluate(parameters);
                if (Double.IsNaN(actual)) {
                    return Double.MaxValue;
                    //                    chromosome.Fitness = Double.MaxValue;
                    //                    continue;
                }
                error += Math.Abs(actual - expected);
            }
            return error;
        }

        [ContractInvariantMethod]
        // ReSharper disable once UnusedMember.Local
        private void ObjectInvariant()
        {
            Contract.Invariant(this.dataSet != null);
        }
    }
}
