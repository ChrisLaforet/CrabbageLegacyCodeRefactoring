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

        //The numerical value assigned to the face cards
        internal const int FaceCardValue = 10;

        //This must be readonly because the values assigned to Faces aren't known at compiletime
        internal readonly FaceType[] FaceCards = { FaceType.Jack, FaceType.Queen, FaceType.King };

        internal Card(int card) 
            : this((FaceType)(card % NumFaces + 1), (SuitType)(card / NumFaces))
        {
            ////Assigns all 13 faces for each suit
            //_face = (FaceType)(card % NumFaces + 1);
            ////Every 13 cards, suit changes
            //_suit = (SuitType)(card / NumFaces);
        }

        public Card(FaceType face, SuitType suit)
		{
            _face = face;
            _suit = suit;
		}

        public override string ToString()
        {
            //Used for debugging
            return Face.ToString() + " of " + Suit.ToString();
        }


        // TODO: convert into proper properties (get/set)
        private readonly SuitType _suit;
        private readonly FaceType _face;

        public SuitType Suit { get { return _suit; } }
        public FaceType Face { get { return _face; } }

        public int Value
        {
            get
            {
                if (FaceCards.Contains(Face))
                {
                    //If the card is a Facecard its value is always 10
                    return FaceCardValue;
                }
                else
                {
                    //If a card is not a Facecard its value is its face
                    return (int)(Face);
                }
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