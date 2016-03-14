using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace Gipper
{
	internal class BadInputException : GipperException
	{
		// constructors
		public BadInputException()
			: base("Bad input")
		{
		}

	
		// properties
		public string ErrorMessage
		{
			get;
			set;
		}

		public string BadInput
		{
			get;
			set;
		}
	}
}
