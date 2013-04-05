using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	class AgentCamp : Agent
	{	
		#region Constructors
			
			public AgentCamp() : base()
			{
			}

		#endregion

		public override string ToString()
		{
			return "Camp: " + Name;
		}
	}
}
