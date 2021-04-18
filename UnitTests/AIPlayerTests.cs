using CribbageEngine.Play;
using CribbageEngine.Exceptions;
using NUnit.Framework;
using System.Collections.Generic;
using CribbageEngine.AI;
using CribbageEngine.AI.Strategy;

namespace UnitTests
{
	public class AIPlayerTests
	{
		[Test]
		public void givenAnAIPlayer_whenRequestingCribCards_thenReturns2CardsFromActiveCardsAndReducesHandTo4Cards()
		{
			AIPlayer player = new AIPlayer(new FCFSCribStrategy(), null);
			Deck deck = new Deck();
			deck.Shuffle();
			for (int count = 0; count < Round.INITIAL_DEAL_CARD_COUNT; count++)
			{
				player.AcceptDealCard(deck.Draw());
			}

			Card[] crib = player.BankCribCards();
			Assert.AreEqual(2, crib.Length);
			Assert.AreEqual(4, player.GetHand().Length);
		}

		[Test]
		public void givenAnAIPlayer_whenRequestingPlayNotBreaking31_thenPlaysAllFourCardsInHand()
		{
			AIPlayer player = new AIPlayer(new FCFSCribStrategy(), new FCFSPlayStrategy());
			foreach (Card card in GetSixLowCards(Card.SuitType.Clubs))
			{
				player.AcceptDealCard(card);
			}
			player.BankCribCards();

			Queue<Card> throwCards = new Queue<Card>(GetSixLowCards(Card.SuitType.Diamonds));

			List<Card> playCards = new List<Card>(); 
			for (int count = 0; count < 4; count++)
			{
				Card card = player.Play(playCards.ToArray()) as Card;
				playCards.Add(card);
				playCards.Add(throwCards.Dequeue());
			}

			Assert.IsFalse(player.HasCards());
		}

		[Test]
		public void givenAnAIPlayer_whenRequestingPlayWhichWillBreak31_thenPlayerSignalsGo()
		{
			AIPlayer player = new AIPlayer(new FCFSCribStrategy(), new FCFSPlayStrategy());
			foreach (Card card in GetSixLowCards(Card.SuitType.Clubs))
			{
				player.AcceptDealCard(card);
			}
			player.BankCribCards();

			List<Card> playCards = new List<Card>();
			playCards.Add(new Card(Card.FaceType.King, Card.SuitType.Diamonds));
			playCards.Add(new Card(Card.FaceType.King, Card.SuitType.Spades));
			playCards.Add(new Card(Card.FaceType.Jack, Card.SuitType.Diamonds));

			IPlayResponse response = player.Play(playCards.ToArray());
			Assert.AreEqual(Pass.PassResponse, response);
		}

		//--------------

		private Card[] GetSixLowCards(Card.SuitType suit)
		{
			List<Card> cards = new List<Card>();
			cards.Add(new Card(Card.FaceType.Ace, suit));
			cards.Add(new Card(Card.FaceType.Two, suit));
			cards.Add(new Card(Card.FaceType.Three, suit));
			cards.Add(new Card(Card.FaceType.Four, suit));
			cards.Add(new Card(Card.FaceType.Five, suit));
			cards.Add(new Card(Card.FaceType.Six, suit));
			return cards.ToArray();
		}
	}
}
