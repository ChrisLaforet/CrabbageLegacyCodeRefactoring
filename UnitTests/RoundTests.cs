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
			Game game = new Game();
			game.RegisterPlayer(new Player(PlayerTests.PLAYER1_NAME));
			game.RegisterPlayer(new Player(PlayerTests.PLAYER2_NAME));

			Round round = game.Start();

			Assert.AreEqual(game, round.Game);
		}

		[Test]
		public void givenANewRound_whenStartIsInvoked_thenPlayersReceive6CardsEach()
		{
			Game game = new Game();
			game.RegisterPlayer(new Player(PlayerTests.PLAYER1_NAME));
			game.RegisterPlayer(new Player(PlayerTests.PLAYER2_NAME));

			Round round = game.Start();
			round.Start();

			foreach (Player player in game.Players) {
				Assert.AreEqual(Round.INITIAL_DEAL_CARD_COUNT, player.GetHand().Count());
			}
		}
	}
}
