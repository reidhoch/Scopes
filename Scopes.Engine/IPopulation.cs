namespace Scopes.Engine
{
    using System.Collections.Generic;

    public interface IPopulation : IEnumerable<Chromosome>
    {
        int Limit { get; set; }
        int Size { get; }

        IPopulation NextGeneration();

        void Add(Chromosome chromosome);
    }
}
