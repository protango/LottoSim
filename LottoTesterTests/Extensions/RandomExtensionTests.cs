using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using LottoTester.Extensions;
using System.Linq;
using Troschuetz.Random;

namespace LottoTesterTests.Extensions
{
    [TestClass]
    public class RandomExtensionTests
    {
        [TestMethod]
        public void NextUniqueInts_Valid()
        {
            // Arrange
            int min = 1;
            int max = 46;
            int count = 8;
            TRandom rng = new TRandom();

            // Act
            IEnumerable<int> result = rng.NextUniqueInts(count, min, max);

            // Assert
            Assert.AreEqual(count, result.Count(), "Incorrect amount of numbers generated");
            Assert.AreEqual(result.Count(), result.Distinct().Count(), "Numbers were not unique");
            Assert.IsFalse(result.Any(x => x >= max), "Result included numbers higher than the maximum allowed");
            Assert.IsFalse(result.Any(x => x < min), "Result included numbers lower than the minimum allowed");
        }

        [TestMethod]
        public void NextUniqueInts_Randomness()
        {
            // Arrange
            int min = 0;
            int max = 45;
            int count = 8;
            int seed1 = 1;
            int seed2 = 2;
            int tests = 100;

            // Act
            TRandom rng = new TRandom(seed1);
            IEnumerable<int> result1 = rng.NextUniqueInts(count, min, max);
            rng = new TRandom(seed1);
            IEnumerable<int> result1_again = rng.NextUniqueInts(count, min, max);

            rng = new TRandom(seed2);
            IEnumerable<int> result2 = rng.NextUniqueInts(count, min, max);

            rng = new TRandom(seed1);
            string[] results = new string[tests];
            for (int i = 0; i < tests; i++) {
                results[i] = string.Join(',', rng.NextUniqueInts(count, min, max).ToArray());
            }

            // Assert
            CollectionAssert.AreEqual(result1.ToArray(), result1_again.ToArray(), "Same seed value produced different results");
            CollectionAssert.AreNotEqual(result1.ToArray(), result2.ToArray(), "Different seed value did not produce a different result");
            Assert.IsTrue(results.Distinct().Count() > 1, "Method is generating the same result over and over again");
        }
    }
}
