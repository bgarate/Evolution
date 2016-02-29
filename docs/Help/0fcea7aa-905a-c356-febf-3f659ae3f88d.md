# ParallelResult(*I*, *O*) Constructor (IEnumerable(*I*), Func(*I*, *O*))
 

Initializes a new instance of the <a href="86418fef-dcb8-b07b-d988-7ec4a507709e">ParallelResult(I, O)</a> class

**Namespace:**&nbsp;<a href="7a43d210-bf66-e44d-0f97-e9e0fe26b1b8">Singular.Evolution.Core</a><br />**Assembly:**&nbsp;Evolution (in Evolution.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public ParallelResult(
	IEnumerable<I> input,
	Func<I, O> task
)
```

**VB**<br />
``` VB
Public Sub New ( 
	input As IEnumerable(Of I),
	task As Func(Of I, O)
)
```

**C++**<br />
``` C++
public:
ParallelResult(
	IEnumerable<I>^ input, 
	Func<I, O>^ task
)
```

**F#**<br />
``` F#
new : 
        input : IEnumerable<'I> * 
        task : Func<'I, 'O> -> ParallelResult
```


#### Parameters
&nbsp;<dl><dt>input</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/9eekhta0" target="_blank">System.Collections.Generic.IEnumerable</a>(<a href="86418fef-dcb8-b07b-d988-7ec4a507709e">*I*</a>)<br />\[Missing <param name="input"/> documentation for "M:Singular.Evolution.Core.ParallelResult`2.#ctor(System.Collections.Generic.IEnumerable{`0},System.Func{`0,`1})"\]</dd><dt>task</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/bb549151" target="_blank">System.Func</a>(<a href="86418fef-dcb8-b07b-d988-7ec4a507709e">*I*</a>, <a href="86418fef-dcb8-b07b-d988-7ec4a507709e">*O*</a>)<br />\[Missing <param name="task"/> documentation for "M:Singular.Evolution.Core.ParallelResult`2.#ctor(System.Collections.Generic.IEnumerable{`0},System.Func{`0,`1})"\]</dd></dl>

## See Also


#### Reference
<a href="86418fef-dcb8-b07b-d988-7ec4a507709e">ParallelResult(I, O) Class</a><br /><a href="ae6a31f1-b6a0-ff61-ef4c-ea7b272001c8">ParallelResult(I, O) Overload</a><br /><a href="7a43d210-bf66-e44d-0f97-e9e0fe26b1b8">Singular.Evolution.Core Namespace</a><br />