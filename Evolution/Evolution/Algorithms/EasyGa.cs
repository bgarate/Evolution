using System;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Core;

namespace Singular.Evolution.Algorithms
{
    public class EasyGa<G,F> : IAlgorithm<G, F> where G : IGenotype where F : IComparable<F>
    {
        private EasyGa()
        {
            Breeders = new List<IBreeder<G>>();
            Selectors = new List<ISelector<G, F>>();
            Alterers = new List<IAlterer<G, F>>();
        }

        public IList<IBreeder<G>> Breeders { get; }
        public IList<ISelector<G, F>> Selectors { get; }
        public IList<IAlterer<G, F>> Alterers { get; }

        
        public FitnessFunctionDelegate<F> FitnessFunction { get; private set; }
        public Predicate<World<G,F>> StopCriteria { get; private set; }

        public IList<Individual<G, F>> Initialize()
        {
            IList<G> firstGeneration = Breeders.SelectMany(b => b.Breed()).ToList();
            return Individual<G, F>.FromGenotypes(firstGeneration);
        }

        public IList<Individual<G, F>> Execute(World<G,F> world)
        {
            IList<Individual<G, F>> original = world.Population;
            IList<Individual<G, F>> individuals = Selectors.SelectMany(s => s.Select(original)).ToList();
            individuals = individuals.Select(i => new Individual<G, F>(i.Genotype,FitnessFunction(i.Genotype))).ToList();
            individuals = Alterers.Aggregate(individuals, (current, alterer) => alterer.Apply(current));
            return individuals;
        }

        public bool ShouldStop(World<G, F> world)
        {
            return StopCriteria(world);
        }

        public class Builder
        {
            private readonly EasyGa<G,F> algorithm = new EasyGa<G, F>(); 

            public Builder()
            {
                
            }

            public void RegisterBreeder(IBreeder<G> breeder)
            {
                algorithm.Breeders.Add(breeder);
            }

            public void Register(ISelector<G,F> selector)
            {
                algorithm.Selectors.Add(selector);
            }

            public void Register(IAlterer<G, F> alterer)
            {
                algorithm.Alterers.Add(alterer);
            }

            FitnessFunctionDelegate<F> FitnessFunction
            {
                set { algorithm.FitnessFunction = value; }
            }

            Predicate<World<G,F>> StopCriteria
            {
                set { algorithm.StopCriteria = value; }
            }

            public EasyGa<G, F> Build()
            {
                if(!algorithm.Breeders.Any())
                    throw new Exception("Must have at least one breeder");

                if (!algorithm.Selectors.Any())
                    throw new Exception("Must have at least one selector");

                if (!algorithm.Alterers.Any())
                    throw new Exception("Must have at least one alterer");

                if(algorithm.FitnessFunction == null)
                    throw new Exception($"Must define a {nameof(algorithm.FitnessFunction)}");

                if (algorithm.StopCriteria == null)
                    throw new Exception($"Must define a {nameof(algorithm.StopCriteria)}");

                return algorithm;
            } 

        }
    }
}
