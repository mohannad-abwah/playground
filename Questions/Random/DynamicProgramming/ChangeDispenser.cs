namespace DynamicProgramming
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// From article: https://avikdas.com/2019/04/15/a-graphical-introduction-to-dynamic-programming.html
    /// 
    /// The Change Making Problem asks what is the fewest number of coins you can use to make a certain amount,
    /// if you have coins of a certain set of denominations. You can use any quantity of each denomination, but
    /// only those denominations.
    /// </summary>
    [TestClass]
    public class ChangeDispenser
    {
        [DataTestMethod]
        [DynamicData(nameof(TestData))]
        public void Recursive(int amount, int[] denominations, int[] expected)
        {
            // easier to think through in decreasing size
            denominations = denominations.Reverse().ToArray();
            expected = expected?.Reverse()?.ToArray();

            var result = RecursiveImpl(amount, denominations);
            CollectionAssert.AreEqual(expected, result);
        }

        private static int[] RecursiveImpl(int amount, int[] denominations)
        {
            if (amount == 0)
                return new int[]{0};

            if (denominations.Length == 0)
                return null;

            // Intentional integer division
            int maxCountOfLargestCoin = amount/denominations[0];
            int[] smallestCombination = null;
            for (int i = maxCountOfLargestCoin; i >= 0; i--)
            {
                var result = RecursiveImpl(amount - i*denominations[0], denominations.Skip(1).ToArray());
                if (result == null)
                    continue;
                
                if (smallestCombination == null || (result.Sum()+i) < smallestCombination.Sum())
                    smallestCombination = result.Prepend(i).ToArray();
            }

            return smallestCombination;
        }

        private static IEnumerable<object[]> TestData =>
            new[]
            {
                new object[] {16, new[]{1, 5, 12, 19}, new[]{1, 3, 0, 0}},
                new object[] {3, new[]{2}, null},
                new object[] {13, new[]{1, 5, 12}, new[]{1, 0, 1}},
                new object[] {15, new[]{1, 5, 12}, new[]{0, 3, 0}},
                new object[] {17, new[]{1, 5, 12}, new[]{0, 1, 1}}
            };
    }
}