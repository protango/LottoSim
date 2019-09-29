using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LottoTester.Extensions;

namespace LottoTester
{
    public class Program
    {
        const int TRIALS = 10000000;
        const int STDGAMESIZE = 12;
        private static ConcurrentRandom rng = new ConcurrentRandom();
        static void Main(string[] args)
        {
            int[] numbers = new int[6], supps = new int[2];
            Func<int[][]>[] strategies = new Func<int[][]>[] {
                GenPickStatic, GenPickRandom, GenPickBigGame
            };
            GenPickStatic();

            ConcurrentDictionary<int, int>[] divResults = new ConcurrentDictionary<int, int>[strategies.Length];
            for (int i = 0; i < strategies.Length; i++) {
                divResults[i] = new ConcurrentDictionary<int, int>();
                for (int j = 1; j <= 6; j++) divResults[i].TryAdd(j, 0);
            }

            var rangePartitioner = Partitioner.Create(0, TRIALS);
            Parallel.ForEach(rangePartitioner, range => {
                for (int trial = range.Item1; trial < range.Item2; trial++) {
                    List<int> rawNumbers = rng.NextUniqueInts(8, 1, 46).ToList();
                    var supIdxs = rng.NextUniqueInts(2, 0, 8).ToArray();
                    for (int i = 0; i < supIdxs.Length; i++)
                    {
                        supps[i] = rawNumbers[supIdxs[i] - i];
                        rawNumbers.RemoveAt(supIdxs[i] - i);
                    }
                    rawNumbers.CopyTo(numbers, 0);

                    for (int i = 0; i < strategies.Length; i++)
                    {
                        var res = CalcDivision(numbers, supps, strategies[i]());
                        foreach (int r in res)
                            divResults[i][r]++;
                    }
                }
            });

            for (int i = 0; i < strategies.Length; i++)
            {
                Console.WriteLine($"Strategy {i} Results:");
                Console.WriteLine(string.Join(" | ", divResults[i].Keys.Select(x => x.ToString().PadRight(10))));
                Console.WriteLine(string.Join(" | ", divResults[i].Values.Select(x => x.ToString().PadRight(10))));
            }
        }

        private static int[][] staticPicks = new int[0][];
        private static int[][] GenPickStatic() 
        {
            if (staticPicks.Length > STDGAMESIZE)
            {
                staticPicks = staticPicks.Take(STDGAMESIZE).ToArray();
            }
            else if (staticPicks.Length < STDGAMESIZE)
            {
                Random staticRng = new Random(10);
                staticPicks = new int[STDGAMESIZE][];
                for (int i = 0; i < staticPicks.Length; i++)
                    staticPicks[i] = staticRng.NextUniqueInts(6, 1, 46).ToArray();
            }
            return staticPicks;
        }

        private static int[][] GenPickRandom()
        {
            int[][] picks = new int[STDGAMESIZE][];
            for (int i = 0; i < picks.Length; i++) 
                picks[i] = rng.NextUniqueInts(6, 1, 46).ToArray();
            return picks;
        }

        static bool firstRun = true;
        private static int[][] GenPickBigGame()
        {
            if (!firstRun) {
                return new int[0][];
            }
            firstRun = false;
            int[][] picks = new int[STDGAMESIZE * TRIALS][];
            for (int i = 0; i < picks.Length; i++)
                picks[i] = rng.NextUniqueInts(6, 1, 46).ToArray();
            return picks;
        }

        private static int[] CalcDivision(int[] numbers, int[] supps, int[][] picks)
        {
            List<int> result = new List<int>();
            foreach (var pick in picks) {
                int numCnt = pick.Count(x => numbers.Contains(x));
                int supCnt = pick.Count(x => supps.Contains(x));
                if ((numCnt == 1 || numCnt == 2) && supCnt == 2) result.Add(6);
                else if (numCnt == 3 && supCnt >= 1) result.Add(5);
                else if (numCnt == 4) result.Add(4);
                else if (numCnt == 5 && supCnt == 0) result.Add(3);
                else if (numCnt == 5 && supCnt == 1) result.Add(2);
                else if (numCnt == 6) result.Add(1);
            }
            return result.ToArray();
        }
    }
}
