//load file
using System.Text.RegularExpressions;


var input = File.ReadAllLines("./input.txt");
var content = input.Select(x => Regex.Split(x, @"\s"));

var parsed = content.Select(x => x.Select(y => int.Parse(y)));

var isSafe = (IEnumerable<int> report) =>
{
    var differences = report.Zip(report.Skip(1), (x, y) => y - x);
    var sign = Math.Sign(differences.First());
    if (sign == 0) return false;
    return !differences.Any(x => Math.Abs(x) < 1 || Math.Abs(x) > 3 || Math.Sign(x) != sign);
};

var safe = parsed.Select(x => isSafe(x));

var total = safe.Where(x => x).Count();

Console.WriteLine(total.ToString());

