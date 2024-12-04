//load file
using System.ComponentModel.Design;
using System.Text.RegularExpressions;


var input = File.ReadAllLines("./input.txt");

//easiet way to do it is loop through row 1 to length -1, and col 1 to -1 and look for A's, then look for valid mas on the diagonals


var xmas = 0;
for (int i = 1;  i < input.Length - 1; i++)
{
    var line = input[i];
    for (int j = 1; j < input[0].Length - 1; j++)
    {
        if (line[j] != 'A') continue;

        // X 88
        // M 77
        // A 65
        // S 83

        //as we only have XMAS in the puzzle we can just add the numerical values of the 2 corners together and make sure they add up correctly without needing to worry.

        var dig1 = input[i - 1][j-1] + input[i+1][j+1];
        var dig2 = input[i-1][j+1] + input[i+1][j-1];
        if (dig1 == dig2 && dig1 == ('M' + 'S'))
        {
            xmas++;
        }

    }
}

Console.WriteLine(xmas);