using System;
using System.Collections.Generic;
using Singular.Evolution.Utils;

namespace Singular.Evolution.Core
{
    public interface IBreeder<G> where G: IGenotype
    {
        IList<G> Breed();
    }
}