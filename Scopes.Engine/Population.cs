namespace Scopes.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Collections;

    public class Population : IPopulation
    {
        private readonly List<Chromosome> chromosomes = new List<Chromosome>();
        private double elitismRate = 0.9;
        private int limit;

        public double ElitismRate
        {
            get
            {
                return this.elitismRate;
            }
            set
            {
                if (value < 0.0d || value > 1.0d)
                {
                    throw new ArgumentOutOfRangeException("value", value, "Elitism rate must be between 0.0 and 1.0");
                }
                this.elitismRate = value;
            }
        }
        public int Limit
        {
            get
            {
                return this.limit;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", value, "Value must be non-negative.");
                }
                this.limit = value;
            }
        }

        public int Size
        {
            get
            {
                return this.chromosomes.Count;
            }
        }

        public IEnumerator<Chromosome> GetEnumerator()
        {
            return this.chromosomes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IPopulation NextGeneration()
        {
            throw new NotImplementedException();
        }

        public void Add(Chromosome chromosome)
        {
            if (chromosomes.Count >= this.Limit)
            {
                throw new InvalidOperationException(String.Format("Population of chromosomes has a count of{0}, the population limit is {1}", chromosomes.Count, this.Limit));
            }
            this.chromosomes.Add(chromosome);
        }
    }
}
