namespace Scopes.Engine
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections;
    using System.Globalization;
    using System.Diagnostics.Contracts;

    public class Population : IPopulation
    {
        private readonly List<Chromosome> chromosomes;
        private double elitismRate = 0.9;

        public double ElitismRate
        {
            get
            {
                return this.elitismRate;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value >= 0.0d && value <= 1.0d, "Elitism rate must be between 0.0 and 1.0");

                this.elitismRate = value;
            }
        }

        public int Limit { get; set; }

        public int Size
        {
            get
            {
                return this.chromosomes.Count;
            }
        }

        public IList<Chromosome> Chromosomes { get { return this.chromosomes; } }

        public Population()
        {
            this.chromosomes = new List<Chromosome>();
        }

        public Population(IEnumerable<Chromosome> collection) : this()
        {
            Contract.Requires<ArgumentNullException>(collection != null);
            this.chromosomes.AddRange(collection);
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
            var list = this.chromosomes.OrderByDescending(val => val.Fitness).ToArray();
            var boundary = (Int32)Math.Ceiling((1.0d - this.ElitismRate) * this.chromosomes.Count);
            var next = new List<Chromosome>(this.Limit);
            for (var idx = boundary; idx < this.chromosomes.Count; idx++) {
                next.Add(list[idx]);
            }
            var nextGeneration = new Population(next.OrderBy(val => val.Fitness)) { ElitismRate = this.ElitismRate, Limit = this.Limit };
            return nextGeneration;
        }

        public void Add(Chromosome chromosome)
        {
            if (chromosomes.Count >= this.Limit)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture,"Population of chromosomes has a count of {0}, the population limit is {1}", chromosomes.Count, this.Limit));
            }
            this.chromosomes.Add(chromosome);
        }
    }
}
