﻿using CribbageEngine.Exceptions;
using CribbageEngine.Play;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
	class GameTests
	{
		[Test]
		public void givenAGame_whenStarting_thenPermitsRegistrationOfPlayer()
		{
			Game game = new Game();
			game.RegisterPlayer(new Player(PlayerTests.PLAYER1_NAME));
			Assert.AreEqual(1, game.Players.Count);
		}

		[Test]
		public void givenAGameWithOnePlayer_whenAddingAnotherPlayer_thenReturnsTwoPlayers()
		{
			Game game = new Game();
			game.RegisterPlayer(new Player(PlayerTests.PLAYER1_NAME));
			game.RegisterPlayer(new Player(PlayerTests.PLAYER2_NAME));
			Assert.AreEqual(2, game.Players.Count);
		}

		[Test]
		public void givenAGameWithTwoPlayers_whenAddingAnotherPlayer_thenThrowsException()
		{
			Game game = new Game();
			game.RegisterPlayer(new Player(PlayerTests.PLAYER1_NAME));
			game.RegisterPlayer(new Player(PlayerTests.PLAYER2_NAME));

			Assert.Throws(typeof(TooManyPlayersException), () => game.RegisterPlayer(new Player()));
		}

		[Test]
		public void givenAGame_whenAddingAPlayer_thenPlayerScoreStartsAt0()
		{
			Game game = new Game();
			game.RegisterPlayer(new Player(PlayerTests.PLAYER1_NAME));

			Assert.AreEqual(0, game.Players[0].Score);
		}

		[Test]
		public void givenAGame_whenStarted_thenReturnsFirstRound()
		{
			Game game = new Game();
			game.RegisterPlayer(new Player(PlayerTests.PLAYER1_NAME));
			game.RegisterPlayer(new Player(PlayerTests.PLAYER2_NAME));

			Assert.IsNotNull(game.Start());
		}

		[Test]
		public void givenAGame_whenStartedWithoutMinumumNumberOfPlayers_thenThrowsException()
		{
			Game game = new Game();
			Assert.Throws(typeof(NotEnoughPlayersException), () => game.Start());
		}

		[Test]
		public void givenAStartedGame_whenAddingPlayerAsDealer_thenReturnsDealerAsLastPlayerInList()
		{
			Game game = new Game();
			Player dealer = new Player(PlayerTests.PLAYER1_NAME);
			dealer.IsDealer = true;
			game.RegisterPlayer(dealer);
			game.RegisterPlayer(new Player(PlayerTests.PLAYER2_NAME));
			game.Start();

			Assert.IsTrue(game.Players.Last().IsDealer);
		}

		[Test]
		public void givenAStartedGame_whenAddingPlayersAndNoneIsDealer_thenReturnsAssignedDealerAsLastPlayerInList()
		{
			Game game = new Game();
			Player player1 = new Player(PlayerTests.PLAYER1_NAME);
			game.RegisterPlayer(player1);
			Player player2 = new Player(PlayerTests.PLAYER2_NAME);
			game.RegisterPlayer(player2);
			game.Start();

			Assert.IsTrue(player2.IsDealer);
			Assert.AreEqual(player2, game.Players.Last());
		}
	}
}
