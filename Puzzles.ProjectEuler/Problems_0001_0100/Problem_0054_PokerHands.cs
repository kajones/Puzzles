using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// In the card game poker, a hand consists of five cards and are ranked, from lowest to highest, in the following way:
    ///
    /// High Card: Highest value card.
    /// One Pair: Two cards of the same value.
    /// Two Pairs: Two different pairs.
    /// Three of a Kind: Three cards of the same value.
    /// Straight: All cards are consecutive values.
    /// Flush: All cards of the same suit.
    /// Full House: Three of a kind and a pair.
    /// Four of a Kind: Four cards of the same value.
    /// Straight Flush: All cards are consecutive values of same suit.
    /// Royal Flush: Ten, Jack, Queen, King, Ace, in same suit.
    /// 
    /// The cards are valued in the order:
    /// 2, 3, 4, 5, 6, 7, 8, 9, 10, Jack, Queen, King, Ace.
    ///
    /// If two players have the same ranked hands then the rank made up of the highest value wins; 
    /// for example, a pair of eights beats a pair of fives (see example 1 below). 
    /// But if two ranks tie, for example, both players have a pair of queens, 
    /// then highest cards in each hand are compared (see example 4 below); 
    /// if the highest cards tie then the next highest cards are compared, and so on.
    ///
    /// Consider the following five hands dealt to two players:
    ///
    /// Hand	 	Player 1	 	Player 2	 	Winner
    /// 1	 	5H 5C 6S 7S KD   2C 3S 8S 8D TD     Player 2
    ///          Pair of Fives   Pair of Eights
    /// 
    /// 2	 	5D 8C 9S JS AC   2C 5C 7D 8S QH     Player 1
    ///        Highest card Ace  Highest card Queen 
    /// 
    /// 3	 	2D 9C AS AH AC   3D 6D 7D TD QD     Player 2
    ///            Three Aces   Flush with Diamonds
    /// 
    /// 4	 	4D 6S 9H QH QC    3D 6D 7H QD QS    Player 1
    ///         Pair of Queens    Pair of Queens
    ///       Highest card Nine   Highest card Seven
    /// 
    /// 5	 	2H 2D 4C 4D 4S    3C 3D 3S 9S 9D    Player 1
    ///            Full House      Full House
    ///         With Three Fours  with Three Threes
    /// 
    /// The file, poker.txt, contains one-thousand random hands dealt to two players. 
    /// Each line of the file contains ten cards (separated by a single space): 
    /// the first five are Player 1's cards and the last five are Player 2's cards. 
    /// You can assume that all hands are valid (no invalid characters or repeated cards), 
    /// each player's hand is in no specific order, and in each hand there is a clear winner.
    ///
    /// How many hands does Player 1 win?
    [TestFixture]
    public class Problem_0054_PokerHands
    {
        [Test]
        [TestCase("8C", 8, "C")]
        [TestCase("TS", 10, "S")]
        [TestCase("KC", 13, "C")]
        [TestCase("9H", 9, "H")]
        [TestCase("4S", 4, "S")]
        [TestCase("7D", 7, "D")]
        [TestCase("2S", 2, "S")]
        [TestCase("5D", 5, "D")]
        [TestCase("3S", 3, "S")]
        [TestCase("AC", 14, "C")]
        [TestCase("6H", 6, "H")]
        [TestCase("JD", 11, "D")]
        [TestCase("QH", 12, "H")]
        public void TranslateCard(string cardText, int value, string suit)
        {
            var card = new Card(cardText);
            Assert.AreEqual(value, card.Value, "Value");
            Assert.AreEqual(suit, card.Suit, "Suit");
        }

        /// <summary>
        /// Highest value card.
        /// </summary>
        [Test]
        public void TranslateHighCardHand()
        {
            var hand = new Hand("2H 4D 7C 8S 9S");
            Assert.AreEqual(HandType.HighCard, hand.HandType, "Highest card");
            Assert.AreEqual(9, hand.UsingCardValue, "Highest card is a 9");
        }

        /// <summary>
        /// One Pair: Two cards of the same value.
        /// </summary>
        [Test]
        public void TranslateOnePairHand()
        {
            var hand = new Hand("5H 5C 6S 7S KD");
            Assert.AreEqual(HandType.OnePair, hand.HandType, "Pair");
            Assert.AreEqual(5, hand.UsingCardValue, "Pair of fives");
        }

        /// <summary>
        /// Two Pairs: Two different pairs.
        /// </summary>
        [Test]
        public void TranslateTwoPairsHand()
        {
            var hand = new Hand("2C 2D 3H 3D 4S");
            Assert.AreEqual(HandType.TwoPairs, hand.HandType, "Two Pairs");
            Assert.AreEqual(3, hand.UsingCardValue, "Higher pair 3");
        }

        /// <summary>
        /// Three of a Kind: Three cards of the same value.
        /// </summary>
        [Test]
        public void TranslateThreeOfAKindHand()
        {
            var hand = new Hand("4D 4H 4S 5H 2S");
            Assert.AreEqual(HandType.ThreeOfAKind, hand.HandType, "Three of a kind");
            Assert.AreEqual(4, hand.UsingCardValue, "Using 4");
        }

        /// <summary>
        /// Straight: All cards are consecutive values.
        /// </summary>
        [Test]
        public void TranslateStraightHand()
        {
            var hand = new Hand("2C 3H 4S 5D 6C");
            Assert.AreEqual(HandType.Straight, hand.HandType, "Straight");
            Assert.AreEqual(6, hand.UsingCardValue, "Highest card 6");
        }

        /// <summary>
        /// Flush: All cards of the same suit.
        /// </summary>
        [Test]
        public void TranslateFlushHand()
        {
            var hand = new Hand("2C 6C 7C TC QC");
            Assert.AreEqual(HandType.Flush, hand.HandType, "Flush");
            Assert.AreEqual(12, hand.UsingCardValue, "Highest card");
        }

        /// Full House: Three of a kind and a pair.
        /// 
        [Test]
        public void TranslateFullHouseHand()
        {
            var hand = new Hand("2C 2D 7D 7H 7S");
            Assert.AreEqual(HandType.FullHouse, hand.HandType, "Full House");
            Assert.AreEqual(7, hand.UsingCardValue, "Highest card");
        }

        /// <summary>
        /// Four of a Kind: Four cards of the same value.
        /// </summary>
        [Test]
        public void TranslateFourOfAKindHand()
        {
            var hand = new Hand("7D 7H 7S 7C 9S");
            Assert.AreEqual(HandType.FourOfAKind, hand.HandType, "Four of a kind");
            Assert.AreEqual(7, hand.UsingCardValue, "7");
        }

        /// <summary>
        /// Straight Flush: All cards are consecutive values of same suit.
        /// </summary>
        [Test]
        public void TranslateStraightFlushHand()
        {
            var hand = new Hand("8D 9D TD JD QD");
            Assert.AreEqual(HandType.StraightFlush, hand.HandType, "Straight flush");
            Assert.AreEqual(12, hand.UsingCardValue, "Q aka 12 highest value");
        }

        /// <summary>
        /// Royal Flush: Ten, Jack, Queen, King, Ace, in same suit.
        /// </summary>
        [Test]
        public void TranslateRoyalFlushHand()
        {
            var hand = new Hand("TH JH QH KH AH");
            Assert.AreEqual(HandType.RoyalFlush, hand.HandType, "Royal Flush");
            Assert.AreEqual(14, hand.UsingCardValue, "Highest card");
        }

        /// 1	 	5H 5C 6S 7S KD   2C 3S 8S 8D TD     Player 2
        ///          Pair of Fives   Pair of Eights
        /// 
        /// 2	 	5D 8C 9S JS AC   2C 5C 7D 8S QH     Player 1
        ///        Highest card Ace  Highest card Queen 
        /// 
        /// 3	 	2D 9C AS AH AC   3D 6D 7D TD QD     Player 2
        ///            Three Aces   Flush with Diamonds
        /// 
        /// 4	 	4D 6S 9H QH QC    3D 6D 7H QD QS    Player 1
        ///         Pair of Queens    Pair of Queens
        ///       Highest card Nine   Highest card Seven
        /// 
        /// 5	 	2H 2D 4C 4D 4S    3C 3D 3S 9S 9D    Player 1
        ///            Full House      Full House
        ///         With Three Fours  with Three Threes
        [Test]
        [TestCase("5H 5C 6S 7S KD", "2C 3S 8S 8D TD", 2)]
        [TestCase("5D 8C 9S JS AC", "2C 5C 7D 8S QH", 1)]
        [TestCase("2D 9C AS AH AC", "3D 6D 7D TD QD", 2)]
        [TestCase("4D 6S 9H QH QC", "3D 6D 7H QD QS", 1)]
        [TestCase("2H 2D 4C 4D 4S", "3C 3D 3S 9S 9D", 1)]
        [TestCase("QD QH AD KD 2D", "QS QC AC JC TC", 1)]
        [TestCase("QD QH AD KD JD", "QS QC AC KC TC", 1)]
        public void ConfirmHandWinners(string player1HandText, string player2HandText, int expectedWinner)
        {
            var player1Hand = new Hand(player1HandText);
            var player2Hand = new Hand(player2HandText);

            var winner = FindWinner(player1Hand, player2Hand);
            Assert.AreEqual(expectedWinner, winner);
        }

        private int FindWinner(Hand player1Hand, Hand player2Hand)
        {
            if (player1Hand.HandType > player2Hand.HandType)
                return 1;
            if (player2Hand.HandType > player1Hand.HandType)
                return 2;

            if (player1Hand.UsingCardValue > player2Hand.UsingCardValue)
                return 1;
            if (player2Hand.UsingCardValue > player1Hand.UsingCardValue)
                return 2;

            // e.g. if two pairs of the same card value, then need the next highest card, then the next etc
            for (var idx = 4; idx >= 0; --idx)
            {
                if (player1Hand.CardValueList[idx] > player2Hand.CardValueList[idx])
                    return 1;
                if (player2Hand.CardValueList[idx] > player1Hand.CardValueList[idx])
                    return 2;
            }

            return 1;
        }

        /// <summary>
        /// 376
        /// </summary>
        [Test, Explicit]
        public void FindNumberOfTimesPlayer1Wins()
        {
            //const string path = @"C:\Development\ProjectEuler\ProjectEuler\Problems075\Problem054_poker.txt";
            const string path = "Puzzles.ProjectEuler.DataFiles.Problem_0054_poker.txt";
            var fileContent = FileHelper.GetEmbeddedResourceContent(path);
            var fileLines = fileContent.Split(new [] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
            fileLines.Count().Should().Be(1000);

            var player1WinCount = 0;

            foreach (var fileLine in fileLines)
            {
                //Console.WriteLine(fileLine);
                var player1Text = fileLine.Substring(0, 14);
                var player2Text = fileLine.Substring(15);
                var player1Hand = new Hand(player1Text);
                var player2Hand = new Hand(player2Text);

                var winner = FindWinner(player1Hand, player2Hand);
                if (winner == 1)
                    player1WinCount++;
            }

            Console.WriteLine("Player 1 wins {0} hands", player1WinCount);
            player1WinCount.Should().Be(376);
        }
    }

    public class Card
    {
      public int Value { get; private set; }
      public string Suit { get; private set; }

      public Card(string cardText)
      {
          var value = cardText.Substring(0,1);
          Value = Card.GetValue(value);
          Suit = cardText.Substring(1,1);
      }

      public static int GetValue(string valueText)
      {
        switch(valueText)
        {
          case "2":
            return 2;
          case "3":
            return 3;
          case "4":
            return 4;
          case "5":
            return 5;
          case "6":
            return 6;
          case "7":
            return 7;
          case "8":
            return 8;
          case "9":
            return 9;
          case "T":
            return 10;
          case "J":
            return 11;
          case "Q":
            return 12;
          case "K":
            return 13;
          case "A":
            return 14;
          default:
            throw new ApplicationException(valueText);
        }
      } 
    }   

    public class Hand
    {
        private readonly bool allSameSuit;
        private readonly List<int> cardValueList;
        public List<Card> Cards { get; private set; }
        public HandType HandType { get; private set; }
        public int UsingCardValue { get; private set; }
        public List<int> CardValueList { get { return cardValueList; }}
    
        public Hand(string cardsText)
        {
            var cards = cardsText.Split(new [] { ' '}, StringSplitOptions.RemoveEmptyEntries);

            Cards = new List<Card>();
            foreach (var card in cards)
            {
                Cards.Add(new Card(card));
            }

            allSameSuit = Cards.Select(c => c.Suit).Distinct().Count() == 1;
            cardValueList = Cards.OrderBy(c => c.Value).Select(c => c.Value).ToList();

            if (IsRoyalFlush()) return;
            if (IsStraightFlush()) return;
            if (IsFourOfAKind()) return;
            if (IsFullHouse()) return;
            if (IsFlush()) return;
            if (IsStraight()) return;
            if (IsThreeOfAKind()) return;
            if (IsTwoPairs()) return;
            if (IsOnePair()) return;
            SetHighCard();
        }

        private bool IsRoyalFlush()
        {
            if (!allSameSuit) return false;

            if (Cards.All(c => c.Value != 10)) return false;
            if (Cards.All(c => c.Value != 11)) return false;
            if (Cards.All(c => c.Value != 12)) return false;
            if (Cards.All(c => c.Value != 13)) return false;
            if (Cards.All(c => c.Value != 14)) return false;

            HandType = HandType.RoyalFlush;
            UsingCardValue = 14;
            return true;
        }

        private bool IsStraightFlush()
        {
            if (!allSameSuit) return false;

            for (var idx = 1; idx < cardValueList.Count; ++idx)
            {
                if (cardValueList[idx] != (cardValueList[idx - 1] + 1))
                    return false;
            }

            HandType = HandType.StraightFlush;
            UsingCardValue = cardValueList[cardValueList.Count - 1];

            return true;
        }

        private bool IsFourOfAKind()
        {
            var groups = cardValueList.GroupBy(cv => cv);

            var groupWithFour = groups.Where(g => g.Count() == 4).ToList();

            if (!groupWithFour.Any()) return false;

            HandType = HandType.FourOfAKind;
            UsingCardValue = groupWithFour.Min(g => g.Key);
            return true;
        }

        private bool IsFullHouse()
        {
            var groups = cardValueList.GroupBy(c => c).ToList();

            if (groups.Count != 2) return false;

            var firstGroupCount = groups[0].Count();
            var secondGroupCount = groups[1].Count();

            if ((firstGroupCount == 2 && secondGroupCount == 3)
                || (firstGroupCount == 3 && secondGroupCount == 2))
            {
                HandType = HandType.FullHouse;
                UsingCardValue = groups.Where(g => g.Count() == 3).Select(g => g.Key).First();
                return true;
            }

            return false;
        }

        private bool IsFlush()
        {
            if (!allSameSuit) return false;

            HandType = HandType.Flush;
            UsingCardValue = cardValueList.Max();
            return true;
        }

        private bool IsStraight()
        {
            for (var idx = 1; idx < cardValueList.Count; ++idx)
            {
                if (cardValueList[idx] != (cardValueList[idx - 1] + 1))
                    return false;
            }

            HandType = HandType.Straight;
            UsingCardValue = cardValueList.Max();
            return true;
        }

        private bool IsThreeOfAKind()
        {
            var groupOfThree = cardValueList.GroupBy(c => c).Where(g => g.Count() == 3).ToList();
            if (!groupOfThree.Any()) return false;

            HandType = HandType.ThreeOfAKind;
            UsingCardValue = groupOfThree[0].Key;
            return true;
        }

        private bool IsTwoPairs()
        {
            var groups = cardValueList.GroupBy(c => c).Where(g => g.Count() == 2).ToList();
            if (groups.Count != 2) return false;

            HandType = HandType.TwoPairs;
            UsingCardValue = groups.Select(g => g.Key).Max();
            return true;
        }

        private bool IsOnePair()
        {
            var groups = cardValueList.GroupBy(c => c).Where(g => g.Count() == 2).ToList();
            if (!groups.Any()) return false;

            HandType = HandType.OnePair;
            UsingCardValue = groups[0].Key;
            return true;
        }

        private void SetHighCard()
        {
            HandType = HandType.HighCard;
            UsingCardValue = cardValueList.Max();
        }
    }

    public enum HandType
    {
      Unspecified,
      HighCard,
      OnePair,
      TwoPairs,
      ThreeOfAKind,
      Straight,
      Flush,
      FullHouse,
      FourOfAKind,
      StraightFlush,
      RoyalFlush
    }
}
