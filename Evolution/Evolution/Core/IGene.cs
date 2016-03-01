namespace Singular.Evolution.Core
{
    /// <summary>
    /// Inteface for genes
    /// </summary>
    public interface IGene
    {
        /// <summary>
        /// Returns true if the state of the gene is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        bool IsValid { get; }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        IGene Clone();
    }
}