using System;
using System.Collections.Generic;

namespace Singular.Evolution.Core
{
    public class ProcessPipe<G,F> : IPipe<G,F> where G: IGenotype where F : IComparable<F>
    {
        private readonly IAlterer alterer;
        private readonly IPipe<G,F> to; 

        private ProcessPipe(IAlterer alterer, IPipe<G,F> to)
        {
            this.alterer = alterer;
            this.to = to;
        }

        public static IntermediatePipeFilter Do(IAlterer alterer)
        {
            return new IntermediatePipeFilter(alterer);
        }

        public class IntermediatePipeFilter
        {
            private readonly IAlterer alterer;

            internal IntermediatePipeFilter(IAlterer alterer)
            {
                this.alterer = alterer;
            }

            public ProcessPipe<G,F> To(IPipe<G,F> to)
            {
                return new ProcessPipe<G,F>(alterer,to);
            }
        }

        public IList<Individual<G,F>> Input(IList<Individual<G,F>> individuals)
        {
            // TODO: Too much recursion? Use an external executor?
            IList<Individual<G,F>> filterOutput = alterer.Apply(individuals);
            return to.Input(filterOutput);
        }
    }
}
