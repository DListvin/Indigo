using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine.Actions
{
	class ActionDoNothing : Action
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		#region Constructors

			public ActionDoNothing(Agent argSubj)
			: base(argSubj, null)
			{
				MayBeConflict = false;
				RequiresObject = false;
				AcceptedSubj.Add(typeof(AgentLivingIndigo));
				AcceptedSubj.Add(typeof(AgentCamp));
				AcceptedSubj.Add(typeof(AgentItemFruit));
				AcceptedSubj.Add(typeof(AgentItemLog));
				AcceptedSubj.Add(typeof(AgentPuddle));
				AcceptedSubj.Add(typeof(AgentTree));
			}

		#endregion
	}
}
