using CribbageEngine.Play;
using System;
using System.Collections.Generic;

namespace UnitTests
{
	public class TestPlayer : Player
	{
		private Stack<Card> _hand = new Stack<Card>();

		private Card[] _playHand;

		public TestPlayer() : base() { }

		public TestPlayer(String playerName) : base(playerName) { }

		public bool DoShortCribCards { get; set; }

		public override Card[] BankCribCards()
		{
			if (DoShortCribCards)
				return new Card[0];

			Card[] crib = new Card[2];
			crib[0] = _hand.Pop();
			crib[1] = _hand.Pop();

			_playHand = GetHand();
			return crib;
		}

		public override IPlayResponse Play(Card[] sessionCards)
		{
			int total = 0;
			foreach (Card card in sessionCards)
			{
				total += card.Value;
			}

			if (_hand.Count > 0)
			{
				Card possible = _hand.Peek();
				if (total + possible.Value <= PlayScore.THIRTY_ONE_SCORE)
					return _hand.Pop();
			}
			return Pass.PassResponse;
		}

		public override void AcceptDealCard(Card card)
		{
			_hand.Push(card);
		}

		public override Card[] GetHand()
		{
			return _hand.ToArray();
		}

		public override Card[] GetPlayHand()
		{
			return _playHand;
		}

		public override bool HasCards()
		{
			return _hand.Count > 0;
		}
	}
}

