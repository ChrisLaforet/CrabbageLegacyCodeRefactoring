using CribbageEngine.Play;
using CribbageEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.AI.Strategy
{
	public class OptimalPlayStrategy : IPlayStrategy
	{
		public Card SelectNextCard(bool isDealer, Card[] sessionCards, Card[] activeCards)
		{
			bool isFirstCard = sessionCards.Count() == 0;
			bool isExistingPair = DetermineIfExistingPair(sessionCards);
			int currentTotal = CardHelperFunctions.CountScoreOfCards(sessionCards);

			if (currentTotal >= (PlayScore.THIRTY_ONE_SCORE - 10))
			{
				Card card = TryForScoringPair(activeCards, sessionCards[sessionCards.Count() - 1], currentTotal);
				if (card != null)
				{
					return card;
				}
				card = TryForScoring31(activeCards, currentTotal);
				if (card != null)
				{
					return card;
				}
			}
			else if (currentTotal >= PlayScore.FIFTEEN_SCORE)
			{
				Card card = TryForScoringPair(activeCards, sessionCards[sessionCards.Count() - 1], currentTotal);
				if (card != null)
				{
					return card;
				}
			}
			else if (isFirstCard)
			{
				return TryForBestPossibleFirstCard(activeCards);
			}
			else if (currentTotal < PlayScore.FIFTEEN_SCORE)
			{
				Card card;
				if (isExistingPair)
				{
					card = TryForScoringPair(activeCards, sessionCards[sessionCards.Count() - 1], currentTotal);
					if (card != null)
					{
						return card;
					}
				}
				card = TryForScoring15(activeCards, currentTotal);
				if (card != null)
				{
					return card;
				}
				if (!isExistingPair)
				{
					card = TryForScoringPair(activeCards, sessionCards[sessionCards.Count() - 1], currentTotal);
					if (card != null)
					{
						return card;
					}
				}
			}
			return TryForLargestPossibleCard(activeCards, currentTotal);
		}

		private bool DetermineIfExistingPair(Card[] activeCards)
		{
			if (activeCards.Count() < 2)
			{
				return false;
			}
			return activeCards[activeCards.Count() - 1].Face == activeCards[activeCards.Count() - 2].Face;
		}

		private Card TryForScoring31(Card[] activeCards, int currentTotal)
		{
			int pendingTotal = PlayScore.THIRTY_ONE_SCORE - currentTotal;
			foreach (Card card in activeCards)
			{
				if (card.Value == pendingTotal)
				{
					return card;
				}
			}
			return null;
		}

		private Card TryForLargestPossibleCard(Card[] activeCards, int currentTotal)
		{
			Card largest = null;
			foreach (Card card in activeCards)
			{
				if (card.Value + currentTotal <= PlayScore.THIRTY_ONE_SCORE)
				{
					if (largest == null || largest.Value < card.Value)
					{
						largest = card;
					}
				}
			}
			return largest;
		}

		private Card TryForBestPossibleFirstCard(Card[] activeCards)
		{
			Card best = null;
			Card secondBest = null;
			foreach (Card card in activeCards)
			{
				if (card.Value == 5 && secondBest == null)
				{
					secondBest = card;
				}
				else if (card.Value == 10 && secondBest != null && secondBest.Value != 10)
				{
					secondBest = card;
				}
				else if (card.Value != 5 && card.Value != 10)
				{
					if (best == null || best.Value < card.Value)
					{
						best = card;
					}
					best = card;
				}
			}
			return best != null ? best : secondBest;
		}

		private Card TryForScoringPair(Card[] activeCards, Card lastCardPlayed, int currentTotal)
		{
			if (currentTotal + lastCardPlayed.Value > PlayScore.THIRTY_ONE_SCORE)
			{
				return null;
			}
			foreach (Card card in activeCards)
			{
				if (card.Value == lastCardPlayed.Value)
				{
					return card;
				}
			}
			return null;
		}

		private Card TryForScoring15(Card[] activeCards, int currentTotal)
		{
			if (currentTotal < (PlayScore.FIFTEEN_SCORE - 10))
			{
				return null;
			}
			foreach (Card card in activeCards)
			{
				if (card.Value + currentTotal == PlayScore.FIFTEEN_SCORE)
				{
					return card;
				}
			}
			return null;
		}

		private Card TryToNotPlay5(Card[] activeCards, int currentTotal)
		{
			if (currentTotal >= 5)
			{
				return null;
			}
			foreach (Card card in activeCards)
			{
				if (card.Value + currentTotal != 5)
				{
					return card;
				}
			}
			return null;
		}
	}
}
