using System;
using System.Collections.Generic;
using System.Text;

namespace LottoTester.Lotto
{
    public class DrawResult
    {
        /// <summary>
        /// The winning numbers of this draw
        /// </summary>
        public IReadOnlyList<int> WinningNumbers { get; }
        /// <summary>
        /// The secondary numbers of this draw
        /// </summary>
        public IReadOnlyList<int> SecondaryNumbers { get; }
        /// <summary>
        /// The number of games played in this draw
        /// </summary>
        public int GamesPlayed { get; }

        public DrawResult(int[] winNums, int[] supNums, int played)
        {
            WinningNumbers = winNums;
            SecondaryNumbers = supNums;
            GamesPlayed = played;
        }
    }
}
