//load file
using System.Text.RegularExpressions;


var input = File.ReadAllText("./input.txt");
var content = Regex.Matches(input, @"do\(\)|don't\(\)|mul\(([0-9]{1,3}),([0-9]{1,3})\)");


var enabled = true;
var sum = 0;
foreach (Match match in content)
{
    if (match.Value == "don't()") { enabled = false; continue; }
    if (match.Value == "do()") { enabled = true; continue; }
    if (enabled)
    {
        sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
    }
}
Console.WriteLine(sum.ToString());

