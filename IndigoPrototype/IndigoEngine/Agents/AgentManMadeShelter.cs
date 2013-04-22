using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;


namespace IndigoEngine.Agents
{
	/// <summary>
	/// Basic class for man-made shelters. Apparently, only for structure. At least in the prototype
	/// </summary>
    [Serializable]
	public abstract class AgentManMadeShelter : AgentManMade
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
	}
}
