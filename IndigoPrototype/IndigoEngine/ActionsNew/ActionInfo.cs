using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine.ActionsNew
{
	/// <summary>
	/// Class to store some info about action (accepted subjects, required skills e.c.)
	/// </summary>
	public class ActionInfo
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
        
        int paramCount;
        List<Quality> RequiredAgentsQuality;
        List<Skill> RequiredSkills;
	}
}
