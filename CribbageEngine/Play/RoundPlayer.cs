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

			Round.RackScore(this, playScore);
		}

		public void AddScores(IEnumerable<PlayScore> playScores)
		{
			foreach (PlayScore playScore in playScores)
			{
				this.AddScore(playScore);
			}
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

		public int Score
		{
			get
			{
				return Player.Score;
			}
		}

		public IPlayResponse Play(Card[] sessionCards)
		{
			IPlayResponse response = Player.Play(sessionCards);
			if (response is Pass)
			{
				Round.NextPlayer.AddScore(PlayScore.ScoreType.Play_Go, Evaluation.GoValue);
			}
			return response;
		}
	}
}
