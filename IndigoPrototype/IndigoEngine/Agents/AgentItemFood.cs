using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;


namespace IndigoEngine.Agents
{
	/// <summary>
	/// Again structure class
	/// </summary>
    [Serializable]
	public abstract class AgentItemFood : AgentItem
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
	}
}
