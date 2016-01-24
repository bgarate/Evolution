namespace Singular.Evolution.Utils
{
    public interface IRandomSource
    {
        double NextDouble();
        int NextInt();
    }
}