# Evolution

Mono.NET [![Build Status](https://travis-ci.org/bgarate/Evolution.svg?branch=master)](https://travis-ci.org/bgarate/Evolution)

Microsoft .NET [![Build status](https://ci.appveyor.com/api/projects/status/uhabegnl9qrlo2ma?svg=true)](https://ci.appveyor.com/project/bgarate/evolution) [![Coverage Status](https://coveralls.io/repos/github/bgarate/Evolution/badge.svg?branch=master)](https://coveralls.io/github/bgarate/Evolution?branch=master)

## Introduction

Evolution is an Evolutionary Computation Framework written in C#. It runs on Microsoft .NET(3.5 and up) and Mono. It was designed to be flexible and powerful. It offers a declarative style interface and modern design patterns.

## Features

- Algorithms can be defined in a fluent, declarative style
- Multithreading and chaching on Fitness calculation
- Completely custom algorithms can be implemented reusing the existing operators and engine
- Generic, flexible and extendable
- Already implemented Mutators, selectors, alterers, chromosomes and executors. More can be implemented
- Pluggable source of Random Number Generator
- Generic fitness: any type F that implements IComparable<F> can be used as a Fitness type

## Implemented operators

- Algorithm:
  - EasyGa: A simple Genetic Algorithm
- Genes
  - Bit
  - Enum
  - Float
- Genotypes
  - List
- Breeders:
  - Bit
  - GeneratorByDelegate
- Selectors
  - Roulette Wheel Selection (Fitness Proportionate Selection)
- Fitness scaling
  - Linear Rank
- Crossovers:
  - Multipoint
  - Partially Matched Crossover (PMX, ordered)
- Mutators:
 - Bit
 - Uniform
 - Gaussian
 - Reverse (ordered)
 - Swap (ordered)
 - ByDelegate
 
## Sample code
(Code is written on C# 6)

This is a simple example in which genotypes are a collection of `BitGene` and the `Fitness` of an Individual is seen as the number of BitGenes with value true.

### Define the algorithm

````c#
  var crossover = new MultipointCrossover<ListGenotype<BitGene>, BitGene, double>(1);

  EasyGa<ListGenotype<BitGene>, double> algorithm =
    new EasyGa<ListGenotype<BitGene>, double>.Builder()
      .WithElitismPercentage(0.5)
      .WithFitnessFunction(g => g.Count(b => b.Value))
      .WithStopCriteria(
          StopCriteriaBuilder.StopAtGeneration<ListGenotype<BitGene>, double>(2500)
              .Or(StopCriteriaBuilder.StopAtFitness<ListGenotype<BitGene>, double>(20)))
      .RegisterBreeder(new BitBreeder(20, 20))
      .Register(new RouletteWheelSelector<ListGenotype<BitGene>>(20))
      .Register(new Recombinator<ListGenotype<BitGene>, double>(crossover, 2, 10,
          Recombinator<ListGenotype<BitGene>, double>.RecombinatioNumberType.Absolute))
      .Register(new BitMutator<ListGenotype<BitGene>, double>(0.05))
      .Build();
  ````
  
### Build the engine:  

````c#
    var engine = new Engine<ListGenotype<BitGene>, double>.Builder()
      .WithAlgorithm(algorithm)
      .Build();
  ````
  
### Evolve:  

````c#
    while (!engine.HasReachedStopCriteria)
    {
      engine.NextGeneration();
    }
  ````
  
### Output the results:  

````c#
    Console.WriteLine($"{engine.CurrentWorld.Generation} generations reached");
    Console.WriteLine($"{engine.Statistics.BestIndividualGeneration} is best indivual's generation");
    Console.WriteLine($"{engine.Statistics.BestFitness} is best Fitness");
    Console.WriteLine($"{engine.Statistics.BestGenotype} is best Genotype");
````

An example output would be:

````
24 generations reached
24 is best indivual's generation
20 is best Fitness
1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1 is best Genotype
````

## Contributing

1. Fork it!
2. Create your feature branch: git checkout -b my-new-feature
3. Commit your changes: git commit -am 'Add some feature'
4. Push to the branch: git push origin my-new-feature
5. Submit a pull request :D

## License

Copyright 2016 Bruno Garate

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
