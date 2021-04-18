using CribbageEngine.Play;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.Utility
{
	public static class CardHelperFunctions
	{
		public static int CountCardsWithFaceType(IEnumerable<Card> cards, Card.FaceType faceType)
		{
			int matches = 0;
			foreach (Card card in cards)
			{
				if (card.Face == faceType)
				{
					matches++;
				}
			}
			return matches;
		}

		public static int CountCardsWithValueOfTen(IEnumerable<Card> cards, bool excludeJacks)
		{
			int matches = 0;
			foreach (Card card in cards)
			{
				if (card.Face == Card.FaceType.Jack && excludeJacks)
				{
					continue;
				}
				if (card.Value == 10)
				{
					matches++;
				}
			}
			return matches;
		}

		public static List<Card> FindCardsWithFaceType(IEnumerable<Card> cards, Card.FaceType faceType)
		{
			List<Card> matches = new List<Card>();
			foreach (Card card in cards)
			{
				if (card.Face == faceType)
				{
					matches.Add(card);
				}
			}
			return matches;
		}

		public static List<Card> FindCardsWithValueOfTen(IEnumerable<Card> cards, bool excludeJacks)
		{
			List<Card> matches = new List<Card>();
			foreach (Card card in cards)
			{
				if (card.Face == Card.FaceType.Jack && excludeJacks)
				{
					continue;
				}
				if (card.Value == 10)
				{
					matches.Add(card);
				}
			}
			return matches;
		}

		public static List<Card[]> FindPairs(IEnumerable<Card> cards, bool excludeJacks)
		{

		}
	}
}
