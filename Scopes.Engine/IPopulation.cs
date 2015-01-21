namespace Scopes.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    [ContractClass(typeof(PopulationContract))]
    public interface IPopulation : IEnumerable<Chromosome>
    {
        int Limit { get; set; }
        int Size { get; }

        IPopulation NextGeneration();

        void Add(Chromosome chromosome);
    }

    [ContractClassFor(typeof(IPopulation))]
    internal abstract class PopulationContract : IPopulation
    {

        public int Limit
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value >= 0, "Value must be non-negative.");
            }
        }

        public int Size
        {
            get { throw new NotImplementedException(); }
        }

        public IPopulation NextGeneration()
        {
            throw new NotImplementedException();
        }

        public void Add(Chromosome chromosome)
        {
            Contract.Requires(chromosome != null);
        }

        public IEnumerator<Chromosome> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
