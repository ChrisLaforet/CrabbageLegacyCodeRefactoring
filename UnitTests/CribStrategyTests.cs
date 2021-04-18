using CribbageEngine.Play;
using CribbageEngine.Exceptions;
using NUnit.Framework;
using System.Collections.Generic;
using CribbageEngine.Utility;
using CribbageEngine.AI.Strategy;

namespace UnitTests
{
	public class CribStrategyTests
	{
		[Test]
		public void givenASequencedHandOfCards_whenUsingFCFSCribStrategy_thenCribCardsAreFirstTwoPassedIn()
		{
			FCFSCribStrategy strategy = new FCFSCribStrategy();
			Card[] cards = GetSixLowCardSequence();
			Card[] crib = strategy.BankCribCards(true, cards);
			Assert.AreEqual(2, crib.Length);
			Assert.AreEqual(cards[0], crib[0]);
			Assert.AreEqual(cards[1], crib[1]);
		}

		[Test]
		public void givenAMixedHandOfCards_whenUsingFCFSCribStrategy_thenCribCardsAreFirstTwoPassedIn()
		{
			FCFSCribStrategy strategy = new FCFSCribStrategy();
			Card[] cards = GetSixMixedCards();
			Card[] crib = strategy.BankCribCards(true, cards);
			Assert.AreEqual(2, crib.Length);
			Assert.AreEqual(cards[0], crib[0]);
			Assert.AreEqual(cards[1], crib[1]);
		}

		[Test]
		public void givenASequencedHandOfCardsWith1Five_whenUsingBestScoringCribStrategy_thenCribCardsContainOneFive()
		{
			BestScoringCribStrategy strategy = new BestScoringCribStrategy();
			Card[] crib = strategy.BankCribCards(true, GetSixLowCardSequence());
			Assert.AreEqual(2, crib.Length);
			Assert.AreEqual(1, CardHelperFunctions.CountCardsWithFaceType(crib, Card.FaceType.Five));
		}

		[Test]
		public void givenAMixedHandOfCardsContaining2Fives_whenUsingBestScoringCribStrategy_thenCribCardsContainTwoFives()
		{
			BestScoringCribStrategy strategy = new BestScoringCribStrategy();
			Card[] crib = strategy.BankCribCards(true, GetSixMixedCards());
			Assert.AreEqual(2, crib.Length);
			Assert.AreEqual(2, CardHelperFunctions.CountCardsWithFaceType(crib, Card.FaceType.Five));
		}

		//--------------

		private Card[] GetSixLowCardSequence()
		{
			List<Card> cards = new List<Card>();
			cards.Add(new Card(Card.FaceType.Ace, Card.SuitType.Clubs));
			cards.Add(new Card(Card.FaceType.Two, Card.SuitType.Clubs));
			cards.Add(new Card(Card.FaceType.Three, Card.SuitType.Clubs));
			cards.Add(new Card(Card.FaceType.Four, Card.SuitType.Clubs));
			cards.Add(new Card(Card.FaceType.Five, Card.SuitType.Clubs));
			cards.Add(new Card(Card.FaceType.Six, Card.SuitType.Clubs));
			return cards.ToArray();
		}

		private Card[] GetSixMixedCards()
		{
			List<Card> cards = new List<Card>();
			cards.Add(new Card(Card.FaceType.King, Card.SuitType.Hearts));
			cards.Add(new Card(Card.FaceType.Three, Card.SuitType.Spades));
			cards.Add(new Card(Card.FaceType.Jack, Card.SuitType.Clubs));
			cards.Add(new Card(Card.FaceType.Five, Card.SuitType.Diamonds));
			cards.Add(new Card(Card.FaceType.Five, Card.SuitType.Clubs));
			cards.Add(new Card(Card.FaceType.Eight, Card.SuitType.Hearts));
			return cards.ToArray();
		}
	}
}
