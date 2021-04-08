using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.Play
{
	public class RoundPlayer
	{
		private List<PlayScore> _playScores = new List<PlayScore>();

		public RoundPlayer(Round round, Player player)
		{
			this.Round = round;
			this.Player = player;
		}

		public void AddScore(PlayScore playScore)
		{
			_playScores.Add(playScore);
			Player.AddScore(playScore.Score);
		}

		public void AddScore(PlayScore.ScoreType type, int count)
		{
			this.AddScore(new PlayScore(type, count));
		}

		public Player Player { get; private set; }

		private Round Round { get; set; }

		public bool HasCards()
		{
			return Player.HasCards();
		}
		
		public List<PlayScore> Scores
		{
			get
			{
				return _playScores;
			}
		}

		public IPlayResponse Play(CountSession currentCount)
		{
			IPlayResponse response = Player.Play();

			if (response is Pass)
			{
				Round.NextPlayer.AddScore();
			}
			// tally play score
			AddScore();

			return response;
		}
	}
}
