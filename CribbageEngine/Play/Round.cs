using CribbageEngine.Exceptions;
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

		public bool IsStarted { get; private set; }

		private void AssertIsNotStarted()
		{
			if (IsStarted)
			{
				throw new OperationNotPermittedException("Round has already started");
			}
		}

		private void AssertIsStarted()
		{
			if (!IsStarted)
			{
				throw new OperationNotPermittedException("Round has not started");
			}
		}

		public bool IsPlayStarted
		{
			get
			{
				return StarterCard != null;
			}
		}

		private void AssertPlayIsNotStarted()
		{
			if (IsPlayStarted)
			{
				throw new OperationNotPermittedException("Round play has already started");
			}
		}

		private void AssertPlayIsStarted()
		{
			if (!IsPlayStarted)
			{
				throw new OperationNotPermittedException("Round play has not started");
			}
		}

		public Card StarterCard { get; private set; }

		public Game Game { get; private set; }

		public void BankCribCards(Card[] cards)
		{
			AssertIsStarted();
			AssertPlayIsNotStarted();
			if (cards == null || cards.Length != PER_PLAYER_CRIB_CARD_COUNT)
			{
				throw new InvalidCribCardCountException("Invalid number of cards to bank into crib");
			}
			foreach (Card card in cards)
			{
				_crib.Add(card);
			}
		}

		public void BankCribCards(IList<Card> cards)
		{
			this.BankCribCards(cards.ToArray());
		}

		public IList<Card> Crib
		{
			get
			{
				return _crib;
			}
		}

		public void Start()
		{
			AssertIsNotStarted();
			AssertPlayIsNotStarted();
			IList<Player> players = Game.Players;
			for (int count = 0; count < INITIAL_DEAL_CARD_COUNT; count++)
			{
				foreach (Player player in Game.Players)
				{
					player.AddCard(_deck.Draw());
				}
			}
			this.IsStarted = true;
		}

		public void StartPlay()
		{
			AssertIsStarted();
			AssertPlayIsNotStarted();
			if (_crib.Count != Game.Players.Count * PER_PLAYER_CRIB_CARD_COUNT)
			{
				throw new CribNotProvidedException("Crib cards have not been banked by all players");
			}

			this.StarterCard = _deck.GetStarterCard();
		}
	}
}
