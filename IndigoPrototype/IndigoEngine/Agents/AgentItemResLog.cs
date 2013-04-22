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
    [Serializable]
	public class AgentItemResLog : AgentItemRes
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		#region Constructors
			
			public AgentItemResLog() 
				: base()
			{
				NeedFromCharacteristic.Add(CurrentState.Health, Needs.NeedNothing);
			}

		#endregion
	}
}
