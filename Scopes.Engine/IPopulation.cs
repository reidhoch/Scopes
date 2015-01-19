namespace Scopes.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    [ContractClass(typeof(IPopulationContract))]
    public interface IPopulation : IEnumerable<Chromosome>
    {
        int Limit { get; set; }
        int Size { get; }

        IPopulation NextGeneration();

        void Add(Chromosome chromosome);
    }

    [ContractClassFor(typeof(IPopulation))]
    internal abstract class IPopulationContract : IPopulation
    {

        public int Limit
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value >= 0, "Value must be non-negative.");
            }
        }

        public int Size
        {
            get { throw new System.NotImplementedException(); }
        }

        public IPopulation NextGeneration()
        {
            throw new System.NotImplementedException();
        }

        public void Add(Chromosome chromosome)
        {
            Contract.Requires(chromosome != null);
        }

        public IEnumerator<Chromosome> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
