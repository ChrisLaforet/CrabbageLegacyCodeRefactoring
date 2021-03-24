using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.Play
{
	public class Round
	{
		public Round(Game game) => this.Game = game;

		public Game Game { get; private set; }
	}
}
