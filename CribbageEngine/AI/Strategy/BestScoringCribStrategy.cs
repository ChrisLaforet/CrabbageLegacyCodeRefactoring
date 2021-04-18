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
	/// Hands-down, looks for the highest scoring pair of cards
	/// that can be in the crib to benefit the player.  Would make
	/// no sense to use this strategy unless player is the dealer!
	/// </summary>
	public class BestScoringCribStrategy : ICribStrategy
	{
		public Card[] BankCribCards(bool isDealer, Card[] activeCards)
		{
			// having a 5 or two in the crib make for interesting matches
			// with the most common value card which is 10
			int fives = CardHelperFunctions.CountCardsWithFaceType(activeCards, Card.FaceType.Five);
			if (fives >= 2)
			{
				return CardHelperFunctions.FindCardsWithFaceType(activeCards, Card.FaceType.Five).GetRange(0, 2).ToArray();
			}

			// Ok, let's try a 5 and a 10 value being careful with possible
			// Nobs in the crib if not the dealer
			int tenValues = CardHelperFunctions.CountCardsWithValueOfTen(activeCards, !isDealer);
			if (fives == 1 && tenValues > 0)
			{
				List<Card> cardsOfTen = CardHelperFunctions.FindCardsWithValueOfTen(activeCards, !isDealer);

				Card[] crib = new Card[2];
				crib[0] = CardHelperFunctions.FindCardsWithFaceType(activeCards, Card.FaceType.Five)[0];
				crib[1] = cardsOfTen[0];
			}

			// pairs are next in value

			// combinations that make 5 are next

			// two 10 values are next (in case a 5 combination shows up from the other hand)


			// finally, throw whatever cannot hurt the playing hand



			throw new NotImplementedException();
		}
	}
}
