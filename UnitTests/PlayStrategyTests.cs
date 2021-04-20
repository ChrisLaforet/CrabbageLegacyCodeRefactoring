using CribbageEngine.AI.Strategy;
using CribbageEngine.Play;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
	public class PlayStrategyTests
	{
		[Test]
		public void givenPlayHandWithFives_whenLeadingOffWithOptimalPlayStrategy_thenDoesNotPlayFive()
		{
			OptimalPlayStrategy strategy = new OptimalPlayStrategy();
			Card playedCard = strategy.SelectNextCard(true, new Card[0], getHandWithThreeFives());
			Assert.AreNotEqual(Card.FaceType.Five, playedCard.Face);
		}

		[Test]
		public void givenPlayHandWithFives_whenLeadingOffWithOptimalPlayStrategy_thenPlaysHighestNonFive()
		{
			OptimalPlayStrategy strategy = new OptimalPlayStrategy();
			Card playedCard = strategy.SelectNextCard(true, new Card[0], getHandWithThreeFives());
			Assert.AreEqual(Card.FaceType.Eight, playedCard.Face);
		}

		[Test]
		public void givenPlayHandWithAllFives_whenLeadingOffWithOptimalPlayStrategy_thenPlaysFive()
		{
			OptimalPlayStrategy strategy = new OptimalPlayStrategy();
			Card playedCard = strategy.SelectNextCard(true, new Card[0], getHandWithAllFives());
			Assert.AreEqual(Card.FaceType.Five, playedCard.Face);
		}

		[Test]
		public void givenPlayHandWithOnlyFivesAndTens_whenLeadingOffWithOptimalPlayStrategy_thenPlaysTen()
		{
			OptimalPlayStrategy strategy = new OptimalPlayStrategy();
			Card playedCard = strategy.SelectNextCard(true, new Card[0], getHandWithFivesAndTens());
			Assert.AreEqual(Card.FaceType.Ten, playedCard.Face);
		}

		[Test]
		public void givenPlayHandWithOnlyFivesAndTens_whenPlayingAgainstTenWithOptimalPlayStrategy_thenPlaysFive()
		{
			OptimalPlayStrategy strategy = new OptimalPlayStrategy();
			Card playedCard = strategy.SelectNextCard(true, GetOneCardSession(Card.FaceType.Queen, Card.SuitType.Hearts), getHandWithFivesAndTens());
			Assert.AreEqual(Card.FaceType.Five, playedCard.Face);
		}

		[Test]
		public void givenPlayHandWithOnlyFivesAndTens_whenPlayingAgainstFiveWithOptimalPlayStrategy_thenPlaysTen()
		{
			OptimalPlayStrategy strategy = new OptimalPlayStrategy();
			Card playedCard = strategy.SelectNextCard(true, GetOneCardSession(Card.FaceType.Five, Card.SuitType.Hearts), getHandWithFivesAndTens());
			Assert.AreEqual(Card.FaceType.Ten, playedCard.Face);
		}

		[Test]
		public void givenMixedPlayHand_whenPlayingAgainstNineWithOptimalPlayStrategy_thenBreaks15()
		{
			OptimalPlayStrategy strategy = new OptimalPlayStrategy();
			Card playedCard = strategy.SelectNextCard(true, GetOneCardSession(Card.FaceType.Nine, Card.SuitType.Hearts), getMixedHand());
			Assert.AreEqual(Card.FaceType.Nine, playedCard.Face);
		}

		[Test]
		public void givenMixedPlayHand_whenPlayingAgainstSevenWithOptimalPlayStrategy_thenPlaysPair()
		{
			OptimalPlayStrategy strategy = new OptimalPlayStrategy();
			Card playedCard = strategy.SelectNextCard(true, GetOneCardSession(Card.FaceType.Seven, Card.SuitType.Hearts), getMixedHand());
			Assert.AreEqual(Card.FaceType.Seven, playedCard.Face);
		}

		[Test]
		public void givenMixedPlayHand_whenPlayingAgainstTwoSevensWithOptimalPlayStrategy_thenPlaysForSeven()
		{
			OptimalPlayStrategy strategy = new OptimalPlayStrategy();
			Card playedCard = strategy.SelectNextCard(true,
				GetTwoCardSession(Card.FaceType.Seven, Card.SuitType.Hearts, Card.FaceType.Seven, Card.SuitType.Diamonds), 
				getMixedHand());
			Assert.AreEqual(Card.FaceType.Seven, playedCard.Face);
		}

		//------------------

		private Card[] GetOneCardSession(Card.FaceType face, Card.SuitType suit)
		{
			Card[] cards = new Card[1];
			cards[0] = new Card(face, suit);
			return cards;
		}

		private Card[] GetTwoCardSession(Card.FaceType face1, Card.SuitType suit1, Card.FaceType face2, Card.SuitType suit2)
		{
			Card[] cards = new Card[2];
			cards[0] = new Card(face1, suit1);
			cards[1] = new Card(face2, suit2);
			return cards;
		}

		private Card[] getHandWithFivesAndTens()
		{
			List<Card> cards = new List<Card>();
			cards.Add(new Card(Card.FaceType.Five, Card.SuitType.Clubs));
			cards.Add(new Card(Card.FaceType.Ten, Card.SuitType.Clubs));
			cards.Add(new Card(Card.FaceType.Five, Card.SuitType.Diamonds));
			cards.Add(new Card(Card.FaceType.Jack, Card.SuitType.Hearts));
			return cards.ToArray();
		}

		private Card[] getHandWithThreeFives()
		{
			List<Card> cards = new List<Card>();
			cards.Add(new Card(Card.FaceType.Five, Card.SuitType.Clubs));
			cards.Add(new Card(Card.FaceType.Eight, Card.SuitType.Clubs));
			cards.Add(new Card(Card.FaceType.Five, Card.SuitType.Diamonds));
			cards.Add(new Card(Card.FaceType.Five, Card.SuitType.Hearts));
			return cards.ToArray();
		}

		private Card[] getHandWithAllFives()
		{
			List<Card> cards = new List<Card>();
			cards.Add(new Card(Card.FaceType.Five, Card.SuitType.Clubs));
			cards.Add(new Card(Card.FaceType.Five, Card.SuitType.Spades));
			cards.Add(new Card(Card.FaceType.Five, Card.SuitType.Diamonds));
			cards.Add(new Card(Card.FaceType.Five, Card.SuitType.Hearts));
			return cards.ToArray();
		}

		private Card[] getMixedHand()
		{
			List<Card> cards = new List<Card>();
			cards.Add(new Card(Card.FaceType.Seven, Card.SuitType.Clubs));
			cards.Add(new Card(Card.FaceType.Nine, Card.SuitType.Spades));
			cards.Add(new Card(Card.FaceType.Ace, Card.SuitType.Diamonds));
			cards.Add(new Card(Card.FaceType.Ten, Card.SuitType.Hearts));
			return cards.ToArray();
		}
	}
}
