namespace Random.DynamicProgramming
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
        public void RecursiveNaiveTest(int n, ulong expected)
        {
            Assert.AreEqual(expected, RecursiveNaive(n));
        }

        private static ulong RecursiveNaive(int n)
        {
            if (n == 0 || n == 1)
                return (ulong)n;

            return RecursiveNaive(n-2) + RecursiveNaive(n-1);
        }

        #endregion

        #region RecursiveMemoized

        [DataTestMethod]
        [DynamicData(nameof(TestData))]
        public void RecursiveMemoizedTest(int n, ulong expected)
        {
            Assert.AreEqual(expected, RecursiveMemoized(n, new Dictionary<int, ulong>(n)));
        }

        private static ulong RecursiveMemoized(int n, Dictionary<int, ulong> cache)
        {
            if (n == 0 || n == 1)
                return (ulong)n;

            if (cache.TryGetValue(n, out ulong value))
                return value;


            Console.WriteLine($"Calculating f({n})");
            value = RecursiveMemoized(n-2, cache) + RecursiveMemoized(n-1, cache);
            cache[n] = value;
            return value;
        }

        #endregion

        #region Iterative

        [DataTestMethod]
        [DynamicData(nameof(TestData))]
        public void Iterative(int n, ulong expected)
        {
            ulong fn_2 = 0;
            ulong fn_1 = 1;
            ulong fn = 1;

            if (n == 0 || n == 1)
            {
                fn = (ulong)n;
            }
            else
            {
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

        private static IEnumerable<object[]> TestData
        {
            get
            {
                yield return new object[] {0, 0UL};
                yield return new object[] {1, 1UL};
                yield return new object[] {2, 1UL};
                yield return new object[] {7, 13UL};
                yield return new object[] {10, 55UL};
                yield return new object[] {30, 832040UL};
            }
        }
    }
}
