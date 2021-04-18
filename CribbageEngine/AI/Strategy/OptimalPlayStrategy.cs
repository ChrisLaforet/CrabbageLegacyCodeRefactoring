using CribbageEngine.Play;
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
			// don't lead off with 5

			// if can make 31, with what is in hand, play it

			// if can make 15 with what is in hand, play it

			// if can make pair, do so

			throw new NotImplementedException();
		}
	}
}
