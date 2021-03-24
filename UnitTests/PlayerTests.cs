using CribbageEngine.Play;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
	class PlayerTests
	{
		public const string PLAYER1_NAME = "Johnny";
		public const string PLAYER2_NAME = "Suzie";

		[Test]
		public void givenAPlayerWithoutCards_whenAddCardToHand_thenReturnsCardWhenQueried()
		{
			Card card = new Card(Card.FaceType.Queen, Card.SuitType.Clubs);
			Player player = new Player();
			player.AddCard(card);
			Assert.AreEqual(card, player.GetHand()[0]);
		}

		[Test]
		public void givenNothing_whenCreatingAPlayerWithAName_thenReturnsTheNameWhenQueried()
		{
			Player player = new Player(PLAYER1_NAME);
			Assert.AreEqual(PLAYER1_NAME, player.Name);
		}
	}
}
