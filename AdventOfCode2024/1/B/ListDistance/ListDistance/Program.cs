//load file
using System.Text.RegularExpressions;


var input = File.ReadAllText("./input.txt");
var content = Regex.Split(input, @"\s+");



//parse each file into 2 sorted lists
var listA = new SortedSet<int>(new DuplicateKeyComparer<int>());
var listB = new SortedSet<int>(new DuplicateKeyComparer<int>());
for (int i = 0; i < content.Length; i+=2)
{
    listA.Add(int.Parse(content[i]));
    listB.Add(int.Parse(content[i+1]));
}

//merge lists via link
var results = listA.Select((x) => x * listB.Where(y => y == x).Count());

var sum = results.Sum();
//sub list
Console.WriteLine(sum);


// --------------------------------------------

//simple sort class
#region Comparer

/// <summary>
/// Comparer for comparing two keys, handling equality as beeing greater
/// Use this Comparer e.g. with SortedLists or SortedDictionaries, that don't allow duplicate keys
/// </summary>
/// <typeparam name="TKey"></typeparam>
public class DuplicateKeyComparer<TKey>
                :
             IComparer<TKey> where TKey : IComparable
{
    #region IComparer<TKey> Members

    public int Compare(TKey x, TKey y)
    {
        int result = x.CompareTo(y);

        if (result == 0)
            return -1; // Handle equality as being greater. Note: this will break Remove(key) or
        else          // IndexOfKey(key) since the comparer never returns 0 to signal key equality
            return result;
    }

    #endregion
}
#endregion
