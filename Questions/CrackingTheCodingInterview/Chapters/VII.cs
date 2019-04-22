namespace CrackingTheCodingInterview.Chapters
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class VII
    {
        /// <summary>
        /// Find the number of pairs in an array of distinct numbers with the specified difference."/>
        /// </summary>
        /// <param name="data"></param>
        /// <param name="difference"></param>
        /// <param name="expected"></param>
        [DataTestMethod]
        [DataRow(new []{1, 7, 5, 9, 2, 12, 3}, 2, 4, DisplayName = "Sample from book")]
        [DataRow(new []{1, 2, 3, 4, 5, 6, 7}, 1, 6, DisplayName = "Simple sequence")]
        [DataRow(new []{3, 5, 7}, 2, 2, DisplayName = "Skip 1")]
        public void NumberOfPairsWithDifference(int[] data, int difference, int expected)
        {
            var set = new HashSet<int>();

            var map = new Dictionary<int, int>();
            for (int i = 0; i < data.Length; i++)
            {
                map[data[i]-difference] = data[i];
                map[data[i]+difference] = data[i];
            }

            for (int i = 0; i < data.Length; i++)
            {
                if (map.TryGetValue(data[i], out int value))
                {
                    set.Add(value);
                }
            }
            
            Assert.AreEqual(expected, set.Count);
        }

        [DataTestMethod]
        [DataRow("cbabadcbbabbcbabaabccbabc", "abbc", new[]{0, 6, 9, 11, 12, 20, 21}, DisplayName = "Sample from book")]
        [DataRow("abbc", "abbc", new[]{0}, DisplayName = "Identical")]
        [DataRow("cbba", "abbc", new[]{0}, DisplayName = "Identical but reversed")]
        [DataRow("bbbbbbb", "bb", new[]{0, 1, 2, 3, 4, 5}, DisplayName = "Repeating character")]
        [DataRow("abbabbc", "abbc", new[]{3}, DisplayName = "Psych!")]
        [DataRow("agdsfsiahfi", "abbc", new int[]{}, DisplayName = "Not found")]
        public void FindAllPermutationsOfSmallStringInBigString(string big, string small, int[] expectedPositions)
        {
            var positions = new List<int>();

            var hist = new Dictionary<char, int>();
            foreach (char c in small)
            {
                if (hist.TryGetValue(c, out int count))
                    hist[c] = count + 1;
                else
                    hist[c] = 1;
            }

            var currentHist = new Dictionary<char, int>(hist);
            var substringLength = 0;
            for (int i = 0; i < big.Length; i++)
            {
                bool found = currentHist.TryGetValue(big[i], out int count);
                if (found)
                {
                    substringLength++;
                    if (count == 1)
                        currentHist.Remove(big[i]);
                    else
                        currentHist[big[i]] = count - 1;
                }

                if (found && substringLength == small.Length)
                    positions.Add(i - (substringLength-1));

                if (!found)
                {                   
                    i -= substringLength; // TODO: It's more efficient to adjust instead of reset
                    substringLength = 0;
                    currentHist = new Dictionary<char, int>(hist);
                }
            }

            CollectionAssert.AreEqual(expectedPositions, positions);
        }
    }
}
