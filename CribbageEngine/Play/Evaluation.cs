using CribbageEngine.Exceptions;
using CribbageEngine.Play;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.Play
{
    public static class Evaluation
    {
        public const int GAME_WINNING_SCORE = 121;

        //If there was a HAND class or something, these could be methods
        //But they should probably be static, logically
        //The rules of cribbage don't require cards to exist?

        public const int FifteenValue = 2;
        public const int PairValue = 2;
        public const int KnobsValue = 1;
        public const int HeelsValue = 2;
        public const int GoValue = 1;
        public const int ThirtyOneValue = 2;

        public static PlayScore[] EvaluatePlayHand(Card[] playOrderedCards)
		{
            List<PlayScore> playScores = new List<PlayScore>();

            int runningScore = TallyRunningScoreFor(playOrderedCards);
            if (runningScore > PlayScore.THIRTY_ONE_SCORE)
			{
                throw new InvalidStateException("A play session cannot exceed " + PlayScore.THIRTY_ONE_SCORE + " points");
			}
            else if (runningScore == PlayScore.FIFTEEN_SCORE)
			{
                playScores.Add(new PlayScore(PlayScore.ScoreType.Play_Fifteen, FifteenValue));
			} 
            else if (runningScore == PlayScore.THIRTY_ONE_SCORE)
			{
                playScores.Add(new PlayScore(PlayScore.ScoreType.Play_ThirtyOne, ThirtyOneValue));
            }

            if (playOrderedCards.Length > 1)
			{
                Card[] lastPlayedOrder = playOrderedCards.Reverse().ToArray();
                Card.FaceType toMatch = lastPlayedOrder[0].Face;
                int matches = 0;
                for (int index = 1; index < lastPlayedOrder.Length; index++)
				{
                    if (lastPlayedOrder[index].Face != toMatch)
					{
                        break;
					}
                    ++matches;
				}
                
                switch (matches)
				{
                    case 1:
                        playScores.Add(new PlayScore(PlayScore.ScoreType.Play_Pair, PairValue));
                        break;

                    case 2:
                        playScores.Add(new PlayScore(PlayScore.ScoreType.Play_Triplet, PairValue * 3));
                        break;

                    case 3:
                        playScores.Add(new PlayScore(PlayScore.ScoreType.Play_Four, PairValue * 6));
                        break;
				}

                int runCount = 1;
                int lastValue = lastPlayedOrder[0].Value;
                bool isAscending = false;
                for (int index = 1; index < lastPlayedOrder.Length; index++)
				{
                    int value = lastPlayedOrder[index].Value;
                    if (index == 1)
					{
                        if (lastValue + 1 == value)
						{
                            isAscending = true;
						}
					}
                    if (isAscending && value == lastValue + 1 ||
                        !isAscending && value == lastValue - 1)
					{
                        ++runCount;
                        lastValue = value;
					}
                    else
					{
                        break;
					}
				}
                if (runCount >= 3)
				{
                    playScores.Add(new PlayScore(PlayScore.ScoreType.Play_Run, runCount));
				}
            }
            return playScores.ToArray();
		}

        private static int TallyRunningScoreFor(Card[] cards)
		{
            int runningScore = 0;
            foreach (Card card in cards)
			{
                runningScore += card.Value;
			}
            return runningScore;
		}

        public static PlayScore[] EvaluateFullHand(Card[] playCards, Card starterCard, bool isCrib)
        {
            List<PlayScore> playScores = new List<PlayScore>();

            // Evaluates the 5 cards according to the rules of Cribbage
            // The cut "starter" card is treated differently than the rest for knobs and flush purposes

            var faces = new Card.FaceType[playCards.Count() + 1];
            var values = new int[playCards.Count() + 1];

            for (int index = 0; index < playCards.Count(); index++)
            {
                faces[index] = playCards[index].Face;
                values[index] = playCards[index].Value;
            }
            faces[playCards.Count()] = starterCard.Face;
            values[playCards.Count()] = starterCard.Value;

            playScores.AddRange(FindFifteens(values));
            playScores.AddRange(FindPairs(faces));
            playScores.AddRange(FindRuns(playCards, starterCard));
            PlayScore playScore = FindKnobs(playCards, starterCard);
            if (playScore != null)
			{
                playScores.Add(playScore);
			}
            playScore = IsFlush(playCards, starterCard, isCrib);
            if (playScore != null)
            {
                playScores.Add(playScore);
            }

            return playScores.ToArray();
        }

        private static PlayScore[] FindFifteens(int[] values)
        {
            //HOYLE RULES:
            //Every way to make 15 in your hand is worth 2 points each

            List<PlayScore> playScores = new List<PlayScore>();
            int sum = 0;

            //Looks at every possible combo you can make from cards in hand
            foreach (var combo in HelperFunctions.GetPowerSet(values.Count()))
            {
                foreach (int index in combo)
                {
                    sum += values[index];
                    if (sum > PlayScore.FIFTEEN_SCORE) 
                        break;
                }
                if (sum == PlayScore.FIFTEEN_SCORE)
                {
                    playScores.Add(new PlayScore(PlayScore.ScoreType.Combination_Fifteen, FifteenValue));
                }
                sum = 0;
            }
            return playScores.ToArray();
        }

        private static PlayScore[] FindPairs(Card.FaceType[] faces)
        {
            List<PlayScore> playScores = new List<PlayScore>();

            //Handshake all of the cards together - pretty simple
            for (int startIndex = 0; startIndex < faces.Count(); startIndex++)
            {
                for (int remainingIndex = startIndex + 1; remainingIndex < faces.Count(); remainingIndex++)
                {
                    if (faces[startIndex] == faces[remainingIndex])
                    {
                        playScores.Add(new PlayScore(PlayScore.ScoreType.Combination_Pair, PairValue));
                    }
                }
            }

            return playScores.ToArray();
        }

        private static PlayScore[] FindRuns(Card[] playCards, Card starterCard)
        {
            var faceValMap = new int[(int)Card.FaceType.King + 1];
            for (int index = 0; index < playCards.Count(); index++)
            {
                faceValMap[(int)playCards[index].Face]++;
            }
            faceValMap[(int)starterCard.Face]++;

            List<PlayScore> playScores = new List<PlayScore>();
            int multiplier = 0;
            int totalInSequence = 0;

            for (int index = 0; index < faceValMap.Count(); index++)
            {
                //Run is over - or hasn't begin
                if (faceValMap[index] == 0)
                {
                    //When leaving run, generate point report
                    if (totalInSequence >= 3)
                    {
                        playScores.Add(new PlayScore(PlayScore.ScoreType.Combination_Run, multiplier * totalInSequence));
                    }
                    totalInSequence = 0;
                    multiplier = 0;
                }
                else
                {
                    if (multiplier == 0)
                    {
                        multiplier = 1;
                    }
                    //Consecutive multiplier allows double-double run to be counted as 4
                    multiplier *= faceValMap[index];
                    totalInSequence++;
                }
            }

            if (totalInSequence >= 3)
            {
                playScores.Add(new PlayScore(PlayScore.ScoreType.Combination_Run, multiplier * totalInSequence));
            }
            return playScores.ToArray();
        }

        private static PlayScore FindKnobs(Card[] playCards, Card starterCard)
        {
            //HOYLE RULES:
            //If a Jack in your hand has a suit matching that of the 
            //card that gets cut up, you have knobs

            //Knobs can't exist if the cut card is a Jack
            if (starterCard.Face == Card.FaceType.Jack)
                return null;

            foreach (Card card in playCards)
            {
                //Check all cards for knobs
                if (card.Face == Card.FaceType.Jack && card.Suit == starterCard.Suit)
                {
                    return new PlayScore(PlayScore.ScoreType.HisNobs, KnobsValue);
                }
            }
            return null;
        }

        private static PlayScore IsFlush(Card[] playCards, Card starterCard, bool isCrib)
        {
            //HOYLE RULES:
            // If all the cards in the hand are the same suit: 4 points (1 per card?)
            // If the cut card IS ALSO the same suit as the rest: 5 points (1 extra point)
            // If counting crib, flush requires all hand cards and started to be the same suit for 5 pts

            for (int index = 1; index < playCards.Length; index++)
			{
                if (playCards[index].Suit != playCards[0].Suit)
				{
                    return null;
				}
			}

            if (starterCard.Suit == playCards[0].Suit)
			{
                return new PlayScore(PlayScore.ScoreType.Combination_Flush, 5);
			}
            else if (!isCrib)
			{
                return new PlayScore(PlayScore.ScoreType.Combination_Flush, 4);
			}
            return null;
        }
    }
}
