using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Singular.Evolution.Core;

namespace Singular.Evolution.Genotypes
{
    /// <summary>
    /// Represents a list-like genotype
    /// </summary>
    /// <typeparam name="G"></typeparam>
    /// <seealso>
    ///     <cref>IListGenotype{Singular.Evolution.Genotypes.ListGenotype{G}, G}</cref>
    /// </seealso>
    [Factory(typeof (ListGenotypeFactory<>))]
    public class ListGenotype<G> : IListGenotype<ListGenotype<G>, G> where G : IGene, new()
    {
        private readonly List<G> genesList;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListGenotype{G}"/> class.
        /// </summary>
        /// <param name="count">The count.</param>
        public ListGenotype(int count)
        {
            genesList = new List<G>(count);

            for (int i = 0; i < count; i++)
            {
                genesList.Add(new G());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListGenotype{G}"/> class.
        /// </summary>
        /// <param name="genes">The genes.</param>
        public ListGenotype(IEnumerable<G> genes)
        {
            genesList = new List<G>(genes);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<G> GetEnumerator()
        {
            return genesList.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return genesList.GetEnumerator();
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        IGenotype IGenotype.Clone()
        {
            return Clone();
        }

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get { return genesList.All(g => g.IsValid); }
        }

        /// <summary>
        /// Gets the number of genes
        /// </summary>
        /// <value>
        /// Number of genes
        /// </value>
        public int Count => genesList.Count;

        /// <summary>
        /// Gets the gene at the specified index.
        /// </summary>
        /// <value>
        /// The gene
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public G this[int index] => genesList[index];

        /// <summary>
        /// Gets a copy of the genotype with genes at positions i and j swaped
        /// </summary>
        /// <param name="i">One gene</param>
        /// <param name="j">The other gene</param>
        /// <returns></returns>
        public ListGenotype<G> Swap(int i, int j)
        {
            List<G> res = new List<G>(genesList);
            G t = res[i];
            res[i] = res[j];
            res[j] = t;
            return new ListGenotype<G>(res);
        }

        /// <summary>
        /// Gets a copy of the genotype with gene gene at position i replaced with g
        /// </summary>
        /// <param name="i">The index</param>
        /// <param name="g">The gene</param>
        /// <returns></returns>
        public ListGenotype<G> Replace(int i, G g)
        {
            List<G> res = new List<G>(genesList);
            res[i] = g;
            return new ListGenotype<G>(res);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public ListGenotype<G> Clone()
        {
            return new ListGenotype<G>(genesList.Select(g => (G) g.Clone()));
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Join("|", genesList.Select(g => g.ToString()).ToArray());
        }
    }
}