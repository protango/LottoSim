using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace LottoTester
{
    class Program
    {
        private static Random rng = new Random();
        static void Main(string[] args)
        {
            Console.ReadLine();
            Random rng = new Random();

            Stopwatch sw = new Stopwatch();
            sw.Stop();
            Console.WriteLine($"{sw.ElapsedMilliseconds}ms");
        }

        static void LottoStatic(string[] args)
        {
            /*const ulong draws = 100000000; // 1 billion tries
            ulong nextScreenUpdate = draws / 100;
            int winCount = 0;
            int[] draw = new int[6];
            int[] pick = new int[6];
            for (int j = 0; j < pick.Length; j++) pick[j] = GetUniqueRandom(1, 45, pick);

            for (ulong i = 0; i < draws; i++)
            {
                for (int j = 0; j < draw.Length; j++)
                    draw[j] = GetUniqueRandom(1, 45, draw);
                if (pick.All(x => draw.Contains(x))) winCount++;
                for (int j = 0; j < draw.Length; j++) draw[j] = 0;
                if (i == nextScreenUpdate)
                {
                    Console.Clear();
                    Console.WriteLine($"{i * 100 / draws}%");
                    nextScreenUpdate += draws / 100;
                }
            }

            Console.WriteLine($"STATIC: We won {winCount} times!");*/
        }

        static int[] DrawLotto()
        {
            const int numbers = 6;
            const int max = 45;
            int[] result = new int[numbers];

            for (int resultCursor = 0; resultCursor < result.Length; resultCursor++)
            {
                int num = rng.Next(max - resultCursor);

                for (int checkCursor = 0; checkCursor < resultCursor; checkCursor++) {
                    if (num >= result[checkCursor]) num++;
                }

                result[resultCursor] = num;

            }
            return result;
        }

        static int[] GetUniqueRandom(int numbers, int max) {
            int[] result = new int[numbers];
            int num;
            for (int j = 0; j < result.Length; j++)
            {
                do num = rng.Next(max) + 1;
                while (result.Contains(num));
                result[j] = num;
            }
            return result;
        }
    }
}
