using System;
using System.Collections.Generic;
using System.Linq;

namespace Singular.Evolution.Core
{
    public class Individual<G, F> : IComparable<Individual<G,F>> where F : IComparable<F> where G : IGenotype
    {
        public Individual(G genotype)
        {
            Genotype = genotype;
            HasFitnessAssigned = false;
        }

        public Individual(G genotype, F fitness)
        {
            Genotype = genotype;
            Fitness = fitness;
        }

        public bool HasFitnessAssigned { get; private set; }

        public G Genotype { get; }

        F fitness = default (F);

        public F Fitness
        {
            get
            {
                if (!HasFitnessAssigned)
                    throw new InvalidOperationException("This individual has not fitness assigned");

                return fitness;
            }
            private set
            {
                fitness = value;
                HasFitnessAssigned = true;
            }
        }

        public F FitnessOrDefault => HasFitnessAssigned ? fitness : default(F);

        public Individual<G, F> Clone()
        {
            return new Individual<G, F>(Genotype);
        }

        public static IList<Individual<G, F>> FromGenotypes(IList<G> genotypes)
        {
            return genotypes.Select(g => new Individual<G, F>(g)).ToList();
        }

        public int CompareTo(Individual<G, F> other)
        {
            return Fitness.CompareTo(other.Fitness);
        }

        public override string ToString()
        {
            return $"{{ Fitness: {(HasFitnessAssigned ? Fitness.ToString() : "-")} Genotype: {Genotype} }}";
        }
    }

    public static class IndividualExtensions
    {
        public static IList<G> GetGenotypes<G, F>(this IList<Individual<G, F>> individuals) where F : IComparable<F>
            where G : IGenotype
        {
            return individuals.Select(i => i.Genotype).ToList();
        }
    }
    
}