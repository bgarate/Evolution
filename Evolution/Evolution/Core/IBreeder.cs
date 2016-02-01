using System.Collections.Generic;

namespace Singular.Evolution.Core
{
    public interface IBreeder<G> : IGenotype
    {
        IList<G> Breed();
    }
}