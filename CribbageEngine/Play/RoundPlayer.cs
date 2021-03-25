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

		public RoundPlayer(Player player)
		{
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
		
		public List<PlayScore> Scores
		{
			get
			{
				return _playScores;
			}
		}
	}
}
