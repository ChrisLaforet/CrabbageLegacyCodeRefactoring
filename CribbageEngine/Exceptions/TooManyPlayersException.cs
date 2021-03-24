using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.Exceptions
{
	public class TooManyPlayersException : Exception
	{
		public TooManyPlayersException(string reason) : base(reason)
		{
		}
	}
}
