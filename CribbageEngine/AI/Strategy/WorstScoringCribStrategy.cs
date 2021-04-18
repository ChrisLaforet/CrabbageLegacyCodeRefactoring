using CribbageEngine.Play;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.AI.Strategy
{
	/// <summary>
	/// The opposite of the BestScoring strategy, best reserved
	/// for a player who will not get the crib back.
	/// </summary>	public class WorstScoringCribStrategy : ICribStrategy
	public class WorstScoringCribStrategy : ICribStrategy
	{
		public Card[] BankCribCards(bool isDealer, Card[] activeCards)
		{
			throw new NotImplementedException();
		}
	}
}
