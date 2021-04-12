using CribbageEngine.Exceptions;
using CribbageEngine.Play;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.AI
{
	public class AIPlayer : Player
	{
		private List<Card> _activeCards = new List<Card>();
		private List<Card> _playHand = new List<Card>();

		private ICribStrategy _cribCardSelector;
		private IPlayStrategy _playCardSelector;

		public AIPlayer(ICribStrategy cribCardSelector, IPlayStrategy playCardSelector) 
			: base("AIPlayer")
		{
			this._cribCardSelector = cribCardSelector;
			this._playCardSelector = playCardSelector;
		}

		public override void AcceptDealCard(Card card)
		{
			_activeCards.Add(card);
		}

		public override Card[] BankCribCards()
		{
			Card[] toBank = _cribCardSelector.BankCribCards(IsDealer, _activeCards.ToArray());
			if (toBank.Count() != Round.PER_PLAYER_CRIB_CARD_COUNT)
			{
				throw new InvalidCribCardCountException("Strategy returned " + toBank.Count() + " cards instead of required " + Round.PER_PLAYER_CRIB_CARD_COUNT);
			}
			foreach (Card card in toBank) 
			{
				_activeCards.Remove(card);
			}
			_playHand = new List<Card>(_activeCards);

			return toBank;
		}

		public override Card[] GetHand()
		{
			return _activeCards.ToArray();
		}

		public override Card[] GetPlayHand()
		{
			return _playHand.ToArray();
		}

		public override bool HasCards()
		{
			return _activeCards.Count != 0;
		}

		public override IPlayResponse Play(Card[] sessionCards)
		{
			int total = 0;
			foreach (Card card in sessionCards)
			{
				total += card.Value;
			}

			if (_activeCards.Count > 0)
			{
				Card possible = _playCardSelector.SelectNextCard(IsDealer, sessionCards, _activeCards.ToArray());
				if (possible != null &&
					(total + possible.Value) <= PlayScore.THIRTY_ONE_SCORE)
				{
					_activeCards.Remove(possible);
					return possible;
				}
			}
			return Pass.PassResponse;
		}
	}
}
