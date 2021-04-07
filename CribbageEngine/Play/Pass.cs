using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.Play
{
	public class Pass : IPlayResponse
	{
		public static readonly Pass PassResponse = new Pass();

		private Pass() { }
	}
}
