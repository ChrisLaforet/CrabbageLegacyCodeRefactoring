using CribbageEngine.Play;
using CribbageEngine.Exceptions;
using NUnit.Framework;
using System.Collections.Generic;
using CribbageEngine.AI;

namespace UnitTests
{
	public class AITests
	{
		[Test]
		public void givenAnAIPlayer_whenRequestingCribCards_thenReturns2CardsFromActiveCardsAndReducesHandTo4Cards()
		{
			AIPlayer player = new AIPlayer(null, null);
			Deck deck = new Deck();
			deck.Shuffle();
			for (int count = 0; count < Round.INITIAL_DEAL_CARD_COUNT; count++)
			{
				player.AddCard(deck.Draw());
			}

			Card[] crib = player.BankCribCards();
			Assert.AreEqual(2, crib.Length);
			Assert.AreEqual(4, player.GetHand().Length);
		}
	}
}
