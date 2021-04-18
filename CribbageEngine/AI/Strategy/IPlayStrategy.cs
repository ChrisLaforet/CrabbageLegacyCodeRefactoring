using CribbageEngine.Play;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.AI.Strategy
{
	public interface IPlayStrategy
	{
		Card SelectNextCard(bool isDealer, Card[] sessionCards, Card[] activeCards);
	}
}
