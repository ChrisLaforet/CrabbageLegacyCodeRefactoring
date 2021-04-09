using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.Play
{
	public interface IScoreBoard
	{
		void RackScore(RoundPlayer player, PlayScore newScore);
	}
}
