# MathHelper.Clamp(*T*) Method 
 

\[Missing <summary> documentation for "M:Singular.Evolution.Utils.MathHelper.Clamp``1(``0,``0,``0)"\]

**Namespace:**&nbsp;<a href="bb7b030e-87d6-8095-f2c6-b0b821b0d323">Singular.Evolution.Utils</a><br />**Assembly:**&nbsp;Evolution (in Evolution.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public static T Clamp<T>(
	T value,
	T min,
	T max
)
where T : Object, IComparable<T>

```

**VB**<br />
``` VB
Public Shared Function Clamp(Of T As {Object, IComparable(Of T)}) ( 
	value As T,
	min As T,
	max As T
) As T
```

**C++**<br />
``` C++
public:
generic<typename T>
where T : Object, IComparable<T>
static T Clamp(
	T value, 
	T min, 
	T max
)
```

**F#**<br />
``` F#
static member Clamp : 
        value : 'T * 
        min : 'T * 
        max : 'T -> 'T  when 'T : Object and IComparable<'T>

```


#### Parameters
&nbsp;<dl><dt>value</dt><dd>Type: *T*<br />\[Missing <param name="value"/> documentation for "M:Singular.Evolution.Utils.MathHelper.Clamp``1(``0,``0,``0)"\]</dd><dt>min</dt><dd>Type: *T*<br />\[Missing <param name="min"/> documentation for "M:Singular.Evolution.Utils.MathHelper.Clamp``1(``0,``0,``0)"\]</dd><dt>max</dt><dd>Type: *T*<br />\[Missing <param name="max"/> documentation for "M:Singular.Evolution.Utils.MathHelper.Clamp``1(``0,``0,``0)"\]</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>T</dt><dd>\[Missing <typeparam name="T"/> documentation for "M:Singular.Evolution.Utils.MathHelper.Clamp``1(``0,``0,``0)"\]</dd></dl>

#### Return Value
Type: *T*<br />\[Missing <returns> documentation for "M:Singular.Evolution.Utils.MathHelper.Clamp``1(``0,``0,``0)"\]

## See Also


#### Reference
<a href="bbce1819-ea5a-d666-8610-6d14b944b981">MathHelper Class</a><br /><a href="bb7b030e-87d6-8095-f2c6-b0b821b0d323">Singular.Evolution.Utils Namespace</a><br />