using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.Play
{
	public class Round
	{
		public const int INITIAL_DEAL_CARD_COUNT = 6;
		public const int PER_PLAYER_CRIB_CARD_COUNT = 2;
		private Deck _deck = new Deck();
		private IList<Card> _crib = new List<Card>();

		public Round(Game game)
		{
			this.Game = game;
			this._deck.Shuffle();
		}

		public Game Game { get; private set; }

		public void Start()
		{
			IList<Player> players = Game.Players;
			for (int count = 0; count < INITIAL_DEAL_CARD_COUNT; count++)
			{
				foreach (Player player in Game.Players)
				{
					player.AddCard(_deck.Draw());
				}
			}
		}
	}
}
