namespace TransitiveDependencies;

public interface ITransitiveDependenciesAnalyzer
{
    Tuple<char, char[]>[] GetFullDependencies(char[][] input);
}