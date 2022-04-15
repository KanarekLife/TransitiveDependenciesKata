using System;
using Xunit;

namespace TransitiveDependencies.Tests;

public class TransitiveDependenciesAnalyzerTests
{
    private ITransitiveDependenciesAnalyzer _analyzer;

    public TransitiveDependenciesAnalyzerTests()
    {
        _analyzer = new TransitiveDependenciesAnalyzer();
    }

    [Fact]
    public void Example1()
    {
        var input = new[]
        {
            new[] { 'A', 'B', 'C' },
            new[] { 'B', 'C', 'E' },
            new[] { 'C', 'G' },
            new[] { 'D', 'A', 'F' },
            new[] { 'E', 'F' },
            new[] { 'F', 'H' }
        };
        var expected = new[]
        {
            new Tuple<char, char[]>('A', new[] { 'B', 'C', 'E', 'F', 'G', 'H' }),
            new Tuple<char, char[]>('B', new[] { 'C', 'E', 'F', 'G', 'H' }),
            new Tuple<char, char[]>('C', new[] { 'G' }),
            new Tuple<char, char[]>('D', new[] { 'A', 'B', 'C', 'E', 'F', 'G', 'H' }),
            new Tuple<char, char[]>('E', new[] { 'F', 'H' }),
            new Tuple<char, char[]>('F', new[] { 'H' }),
        };

        var actual = _analyzer.GetFullDependencies(input);
        
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Example2()
    {
        var input = new[]
        {
            new[] { 'A', 'B' },
            new[] { 'B', 'C' },
            new[] { 'C', 'A' }
        };
        var expected = new[]
        {
            new Tuple<char, char[]>('A', new[] { 'B', 'C' }),
            new Tuple<char, char[]>('B', new[] { 'A', 'C' }),
            new Tuple<char, char[]>('C', new[] { 'A', 'B' })
        };

        var actual = _analyzer.GetFullDependencies(input);

        Assert.Equal(expected, actual);
    }
}