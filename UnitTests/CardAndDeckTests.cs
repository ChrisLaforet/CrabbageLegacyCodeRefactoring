using CribbageEngine.Play;
using CribbageEngine.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
	class CardAndDeckTests
	{
		[Test]
		public void givenADeckOfCards_whenFirstCreated_thenReturns52Cards()
		{
			Deck deck = new Deck();
			Assert.AreEqual(52, deck.Remaining);
		}

		[Test]
		public void givenADeckOfCards_whenCardsAreRemoved_thenReturnsCorrectRemaining()
		{
			Deck deck = new Deck();
			for (int count = 0; count < 32; count++)
			{
				deck.Draw();
			}
			Assert.AreEqual(20, deck.Remaining);
		}

		[Test]
		public void givenADeckOfCards_whenAllCardsAreRemoved_thenThrowsException()
		{
			Deck deck = new Deck();
			for (int count = 0; count < 52; count++)
			{
				deck.Draw();
			}
			Assert.Throws(typeof(DeckOutOfCardsException), () => deck.Draw());
		}

		[Test]
		public void givenADeckOfCards_whenCheckingCards_thenAllCardsAreUnique()
		{
			IDictionary<int, Card> cards = new Dictionary<int, Card>();
			Deck deck = new Deck();
			for (int count = 0; count < 52; count++)
			{
				Card card = deck.Draw();
				if (cards.ContainsKey(card.GetHashCode()))
					Assert.Fail("Duplicate card has been found - " + card);
				cards.Add(card.GetHashCode(), card);
			}
			Assert.Pass();
		}

		[Test]
		public void givenACard_whenCheckingFace_thenReturnsFace()
		{
			Card card = new Card(Card.FaceType.Jack, Card.SuitType.Hearts);
			Assert.AreEqual(Card.FaceType.Jack, card.Face);
		}

		[Test]
		public void givenACard_whenCheckingSuit_thenReturnsSuit()
		{
			Card card = new Card(Card.FaceType.Jack, Card.SuitType.Hearts);
			Assert.AreEqual(Card.SuitType.Hearts, card.Suit);
		}
	}
}
