using CribbageEngine.Play;
using CribbageEngine.Utility;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
	public class UtilityTests
	{
		[Test]
		public void givenAHandOfLowCards_whenCalculatingBestHand_thenBestHandEquals()
		{
			List<PlayingHand> hands = CardHelperFunctions.GetBestPlayingHands(GetSixLowCardSequence());
			Assert.AreEqual(18, hands[0].Value);
		}

		[Test]
		public void givenAHandOfMixedCards_whenCalculatingBestHand_thenBestHandEquals()
		{
			List<PlayingHand> hands = CardHelperFunctions.GetBestPlayingHands(GetSixMixedCards());
			Assert.AreEqual(33, hands[0].Value);
		}

		[Test]
		public void givenAHandOfTwoPairsOfCards_whenCalculatingBestHand_thenBestHandEquals()
		{
			List<PlayingHand> hands = CardHelperFunctions.GetBestPlayingHands(GetTwoDistinctPairsOfCards());
			Assert.AreEqual(40, hands[0].Value);
		}


		//--------------------

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
	}
}
