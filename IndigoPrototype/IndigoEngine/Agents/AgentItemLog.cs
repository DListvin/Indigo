using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Agent for log
	/// </summary>
	public class AgentItemLog : Agent
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		#region Constructors
			
			public AgentItemLog() 
				: base()
			{
				NeedFromCharacteristic.Add(CurrentState.Health, Needs.NeedNothing);
			}

		#endregion
	}
}
