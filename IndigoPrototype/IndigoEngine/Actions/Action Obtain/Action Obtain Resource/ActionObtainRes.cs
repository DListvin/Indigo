using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;


namespace IndigoEngine.Actions
{
    /// <summary>
    /// Action to Obtain resource. For structure
    /// </summary>
    [Serializable]
	[ActionInfo(
					new Type[]
					{
						typeof(AgentLivingIndigo),
					},
					new Type[]
					{
						typeof(AgentTree),
						typeof(AgentItemResLog)
					},
					IsConflict = true,
					RequiresObject = true
				)]
	public abstract class ActionObtainRes : ActionObtain
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Constructors

			public ActionObtainRes(Agent argSubj, Agent argObj)
				: base(argSubj, argObj)
			{
			}

        #endregion	
	}
}
