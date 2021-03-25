using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.Exceptions
{
	public class CribNotProvidedException : Exception
	{
		public CribNotProvidedException(string reason) : base(reason)
		{
		}
	}
}
