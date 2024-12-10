using System.Security;

var input = File.ReadAllLines("./input.txt");

var map = input.Select((x, row) =>
        x.Select((y, col) => new Toppography(int.Parse($"{y}"))).ToArray()
    ).ToArray();

for (int i = 0; i < map.Length; i++)
{
    for (int j = 0; j < map[0].Length; j++)
    {
        if (i > 0)
        {
            map[i][j].Left = map[i - 1][j];
            map[i - 1][j].Right = map[i][j];
        }
        if (j > 0)
        {
            map[i][j].Top = map[i][j - 1];
            map[i][j - 1].Bottom = map[i][j];
        }
    }
}

var trailHeads = map.SelectMany(x => x.Where(y => y.Height == 0)).Select(x => x.TrailHead()).Sum();

Console.WriteLine(trailHeads.ToString());

class Toppography
{
    public int Height { get; private set; }
    public Toppography? Left { get; set; } = null;
    public Toppography? Right { get; set; } = null;
    public Toppography? Top { get; set; } = null;
    public Toppography? Bottom { get; set; } = null;

    public Toppography(int height)
    {
        Height = height;
    }

    public int TrailHead()
    {
        if (Height != 0) return 0;

        var destinations = find9(this);
        return destinations.Length;
    }

    private static Toppography[] find9(Toppography next)
    {

        if (next.Height == 9) return [next];
        var dest = Enumerable.Empty<Toppography>();
        if (next.Left?.Height == next.Height + 1) dest = dest.Concat(find9(next.Left));
        if (next.Right?.Height == next.Height + 1) dest = dest.Concat(find9(next.Right));
        if (next.Top?.Height == next.Height + 1) dest = dest.Concat(find9(next.Top));
        if (next.Bottom?.Height == next.Height + 1) dest = dest.Concat(find9(next.Bottom));
        return dest.Distinct().ToArray();
    }
}