using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// In the game, Monopoly, the standard board is set up in the following way:
    ///
    /// GO	A1	CC1	A2	T1	R1	B1	CH1	B2	B3	JAIL
    /// H2	                                 	C1
    /// T2	                                 	U1
    /// H1	 	                                C2
    /// CH3	 	                                C3
    /// R4	 	                                R2
    /// G3	 	                                D1
    /// CC3	 	                                CC2
    /// G2	 	                                D2
    /// G1	 	                                D3
    /// G2J	F3	U2	F2	F1	R3	E3	E2	CH2	E1	FP
    /// 
    /// A player starts on the GO square and adds the scores on two 6-sided dice to determine the number of squares they advance 
    /// in a clockwise direction. Without any further rules we would expect to visit each square with equal probability: 2.5%. 
    /// However, landing on G2J (Go To Jail), CC (community chest), and CH (chance) changes this distribution.
    ///
    /// In addition to G2J, and one card from each of CC and CH, that orders the player to go directly to jail, 
    /// if a player rolls three consecutive doubles, they do not advance the result of their 3rd roll. Instead they proceed directly to jail.
    ///
    /// At the beginning of the game, the CC and CH cards are shuffled. 
    /// When a player lands on CC or CH they take a card from the top of the respective pile and, after following the instructions, 
    /// it is returned to the bottom of the pile. There are sixteen cards in each pile, but for the purpose of this problem we are only concerned 
    /// with cards that order a movement; any instruction not concerned with movement will be ignored and the player will remain 
    /// on the CC/CH square.
    ///
    /// Community Chest (2/16 cards):
    ///  Advance to GO
    ///  Go to JAIL
    /// Chance (10/16 cards):
    ///  Advance to GO
    ///  Go to JAIL
    ///  Go to C1
    ///  Go to E3
    ///  Go to H2
    ///  Go to R1
    ///  Go to next R (railway company)
    ///  Go to next R
    ///  Go to next U (utility company)
    ///  Go back 3 squares.
    /// 
    /// The heart of this problem concerns the likelihood of visiting a particular square. 
    /// That is, the probability of finishing at that square after a roll. 
    /// For this reason it should be clear that, with the exception of G2J for which the probability of finishing on it is zero, 
    /// the CH squares will have the lowest probabilities, as 5/8 request a movement to another square, 
    /// and it is the final square that the player finishes at on each roll that we are interested in. 
    /// We shall make no distinction between "Just Visiting" and being sent to JAIL, 
    /// and we shall also ignore the rule about requiring a double to "get out of jail", assuming that they pay to get out on their next turn.
    ///
    /// By starting at GO and numbering the squares sequentially from 00 to 39 we can concatenate these two-digit numbers to produce strings 
    /// that correspond with sets of squares.
    ///
    /// Statistically it can be shown that the three most popular squares, in order, are 
    /// JAIL (6.24%) = Square 10, E3 (3.18%) = Square 24, and GO (3.09%) = Square 00. 
    /// So these three most popular squares can be listed with the six-digit modal string: 102400.
    ///
    /// If, instead of using two 6-sided dice, two 4-sided dice are used, find the six-digit modal string.
    /// </summary>
    [TestFixture]
    public class Problem_0084_MonopolyOdds
    {
        private const string GoToJailSquareName = "GoToJail";
        private const string JailSquareName = "JAIL";
        private const string GoSquareName = "GO";
        private const string c1SquareName = "C1";
        private const string e3SquareName = "E3";
        private const string h2SquareName = "H2";
        private const string r1SquareName = "R1";

        private const int NumberOfSquares = 40;

        private const int goSquareIndex = 0;
        private int goToJailSquareIndex;
        private int jailSquareIndex;
        private int c1SquareIndex;
        private int e3SquareIndex;
        private int h2SquareIndex;
        private int r1SquareIndex;

        private List<MonopolySquare> squares;
        private List<MonopolyType> communityChest;
        private List<ChanceCard> chance;

        private readonly List<bool> doubles = new List<bool> { false, false, false };

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            SetUpSquares();
            SetUpCommunityChest();
            SetUpChance();
        }

        [Test]
        public void MetadataSetUp()
        {
            Assert.AreEqual(1, 1, "Confirm all set up correct (checks in set up methods)");
        }

        [Test]
        [TestCase(1, 1, 2, 2, 3, 3, true)]
        [TestCase(1, 1, 2, 2, 3, 4, false)]
        [TestCase(1, 1, 2, 3, 4, 4, false)]
        [TestCase(1, 2, 3, 4, 5, 6, false)]
        public void ShouldGoToJailFromRollingDoubles(int r1First, int r1Second, int r2First, int r2Second, int r3First, int r3Second, bool expectedResult)
        {
            GoToJailFromDoubles(r1First, r1Second);
            GoToJailFromDoubles(r2First, r2Second);
            var result = GoToJailFromDoubles(r3First, r3Second);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ForChanceCardDoNothingTheSameIndexIsReturned()
        {
            for (var startIndex = 0; startIndex < NumberOfSquares; ++startIndex)
            {
                var nextIndex = ApplyChanceCard(startIndex, new ChanceCard(ChanceType.DoNothing, string.Empty));
                Assert.AreEqual(startIndex, nextIndex);
            }
        }

        [Test]
        [TestCase(0, 37)]
        [TestCase(1, 38)]
        [TestCase(2, 39)]
        [TestCase(3, 0)]
        [TestCase(4, 1)]
        [TestCase(10, 7)]
        public void ForChanceCardGoBackThreeThisCanHandleGoingBackRoundTheBoard(int startIndex, int expectedIndex)
        {
            var result = ApplyChanceCard(startIndex, new ChanceCard(ChanceType.GoBackThree, string.Empty));
            Assert.AreEqual(expectedIndex, result);
        }

        [Test]
        [TestCase("GO", 0)]
        [TestCase("JAIL", 10)]
        [TestCase("C1", 11)]
        [TestCase("E3", 24)]
        [TestCase("H2", 39)]
        [TestCase("R1", 5)]
        public void ForChanceCardGoToSpecificSquareTheIndexIsCorrect(string squareName, int expectedIndex)
        {
            var resultantIndex = ApplyChanceCard(39, new ChanceCard(ChanceType.GoToSpecificSquare, squareName));
            Assert.AreEqual(expectedIndex, resultantIndex, squares[resultantIndex].Name);
        }

        [Test]
        [TestCase(0, 5, 12)]
        [TestCase(1, 5, 12)]
        [TestCase(2, 5, 12)]
        [TestCase(3, 5, 12)]
        [TestCase(4, 5, 12)]
        [TestCase(5, 15, 12)]
        [TestCase(6, 15, 12)]
        [TestCase(7, 15, 12)]
        [TestCase(8, 15, 12)]
        [TestCase(9, 15, 12)]
        [TestCase(10, 15, 12)]
        [TestCase(11, 15, 12)]
        [TestCase(12, 15, 28)]
        [TestCase(13, 15, 28)]
        [TestCase(14, 15, 28)]
        [TestCase(15, 25, 28)]
        [TestCase(16, 25, 28)]
        [TestCase(17, 25, 28)]
        [TestCase(18, 25, 28)]
        [TestCase(19, 25, 28)]
        [TestCase(20, 25, 28)]
        [TestCase(21, 25, 28)]
        [TestCase(22, 25, 28)]
        [TestCase(23, 25, 28)]
        [TestCase(24, 25, 28)]
        [TestCase(25, 35, 28)]
        [TestCase(26, 35, 28)]
        [TestCase(27, 35, 28)]
        [TestCase(28, 35, 12)]
        [TestCase(29, 35, 12)]
        [TestCase(30, 35, 12)]
        [TestCase(31, 35, 12)]
        [TestCase(32, 35, 12)]
        [TestCase(33, 35, 12)]
        [TestCase(34, 35, 12)]
        [TestCase(35, 5, 12)]
        [TestCase(36, 5, 12)]
        [TestCase(37, 5, 12)]
        [TestCase(38, 5, 12)]
        [TestCase(39, 5, 12)]
        public void FindTheNextSquareOfAGivenType(int startIndex, int railwayIndex, int utilityIndex)
        {
            var nextRailway = NextSquareOfType(MonopolyType.Railway, startIndex);
            var nextUtility = NextSquareOfType(MonopolyType.Utility, startIndex);

            Assert.AreEqual(railwayIndex, nextRailway, "Railway");
            Assert.AreEqual(utilityIndex, nextUtility, "Utility");
        }

        [Test]
        public void ApplyCommunityChestCardToCurrentSquare()
        {
            const int ExpectedJail = 10;
            const int ExpectedGo = 0;

            for (var index = 0; index < NumberOfSquares; ++index)
            {
                var goToJail = ApplyCommunityChest(index, MonopolyType.GoToJail);
                var goToGo = ApplyCommunityChest(index, MonopolyType.Go);
                var noAction = ApplyCommunityChest(index, MonopolyType.Standard);

                Assert.AreEqual(ExpectedJail, goToJail, "Starting from {0} to jail", index);
                Assert.AreEqual(ExpectedGo, goToGo, "Starting from {0} to Go", index);
                Assert.AreEqual(index, noAction, "Standard type doesn't change square");
            }
        }

        [Test, Explicit]
        public void ConfirmStandardDiceExample()
        {
            var modalString = PlayMonopoly(6);
            Assert.AreEqual("102400", modalString);
        }

        /// <summary>
        /// 101524
        /// </summary>
        [Test, Explicit]
        public void UseFourSidedDice()
        {
            var modalString = PlayMonopoly(4);
            Console.WriteLine(modalString);

            modalString.Should().Be("101524"); 
        }

        private string PlayMonopoly(int dieUpperLimit)
        {
            var squarePopularity = new List<int>();
            for (var idx = 0; idx < NumberOfSquares; ++idx)
            {
                squarePopularity.Add(0);
            }

            var dieHelper = new DieHelper(1, dieUpperLimit);

            var currentSquareIndex = 0;
            for (var turn = 0; turn < 100000000; ++turn)
            {
                var firstRoll = dieHelper.Roll();
                var secondRoll = dieHelper.Roll();

                var goToJailFromDoubles = GoToJailFromDoubles(firstRoll, secondRoll);
                if (goToJailFromDoubles)
                {
                    currentSquareIndex = jailSquareIndex;
                    squarePopularity[currentSquareIndex]++;
                    continue;
                }

                currentSquareIndex += (firstRoll + secondRoll);
                currentSquareIndex %= NumberOfSquares;

                if (currentSquareIndex == goToJailSquareIndex)
                {
                    currentSquareIndex = jailSquareIndex;
                    squarePopularity[currentSquareIndex]++;
                    continue;
                }

                var currentSquareType = squares[currentSquareIndex].SquareType;
                switch (currentSquareType)
                {
                    case MonopolyType.Chance:
                        var chanceCard = UseChance();
                        currentSquareIndex = ApplyChanceCard(currentSquareIndex, chanceCard);
                        break;

                    case MonopolyType.CommunityChest:
                        var communityChestCard = UseCommunityChest();
                        currentSquareIndex = ApplyCommunityChest(currentSquareIndex, communityChestCard);
                        break;
                }

                squarePopularity[currentSquareIndex]++;
            }

            return GetModalString(squarePopularity);
        }

        private MonopolyType UseCommunityChest()
        {
            var communityChestCard = communityChest[0];
            communityChest.RemoveAt(0);
            communityChest.Add(communityChestCard);

            return communityChestCard;
        }

        private int ApplyCommunityChest(int currentStartIndex, MonopolyType monopolyType)
        {
            switch (monopolyType)
            {
                case MonopolyType.Standard:
                    return currentStartIndex;
                case MonopolyType.GoToJail:
                    return jailSquareIndex;
                case MonopolyType.Go:
                    return goSquareIndex;
                default:
                    throw new ApplicationException(string.Format("Unexpected community chest: {0}", monopolyType));
            }
        }

        private ChanceCard UseChance()
        {
            var chanceCard = chance[0];
            chance.RemoveAt(0);
            chance.Add(chanceCard);

            return chanceCard;
        }

        private int ApplyChanceCard(int currentSquareIndex, ChanceCard chanceCard)
        {
            switch (chanceCard.ChanceType)
            {
                case ChanceType.DoNothing:
                    return currentSquareIndex;

                case ChanceType.GoBackThree:
                    currentSquareIndex -= 3;
                    if (currentSquareIndex < 0)
                        currentSquareIndex += NumberOfSquares;
                    return currentSquareIndex;

                case ChanceType.GoToNextSquareOfType:
                    switch (chanceCard.Square)
                    {
                        case "R":
                            return NextSquareOfType(MonopolyType.Railway, currentSquareIndex);
                        case "U":
                            return NextSquareOfType(MonopolyType.Utility, currentSquareIndex);
                        default:
                            throw new ApplicationException(string.Format("Looking for next square of type {0}", chanceCard.Square));
                    }

                case ChanceType.GoToSpecificSquare:
                    switch (chanceCard.Square)
                    {
                        case GoSquareName:
                            return goSquareIndex;
                        case JailSquareName:
                            return jailSquareIndex;
                        case c1SquareName:
                            return c1SquareIndex;
                        case e3SquareName:
                            return e3SquareIndex;
                        case h2SquareName:
                            return h2SquareIndex;
                        case r1SquareName:
                            return r1SquareIndex;
                        default:
                            throw new ApplicationException(string.Format("Chance to go to {0}", chanceCard.Square));
                    }
                default:
                    throw new ApplicationException(string.Format("Unexpected chance card type: {0}", chanceCard.ChanceType));
            }
        }

        private int NextSquareOfType(MonopolyType monopolyType, int currentSquareIndex)
        {
            var nextSquareOfType = squares.Skip(currentSquareIndex + 1)
                                    .FirstOrDefault(sq => sq.SquareType == monopolyType) ??
                                        squares.FirstOrDefault(sq => sq.SquareType == monopolyType);
            return squares.FindIndex(sq => sq == nextSquareOfType);
        }

        private bool GoToJailFromDoubles(int firstRoll, int secondRoll)
        {
            doubles.RemoveAt(0);
            doubles.Add(firstRoll == secondRoll);

            return doubles.All(d => d);
        }

        private string GetModalString(IReadOnlyList<int> squarePopularity)
        {
            for (var idx = 0; idx < NumberOfSquares; ++idx)
            {
                Console.WriteLine("{0:00}: {1:00000000} ({2})", idx, squarePopularity[idx], squares[idx].Name);
            }

            var totalVisits = squarePopularity.Sum(sp => sp);
            var squaresWithIndex = squarePopularity.Select((item, index) => new { Popularity = (double)item / (double)totalVisits, Position = index });
            var mostPopularSquares = squaresWithIndex.OrderByDescending(sp => sp.Popularity).Take(3).ToList();

            return string.Format("{0:00}{1:00}{2:00}", mostPopularSquares[0].Position, mostPopularSquares[1].Position,
                mostPopularSquares[2].Position);
        }

        private void SetUpSquares()
        {
            squares = new List<MonopolySquare>
            {
                new MonopolySquare(GoSquareName, MonopolyType.Go),
                new MonopolySquare("A1", MonopolyType.Standard),
                new MonopolySquare("CC1", MonopolyType.CommunityChest),
                new MonopolySquare("A2", MonopolyType.Standard),
                new MonopolySquare("T1", MonopolyType.Standard),
                new MonopolySquare(r1SquareName, MonopolyType.Railway),
                new MonopolySquare("B1", MonopolyType.Standard),
                new MonopolySquare("CH1", MonopolyType.Chance),
                new MonopolySquare("B2", MonopolyType.Standard),
                new MonopolySquare("B3", MonopolyType.Standard),
                new MonopolySquare(JailSquareName, MonopolyType.Jail),
                new MonopolySquare(c1SquareName, MonopolyType.Standard),
                new MonopolySquare("U1", MonopolyType.Utility),
                new MonopolySquare("C2", MonopolyType.Standard),
                new MonopolySquare("C3", MonopolyType.Standard),
                new MonopolySquare("R2", MonopolyType.Railway),
                new MonopolySquare("D1", MonopolyType.Standard),
                new MonopolySquare("CC2", MonopolyType.CommunityChest),
                new MonopolySquare("D2", MonopolyType.Standard),
                new MonopolySquare("D3", MonopolyType.Standard),
                new MonopolySquare("FP", MonopolyType.Standard),
                new MonopolySquare("E1", MonopolyType.Standard),
                new MonopolySquare("CH2", MonopolyType.Chance),
                new MonopolySquare("E2", MonopolyType.Standard),
                new MonopolySquare(e3SquareName, MonopolyType.Standard),
                new MonopolySquare("R3", MonopolyType.Railway),
                new MonopolySquare("F1", MonopolyType.Standard),
                new MonopolySquare("F2", MonopolyType.Standard),
                new MonopolySquare("U2", MonopolyType.Utility),
                new MonopolySquare("F3", MonopolyType.Standard),
                new MonopolySquare(GoToJailSquareName, MonopolyType.GoToJail),
                new MonopolySquare("G1", MonopolyType.Standard),
                new MonopolySquare("G2", MonopolyType.Standard),
                new MonopolySquare("CC3", MonopolyType.CommunityChest),
                new MonopolySquare("G3", MonopolyType.Standard),
                new MonopolySquare("R4", MonopolyType.Railway),
                new MonopolySquare("CH3", MonopolyType.Chance),
                new MonopolySquare("H1", MonopolyType.Standard),
                new MonopolySquare("T2", MonopolyType.Standard),
                new MonopolySquare(h2SquareName, MonopolyType.Standard),
            };

            jailSquareIndex = squares.FindIndex(sq => sq.SquareType == MonopolyType.Jail);
            Assert.AreEqual(10, jailSquareIndex, "Index of JAIL");
            c1SquareIndex = squares.FindIndex(sq => sq.Name == c1SquareName);
            e3SquareIndex = squares.FindIndex(sq => sq.Name == e3SquareName);
            h2SquareIndex = squares.FindIndex(sq => sq.Name == h2SquareName);
            r1SquareIndex = squares.FindIndex(sq => sq.Name == r1SquareName);
            goToJailSquareIndex = squares.FindIndex(sq => sq.Name == GoToJailSquareName);
            Assert.AreNotEqual(0, c1SquareIndex, "C1");
            Assert.AreNotEqual(0, e3SquareIndex, "E3");
            Assert.AreNotEqual(0, h2SquareIndex, "H2");
            Assert.AreNotEqual(0, r1SquareIndex, "R1");
            Assert.AreNotEqual(0, goToJailSquareIndex, "Go to jail");

            Assert.AreEqual(40, squares.Count, "Forty squares defined");
            Assert.AreEqual(1, squares.Count(sq => sq.SquareType == MonopolyType.Go), "One GO");
            Assert.AreEqual(1, squares.Count(sq => sq.SquareType == MonopolyType.Jail), "One Jail");
            Assert.AreEqual(1, squares.Count(sq => sq.SquareType == MonopolyType.GoToJail), "One go to jail");
            Assert.AreEqual(3, squares.Count(sq => sq.SquareType == MonopolyType.Chance), "Three chance");
            Assert.AreEqual(3, squares.Count(sq => sq.SquareType == MonopolyType.CommunityChest), "Three CC");
            Assert.AreEqual(4, squares.Count(sq => sq.SquareType == MonopolyType.Railway), "Four railway");
            Assert.AreEqual(2, squares.Count(sq => sq.SquareType == MonopolyType.Utility), "Two utility");
        }

        private void SetUpCommunityChest()
        {
            // 2/16 involve forcing a move
            // Advance to GO
            // Go to JAIL
            communityChest = new List<MonopolyType> { MonopolyType.GoToJail, MonopolyType.Go };

            var rdm = new Random(DateTime.Now.Millisecond);
            for (var cards = 0; cards < 14; ++cards)
            {
                var idx = rdm.Next(0, communityChest.Count);
                if (idx == communityChest.Count)
                    communityChest.Add(MonopolyType.Standard);
                else
                {
                    communityChest.Insert(idx, MonopolyType.Standard);
                }
            }

            Console.WriteLine("Community Chest:");
            Assert.AreEqual(16, communityChest.Count, "Created 16 cards");

            foreach (var monopolyType in communityChest)
            {
                Console.WriteLine("{0} ", monopolyType);
            }
            // Now shuffle so that the go to jail/go order is not fixed (may wrap)
            for (var shuffle = 0; shuffle < rdm.Next(100); ++shuffle)
            {
                var firstCard = communityChest[0];
                communityChest.RemoveAt(0);
                communityChest.Add(firstCard);
            }

            Console.WriteLine("Shuffled community chest:");
            Assert.AreEqual(16, communityChest.Count, "Still have 16 cards");

            foreach (var monopolyType in communityChest)
            {
                Console.WriteLine("{0} ", monopolyType);
            }
        }

        public void SetUpChance()
        {
            // Chance (10/16 cards):
            //  Advance to GO
            //  Go to JAIL
            //  Go to C1
            //  Go to E3
            //  Go to H2
            //  Go to R1
            //  Go to next R (railway company)
            //  Go to next R
            //  Go to next U (utility company)
            //  Go back 3 squares.

            var source = new List<ChanceCard>
            {
                new ChanceCard(ChanceType.GoToSpecificSquare, GoSquareName),
                new ChanceCard(ChanceType.GoToSpecificSquare, JailSquareName),
                new ChanceCard(ChanceType.GoToSpecificSquare, "C1"),
                new ChanceCard(ChanceType.GoToSpecificSquare, "E3"),
                new ChanceCard(ChanceType.GoToSpecificSquare, "H2"),
                new ChanceCard(ChanceType.GoToSpecificSquare, "R1"),
                new ChanceCard(ChanceType.GoToNextSquareOfType, "R"),
                new ChanceCard(ChanceType.GoToNextSquareOfType, "R"),
                new ChanceCard(ChanceType.GoToNextSquareOfType, "U"),
                new ChanceCard(ChanceType.GoBackThree, string.Empty)
            };

            chance = new List<ChanceCard>();
            var rdm = new Random(DateTime.Now.Millisecond);

            while (source.Count > 0)
            {
                var idx = rdm.Next(0, source.Count);
                var sourceCard = source[idx];
                source.Remove(sourceCard);
                chance.Add(sourceCard);
            }

            // Now add 6 no action cards
            for (var i = 0; i < 6; ++i)
            {
                var idx = rdm.Next(0, chance.Count);
                if (idx == chance.Count) chance.Add(new ChanceCard(ChanceType.DoNothing, string.Empty));
                else
                {
                    chance.Insert(idx, new ChanceCard(ChanceType.DoNothing, string.Empty));
                }
            }

            Assert.AreEqual(16, chance.Count, "16 chance cards");
            Console.WriteLine("Chance: ");
            foreach (var chanceCard in chance)
            {
                Console.WriteLine("{0} {1}", chanceCard.ChanceType, chanceCard.Square);
            }
        }
    }

    public class MonopolySquare
    {
        public string Name { get; private set; }
        public MonopolyType SquareType { get; private set; }

        public MonopolySquare(string name, MonopolyType squareType)
        {
            Name = name;
            SquareType = squareType;
        }
    }

    public class ChanceCard
    {
        public ChanceType ChanceType { get; private set; }
        public string Square { get; private set; }

        public ChanceCard(ChanceType chanceType, string square)
        {
            ChanceType = chanceType;
            Square = square;
        }
    }

    public enum MonopolyType
    {
        Go,
        CommunityChest,
        Chance,
        Jail,
        GoToJail,
        Railway,
        Utility,
        Standard
    }

    public enum ChanceType
    {
        GoToSpecificSquare,
        GoToNextSquareOfType,
        GoBackThree,
        DoNothing
    }
}
