namespace CrackingTheCodingInterview.Chapters
{
    using System.Collections.Generic;

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
    }
}
