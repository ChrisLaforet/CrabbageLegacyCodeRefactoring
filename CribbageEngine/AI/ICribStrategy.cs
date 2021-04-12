using CribbageEngine.Play;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.AI
{
	public interface ICribStrategy
	{
		Card[] BankCribCards(bool isDealer, Card[] activeCards);
	}

	/// <summary>
	/// Foolish basic stratgy of tossing the first cards out into
	/// the crib.
	/// </summary>
	public class FCFSCribStrategy : ICribStrategy
	{
		public Card[] BankCribCards(bool isDealer, Card[] activeCards)
		{
			return activeCards.Take(2).ToArray();
		}
	}

	/// <summary>
	/// Hands-down, looks for the highest scoring pair of cards
	/// that can be in the crib to benefit the player.  Would make
	/// no sense to use this strategy unless player is the dealer!
	/// </summary>
	public class BestScoringCribStrategy : ICribStrategy
	{
		public Card[] BankCribCards(bool isDealer, Card[] activeCards)
		{
			throw new NotImplementedException();
		}
	}


	/// <summary>
	/// The opposite of the BestScoring strategy, best reserved
	/// for a player who will not get the crib back.
	/// </summary>
	public class WorstScoringCribStrategy : ICribStrategy
	{
		public Card[] BankCribCards(bool isDealer, Card[] activeCards)
		{
			throw new NotImplementedException();
		}
	}
}
