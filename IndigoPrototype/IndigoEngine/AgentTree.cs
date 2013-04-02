using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine
{
	class AgentTree : Agent
	{		
		#region Constructors
			
			public AgentTree() : base()
			{
			}

		#endregion

		public override string ToString()
		{
			return "Tree: " + Name;
		}
	}
}
