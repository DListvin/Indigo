using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace IndigoEngine.Agents
{	
	/// <summary>
	/// Agent for tree
	/// </summary>
	public class AgentTree : Agent
	{	
		private static Logger logger = LogManager.GetCurrentClassLogger();
			
		#region Constructors
			
			public AgentTree() 
				: base()
			{
			}

		#endregion

		public override string ToString()
		{
			return "Tree: " + Name;
		}
	}
}
