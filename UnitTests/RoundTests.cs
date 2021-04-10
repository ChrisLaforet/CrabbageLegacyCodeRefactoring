using CribbageEngine.Exceptions;
using CribbageEngine.Play;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
	class RoundTests
	{
		[Test]
		public void givenAStartedRound_whenRequestingTheParentGame_thenReturnsTheParentGame()
		{
			Game game = Prepare2PlayerGame();
			Round round = game.Start();

			Assert.AreEqual(game, round.Game);
		}

		[Test]
		public void givenANewRound_whenStartIsInvoked_thenPlayersReceive6CardsEach()
		{
			Game game = Prepare2PlayerGame();
			Round round = game.Start();
			round.Start();

			foreach (Player player in game.Players)
			{
				Assert.AreEqual(Round.INITIAL_DEAL_CARD_COUNT, player.GetHand().Count());
			}
		}

		[Ignore("Delete eventually - exceptions are thrown when crib card counts are accepted ahead of this check")]
		[Test]
		public void givenAStartedRound_whenPlayStartIsInvoked_thenThrowsExceptionIfCribIsNotPrepared()
		{
			Game game = Prepare2PlayerGame();
			Round round = game.Start();
			round.Start();
			Assert.Throws(typeof(CribNotProvidedException), () => round.StartPlay());
		}

		[Test]
		public void givenARound_whenBanking2CribCards_then2CardsAreAcceptedFromEachPlayer()
		{
			Game game = Prepare2PlayerGame();
			Round round = game.Start();
			Card[] cards = CreateTwoCardCrib();
			foreach (var player in game.Players)
			{
				(player as TestPlayer).SetMockCribCards(cards);
			}
			round.Start();
			round.StartPlay();
			Assert.AreEqual(4, round.Crib.Count());
		}

		[Test]
		public void givenARound_whenBankingTooFewCribCards_thenThrowsException()
		{
			Game game = Prepare2PlayerGame();
			Round round = game.Start();
			Card[] cards = new Card[1];
			cards[0] = new Card(Card.FaceType.Ace, Card.SuitType.Clubs);
			foreach (var player in game.Players)
			{
				(player as TestPlayer).SetMockCribCards(cards);
			}

			round.Start();
			Assert.Throws(typeof(InvalidCribCardCountException), () => round.StartPlay());
		}

		[Test]
		public void givenARound_whenBankingTooManyCribCards_thenThrowsException()
		{
			Game game = Prepare2PlayerGame();
			Round round = game.Start();
			Card[] cards = new Card[3];
			cards[0] = new Card(Card.FaceType.Ace, Card.SuitType.Clubs);
			cards[1] = new Card(Card.FaceType.Queen, Card.SuitType.Diamonds);
			cards[2] = new Card(Card.FaceType.King, Card.SuitType.Diamonds);
			round.Start();
			Assert.Throws(typeof(InvalidCribCardCountException), () => round.StartPlay());
		}

		[Ignore("Delete eventually - exceptions are thrown when crib card counts are accepted ahead of this check")]
		[Test]
		public void givenARound_whenPlayStartedWithoutFullCrib_thenThrowsException()
		{
			Game game = Prepare2PlayerGame();
			Round round = game.Start();
			Card[] cards = CreateTwoCardCrib();
			foreach (var player in game.Players)
			{
				(player as TestPlayer).SetMockCribCards(cards);
			}
			round.Start();
			Assert.Throws(typeof(CribNotProvidedException), () => round.StartPlay());
		}

		[Test]
		public void givenARound_whenPlayStartedWithFullCrib_thenRoundPlayIsStarted()
		{
			Game game = Prepare2PlayerGame();
			Round round = game.Start();
			Card[] cards = CreateTwoCardCrib();
			round.Start();
			foreach (var player in game.Players)
			{
				(player as TestPlayer).SetMockCribCards(cards);
			}
			round.StartPlay();
			Assert.IsTrue(round.IsPlayStarted);
		}

		//[Test]
		//public void givenARound_whenPlayStarted_thenRoundPlayContinuesUntilPlayersCardsDepleted()
		//{
		//	Game game = Prepare2PlayerGame();
		//	Round round = game.Start();
		//	round.Start();

		//}


		//--------------------------------

		private static Game Prepare2PlayerGame()
		{
			Game game = new Game();
			game.RegisterPlayer(new TestPlayer(PlayerTests.PLAYER1_NAME));
			game.RegisterPlayer(new TestPlayer(PlayerTests.PLAYER2_NAME));
			return game;
		}

		private static Card[] CreateTwoCardCrib()
		{
			Card[] cards = new Card[2];
			cards[0] = new Card(Card.FaceType.Ace, Card.SuitType.Clubs);
			cards[1] = new Card(Card.FaceType.Queen, Card.SuitType.Diamonds);
			return cards;
		}
	}
}
