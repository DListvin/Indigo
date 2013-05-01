using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.ActionsOld
{
	class SkillRequiredException : Exception
	{
        public SkillRequiredException()
        {
        }

        public SkillRequiredException(string message) 
			: base(message)
        {
        }

        public SkillRequiredException(string message, Exception inner)
            : base(message, inner)
        {
        }
	}
}
