
var input = File.ReadAllLines("./input.txt");

var spaces = new Space[input.Length, input[0].Length];
var player = (0, 0);
var direction = (-1, 0);

for (var i = 0; i < input.Length; i++)
{
    for (var j = 0; j < input[i].Length; j++)
    {
        spaces[i, j] = new Space(input[i][j] == '#');
        if (input[i][j] == '^')
        {
            player = (i,j);
            spaces[i,j].Visited = true;
        }
    }
}

//now walk the maze
while (player.Item1 + direction.Item1 < spaces.GetLength(0) && player.Item1 + direction.Item1 >= 0 && player.Item2 + direction.Item2 < spaces.GetLength(1) && player.Item2 + direction.Item2 >= 0)
{
    var next = (player.Item1 + direction.Item1, player.Item2 + direction.Item2);
    if (spaces[next.Item1, next.Item2].Occupied)
    {
        //rotate right
        switch (direction.Item1)
        {
            case 1:
                direction.Item1 = 0;
                direction.Item2 = -1;
                break;
            case 0:
                direction.Item1 = direction.Item2 < 0 ? -1 : 1;
                direction.Item2 = 0;
                break;
            case -1:
                direction.Item1 = 0;
                direction.Item2 = 1;
                break;
        }
    } else
    {
        player.Item1 = next.Item1;
        player.Item2 = next.Item2;
        spaces[next.Item1, next.Item2].Visited = true;
    }
}

var unique = spaces.Cast<Space>().Where(x => x.Visited).Count();

Console.WriteLine(unique.ToString());


class Space
{
    public bool Occupied { get; private set; }
    public bool Visited { get; set; } = false;

    public Space(bool occupied)
    {
        this.Occupied = occupied;
    }
}
