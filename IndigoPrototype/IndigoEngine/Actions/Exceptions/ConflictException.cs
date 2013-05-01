using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.ActionsOld
{
	class ConflictException : Exception
	{
        public ConflictException()
        {
        }

        public ConflictException(string message) 
			: base(message)
        {
        }

        public ConflictException(string message, Exception inner)
            : base(message, inner)
        {
        }
	}
}
