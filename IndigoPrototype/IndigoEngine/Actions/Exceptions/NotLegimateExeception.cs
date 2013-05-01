using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.ActionsOld
{
	class NotLegimateExeception : Exception
	{
        public NotLegimateExeception()
        {
        }

        public NotLegimateExeception(string message) 
			: base(message)
        {
        }

        public NotLegimateExeception(string message, Exception inner)
            : base(message, inner)
        {
        }
	}
}
