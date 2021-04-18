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
			Assert.AreEqual(1, CardHelperFunctions.CountCardsWithFaceType(crib, Card.FaceType.Ace));
			Assert.AreEqual(1, CardHelperFunctions.CountCardsWithFaceType(crib, Card.FaceType.Four));
		}

		[Test]
		public void givenAMixedHandOfCardsContaining2Fives_whenUsingBestScoringCribStrategy_thenCribCardsContainTwoFives()
		{
			BestScoringCribStrategy strategy = new BestScoringCribStrategy();
			Card[] crib = strategy.BankCribCards(true, GetSixMixedCards());
			Assert.AreEqual(2, crib.Length);
			Assert.AreEqual(2, CardHelperFunctions.CountCardsWithFaceType(crib, Card.FaceType.Five));
		}

		[Test]
		public void givenAHandOfCardsContaining2Pairs_whenUsingBestScoringCribStrategyForDealer_thenCribCardsPairOfJacks()
		{
			BestScoringCribStrategy strategy = new BestScoringCribStrategy();
			Card[] crib = strategy.BankCribCards(true, GetTwoDistinctPairsOfCards());
			Assert.AreEqual(2, crib.Length);
			Assert.AreEqual(2, CardHelperFunctions.CountCardsWithFaceType(crib, Card.FaceType.Jack));
		}

		[Test]
		public void givenAHandOfCardsContaining2Pairs_whenUsingBestScoringCribStrategyForNonDealer_thenCribCardsPairOfKing()
		{
			BestScoringCribStrategy strategy = new BestScoringCribStrategy();
			Card[] crib = strategy.BankCribCards(false, GetTwoDistinctPairsOfCards());
			Assert.AreEqual(2, crib.Length);
			Assert.AreEqual(2, CardHelperFunctions.CountCardsWithFaceType(crib, Card.FaceType.King));
		}

		[Test]
		public void givenAHandOfCardsContainingSumOfFive_whenUsingBestScoringCribStrategy_thenCribCardsSumToFive()
		{
			BestScoringCribStrategy strategy = new BestScoringCribStrategy();
			Card[] crib = strategy.BankCribCards(true, GetHandWithSumOfFive());
			Assert.AreEqual(2, crib.Length);
			Assert.AreEqual(5, crib[0].Value + crib[1].Value);
		}

		[Test]
		public void givenAHandOfCardsContainingSumOfFfteen_whenUsingBestScoringCribStrategyForDealer_thenCribCardsSumToFifteen()
		{
			BestScoringCribStrategy strategy = new BestScoringCribStrategy();
			Card[] crib = strategy.BankCribCards(true, GetHandWithSumOfFifteen());
			Assert.AreEqual(2, crib.Length);
			Assert.AreEqual(15, crib[0].Value + crib[1].Value);
		}

		//[Test]
		//public void givenAHandOfCardsContainingSumOfFfteen_whenUsingBestScoringCribStrategyForNonDealer_thenCribCardsSumToFifteen()
		//{
		//	BestScoringCribStrategy strategy = new BestScoringCribStrategy();
		//	Card[] crib = strategy.BankCribCards(true, GetHandWithPairOfTens());
		//	Assert.AreEqual(2, crib.Length);
		//	Assert.AreEqual(15, crib[0].Value + crib[1].Value);
		//}
		

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

		private Card[] GetTwoDistinctPairsOfCards()
		{
			List<Card> cards = new List<Card>();
			cards.Add(new Card(Card.FaceType.Jack, Card.SuitType.Hearts));
			cards.Add(new Card(Card.FaceType.Three, Card.SuitType.Spades));
			cards.Add(new Card(Card.FaceType.Jack, Card.SuitType.Clubs));
			cards.Add(new Card(Card.FaceType.King, Card.SuitType.Diamonds));
			cards.Add(new Card(Card.FaceType.Two, Card.SuitType.Clubs));
			cards.Add(new Card(Card.FaceType.King, Card.SuitType.Hearts));
			return cards.ToArray();
		}

		private Card[] GetHandWithSumOfFive()
		{
			List<Card> cards = new List<Card>();
			cards.Add(new Card(Card.FaceType.Ace, Card.SuitType.Hearts));
			cards.Add(new Card(Card.FaceType.Three, Card.SuitType.Spades));
			cards.Add(new Card(Card.FaceType.Eight, Card.SuitType.Clubs));
			cards.Add(new Card(Card.FaceType.Four, Card.SuitType.Diamonds));
			cards.Add(new Card(Card.FaceType.Two, Card.SuitType.Clubs));
			cards.Add(new Card(Card.FaceType.King, Card.SuitType.Hearts));
			return cards.ToArray();
		}

		private Card[] GetHandWithSumOfFifteen()
		{
			List<Card> cards = new List<Card>();
			cards.Add(new Card(Card.FaceType.Eight, Card.SuitType.Hearts));
			cards.Add(new Card(Card.FaceType.Six, Card.SuitType.Spades));
			cards.Add(new Card(Card.FaceType.Seven, Card.SuitType.Clubs));
			cards.Add(new Card(Card.FaceType.Nine, Card.SuitType.Diamonds));
			cards.Add(new Card(Card.FaceType.Two, Card.SuitType.Clubs));
			cards.Add(new Card(Card.FaceType.King, Card.SuitType.Hearts));
			return cards.ToArray();
		}

		private Card[] GetHandWithPairOfTens()
		{
			List<Card> cards = new List<Card>();
			cards.Add(new Card(Card.FaceType.Eight, Card.SuitType.Hearts));
			cards.Add(new Card(Card.FaceType.Six, Card.SuitType.Spades));
			cards.Add(new Card(Card.FaceType.Ten, Card.SuitType.Clubs));
			cards.Add(new Card(Card.FaceType.Jack, Card.SuitType.Diamonds));
			cards.Add(new Card(Card.FaceType.Two, Card.SuitType.Clubs));
			cards.Add(new Card(Card.FaceType.Four, Card.SuitType.Hearts));
			return cards.ToArray();
		}
	}
}
