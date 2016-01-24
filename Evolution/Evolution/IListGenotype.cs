using System.Collections.Generic;

namespace Singular.Evolution
{
    public interface IListGenotype<G> : IGenotype, IEnumerable<G> where G : IGene, new()
    {
        int Count { get; }
        G this[int index] { get; }
        IListGenotype<G> Swap(int i, int j);
        IListGenotype<G> Replace(int i, G g);
    }
}