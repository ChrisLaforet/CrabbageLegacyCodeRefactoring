using CribbageEngine.Play;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.AI.Strategy
{
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
}
