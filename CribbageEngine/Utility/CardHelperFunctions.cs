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

		public static int CountScoreOfCards(IEnumerable<Card> cards)
		{
			int total = 0;
			foreach (Card card in cards)
			{
				if (card != null)
					total += card.Value;
			}
			return total;
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

		public static List<CardPair> FindPairedCards(IEnumerable<Card> cards, bool excludeJacks)
		{
			List<CardPair> matches = new List<CardPair>();
			for (int index = 0; index < cards.Count(); index++)
			{
				Card card = cards.ElementAt(index);
				if (card.Face == Card.FaceType.Jack && excludeJacks)
				{
					continue;
				}
				for (int index2 = index + 1; index2 < cards.Count(); index2++)
				{
					Card card2 = cards.ElementAt(index2);
					if (card.Face == card2.Face)
					{
						matches.Add(new CardPair(card, card2));
					}
				}
			}
			return matches;
		}

		public static List<CardPair> FindPairsThatMatchFive(IEnumerable<Card> cards)
		{
			List<CardPair> matches = new List<CardPair>();
			for (int index = 0; index < cards.Count(); index++)
			{
				Card card = cards.ElementAt(index);
				if (card.Value > 4)
				{
					continue;
				}
				for (int index2 = index + 1; index2 < cards.Count(); index2++)
				{
					Card card2 = cards.ElementAt(index2);
					if (card.Value + card2.Value == 5)
					{
						matches.Add(new CardPair(card, card2));
					}
				}
			}
			return matches;
		}

		public static List<CardPair> FindPairsThatMatchFifteen(IEnumerable<Card> cards, bool excludeJacks)
		{
			List<CardPair> matches = new List<CardPair>();
			for (int index = 0; index < cards.Count(); index++)
			{
				Card card = cards.ElementAt(index);
				if (card.Face == Card.FaceType.Jack && excludeJacks)
				{
					continue;
				}
				for (int index2 = index + 1; index2 < cards.Count(); index2++)
				{
					Card card2 = cards.ElementAt(index2);
					if (card2.Face == Card.FaceType.Jack && excludeJacks)
					{
						continue;
					}

					if (card.Value + card2.Value == 15)
					{
						matches.Add(new CardPair(card, card2));
					}
				}
			}
			return matches;
		}

		public static List<PlayingHand> GetBestPlayingHands(IEnumerable<Card> cards)
		{
			Dictionary<int, List<PlayingHand>> score = new Dictionary<int, List<PlayingHand>>();
			int maxValue = 0;
			for (int index = 0; index < (cards.Count() - 3); index++)
			{
				for (int index2 = index + 1; index2 < (cards.Count() - 2); index2++)
				{
					for (int index3 = index2 + 1; index3 < (cards.Count() - 1); index3++)
					{
						for (int index4 = index3 + 1; index4 < cards.Count(); index4++)
						{
							PlayingHand hand = new PlayingHand(cards.ElementAt(index),
												cards.ElementAt(index2),
												cards.ElementAt(index3),
												cards.ElementAt(index4));

							int value = hand.Value;
							if (!score.ContainsKey(value))
							{
								score.Add(value, new List<PlayingHand>());
							}
							score[value].Add(hand);
							if (value > maxValue)
							{
								maxValue = value;
							}
						}
					}
				}
			}
			return score[maxValue];
		}

		public static Card[] ExtractCribCards(IEnumerable<Card> cards, PlayingHand playingHand)
		{
			List<Card> crib = new List<Card>();
			foreach (Card card in cards)
			{
				if (!playingHand.ContainsCard(card))
				{
					crib.Add(card);
				}
			}

			return crib.ToArray();
		}
	}
}
