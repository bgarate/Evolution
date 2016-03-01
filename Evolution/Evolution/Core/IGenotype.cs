namespace Singular.Evolution.Core
{
    /// <summary>
    /// Interface for genotypes
    /// </summary>
    public interface IGenotype
    {
        /// <summary>
        /// Returns true if this genotype is valid.
        /// Generally a genotype is valid is all their genes are valid. Sometimes another
        /// condition is added.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        bool IsValid { get; }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        IGenotype Clone();
    }
}