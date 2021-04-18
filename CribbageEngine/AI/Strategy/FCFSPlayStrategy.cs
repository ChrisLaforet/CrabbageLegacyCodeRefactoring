using CribbageEngine.Play;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.AI.Strategy
{
	/// <summary>
	/// Foolish basic strategy of tossing the first valid card
	/// that doesn't bust 31 out into the play.
	/// </summary>
	public class FCFSPlayStrategy : IPlayStrategy
	{
		public Card SelectNextCard(bool isDealer, Card[] sessionCards, Card[] activeCards)
		{
			int total = 0;
			foreach (Card card in sessionCards)
			{
				total += card.Value;
			}

			int remainder = PlayScore.THIRTY_ONE_SCORE - total;
			if (remainder < 0 || activeCards.Count() == 0)
			{
				return null;
			}

			foreach (Card card in activeCards)
			{
				if (card.Value <= remainder)
				{
					return card;
				}
			}
			return null;
		}
	}

}
