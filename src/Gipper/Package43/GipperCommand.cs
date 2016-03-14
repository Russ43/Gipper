using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gipper
{
	abstract public class GipperCommand
	{
		// methods
		public void Execute()
		{
			Helper.TrapExceptions(() => DoExecute(), "Executing command");
		}


		// protected methods
		abstract protected void DoExecute();
	}
}
