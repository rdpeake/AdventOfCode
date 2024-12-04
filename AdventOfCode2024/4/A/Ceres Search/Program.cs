//load file
using System.ComponentModel.Design;
using System.Text.RegularExpressions;


var input = File.ReadAllLines("./input.txt");

var height = input.Length;
var width = input[0].Length;

var forward = String.Join("\n", input);
var backward = String.Join("\n", input.Select(x => new string(x.Reverse().ToArray())));

var diagonal = String.Join("\n", input.SelectMany((row, rowIdx) =>
         row.Select((x, colIdx) => new { Key = rowIdx - colIdx, Value = x }))
    .GroupBy(x => x.Key)
    .OrderBy(x => x.Key)
    .Select(values => new string(values.Select(i => i.Value).ToArray())));

var reverseDiagonal = string.Join("\n", diagonal.Split('\n').Select(x => new string(x.Reverse().ToArray())));

var altDiagonal = String.Join("\n", input.SelectMany((row, rowIdx) =>
         row.Select((x, colIdx) => new { Key = rowIdx + colIdx, Value = x }))
    .GroupBy(x => x.Key)
    .OrderBy(x => x.Key)
    .Select(values => new string(values.Select(i => i.Value).ToArray())));

var reverseAltDiagonal = string.Join("\n", altDiagonal.Split('\n').Select(x => new string(x.Reverse().ToArray())));

var transpose = String.Join("\n", input.SelectMany((row, rowIdx) =>
         row.Select((x, colIdx) => new { Key = colIdx, Value = x }))
    .GroupBy(x => x.Key)
    .OrderBy(x => x.Key)
    .Select(values => new string(values.Select(i => i.Value).ToArray())));

var reverseTranspose = string.Join("\n", transpose.Split('\n').Select(x => new string(x.Reverse().ToArray())));

var strings = new string[] { forward, backward, diagonal, reverseDiagonal, altDiagonal, reverseAltDiagonal, transpose, reverseTranspose };
var count = 0;

foreach (var s in strings)
{
    count += Regex.Count(s, "XMAS");
    Console.WriteLine(s + "\n\n");
}

Console.WriteLine(count);

/*
char[] SeekWord = ['X', 'M', 'A', 'S'];


var input = File.ReadAllLines("./input.txt");

var height = input.Length;
var width = input[0].Length;
var board = new Letter[height, width];


var startState = (char active) =>
{
    if (active == SeekWord[0])
    {
        return 1;
    }
    else if (active == SeekWord[SeekWord.Length - 1])
    {
        return -1;
    }
    return 0;
};

var checkLetter = (Char active, int preState) =>
{
    var completed = 0;
    var newState = startState(active);
    switch (Math.Sign(preState))
    {
        case 0:
            //the letter is not within a word itself so check if we are either the start of end of seek word
            if (active == SeekWord[0])
            {
                newState = 1;
            }
            else if (active == SeekWord[SeekWord.Length - 1])
            {
                newState = -1;
            }
            break;
        case -1:
            //the letter before is within a reverse word, so check index from right of seek word
            //get the index into seek word
            var index = SeekWord.Length + preState - 1;
            if (active == SeekWord[index])
            {
                newState = preState - 1;
                if (index == 0)
                {
                    //we have completed a word
                    completed++;
                    //we don't suport cycle so set it as no longer in a word
                    newState = startState(active);
                }
            }
            break;
        case 1:
            //the letter before is within a normal word so check index from left of seek word
            //get the index into seek word
            index = preState;
            if (active == SeekWord[index])
            {
                newState = preState + 1;
                if (index == SeekWord.Length - 1)
                {
                    completed++;
                    newState = startState(active);
                }
            }
            break;
    }
    return (newState, completed);
};

var total = 0;

for (var i = 0;i < input.Length;i++)
{
    var line = input[i];
    for (var j = 0;j < line.Length; j++)
    {
        var completed = 0;
        int newlyCompleted;
        //get the active letter
        var active = line[j];
        //check west (-j)
        var westState = startState(active);
        if (j > 1)
        {
            var west = board[i, j - 1];
            (westState, newlyCompleted) = checkLetter(active, west.westState);
            completed += newlyCompleted;
        }
        //check north west (-j, -i)
        var northwestState = startState(active);
        if (j > 1 && i > 1)
        {
            var northWest = board[i - 1, j - 1];
            (northwestState, newlyCompleted) = checkLetter(active, northWest.northwestState);
            completed += newlyCompleted;
        }
        //check north (-i)
        var northState = startState(active);
        if (i > 1)
        {
            var north = board[i - 1, j];
            (northState, newlyCompleted) = checkLetter(active, north.northState);
            completed += newlyCompleted;
        }
        //check north east (-i, +j)
        var northeastState = startState(active);
        if (i > 1 && j < line.Length - 1)
        {
            var northEast = board[i - 1, j + 1];
            (northeastState, newlyCompleted) = checkLetter(active, northEast.northeastState);
            completed += newlyCompleted;
        }
        board[i,j] = new Letter(active, westState, northwestState, northState, northeastState, completed);
        total += completed;
    }
}


Console.WriteLine(total.ToString());







//tracking class
public class Letter
{
    public Char letter { get; private set; }
    // values of 1 through Seekword.length are used for positive words
    // values of -1 through -seekWord.length are used for negative words
    // values of 0 are used letter not in a word
    public int westState { get; private set; }
    public int northwestState { get; private set; }
    public int northState { get; private set; }
    public int northeastState { get; private set; }
    public int completed { get; private set; }

    public Letter(char letter, int westState, int northwestState, int northState, int northeastState, int completed)
    {
        this.letter = letter;
        this.westState = westState;
        this.northwestState = northwestState;
        this.northState = northState;
        this.northeastState = northeastState;
        this.completed = completed;
    }
}
*/