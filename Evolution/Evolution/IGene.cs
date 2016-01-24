namespace Singular.Evolution
{
    public interface IGene
    {
        bool IsValid { get; }
        IGene Clone();
    }
}