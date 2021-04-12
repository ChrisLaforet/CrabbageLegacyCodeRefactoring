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
}
