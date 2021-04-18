using CribbageEngine.Play;

namespace CribbageEngine.Utility
{
	public class CardPair
	{
		private Card[] _pair = new Card[2];

		public CardPair(Card card1, Card card2)
		{
			_pair[0] = card1;
			_pair[1] = card2;
		}

		public Card[] Cards
		{
			get
			{
				return _pair;
			}
		}
	}
}
