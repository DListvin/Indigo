using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Agent for puddle
	/// </summary>
	class AgentPuddle : Agent
	{
		#region Constructors
			
			public AgentPuddle()
				: base()
			{
			}

		#endregion

		public override string ToString()
		{
			return "Puddle: " + Name;
		}
	}
}
