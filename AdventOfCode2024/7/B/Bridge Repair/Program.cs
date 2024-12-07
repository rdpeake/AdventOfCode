
var input = File.ReadAllLines("./input.txt");

var validPermutations = ((long Result, IEnumerable<long> Components) input) =>
{
    var operators = new Func<long, long, long>[] { (a, b) => a + b, (a, b) => a * b, (a, b) => long.Parse($"{a}{b}") };
    var current = input.Components.Take(1);
    for (int i = 1; i < input.Components.Count(); i++)
    {
        var element = input.Components.ElementAt(i);
        current = current.SelectMany(x => operators.Select(y => y(x, element)));
    }
    return current.Any(x => x == input.Result);
};


var equations = input.Select(x => x.Split(": ")).Select(y => (long.Parse(y[0]), y[1].Split(' ').Select(y => long.Parse(y))));
var valid = equations.Where(x => validPermutations(x));

var sum = valid.Select(x => x.Item1).Sum();

Console.WriteLine(sum.ToString());