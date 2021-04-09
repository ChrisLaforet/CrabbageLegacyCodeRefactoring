using CribbageEngine.Exceptions;
using CribbageEngine.Play;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
	public class ScoreTests
	{
		[Test]
		public void givenASessionPlayExceeding31Points_whenEvaluatingPlayHand_thenThrowsException()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.King, Card.SuitType.Clubs),
				new Card(Card.FaceType.King, Card.SuitType.Diamonds),
				new Card(Card.FaceType.King, Card.SuitType.Spades),
				new Card(Card.FaceType.King, Card.SuitType.Hearts)
			};

			Assert.Throws(typeof(InvalidStateException), () => Evaluation.EvaluatePlayHand(cards));
		}

		[Test]
		public void givenASessionPlayWith15Points_whenEvaluatingPlayHand_thenReturnsPlayScoreOf2()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.King, Card.SuitType.Clubs),
				new Card(Card.FaceType.Five, Card.SuitType.Diamonds)
			};

			PlayScore[] scores = Evaluation.EvaluatePlayHand(cards);
			Assert.AreEqual(1, scores.Length);
			Assert.AreEqual(PlayScore.ScoreType.Play_Fifteen, scores[0].Type);
			Assert.AreEqual(2, scores[0].Score);
		}

		[Test]
		public void givenASessionPlayWith31Points_whenEvaluatingPlayHand_thenReturnsPlayScoreOf2()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.King, Card.SuitType.Clubs),
				new Card(Card.FaceType.Queen, Card.SuitType.Clubs),
				new Card(Card.FaceType.Jack, Card.SuitType.Clubs),
				new Card(Card.FaceType.Ace, Card.SuitType.Diamonds)
			};

			PlayScore[] scores = Evaluation.EvaluatePlayHand(cards);
			Assert.AreEqual(1, scores.Length);
			Assert.AreEqual(PlayScore.ScoreType.Play_ThirtyOne, scores[0].Type);
			Assert.AreEqual(2, scores[0].Score);
		}

		[Test]
		public void givenASessionPlayWith1Pair_whenEvaluatingPlayHand_thenReturnsPlayScoreOf2()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.King, Card.SuitType.Clubs),
				new Card(Card.FaceType.Queen, Card.SuitType.Clubs),
				new Card(Card.FaceType.Queen, Card.SuitType.Diamonds)
			};

			PlayScore[] scores = Evaluation.EvaluatePlayHand(cards);
			Assert.AreEqual(1, scores.Length);
			Assert.AreEqual(PlayScore.ScoreType.Play_Pair, scores[0].Type);
			Assert.AreEqual(2, scores[0].Score);
		}

		[Test]
		public void givenASessionPlayWith3PairFour_whenEvaluatingPlayHand_thenReturnsPlayScoreOf12()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.Four, Card.SuitType.Spades),
				new Card(Card.FaceType.Four, Card.SuitType.Clubs),
				new Card(Card.FaceType.Four, Card.SuitType.Hearts),
				new Card(Card.FaceType.Four, Card.SuitType.Diamonds)
			};

			PlayScore[] scores = Evaluation.EvaluatePlayHand(cards);
			Assert.AreEqual(1, scores.Length);
			Assert.AreEqual(PlayScore.ScoreType.Play_Four, scores[0].Type);
			Assert.AreEqual(12, scores[0].Score);
		}

		[Test]
		public void givenASessionPlayWith2PairFAndFifteenour_whenEvaluatingPlayHand_thenReturnsPlayScoreOf8()
		{
			Card[] cards = new Card[]
			{
				new Card(Card.FaceType.Five, Card.SuitType.Spades),
				new Card(Card.FaceType.Five, Card.SuitType.Hearts),
				new Card(Card.FaceType.Five, Card.SuitType.Diamonds)
			};

			PlayScore[] scores = Evaluation.EvaluatePlayHand(cards);
			Assert.AreEqual(2, scores.Length);

			int sum = scores[0].Score + scores[1].Score;
			Assert.AreEqual(8, sum);
		}
	}
}
