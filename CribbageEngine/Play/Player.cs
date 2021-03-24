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

        public Player() 
        {
            this.Score = 0;
        }

        public Player(string name) : this()
		{
            this.Name = name;
		}

        public string Name { get; private set; }

        public void AddCard(Card card)
        {
            _hand.Add(card);
        }

        public Card[] GetHand()
        {
            return _hand.ToArray();
        }

        public int Score { get; private set; }

        //public int ScoreHand(Card starter) 
        //{
        //    return Evaluation.EvaluateFullHand(GetHand(), starter);
        //}
    }
}
