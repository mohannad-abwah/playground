namespace DynamicProgramming
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Fibonacci
    {

        #region RecursiveNaive

        [DataTestMethod]
        [DynamicData(nameof(TestData))]
        public void RecursiveNaive(int n, ulong expected)
        {
            Assert.AreEqual(expected, RecursiveNaiveImpl(n));
        }

        private static ulong RecursiveNaiveImpl(int n)
        {
            if (n == 0 || n == 1)
                return (ulong)n;

            return RecursiveNaiveImpl(n-2) + RecursiveNaiveImpl(n-1);
        }

        #endregion

        #region RecursiveMemoized

        [DataTestMethod]
        [DynamicData(nameof(TestData))]
        public void RecursiveMemoized(int n, ulong expected)
        {
            Assert.AreEqual(expected, RecursiveMemoizedImpl(n, new Dictionary<int, ulong>(n)));
        }

        private static ulong RecursiveMemoizedImpl(int n, Dictionary<int, ulong> cache)
        {
            if (n == 0 || n == 1)
                return (ulong)n;

            if (cache.TryGetValue(n, out ulong value))
                return value;


            Console.WriteLine($"Calculating f({n})");
            value = RecursiveMemoizedImpl(n-2, cache) + RecursiveMemoizedImpl(n-1, cache);
            cache[n] = value;
            return value;
        }

        #endregion

        #region Iterative

        [DataTestMethod]
        [DynamicData(nameof(TestData))]
        public void Iterative(int n, ulong expected)
        {
            ulong fn = 1;

            if (n == 0 || n == 1)
            {
                fn = (ulong)n;
            }
            else
            {
                ulong fn_2 = 0;
                ulong fn_1 = 1;

                for (int i = 2; i < n; i++)
                {
                    fn_2 = fn_1;
                    fn_1 = fn;
                    fn = fn_2 + fn_1;
                }
            }

            Assert.AreEqual(expected, fn);
        }

        #endregion

        private static IEnumerable<object[]> TestData =>
            new[]
            {
                new object[] {0, 0UL},
                new object[] {1, 1UL},
                new object[] {2, 1UL},
                new object[] {7, 13UL},
                new object[] {10, 55UL},
                new object[] {30, 832040UL}
            };
    }
}
