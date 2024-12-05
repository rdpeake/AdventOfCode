
var input = File.ReadAllLines("./input.txt");

//get all rules:
var rules = input.Where(x => x.Contains('|')).Select(x => x.Split('|').Select((x) => int.Parse(x)))
    .Select(x => new { Key = x.ElementAt(0), Value = x.ElementAt(1) })
    .GroupBy(x => x.Key)
    .ToDictionary(x => x.Key, x=> x.Select(y => y.Value));

var printOrders = input.Where(x => x.Contains(',')).Select(x => x.Split(',').Select((x) => int.Parse(x)));

var isValid = (IEnumerable<int> printOrder) =>
{
    for (int i = 1; i < printOrder.Count(); i++)
    {
        if (!rules.ContainsKey(printOrder.ElementAt(i))) continue;
        if (printOrder.Take(i).Any(x => rules[printOrder.ElementAt(i)].Contains(x)))
        {
            return false;
        }
    }
    return true;
};

var makeValid = (IEnumerable<int> printOrder) =>
{
    return printOrder.Order(new RuleComparer(rules));
};

var invalid = printOrders.Where((x) => !isValid(x));

var valid = invalid.Select(x => makeValid(x)).Select(x => new { Order = x, Median = x.Median() }); ;

foreach (var x in valid)
{
    Console.WriteLine($"{String.Join(',', x.Order)}: {x.Median}" );
}

var total = valid.Select(x => x.Median).Sum();

Console.WriteLine(total.ToString());


class RuleComparer : IComparer<int>
{
    private Dictionary<int, IEnumerable<int>> rules;

    public RuleComparer(Dictionary<int, IEnumerable<int>> rules)
    {
        this.rules = rules;
    }

    public int Compare(int x, int y)
    {
        if (rules.ContainsKey(x))
        {
            return (rules[x].Contains(y) ? -1 : 1);
        }
        else if (rules.ContainsKey(y))
        {
            return (rules[y].Contains(x) ? 1 : -1);
        }
        return 0;
    }
}

static class MedianExtensions
{
    public static T Median<T>(this IEnumerable<T> source)
    {
        if (source == null)
            throw new ArgumentNullException("source");
        //var data = source.OrderBy(n => n).ToArray();
        var count = source.Count();
        if (count == 0 || count % 2 == 0)
            throw new InvalidOperationException();
        return source.ElementAt(count / 2);
    }
}