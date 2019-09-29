using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LottoTester.Extensions
{
    /// <summary>
    /// Extensions to the int[] class
    /// </summary>
    public static class IntArrayExtensions
    {
        public static bool SequenceEquals(this int[] collection, int[] otherCollection)
        {
            return collection.SequenceEquals(otherCollection, collection.Length);
        }
        public static bool SequenceEquals(this int[] collection, int[] otherCollection, int length)
        {
            if (Math.Min(collection.Length, otherCollection.Length) < length) return false;
            for (int i = 0; i < length; i++) {
                if (!collection[i].Equals(otherCollection[i]))
                    return false;
            }
            return true;
        }
        public static bool CollectionEquals(this int[] collection, int[] otherCollection)
        {
            if (collection.Length != otherCollection.Length) return false;
            int[] a1 = new int[collection.Length], a2 = new int[otherCollection.Length];
            collection.CopyTo(a1, 0);
            otherCollection.CopyTo(a2, 0);

            Array.Sort(a1);
            Array.Sort(a2);

            return a1.SequenceEquals(a2);
        }
        public static void ReverseInPlace(this int[] collection)
        {
            int[] copy = new int[collection.Length];
            collection.CopyTo(copy, 0);
            for (int i = 0; i < collection.Length; i++) {
                collection[i] = copy[copy.Length - i - 1];
            }
        }
    }
}
