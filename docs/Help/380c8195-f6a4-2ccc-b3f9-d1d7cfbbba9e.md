# IAlgorithm(*G*, *F*) Interface
 

\[Missing <summary> documentation for "T:Singular.Evolution.Algorithms.IAlgorithm`2"\]

**Namespace:**&nbsp;<a href="abe06fa4-bd7d-97b9-28d0-1b08952971eb">Singular.Evolution.Algorithms</a><br />**Assembly:**&nbsp;Evolution (in Evolution.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public interface IAlgorithm<G, F>
where G : IGenotype
where F : Object, IComparable<F>

```

**VB**<br />
``` VB
Public Interface IAlgorithm(Of G As IGenotype, F As {Object, IComparable(Of F)})
```

**C++**<br />
``` C++
generic<typename G, typename F>
where G : IGenotype
where F : Object, IComparable<F>
public interface class IAlgorithm
```

**F#**<br />
``` F#
type IAlgorithm<'G, 'F when 'G : IGenotype when 'F : Object and IComparable<'F>> =  interface end
```


#### Type Parameters
&nbsp;<dl><dt>G</dt><dd>\[Missing <typeparam name="G"/> documentation for "T:Singular.Evolution.Algorithms.IAlgorithm`2"\]</dd><dt>F</dt><dd>\[Missing <typeparam name="F"/> documentation for "T:Singular.Evolution.Algorithms.IAlgorithm`2"\]</dd></dl>&nbsp;
The IAlgorithm(G, F) type exposes the following members.


## Properties
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="06539222-fa82-5012-c9f9-c893fc58fef2">Engine</a></td><td /></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="f133172f-f2b0-ebb2-3a64-6be4edd8008e">FitnessFunction</a></td><td /></tr></table>&nbsp;
<a href="#ialgorithm(*g*,-*f*)-interface">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="4a2f4a6e-c35a-a5a4-1c08-ecd677c46685">Execute</a></td><td /></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="008a96f2-807c-69cb-9ff2-0289c19235d3">Initialize</a></td><td /></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="6abe113b-a53f-58c4-e026-b24291c92e1e">ShouldStop</a></td><td /></tr></table>&nbsp;
<a href="#ialgorithm(*g*,-*f*)-interface">Back to Top</a>

## See Also


#### Reference
<a href="abe06fa4-bd7d-97b9-28d0-1b08952971eb">Singular.Evolution.Algorithms Namespace</a><br />