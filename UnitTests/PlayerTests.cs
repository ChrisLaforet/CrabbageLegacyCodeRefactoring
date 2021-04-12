using CribbageEngine.Play;
using NUnit.Framework;


namespace UnitTests
{
	partial class PlayerTests
	{
		public const string PLAYER1_NAME = "Johnny";
		public const string PLAYER2_NAME = "Suzie";

		[Test]
		public void givenAPlayerWithoutCards_whenAddCardToHand_thenReturnsCardWhenQueried()
		{
			Card card = new Card(Card.FaceType.Queen, Card.SuitType.Clubs);
			Player player = new TestPlayer();
			player.AcceptDealCard(card);
			Assert.AreEqual(card, player.GetHand()[0]);
		}

		[Test]
		public void givenNothing_whenCreatingAPlayerWithAName_thenReturnsTheNameWhenQueried()
		{
			Player player = new TestPlayer(PLAYER1_NAME);
			Assert.AreEqual(PLAYER1_NAME, player.Name);
		}
	}
}
