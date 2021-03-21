using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.Play
{
    public class Player
    {
        private List<Card> _hand = new List<Card>();

        public Card CutCard
		{
            get;
            private set;
		}

        public Player() { }

        public Player(Card cutCard)
		{
            CutCard = cutCard;
		}

        public void AddCard(Card card)
        {
            _hand.Add(card);
        }

        public Card[] GetHand()
        {
            return _hand.ToArray();
        }

        public int ScoreHand() 
        {
            return Evaluation.EvaluateFullHand(GetHand(), CutCard);
        }
    }
}
