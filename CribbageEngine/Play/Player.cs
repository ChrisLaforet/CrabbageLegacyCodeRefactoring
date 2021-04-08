using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.Play
{
    public abstract class Player
    {
        private readonly List<Card> _hand = new List<Card>();

        protected Player() 
        {
            this.Score = 0;
        }

        protected Player(string name) : this()
		{
            this.Name = name;
		}

        public string Name { get; private set; }

        public void AddCard(Card card)
        {
            _hand.Add(card);
        }

        public bool IsDealer { get; set; }

        public Card[] GetHand()
        {
            return _hand.ToArray();
        }

        public bool HasCards()
		{
            return _hand.Count > 0;
		}

        public int Score { get; private set;  }

        public void AddScore(int points)
		{
            this.Score += points; 
		}

        public abstract IPlayResponse Play(CountSession currentCount);

        //public int ScoreHand(Card starter) 
        //{
        //    return Evaluation.EvaluateFullHand(GetHand(), starter);
        //}
    }
}
