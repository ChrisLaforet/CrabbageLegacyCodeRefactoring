using CribbageEngine.Exceptions;
using CribbageEngine.Play;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
	public class EvaluationTests
	{
		private static Card StarterCard_QueenOfClubs = new Card(Card.FaceType.Queen, Card.SuitType.Clubs);
		private static Card StarterCard_JackOfClubs = new Card(Card.FaceType.Jack, Card.SuitType.Clubs);

		[Test]
		public void givenAHandWithOnePair_whenEvaluatingFullHandOtherThanCrib_thenReturnsPairScoreOf2()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.King, Card.SuitType.Clubs),
				new Card(Card.FaceType.King, Card.SuitType.Diamonds),
				new Card(Card.FaceType.Ace, Card.SuitType.Spades),
				new Card(Card.FaceType.Nine, Card.SuitType.Hearts)
			};

			PlayScore[] scores = Evaluation.EvaluateFullHand(cards, StarterCard_QueenOfClubs, false);
			Assert.AreEqual(1, scores.Count());
			Assert.AreEqual(2, scores[0].Score);
			Assert.AreEqual(PlayScore.ScoreType.Combination_Pair, scores[0].Type);
		}

		[Test]
		public void givenAPlaySessionWithFinalConsecutivePair_whenEvaluatingPlayHand_thenReturnsPairScoreOf2()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.King, Card.SuitType.Clubs),
				new Card(Card.FaceType.Queen, Card.SuitType.Diamonds),
				new Card(Card.FaceType.Ace, Card.SuitType.Spades),
				new Card(Card.FaceType.Ace, Card.SuitType.Hearts)
			};

			PlayScore[] scores = Evaluation.EvaluatePlaySession(cards);
			Assert.AreEqual(1, scores.Count());
			Assert.AreEqual(2, scores[0].Score);
			Assert.AreEqual(PlayScore.ScoreType.Play_Pair, scores[0].Type);
		}

		[Test]
		public void givenAHandWithTwoPair_whenEvaluatingFullHandOtherThanCrib_thenReturnsPairScoreOf6()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.King, Card.SuitType.Clubs),
				new Card(Card.FaceType.King, Card.SuitType.Diamonds),
				new Card(Card.FaceType.King, Card.SuitType.Spades),
				new Card(Card.FaceType.Nine, Card.SuitType.Hearts)
			};

			PlayScore[] scores = Evaluation.EvaluateFullHand(cards, StarterCard_QueenOfClubs, false);
			Assert.AreEqual(3, scores.Count());
			Assert.AreEqual(6, SumScores(scores));
		}

		[Test]
		public void givenAPlaySessionWithFinalConsecutiveTriplet_whenEvaluatingPlayHand_thenReturnsPairScoreOf6()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.King, Card.SuitType.Clubs),
				new Card(Card.FaceType.Ace, Card.SuitType.Diamonds),
				new Card(Card.FaceType.Ace, Card.SuitType.Spades),
				new Card(Card.FaceType.Ace, Card.SuitType.Hearts)
			};

			PlayScore[] scores = Evaluation.EvaluatePlaySession(cards);
			Assert.AreEqual(1, scores.Count());
			Assert.AreEqual(6, scores[0].Score);
			Assert.AreEqual(PlayScore.ScoreType.Play_Triplet, scores[0].Type);
		}

		[Test]
		public void givenAHandWithThreePair_whenEvaluatingFullHandOtherThanCrib_thenReturnsPairScoreOf12()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.King, Card.SuitType.Clubs),
				new Card(Card.FaceType.King, Card.SuitType.Diamonds),
				new Card(Card.FaceType.King, Card.SuitType.Spades),
				new Card(Card.FaceType.King, Card.SuitType.Hearts)
			};

			PlayScore[] scores = Evaluation.EvaluateFullHand(cards, StarterCard_QueenOfClubs, false);
			Assert.AreEqual(6, scores.Count());
			Assert.AreEqual(12, SumScores(scores));
		}

		[Test]
		public void givenAPlaySessionWithFinalConsecutiveFour_whenEvaluatingPlayHand_thenReturnsPairScoreOf12()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.Ace, Card.SuitType.Clubs),
				new Card(Card.FaceType.Ace, Card.SuitType.Diamonds),
				new Card(Card.FaceType.Ace, Card.SuitType.Spades),
				new Card(Card.FaceType.Ace, Card.SuitType.Hearts)
			};

			PlayScore[] scores = Evaluation.EvaluatePlaySession(cards);
			Assert.AreEqual(1, scores.Count());
			Assert.AreEqual(12, scores[0].Score);
			Assert.AreEqual(PlayScore.ScoreType.Play_Four, scores[0].Type);
		}

		[Test]
		public void givenAHandWithNobs_whenEvaluatingFullHandOtherThanCrib_thenReturnsNobsScoreOf1()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.Jack, Card.SuitType.Clubs),
				new Card(Card.FaceType.Three, Card.SuitType.Diamonds),
				new Card(Card.FaceType.Ace, Card.SuitType.Spades),
				new Card(Card.FaceType.Nine, Card.SuitType.Hearts)
			};

			PlayScore[] scores = Evaluation.EvaluateFullHand(cards, StarterCard_QueenOfClubs, false);
			Assert.AreEqual(1, scores.Count());
			Assert.AreEqual(1, scores[0].Score);
			Assert.AreEqual(PlayScore.ScoreType.HisNobs, scores[0].Type);
		}

		[Test]
		public void givenAHandWithFifteen_whenEvaluatingFullHandOtherThanCrib_thenReturnsFifteenScoreOf2()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.Two, Card.SuitType.Clubs),
				new Card(Card.FaceType.Five, Card.SuitType.Diamonds),
				new Card(Card.FaceType.Seven, Card.SuitType.Spades),
				new Card(Card.FaceType.Nine, Card.SuitType.Hearts)
			};

			PlayScore[] scores = Evaluation.EvaluateFullHand(cards, StarterCard_QueenOfClubs, false);
			Assert.AreEqual(1, scores.Count());
			Assert.AreEqual(2, scores[0].Score);
			Assert.AreEqual(PlayScore.ScoreType.Combination_Fifteen, scores[0].Type);
		}

		[Test]
		public void givenAHandWithTwoFifteens_whenEvaluatingFullHandOtherThanCrib_thenReturnsFifteenScoreOf4()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.King, Card.SuitType.Clubs),
				new Card(Card.FaceType.Five, Card.SuitType.Diamonds),
				new Card(Card.FaceType.Seven, Card.SuitType.Spades),
				new Card(Card.FaceType.Nine, Card.SuitType.Hearts)
			};

			PlayScore[] scores = Evaluation.EvaluateFullHand(cards, StarterCard_QueenOfClubs, false);
			Assert.AreEqual(2, scores.Count());
			Assert.AreEqual(4, SumScores(scores));
		}

		[Test]
		public void givenAHandWithThreeFifteens_whenEvaluatingFullHandOtherThanCrib_thenReturnsFifteenScoreOf6()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.King, Card.SuitType.Clubs),
				new Card(Card.FaceType.Five, Card.SuitType.Diamonds),
				new Card(Card.FaceType.Seven, Card.SuitType.Spades),
				new Card(Card.FaceType.Ten, Card.SuitType.Hearts)
			};

			PlayScore[] scores = Evaluation.EvaluateFullHand(cards, StarterCard_QueenOfClubs, false);
			Assert.AreEqual(3, scores.Count());
			Assert.AreEqual(6, SumScores(scores));
		}

		[Test]
		public void givenAHandWithFourFifteens_whenEvaluatingFullHandOtherThanCrib_thenReturnsFifteenScoreOf8()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.King, Card.SuitType.Clubs),
				new Card(Card.FaceType.Five, Card.SuitType.Diamonds),
				new Card(Card.FaceType.Three, Card.SuitType.Spades),
				new Card(Card.FaceType.Two, Card.SuitType.Hearts)
			};

			PlayScore[] scores = Evaluation.EvaluateFullHand(cards, StarterCard_QueenOfClubs, false);
			Assert.AreEqual(4, scores.Count());
			Assert.AreEqual(8, SumScores(scores));
		}

		[Test]
		public void givenAHandWithRunOf3_whenEvaluatingFullHandOtherThanCrib_thenReturnsRunScoreOf3()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.Eight, Card.SuitType.Clubs),
				new Card(Card.FaceType.Nine, Card.SuitType.Diamonds),
				new Card(Card.FaceType.Ten, Card.SuitType.Spades),
				new Card(Card.FaceType.King, Card.SuitType.Hearts)
			};

			PlayScore[] scores = Evaluation.EvaluateFullHand(cards, StarterCard_QueenOfClubs, false);
			Assert.AreEqual(1, scores.Count());
			Assert.AreEqual(3, scores[0].Score);
			Assert.AreEqual(PlayScore.ScoreType.Combination_Run, scores[0].Type);
		}

		[Test]
		public void givenAHandWithTwoRunsOf3_whenEvaluatingFullHandOtherThanCrib_thenReturnsRunAndPairScoreOf8()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.Eight, Card.SuitType.Clubs),
				new Card(Card.FaceType.Nine, Card.SuitType.Diamonds),
				new Card(Card.FaceType.Ten, Card.SuitType.Spades),
				new Card(Card.FaceType.Eight, Card.SuitType.Hearts)
			};

			PlayScore[] scores = Evaluation.EvaluateFullHand(cards, StarterCard_QueenOfClubs, false);
			Assert.AreEqual(2, scores.Count());
			Assert.AreEqual(8, SumScores(scores));
		}

		[Test]
		public void givenAHandWithFlushHearts_whenEvaluatingFullHandOtherThanCrib_thenReturnsRunScoreOf3()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.Eight, Card.SuitType.Hearts),
				new Card(Card.FaceType.Six, Card.SuitType.Hearts),
				new Card(Card.FaceType.Ten, Card.SuitType.Hearts),
				new Card(Card.FaceType.King, Card.SuitType.Hearts)
			};

			PlayScore[] scores = Evaluation.EvaluateFullHand(cards, StarterCard_QueenOfClubs, false);
			Assert.AreEqual(1, scores.Count());
			Assert.AreEqual(4, scores[0].Score);
			Assert.AreEqual(PlayScore.ScoreType.Combination_Flush, scores[0].Type);
		}


		[Test]
		public void givenAHandWithFlushHearts_whenEvaluatingCrib_thenReturnsRunScoreOf0()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.Eight, Card.SuitType.Hearts),
				new Card(Card.FaceType.Six, Card.SuitType.Hearts),
				new Card(Card.FaceType.Ten, Card.SuitType.Hearts),
				new Card(Card.FaceType.King, Card.SuitType.Hearts)
			};

			PlayScore[] scores = Evaluation.EvaluateFullHand(cards, StarterCard_QueenOfClubs, true);
			Assert.AreEqual(0, scores.Count());
		}

		[Test]
		public void givenAHandAndStarterWithFlushClubs_whenEvaluatingFullHandOtherThanCrib_thenReturnsRunScoreOf5()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.Eight, Card.SuitType.Clubs),
				new Card(Card.FaceType.Six, Card.SuitType.Clubs),
				new Card(Card.FaceType.Ten, Card.SuitType.Clubs),
				new Card(Card.FaceType.King, Card.SuitType.Clubs)
			};

			PlayScore[] scores = Evaluation.EvaluateFullHand(cards, StarterCard_QueenOfClubs, false);
			Assert.AreEqual(1, scores.Count());
			Assert.AreEqual(5, scores[0].Score);
			Assert.AreEqual(PlayScore.ScoreType.Combination_Flush, scores[0].Type);
		}

		[Test]
		public void givenAHandAndStarterWithFlushClubs_whenEvaluatingCrib_thenReturnsRunScoreOf5()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.Eight, Card.SuitType.Clubs),
				new Card(Card.FaceType.Six, Card.SuitType.Clubs),
				new Card(Card.FaceType.Ten, Card.SuitType.Clubs),
				new Card(Card.FaceType.King, Card.SuitType.Clubs)
			};

			PlayScore[] scores = Evaluation.EvaluateFullHand(cards, StarterCard_QueenOfClubs, true);
			Assert.AreEqual(1, scores.Count());
			Assert.AreEqual(5, scores[0].Score);
			Assert.AreEqual(PlayScore.ScoreType.Combination_Flush, scores[0].Type);
		}

		//-------------------------

		private int SumScores(PlayScore[] scores)
		{
			int total = 0;
			foreach (PlayScore score in scores)
			{
				total += score.Score;
			}
			return total;
		}

	}
}
