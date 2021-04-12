using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.Play
{
    public abstract class Player
    {
        protected Player() 
        {
            this.Score = 0;
        }

        protected Player(string name) : this()
		{
            this.Name = name;
		}

        public string Name { get; private set; }

        public bool IsDealer { get; set; }

        public abstract void AcceptDealCard(Card card);

        public abstract Card[] GetHand();

        public abstract Card[] GetPlayHand();

        public abstract bool HasCards();

        public int Score { get; private set;  }

        public void AddScore(int points)
		{
            this.Score += points; 
		}

        public abstract Card[] BankCribCards();

        public abstract IPlayResponse Play(Card[] sessionCards);
    }
}
