using CribbageEngine.Play;
using System;

namespace UnitTests
{
	public class TestPlayer : Player
	{
		private Card[] _mockCribCards = new Card[0];

		public TestPlayer() : base() { }

		public TestPlayer(String playerName) : base(playerName) { }

		public void SetMockCribCards(Card[] cards)
		{
			_mockCribCards = cards;
		}

		public override Card[] BankCribCards()
		{
			return _mockCribCards;
		}

		public override IPlayResponse Play(Card[] sessionCards)
		{
			throw new NotImplementedException();
		}
	}
}

