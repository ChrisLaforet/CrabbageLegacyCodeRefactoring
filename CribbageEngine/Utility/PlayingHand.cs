using CribbageEngine.Exceptions;
using CribbageEngine.Play;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.Utility
{
	public class PlayingHand
	{
		private Card[] _cards = new Card[4];

		public PlayingHand(Card card1, Card card2, Card card3, Card card4)
		{
			_cards[0] = card1;
			_cards[1] = card2;
			_cards[2] = card3;
			_cards[3] = card4;
		}

		public PlayingHand(Card[] cards)
		{
			if (cards.Count() != _cards.Length)
			{
				throw new InvalidStateException("Invalid number of cards provided for PlayingHand");
			}
			for (int index = 0; index < _cards.Length; index++)
			{
				_cards[index] = cards[index];
			}
		}

		public int Value
		{
			get
			{
				int value = 0;
				foreach (Card card in _cards)
				{
					value += card.Value;
				}
				return value;
			}
		}

		public Card[] Cards
		{
			get
			{
				return _cards;
			}
		}

		public bool ContainsCard(Card card)
		{
			foreach (Card toCheck in _cards)
			{
				if (toCheck.Equals(card))
				{
					return true;
				}
			}
			return false;
		}
	}
}
