using LottoTester.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Troschuetz.Random;

namespace LottoTester.Lotto
{
    public class Draw
    {
        private const double gameCntMean = 7742577.6;
        private const double gameCntStDev = 1404830.011;
        private static TRandom rng = new TRandom();

        /// <summary>
        /// The total division 1 prize pool in dollars for this draw
        /// </summary>
        public decimal Jackpot { get; }
        /// <summary>
        /// The date on which this draw happened/will happen
        /// </summary>
        public DateTime Date { get; }
        /// <summary>
        /// The results of this draw, is null if this draw has not happened yet
        /// </summary>
        public DrawResult Result { get; private set; } = null;

        public Draw(decimal jackpot, DateTime date) {
            Jackpot = jackpot;
            Date = date;
        }

        public void DoDraw() {
            int gamesPlayed = (int)Math.Round(rng.Normal(gameCntMean, gameCntStDev));
            int[] nums = rng.NextUniqueInts(8, 1, 46).ToArray();
            int[] winNums = nums.Take(6).ToArray();
            int[] supNums = nums.TakeLast(2).ToArray();

            Result = new DrawResult(winNums, supNums, gamesPlayed);
        }
    }
}
