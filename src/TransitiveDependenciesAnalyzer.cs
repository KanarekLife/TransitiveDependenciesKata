namespace TransitiveDependencies;

public class TransitiveDependenciesAnalyzer : ITransitiveDependenciesAnalyzer
{
    public Tuple<char, char[]>[] GetFullDependencies(char[][] input)
    {
        return input
            .Select(x => x[0])
            .Select(x => new Tuple<char, char[]>(x, GetDependencies(ref input, x)))
            .ToArray();
    }

    private static char[] GetDependencies(ref char[][] input, char startingPoint)
    {
        var toCheck = new Queue<char>(new [] {startingPoint});
        var dependencies = new HashSet<char>();
        while (toCheck.Count > 0)
        {
            var target = toCheck.Dequeue();
            if (target != startingPoint)
            {
                if (dependencies.Contains(target)) continue;
                dependencies.Add(target);
            }
            foreach (var line in input)
            {
                if (line[0] != target) continue;
                for (var j = 1; j < line.Length; j++)
                {
                    if (dependencies.Contains(line[j]) || line[j] == startingPoint) continue;
                    toCheck.Enqueue(line[j]);
                }
            }
        }

        return dependencies.OrderBy(x => x).ToArray();
    }
}