using CribbageEngine.Play;
using CribbageEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.AI.Strategy
{
	/// <summary>
	/// Another approach for both dealer and non-dealer which
	/// populates the crib with whatever remains after optimizing
	/// the points that can be achieved in the count of the hand.
	/// </summary>
	public class OptimalHandCribStrategy : ICribStrategy
	{
		public Card[] BankCribCards(bool isDealer, Card[] activeCards)
		{
			List<PlayingHand> bestPlayingHands = CardHelperFunctions.GetBestPlayingHands(activeCards);
			if (bestPlayingHands.Count() == 1)
			{
				return CardHelperFunctions.ExtractCribCards(activeCards, bestPlayingHands[0]);
			}

			int maxCribValue = 0;
			Card[] maxCrib = new Card[0];
			int minCribValue = int.MaxValue;
			Card[] minCrib = maxCrib;
			foreach (PlayingHand hand in bestPlayingHands)
			{
				Card[] crib = CardHelperFunctions.ExtractCribCards(activeCards, hand);
				int value = crib[0].Value + crib[1].Value;
				if (value > maxCribValue)
				{
					maxCribValue = value;
					maxCrib = crib;
				}
				if (value < minCribValue)
				{
					minCribValue = value;
					minCrib = crib;
				}
			}
			
			if (isDealer)
			{
				return maxCrib;
			}
			return minCrib;
		}
	}
}
