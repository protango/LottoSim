using System;
using System.Collections.Generic;
using System.Text;
using LottoTester.Extensions;


namespace LottoTester
{
    class ConcurrentRandom
    {
        private static readonly Random _global = new Random();
        [ThreadStatic] private static Random _local = null;

        /// <summary>
        /// Generates a series of unique and random numbers. These numbers will be sorted in ascending order
        /// </summary>
        /// <param name="rng">The random number generator</param>
        /// <param name="count">The number of values to be generated</param>
        /// <param name="min">The inclusive minimum number value</param>
        /// <param name="max">The exclusive maximum number value</param>
        /// <returns>A series of unique and random numbers, sorted in ascending order.</returns>
        public IEnumerable<int> NextUniqueInts(int count, int min, int max)
        {
            EnsureLocalExists();
            return _local.NextUniqueInts(count, min, max);
        }

        //
        // Summary:
        //     Returns a non-negative random integer.
        //
        // Returns:
        //     A 32-bit signed integer that is greater than or equal to 0 and less than System.Int32.MaxValue.
        public virtual int Next() {
            EnsureLocalExists();
            return _local.Next();
        }
        //
        // Summary:
        //     Returns a non-negative random integer that is less than the specified maximum.
        //
        // Parameters:
        //   maxValue:
        //     The exclusive upper bound of the random number to be generated. maxValue must
        //     be greater than or equal to 0.
        //
        // Returns:
        //     A 32-bit signed integer that is greater than or equal to 0, and less than maxValue;
        //     that is, the range of return values ordinarily includes 0 but not maxValue. However,
        //     if maxValue equals 0, maxValue is returned.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     maxValue is less than 0.
        public virtual int Next(int maxValue) {
            EnsureLocalExists();
            return _local.Next(maxValue);
        }
        //
        // Summary:
        //     Returns a random integer that is within a specified range.
        //
        // Parameters:
        //   minValue:
        //     The inclusive lower bound of the random number returned.
        //
        //   maxValue:
        //     The exclusive upper bound of the random number returned. maxValue must be greater
        //     than or equal to minValue.
        //
        // Returns:
        //     A 32-bit signed integer greater than or equal to minValue and less than maxValue;
        //     that is, the range of return values includes minValue but not maxValue. If minValue
        //     equals maxValue, minValue is returned.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     minValue is greater than maxValue.
        public virtual int Next(int minValue, int maxValue) {
            EnsureLocalExists();
            return _local.Next(minValue, maxValue);
        }
        //
        // Summary:
        //     Fills the elements of a specified array of bytes with random numbers.
        //
        // Parameters:
        //   buffer:
        //     An array of bytes to contain random numbers.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     buffer is null.
        public virtual void NextBytes(byte[] buffer) {
            EnsureLocalExists();
            _local.NextBytes(buffer);
        }
        public virtual void NextBytes(Span<byte> buffer) {
            EnsureLocalExists();
            _local.NextBytes(buffer);
        }
        //
        // Summary:
        //     Returns a random floating-point number that is greater than or equal to 0.0,
        //     and less than 1.0.
        //
        // Returns:
        //     A double-precision floating point number that is greater than or equal to 0.0,
        //     and less than 1.0.
        public virtual double NextDouble() {
            EnsureLocalExists();
            return _local.NextDouble();
        }

        private void EnsureLocalExists() {
            if (_local == null)
            {
                lock (_global)
                {
                    if (_local == null)
                    {
                        int seed = _global.Next();
                        _local = new Random(seed);
                    }
                }
            }
        }
    }
}
