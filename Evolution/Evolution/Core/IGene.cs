namespace Singular.Evolution.Core
{
    public interface IGene
    {
        bool IsValid { get; }
        IGene Clone();
    }
}