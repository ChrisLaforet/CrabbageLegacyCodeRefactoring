using CribbageEngine.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.Play
{
    public class Deck
    {
        public const int NumCards = Card.NumFaces * Card.NumSuits;
        public const int MINIMUM_CUT_CARDS = 6;

        private readonly Card[] _cards;
        private Random rng = new Random();


        //Keeps track of the top of the deck, like a stack
        //Only the deck needs to know where this is
        private int _top;

        public Deck()
        {
            //Allocates card array (the box of cards I guess)
            //Adds all 52 cards to the deck
            _cards = Deck.All;
            
            //Initially shuffles the deck
            Shuffle();
        }

        public static Card[] All
        {
            //This should probably be a readonly singleton to save memory... research later
            get
            {
                var cards = new Card[NumCards];
                for (int i = 0; i < NumCards; i++)
                {
                    cards[i] = new Card(i);
                }
                return cards;
            }
        }

        public int Remaining 
        { 
            get
			{
                return _cards.Length - _top;
			}
        }

        public Card GetStarterCard()
		{
            int cutSize = Remaining - 2 * MINIMUM_CUT_CARDS;
            int cutSpot = rng.Next(0, cutSize) + MINIMUM_CUT_CARDS;
            return _cards[cutSpot];
        }

        public void Shuffle()
        {
            for (int round = 0; round < 2 + rng.Next(3); round++)
			{
                Shuffle(rng);
			}
            //Resets top of the deck to 0
            _top = 0;
        }

        private void Shuffle(Random rng)
		{
            //Swaps every card with a random position (can be itself)
            //Ensures virtually infinite entropy I think
            for (int index = 0; index < NumCards; index++)
            {
                int swap = rng.Next(NumCards);
                if (index != swap)
                {
                    Card temp = _cards[index];
                    _cards[index] = _cards[swap];
                    _cards[swap] = temp;
                }
            }
        }
        
        public Card Draw()
        {
            if (_top < NumCards)
            {
                return _cards[_top++];
            }
            throw new DeckOutOfCardsException("Out of cards error!");
        }
        
        public override string ToString()
        {
            //Used for debugging
            StringBuilder allCards = new StringBuilder(1024);
            foreach (Card card in _cards)
            {
                allCards.Append(card.ToString());
                allCards.Append(Environment.NewLine);
            }
            return allCards.ToString();
        }
    }
}
