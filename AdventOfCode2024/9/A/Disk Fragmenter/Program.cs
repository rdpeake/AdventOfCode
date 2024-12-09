var input = File.ReadAllText("./input.txt");

var blockId = 0;
var fileId = 0;

var Disk = input.SelectMany((x, i) =>
{
    if (i % 2 == 0)
    {
        var file = fileId++;
        return Enumerable.Range(0, int.Parse($"{x}")).Select(y => new Block() { FileID = file, BlockIndex = blockId++ });
    } else
    {
        //return free space
        return Enumerable.Range(0, int.Parse($"{x}")).Select(y => new Block() { FileID = -1, BlockIndex = blockId++ });
        
    }
}).ToList();


var endIndex = Disk.Count() - 1;

for (var i = 0; i < endIndex; i++)
{
    if (Disk[i].FileID > -1) continue;
    while (Disk[endIndex].FileID == -1)
        endIndex--;
    if (endIndex < i) continue;
    Disk[i].FileID = Disk[endIndex].FileID;
    Disk[endIndex].FileID = -1;

}


Console.WriteLine(String.Join("", Disk.Select(x => $"{(x.FileID > -1 ? x.FileID : ".")}")));
Console.WriteLine();
Console.WriteLine(Disk.Select(x => x.Checksum).Sum().ToString());


class Block
{
    public int FileID { get; set; } = -1;
    public int BlockIndex { get; set; }
    public long Checksum => FileID > -1 ? BlockIndex * FileID : 0;
}