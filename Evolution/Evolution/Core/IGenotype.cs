namespace Singular.Evolution.Core
{
    public interface IGenotype
    {
        bool IsValid { get; }
        IGenotype Clone();
    }
}