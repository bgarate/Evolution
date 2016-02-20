# Evolution

Mono.NET [![Build Status](https://travis-ci.org/bgarate/Evolution.svg?branch=master)](https://travis-ci.org/bgarate/Evolution)

Microsoft .NET [![Build status](https://ci.appveyor.com/api/projects/status/uhabegnl9qrlo2ma?svg=true)](https://ci.appveyor.com/project/bgarate/evolution) [![Coverage Status](https://coveralls.io/repos/github/bgarate/Evolution/badge.svg?branch=master)](https://coveralls.io/github/bgarate/Evolution?branch=master)

## Introduction

Evolution is an Evolutionary Computation Framework written in C# compatible with Microsoft .NET(3.5 and up) and Mono. It was designed to be flexible and powerful. It offers a declarative style interface and modern design patterns.

## Features

- Algorithms can be defined in a fluent, declarative style
- Completely custom algorithms can be implemented reusing the existing operators and engine
- Generic, flexible and extendable
- Already implemented Mutators, selectors, alterers and chromosomes. More can be implemented
- Pluggable source of Random Number Generator
- Generic fitness: any type F that implements IComparable<F> can be used as a Fitness type

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

````
24 generations reached
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
