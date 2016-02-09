using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Singular.Evolution.Core;

namespace Singular.Evolution.Genotypes
{
    [Factory(typeof (ListGenotypeFactory<>))]
    public class ListGenotype<G> : IListGenotype<ListGenotype<G>,G> where G : IGene, new()
    {
        private readonly List<G> genesList;

        public ListGenotype(int count)
        {
            genesList = new List<G>(count);

            for (int i = 0; i < count; i++)
            {
                genesList.Add(new G());
            }
        }

        public ListGenotype(IEnumerable<G> genes)
        {
            genesList = new List<G>(genes);
        }

        public IEnumerator<G> GetEnumerator()
        {
            return new GeneEnumerator<G>(genesList);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return genesList.GetEnumerator();
        }

        IGenotype IGenotype.Clone()
        {
            return Clone();
        }

        public bool IsValid
        {
            get { return genesList.All(g => g.IsValid); }
        }

        public int Count => genesList.Count;

        public G this[int index] => genesList[index];

        public ListGenotype<G> Swap(int i, int j)
        {
            List<G> res = new List<G>(genesList);
            G t = res[i];
            res[i] = res[j];
            res[j] = t;
            return new ListGenotype<G>(res);
        }

        public ListGenotype<G> Replace(int i, G g)
        {
            List<G> res = new List<G>(genesList);
            res[i] = g;
            return new ListGenotype<G>(res);
        }

        public ListGenotype<G> Clone()
        {
            return new ListGenotype<G>(genesList.Select(g => (G) g.Clone()));
        }

        public override string ToString()
        {
            return string.Join("|", genesList.Select(g => g.ToString()).ToArray());
        }
    }
}