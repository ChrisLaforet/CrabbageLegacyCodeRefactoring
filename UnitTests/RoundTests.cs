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
			Round round = game.StartRound();

			Assert.AreEqual(game, round.Game);
		}

		[Test]
		public void givenANewRound_whenStartIsInvoked_thenPlayersReceive6CardsEach()
		{
			Game game = Prepare2PlayerGame();
			Round round = game.StartRound();
			round.Start();

			foreach (Player player in game.Players)
			{
				Assert.AreEqual(Round.INITIAL_DEAL_CARD_COUNT, player.GetHand().Count());
			}
		}

		[Test]
		public void givenARound_whenBankingCribCards_then2CardsAreAcceptedFromEachPlayer()
		{
			Game game = Prepare2PlayerGame();
			Round round = game.StartRound();
			Card[] cards = CreateTwoCardCrib();
			round.Start();
			round.StartPlay();
			Assert.AreEqual(4, round.Crib.Count());
		}

		[Test]
		public void givenARound_whenBankingTooFewCribCards_thenThrowsException()
		{
			Game game = Prepare2PlayerGame();
			Round round = game.StartRound();
			foreach (var player in game.Players)
			{
				(player as TestPlayer).DoShortCribCards = true;
			}

			round.Start();
			Assert.Throws(typeof(InvalidCribCardCountException), () => round.StartPlay());
		}

		[Test]
		public void givenARound_whenPlayStartedWithFullCrib_thenRoundPlayIsStarted()
		{
			Game game = Prepare2PlayerGame();
			Round round = game.StartRound();
			round.Start();
			round.StartPlay();
			Assert.IsTrue(round.IsPlayStarted);
		}

		[Test]
		public void givenARound_whenPlayStarted_thenRoundPlayContinuesUntilPlayersCardsDepleted()
		{
			Game game = Prepare2PlayerGame();
			Round round = game.StartRound();
			round.Start();
			round.StartPlay();
			Assert.IsTrue(round.IsFinished);
		}

		[Test]
		public void givenARound_whenPlayContinuesToEnd_thenPlayersScoresAreNotZero()
		{
			Game game = Prepare2PlayerGame();
			Round round = game.StartRound();
			round.Start();
			round.StartPlay();
			int total = 0;
			foreach (var player in game.Players)
			{
				total += player.Score;
			}
			Assert.IsTrue(total > 0);
		}


		[Test]
		public void givenARoundThatHasNotFinished_whenCalculateAfterPlayScores_thenThrowsException()
		{
			Game game = Prepare2PlayerGame();
			Round round = game.StartRound();
			round.Start();
			Assert.Throws(typeof(OperationNotPermittedException), () => round.CalculateAfterPlayScores());
		}

		[Test]
		public void givenARoundThatHasFinished_whenCalculateAfterPlayScores_thenScoresForPlayerHandsIncrease()
		{
			Game game = Prepare2PlayerGame();
			Round round = game.StartRound();
			round.Start();
			round.StartPlay();
			int player1Score = game.Players[0].Score;
			int player2Score = game.Players[1].Score;
			round.CalculateAfterPlayScores();
			Assert.AreNotEqual(player1Score, game.Players[0].Score);
			Assert.AreNotEqual(player2Score, game.Players[1].Score);
		}


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
