namespace DynamicProgramming
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// From article: https://avikdas.com/2019/04/15/a-graphical-introduction-to-dynamic-programming.html
    /// 
    /// In the House Robber Problem, you are a robber who has found a block of houses to rob. Each house has a non-negative value
    /// inside that you can steal. However, due to the way the security systems of the houses are connected, you’ll get caught if
    /// you rob two adjacent houses. What’s the maximum value you can steal from the block?
    /// 
    /// Note: There's a slightly more complex version of this where the houses are in a circle.
    /// </summary>
    [TestClass]
    public class HouseRobber
    {
        [DataTestMethod]
        [DynamicData(nameof(TestData))]
        public void Recursive(int[] houses, int expected)
        {
            Assert.AreEqual(expected, RecursiveImpl(houses));
        }

        private static int RecursiveImpl(int[] houses)
        {
            if (houses.Length == 0)
                return 0;

            if (houses.Length == 1)
                return houses[0];

            if (houses.Length == 2)
                return Math.Max(houses[0], houses[1]);

            int largestStolenAmount = 0;
            for (int i = 0; i < houses.Length; i++)
            {
                int potentialStolenAmount = houses[i] + RecursiveImpl(houses.Skip(i+2).ToArray());
                largestStolenAmount = Math.Max(largestStolenAmount, potentialStolenAmount);
            }
            return largestStolenAmount;
        }
        
        [DataTestMethod]
        [DynamicData(nameof(TestData))]
        public void Iterative(int[] houses, int expected)
        {
            int stolenAmount;

            if (!houses.Any())
            {
                stolenAmount = 0;
            }
            else if (houses.Length < 3)
            {
                stolenAmount = houses.Max();
            }
            else
            {
                int[] maxAmounts = new int[houses.Length];
                maxAmounts[0] = houses[0];
                maxAmounts[1] = Math.Max(houses[0], houses[1]);
                for (int i = 2; i < houses.Length; i++)
                {
                    maxAmounts[i] = Math.Max(maxAmounts[i-1], maxAmounts[i-2] + houses[i]);
                }

                stolenAmount = maxAmounts.Last();
            }

            Assert.AreEqual(expected, stolenAmount);
        }

        private static IEnumerable<object[]> TestData =>
            new[]
            {
                new object[] {new[]{3, 10, 3, 1, 2}, 12},
                new object[] {new[]{1, 2, 3, 1}, 4},
                new object[] {new[]{2, 7, 9, 3, 1}, 12},
                new object[] {new int[]{}, 0},
                new object[] {new[]{4}, 4},
                new object[] {new[]{5, 3}, 5},
                new object[] {new[]{3, 5}, 5},
                new object[] {new[]{9, 9}, 9}
            };
    }
}
