using System;
using System.Collections.Generic;
using System.Text;

namespace LottoTester.Lotto
{
    interface IStrategy
    {
        /// <summary>
        /// Determines whether this strategy decides to play this draw
        /// </summary>
        /// <returns>A boolean value representing whether this strategy will play this draw</returns>
        bool WillPlay(Draw draw);
        /// <summary>
        /// Gets the numbers that will be played in this game
        /// </summary>
        /// <returns></returns>
        int[][] GetNumbers();
    }
}
