using System;
using System.Collections.Generic;
using System.Linq;

namespace Singular.Evolution.Core
{
    public class Individual<G, F> where F : IComparable<F> where G : IGenotype
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
            HasFitnessAssigned = true;
        }

        public bool HasFitnessAssigned { get; }

        public G Genotype { get; }
        public F Fitness { get; }

        public Individual<G, F> Clone()
        {
            return new Individual<G, F>(Genotype);
        }

        public static IList<Individual<G, F>> FromGenotypes(IList<G> genotypes)
        {
            return genotypes.Select(g => new Individual<G, F>(g)).ToList();
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