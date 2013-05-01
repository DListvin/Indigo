using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;


namespace IndigoEngine.ActionsOld
{
    /// <summary>
    /// Action to Obtain food. For structure
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
						typeof(AgentItemFoodFruit)
					},
					IsConflict = true,
					RequiresObject = true
				)]
	public abstract class ActionObtainFood : ActionObtain
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Constructors

			public ActionObtainFood(Agent argSubj, Agent argObj)
				: base(argSubj, argObj)
			{
			}

        #endregion	
	}
}
