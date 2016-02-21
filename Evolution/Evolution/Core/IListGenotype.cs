using System.Collections.Generic;

namespace Singular.Evolution.Core
{
    public interface IListGenotype<G, R> : IGenotype, IEnumerable<R> where G : IListGenotype<G, R>
        where R : IGene, new()
    {
        int Count { get; }
        R this[int index] { get; }
        G Swap(int i, int j);
        G Replace(int i, R g);
    }
}