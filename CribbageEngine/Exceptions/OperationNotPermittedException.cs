using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.Exceptions
{
	public class OperationNotPermittedException : Exception
	{
		public OperationNotPermittedException(string reason) : base(reason)
		{
		}
	}
}
