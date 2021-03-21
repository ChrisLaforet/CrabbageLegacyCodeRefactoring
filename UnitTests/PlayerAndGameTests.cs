using CribbageEngine.Play;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
	class PlayerAndGameTests
	{
		[Test]
		public void givenAPlayerWithoutCards_whenAddCardToHand_thenReturnsCardWhenQueried()
		{
			Card card = new Card(Card.FaceType.Queen, Card.SuitType.Clubs);
			Player player = new Player();
			player.AddCard(card);
			Assert.AreEqual(card, player.GetHand()[0]);

		}
	}
}
