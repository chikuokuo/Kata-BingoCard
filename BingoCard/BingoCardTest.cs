using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BingoCard
{
    [TestClass]
    public class BingoCardTest
    {

        [TestMethod]
        public void Card_Length_Is_24()
        {
            var bingoCard = BingoCard.GetCard();
            Assert.AreEqual(24, bingoCard.Length);
        }

        [TestMethod]
        public void Card_Column_B_Have_5_Number()
        {
            var bingoCard = BingoCard.GetCard();

            Assert.AreEqual(5, bingoCard.Count(x => x.StartsWith("B")));
        }

        [TestMethod]
        public void Card_Column_I_Have_5_Number()
        {
            var bingoCard = BingoCard.GetCard();

            Assert.AreEqual(5, bingoCard.Count(x => x.StartsWith("I")));
        }

        [TestMethod]
        public void Card_Column_N_Have_4_Number()
        {
            var bingoCard = BingoCard.GetCard();

            Assert.AreEqual(4, bingoCard.Count(x => x.StartsWith("N")));
        }

        [TestMethod]
        public void Card_Column_G_Have_5_Number()
        {
            var bingoCard = BingoCard.GetCard();

            Assert.AreEqual(5, bingoCard.Count(x => x.StartsWith("G")));
        }

        [TestMethod]
        public void Card_Column_O_Have_5_Number()
        {
            var bingoCard = BingoCard.GetCard();

            Assert.AreEqual(5, bingoCard.Count(x => x.StartsWith("O")));
        }

        [TestMethod]
        public void Card_Column_Is_Word_With_Number()
        {
            var bingoCard = BingoCard.GetCard();

            Assert.AreEqual(typeof(string),bingoCard[0].Substring(0,1).GetType());
            Assert.AreEqual(true, int.TryParse(bingoCard[0].Substring(1), out _));
        }

        [TestMethod]
        public void Card_Column_Word_B_Number_Is_Between_1_15()
        {
            var numbers = BingoCard.GetCard().Where(x => x.StartsWith("B")).ToList();

            foreach (var number in numbers)
            {
                var n = Convert.ToInt32(number.Substring(1));
                Assert.AreEqual(true, n <= 15);
                Assert.AreEqual(true, n >= 0);
            }
        }

        [TestMethod]
        public void Card_Column_Word_I_Number_Is_Between_16_30()
        {
            var numbers = BingoCard.GetCard().Where(x => x.StartsWith("I")).ToList();

            foreach (var number in numbers)
            {
                var n = Convert.ToInt32(number.Substring(1));
                Assert.AreEqual(true, n <= 30);
                Assert.AreEqual(true, n >= 16);
            }
        }

        [TestMethod]
        public void Card_Column_Word_N_Number_Is_Between_31_45()
        {
            var numbers = BingoCard.GetCard().Where(x => x.StartsWith("N")).ToList();

            foreach (var number in numbers)
            {
                var n = Convert.ToInt32(number.Substring(1));
                Assert.AreEqual(true, n <= 45);
                Assert.AreEqual(true, n >= 31);
            }
        }

        [TestMethod]
        public void Card_Column_Word_G_Number_Is_Between_46_60()
        {
            var numbers = BingoCard.GetCard().Where(x => x.StartsWith("G")).ToList();

            foreach (var number in numbers)
            {
                var n = Convert.ToInt32(number.Substring(1));
                Assert.AreEqual(true, n <= 60);
                Assert.AreEqual(true, n >= 46);
            }
        }

        [TestMethod]
        public void Card_Column_Word_O_Number_Is_Between_61_75()
        {
            var numbers = BingoCard.GetCard().Where(x => x.StartsWith("O")).ToList();

            foreach (var number in numbers)
            {
                var n = Convert.ToInt32(number.Substring(1));
                Assert.AreEqual(true, n <= 75);
                Assert.AreEqual(true, n >= 61);
            }
        }

        [TestMethod]
        public void Card_Column_IsUnique()
        {
            var card = BingoCard.GetCard();
            Assert.AreEqual(card.Length, card.ToList().Distinct().Count());
        }
    }

    public class BingoCard
    {
        private static readonly List<BingoColumnData> BingoColumnData = new List<BingoColumnData>()
        {
            new BingoColumnData{ ColumnWord = "B", CellCount = 5, StartNumber = 1, EndNumber = 15},
            new BingoColumnData{ ColumnWord = "I", CellCount = 5, StartNumber = 16, EndNumber = 30},
            new BingoColumnData{ ColumnWord = "N", CellCount = 4, StartNumber = 31, EndNumber = 45},
            new BingoColumnData{ ColumnWord = "G", CellCount = 5, StartNumber = 46, EndNumber = 60},
            new BingoColumnData{ ColumnWord = "O", CellCount = 5, StartNumber = 61, EndNumber = 75}
        };
        public static string[] GetCard()
        {
            return new BingoData(BingoColumnData).GenerateCardData().ToArray();
        }
    }

    public class BingoData
    {
        private List<BingoColumnData> bingoDataInfo;

        public BingoData(List<BingoColumnData> bingodata)
        {
            bingoDataInfo = bingodata;
        }

        public IEnumerable<string> GenerateCardData()
        {
            return bingoDataInfo.SelectMany(x=>x.GenerateColumnData());
        }
    }

    public class BingoColumnData
    {
        public string ColumnWord { get; set; }

        public int CellCount { get; set; }

        public int StartNumber { get; set; }

        public int EndNumber { get; set; }

        public IEnumerable<string> GenerateColumnData()
        {
            var randomNumber = GenerateRandomNumber();
            for (var i = 0; i < CellCount; i++)
            {
                yield return ColumnWord + randomNumber[i];
            }
        }
        public List<int> GenerateRandomNumber()
        {
            var numberCount = EndNumber - StartNumber + 1;
            var randomMethod = new Random(Guid.NewGuid().GetHashCode());
            return Enumerable.Range(StartNumber, numberCount).OrderBy(x => randomMethod.Next()).ToList();
        }
    }
}
