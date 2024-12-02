//load file
using System.Text.RegularExpressions;


var input = File.ReadAllLines("./input.txt");
var content = input.Select(x => Regex.Split(x, @"\s"));

var parsed = content.Select(x => x.Select(y => int.Parse(y)));

Func<IEnumerable<int>, bool, bool> isSafe = null;
isSafe = (IEnumerable<int> report, bool again) =>
{
    var differences = report.Zip(report.Skip(1), (x, y) => y - x);
    var sign = Math.Sign(differences.First());
    var issafe = true;
    if (sign == 0) issafe = false;
    issafe &= !differences.Any(x => Math.Abs(x) < 1 || Math.Abs(x) > 3 || Math.Sign(x) != sign);
    if (issafe) return true;
    if (again) return false;
    var permutation = report.Select((v, i) => i).Select((x) => isSafe(report.Where((v, i) => i != x), true));
    return permutation.Any(x => x);
};

var safe = parsed.Select(x => isSafe(x, false));

var total = safe.Where(x => x).Count();

Console.WriteLine(total.ToString());

