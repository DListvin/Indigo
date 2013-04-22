using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Class for item agents. I think, it is just for structure. Nothing realy important here
	/// </summary>
    [Serializable]
	public abstract class AgentItem : Agent
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
	}
}
