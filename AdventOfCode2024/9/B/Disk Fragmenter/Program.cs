using System.Runtime.ExceptionServices;

var input = File.ReadAllText("./input.txt");

var blockId = 0;
var fileId = 0;

var FileMap = new Dictionary<int, (int, int)>();
var Free = new List<(int, int)>();

var Disk = input.SelectMany((x, i) =>
{
    if (i % 2 == 0)
    {
        var file = fileId++;
        var length = int.Parse($"{x}");
        var fileDescriptor = (length, blockId);
        FileMap.Add(file, fileDescriptor);
        return Enumerable.Range(0, length).Select(y => new Block() { FileID = file, BlockIndex = blockId++ });
    } else
    {
        //return free space
        var length = int.Parse($"{x}");
        Free.Add((length, blockId));
        return Enumerable.Range(0, length).Select(y => new Block() { FileID = -1, BlockIndex = blockId++ });
        
    }
}).ToList();

var files = FileMap.Keys.OrderByDescending(x => x);
for (int i = 0; i < files.Count(); i++)
{
    var File = files.ElementAt(i);
    //find where the file could fit
    var (flength, startBlock) = FileMap[File];
    var freeEntry = Free.Where(x => x.Item1 >= flength).OrderBy(x => x.Item2).FirstOrDefault((-1, -1));
    var (freeLength, freeBlock) = freeEntry;
    if (freeBlock < 0) continue;
    //now move it
    for (int j = 0; j < flength; j++)
    {
        Disk[freeBlock + j].FileID = File;
        Disk[startBlock + j].FileID = -1;
    }
    var newEntry = (freeLength - flength, freeBlock + flength);
    if (newEntry.Item1 == 0)
    {
        Free.Remove(freeEntry);
    } else
    {
        Free[Free.IndexOf(freeEntry)] = newEntry;
    }

}


Console.WriteLine(String.Join("", Disk.Select(x => $"{(x.FileID > -1 ? $"|{x.FileID}" : ".")}")));
Console.WriteLine();
Console.WriteLine(Disk.Select(x => x.Checksum).Sum().ToString());


class Block
{
    public int FileID { get; set; } = -1;
    public int BlockIndex { get; set; }
    public long Checksum => FileID > -1 ? BlockIndex * FileID : 0;
}