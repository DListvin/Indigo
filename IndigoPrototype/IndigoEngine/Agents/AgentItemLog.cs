using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	class AgentItemLog : Agent
	{
		#region Constructors
			
			public AgentItemLog() : base()
			{
			}

		#endregion

		public override string ToString()
		{
			return "Item log: " + Name;
		}
	}
}
