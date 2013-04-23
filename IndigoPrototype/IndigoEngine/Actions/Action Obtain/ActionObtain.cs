using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;


namespace IndigoEngine.Actions
{
    /// <summary>
    /// Action to Obtain something. For structure
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
						typeof(AgentItemResLog),
						typeof(AgentItemFoodFruit)
					},
					IsConflict = true,
					RequiresObject = true
				)]
	public abstract class ActionObtain : ActionAbstract
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Constructors

			public ActionObtain(Agent argSubj, Agent argObj)
				: base(argSubj, argObj)
			{
			}

        #endregion	

		public override bool CheckForLegitimacy()
		{
			if(!base.CheckForLegitimacy())
			{
				return false;
			}

			if(Object is AgentItem)
			{
				if(Object.CurrentLocation.HasOwner)
				{
					return false;
				}
			}

			return true;
		}

		public override void CalculateFeedbacks()
		{
			base.CalculateFeedbacks();

			if(Object is AgentItem)
			{
				Subject.CurrentActionFeedback += new ActionFeedback(() =>
				{
					Subject.Inventory.AddAgentToStorage(Object);
				});
			}
		}
	}
}
