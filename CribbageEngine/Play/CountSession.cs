using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.Play
{
	public class CountSession
	{
		private List<Card> _cards = new List<Card>();

		public CountSession(Round round, Player firstPlayer)
		{
			this.Round = round;
			NextPlayer = firstPlayer;
		}

		public Round Round { get; private set; }

		public Player NextPlayer { get; private set; }

		public Card[] PlayedCards
		{
			get
			{
				return _cards.AsReadOnly().ToArray();
			}
		}
			
		internal void AddCard(Card card)
		{
			_cards.Add(card);
		}
	}
}
