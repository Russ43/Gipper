using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gipper
{
	internal class GipperException : Exception
	{
		// constructors
		public GipperException(string message)
			: base(message)
		{
		}
	}
}
