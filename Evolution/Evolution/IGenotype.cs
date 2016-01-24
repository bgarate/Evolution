namespace Singular.Evolution
{
    public interface IGenotype
    {
        bool IsValid { get; }
        IGenotype Clone();
    }
}