using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.Play
{
    public class Card
    {
        public enum SuitType
        {
            Spades, Hearts, Clubs, Diamonds
        }

        public enum FaceType
        {
            Ace = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King
        }

        public const int NumSuits = 4;
        public const int NumFaces = 13;

        internal const int FaceCardValue = 10;

        internal readonly FaceType[] FaceCards = { FaceType.Jack, FaceType.Queen, FaceType.King };

        internal Card(int card) 
            : this((FaceType)(card % NumFaces + 1), (SuitType)(card / NumFaces)) { }

        public Card(FaceType face, SuitType suit)
		{
            Face = face;
            Suit = suit;
		}

        public override string ToString()
        {
            return Face.ToString() + " of " + Suit.ToString();
        }

        public SuitType Suit { get; private set; }
        public FaceType Face { get; private set; }

        public int Value
        {
            get
            {
                if (FaceCards.Contains(Face))
                {
                    return FaceCardValue;
                }
            return (int)(Face);
            }
        }

        public override bool Equals(object obj)
        {
            return this == (Card)obj;
        }

        public static bool operator ==(Card lhs, Card rhs)
        {
            if (ReferenceEquals(lhs, null) && ReferenceEquals(rhs, null))
            {
                return true;
            }
            else if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
            {
                return false;
            }
            return lhs.Suit == rhs.Suit && lhs.Face == rhs.Face;
        }

        public static bool operator !=(Card lhs, Card rhs)
        {
            return !(lhs == rhs);
        }

		public override int GetHashCode()
		{
            return (int)this.Face + ((int)this.Suit * NumFaces);
		}
	}
}