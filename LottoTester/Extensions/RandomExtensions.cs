using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LottoTester.Extensions
{
    /// <summary>
    /// Extensions to the Random class
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Generates a series of unique and random numbers. These numbers will be sorted in ascending order
        /// </summary>
        /// <param name="rng">The random number generator</param>
        /// <param name="count">The number of values to be generated</param>
        /// <param name="min">The inclusive minimum number value</param>
        /// <param name="max">The exclusive maximum number value</param>
        /// <returns>A series of unique and random numbers, sorted in ascending order.</returns>
        public static IEnumerable<int> NextUniqueInts(this Random rng, int count, int min, int max) {
            if (max - min < count)
                throw new ArgumentException("Cannot generate more numbers than what is between min and max");
            List<int> numbers = new List<int>(count);
            for (int i = 0; i < count; i++)
            {
                int randNum = rng.Next(min, max - i);
                int j = 0;
                foreach (int prevNum in numbers)
                {
                    if (randNum == prevNum) randNum++;
                    else if (prevNum > randNum) break;
                    j++;
                }
                numbers.Insert(j, randNum);
            }
            return numbers;
        }
    }
}
