namespace Scopes.Engine.Factories
{
    using System;

    using MathNet.Numerics.Random;

    using Scopes.Engine.Nodes;

    public class TerminalFactory
    {
        private static readonly Lazy<TerminalFactory> Lazy = new Lazy<TerminalFactory>(() => new TerminalFactory());
        private readonly MersenneTwister random = MersenneTwister.Default;

        public static TerminalFactory Instance { get { return Lazy.Value; } }

        public ITerminalNode Generate(int parameterCount, int min, int max)
        {
            if (random.NextDouble() > 0.5d) {
                return new VariableNode(random.Next(parameterCount));
            }
            return new ConstantNode { Value = random.Next(min, max) };
        }
    }
}
