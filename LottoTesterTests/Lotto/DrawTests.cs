using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using LottoTester.Lotto;
using System.Linq;

namespace LottoTesterTests.Lotto
{
    [TestClass]
    public class DrawTests
    {
        [TestMethod]
        public void UnDrawn_Draw_Valid() {
            // Arrange
            decimal jackpot = 1e6M;
            DateTime drawDate = new DateTime(2000, 01, 01);
            Draw draw = new Draw(jackpot, drawDate);

            // Act


            // Assert
            Assert.IsNull(draw.Result, "Undrawn draw should not have a result object");
            Assert.AreEqual(jackpot, draw.Jackpot, "Incorrect jackpot amount");
            Assert.AreEqual(drawDate, draw.Date, "Incorrect draw date");
        }

        [TestMethod]
        public void Draw_DoDraw_Valid()
        {
            // Arrange
            decimal jackpot = 1e6M;
            DateTime drawDate = new DateTime(2000, 01, 01);
            Draw draw = new Draw(jackpot, drawDate);

            // Act
            draw.DoDraw();

            // Assert
            Assert.IsNotNull(draw.Result, "Draw result cannot be null after a draw");
            Assert.AreEqual(6, draw.Result.WinningNumbers.Count, "There need to be 6 winning numbers");
            Assert.AreEqual(2, draw.Result.SecondaryNumbers.Count, "There need to be 2 supplementary numbers");
            Assert.AreEqual(draw.Result.WinningNumbers.Count, draw.Result.WinningNumbers.Distinct().Count(),
                "Winning numbers should be unique");
            Assert.AreEqual(draw.Result.SecondaryNumbers.Count, draw.Result.SecondaryNumbers.Distinct().Count(),
                "Supplementary numbers should be unique");
            var allNumbers = draw.Result.SecondaryNumbers.Union(draw.Result.WinningNumbers);
            Assert.IsTrue(allNumbers.Max() < 46 && allNumbers.Min() > 0, "Numbers need to be between 1 and 45 inclusive");
        }
    }
}
