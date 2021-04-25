using CribbageEngine.AI;
using CribbageEngine.AI.Strategy;
using CribbageEngine.Play;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
	class AIGameTests
	{
		[Test]
		public void givenTwoAIPlayersWithOptimalStrategies_whenStartingAGameRound_thenRoundPlaysToEnd()
		{
			AIPlayer player1 = new AIPlayer(PlayerTests.PLAYER1_NAME, new BestScoringCribStrategy(), new OptimalPlayStrategy());
			AIPlayer player2 = new AIPlayer(PlayerTests.PLAYER2_NAME, new BestScoringCribStrategy(), new OptimalPlayStrategy());

			Game game = new Game();
			game.RegisterPlayer(player1);
			game.RegisterPlayer(player2);

			Round round = game.StartRound();
			round.Start();
			round.StartPlay();
			round.CalculateAfterPlayScores();
			Assert.IsTrue(player1.Score > 0);
			Assert.IsTrue(player2.Score > 0);
		}

		[Test]
		public void givenTwoAIPlayersWithOptimalStrategies_whenPlayingGame_thenPlaysRoundsUntilAPlayerWins()
		{
			AIPlayer player1 = new AIPlayer(PlayerTests.PLAYER1_NAME, new BestScoringCribStrategy(), new OptimalPlayStrategy());
			AIPlayer player2 = new AIPlayer(PlayerTests.PLAYER2_NAME, new BestScoringCribStrategy(), new OptimalPlayStrategy());

			Game game = new Game();
			game.RegisterPlayer(player1);
			game.RegisterPlayer(player2);

			Player winner = null;
			int totalRounds = 0;
			while (true)
			{
				Round round = game.StartRound();
				++totalRounds;
				round.Start();
				round.StartPlay();
				winner = round.GetWinner();
				if (winner != null)
				{
					break;
				}
				round.CalculateAfterPlayScores();
			}

			Assert.IsTrue(totalRounds > 1);
			Assert.IsTrue(winner.Score >= Evaluation.GAME_WINNING_SCORE);
			Assert.IsTrue(winner == player1 ? player2.Score < Evaluation.GAME_WINNING_SCORE : player1.Score < Evaluation.GAME_WINNING_SCORE);
		}
	}
}
