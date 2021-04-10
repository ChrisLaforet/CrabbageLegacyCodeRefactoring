using CribbageEngine.Play;
using System;

namespace UnitTests
{
	public class TestPlayer : Player
	{
		public TestPlayer() : base() { }

		public TestPlayer(String playerName) : base(playerName) { }

		public override Card[] BankCribCards()
		{
			throw new NotImplementedException();
		}

		public override IPlayResponse Play(Card[] sessionCards)
		{
			throw new NotImplementedException();
		}
	}
}

