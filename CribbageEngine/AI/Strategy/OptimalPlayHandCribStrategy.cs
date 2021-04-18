using CribbageEngine.Play;
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
			throw new NotImplementedException();
		}
	}
}
