using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.Play
{
	public class PlayScore
	{
		public enum ScoreType
		{
			HisHeels,
			Play_Fifteen,
			Play_Pair,
			Play_Triplet,
			Play_Four,
			Play_Sequence,
			Play_Go,
			Play_ThirtyOne,
			Combination_Fifteen,
			Combination_Pair,
			Combination_Run,
			Combination_Flush,
			HisNobs,
		};

		public PlayScore(ScoreType type, int score)
		{
			this.Type = type;
			this.Score = score;
		}

		public int Score { get; private set; }
		public ScoreType Type { get; private set; }
	}
}
