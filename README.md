# Evolution

Mono.NET [![Build Status](https://travis-ci.org/bgarate/Evolution.svg?branch=master)](https://travis-ci.org/bgarate/Evolution)

Microsoft .NET [![Build status](https://ci.appveyor.com/api/projects/status/uhabegnl9qrlo2ma?svg=true)](https://ci.appveyor.com/project/bgarate/evolution) [![Coverage Status](https://coveralls.io/repos/github/bgarate/Evolution/badge.svg?branch=master)](https://coveralls.io/github/bgarate/Evolution?branch=master)

## What is Singular.Evolution?

Singular.Evolution or simply Evolution is an Evolutionary Computation Framework written in C# compatible with Microsoft .NET and Mono frameworks 3.5 and up. It was designed to be flexible and powerful. It offers a declarative style interface and modern architecture.

## Architecture

### Engine

The engine is responsible for providing the runtime environment. After the algorithm has been defined, the engine is responsible of running and keep the state of the evolution.

### Algorithm

The algorithm knows all the steps that must be taken place in each epoch or generation. Before building the `Engine`, an `Algorithm` must be defined. At the moment, only `EasyGa`, a basic genetic algorithm has been implemented. To create a custom one, `IAlgorithm` can be implemented or `EasyGa` can be inherited.

### Genes and Genotypes

Genes (which implement `IGene`) are the basic information blocks processed by the `Algorithm`. All the genes of an `Individual` form a `Genotype`. There exist multiple already implemented `Gene` types:

* Numeric genes. They wrap another type (generally a primitive type) and provide basic arithmetic operations
 * `FloatGene`: They store a `double` value. They can be optionally *bounded*, *minimum* and *maximum* values can be defined for its value.
 * `BitGene`: They store a boolean value.
* `EnumGene`: They wrap an `Enum`.

Genotypes implement the `IGenotype` interface. By now only the `ListGenotype` genotype is provided as an implementation of the `IListGenotype` interface. In it, genes are arranged in a constant size list.

### Fitness and Individuals

When declaring the engine any type `F` that implements `IComparable<F>` can hold the fitness value for the individuals. `Individuals` have a `Genotype` and can be assigned a `Fitness`.

### World

World represents the state of the population of individuals in a particular generation. The engine returns a `World` anytime a step (generation or epoch) of the algorithm is run.

### Selectors, Alterers and Breeders

The algorithm makes use of this three elements to initialize, modify and return a new population. An `Alterer`, given a pool of individuals, returns a new set. A `Selector` selects some individuals from a population. In fact, a `Selector` can be seen as an alterer that doesn't change the individuals. `Breeders` provide a way to get the starting population for the algorithm.

## Example
(Code is written on C# 6)

This is a simple example in which genotypes are a collection of `BitGene` and the `Fitness` of an Individual is seen as the number of BitGenes with value true.

````c#
public void EasyGaTest()
  {
    // Crossover with one cut ppoin 
    var crossover = new MultipointCrossover<ListGenotype<BitGene>, BitGene, double>(1);

    var algorithm =
    new EasyGa<ListGenotype<BitGene>, double>.Builder()
          .WithElitismPercentage(0.5)             // Best 50% of previous population remains
          .WithFitnessFunction(g => ((ListGenotype<BitGene>) g).Count(b => b.Value))  // Fitness is # of Genes with value 'true'
          .WithStopCriteria(w => w.Generation > 2500 || w.BestFitness == 20) // Stop if I reach a Genotype full of genes with value 'true'
                                                                            // or generation 2501
          .RegisterBreeder(new BitBreeder(20,20))         // Start with 20 individuals with 20 BitGenes
          .Register(new RouletteWheelSelector<ListGenotype<BitGene>>(20)) // Select 20 individuals with RouletteWheelSelector (Fitness Proportionate Selection)
          .Register(new Recombinator<ListGenotype<BitGene>, double>(crossover, 2, 10)) // Apply the crossover operation 10 times taking two parents each time
          .Register(new BitMutator<ListGenotype<BitGene>, double>(0.05)) // Mutation with a 5% probability over all population's genes
          .Build(); // Build the algorithm

    var engine = new Engine<ListGenotype<BitGene>, double>.Builder()
          .WithAlgorithm(algorithm)
          .Build(); // Build the engine

    while (!engine.HasReachedStopCriteria) // Run until the stop criteria is met
    {
      engine.NextGeneration(); // Run the next generation
    }

    Debug.WriteLine($"{engine.World.Generation} generations reached");
    Debug.WriteLine($"{engine.World.BestFitness} is best Fitness");
    Debug.WriteLine($"{engine.World.BestGenotype} is best Genotype");

  }
````

An example output would be:

> &nbsp;</br>
> 24 generations reached</br>
> 20 is best Fitness</br>
> 1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1 is best Genotype</br>
> &nbsp;
