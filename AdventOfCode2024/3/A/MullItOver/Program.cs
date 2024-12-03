//load file
using System.Text.RegularExpressions;


var input = File.ReadAllText("./input.txt");
var content = Regex.Matches(input, @"mul\(([0-9]{1,3}),([0-9]{1,3})\)");

var result = content.Select(x => int.Parse(x.Groups[1].Value) * int.Parse(x.Groups[2].Value)).Sum();

Console.WriteLine(result.ToString());

