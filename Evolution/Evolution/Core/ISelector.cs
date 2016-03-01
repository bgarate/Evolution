using System;

namespace Singular.Evolution.Core
{
    /// <summary>
    /// Interface for selectors. Selectors return a list of <see cref="Individual{G,F}"/> taken from its input <see cref="Individual{G,F}"/>.
    /// Generally, the 
    /// </summary>
    /// <typeparam name="G"></typeparam>
    /// <typeparam name="F"></typeparam>
    /// <seealso cref="Singular.Evolution.Core.IAlterer{G, F}" />
    public interface ISelector<G, F> : IAlterer<G, F> where G : IGenotype where F : IComparable<F>
    {
        /// <summary>
        /// The fitness scaling that is performed to the fitness of individuals to take into account on selection
        /// </summary>
        /// <value>
        /// The scaling.
        /// </value>
        IFitnessScaling<F> Scaling { get; }
    }
}