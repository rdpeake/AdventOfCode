
using System.Text.RegularExpressions;

var input = File.ReadAllLines("./input.txt");
var height = input.Length;
var width = input[0].Length;

var board = input.SelectMany((x, i) => x.Select((y, j) => new Antenna(y, j, i)));

var activeAntenna = board.Where(x => Regex.IsMatch($"{x.Frequency}", "[0-9a-zA-Z]")).GroupBy(x => x.Frequency);








class Antenna
{
    public char Frequency { get; private set; }
    public List<char> Antinodes { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }

    public Antenna(char frequency, int x, int y)
    {
        Frequency = frequency;
        X = x;
        Y = y;
        Antinodes = new List<char>();
    }
}