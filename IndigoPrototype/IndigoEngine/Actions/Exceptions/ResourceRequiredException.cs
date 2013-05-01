using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.ActionsOld
{
	class ResourceRequiredException : Exception
	{
        public ResourceRequiredException()
        {
        }

        public ResourceRequiredException(string message) 
			: base(message)
        {
        }

        public ResourceRequiredException(string message, Exception inner)
            : base(message, inner)
        {
        }
	}
}
