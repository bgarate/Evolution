using System.Collections.Generic;

namespace Singular.Evolution.Core
{
    public interface IBreeder<G> where G : IGenotype
    {
        IList<G> Breed();
    }
}