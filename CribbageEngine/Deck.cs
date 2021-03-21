using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine
{
    public class Deck
    {
        public const int NumCards = Card.NumFaces * Card.NumSuits;

        private readonly Card[] _cards;

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

        public void Shuffle()
        {
            //TODO: It might be better to refactor this random object
            Random r = new Random();
            //Swaps every card with a random position (can be itself)
            //Ensures virtually infinite entropy I think
            for(int i = 0; i < NumCards; i++)
            {
                //Stores swapcard because random
                int swap = r.Next(NumCards);

                //Swap
                Card temp = _cards[i];
                _cards[i] = _cards[swap];
                _cards[swap] = temp;
            }
            //Resets top of the deck to 0
            _top = 0;
        }
        
        public Card Draw()
        {
            //Returns top card, moves top position down one
            if (_top < NumCards)
            {
                return _cards[_top++];
            }
            else
            {
                throw new IndexOutOfRangeException("Out of cards error!");
            }
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
