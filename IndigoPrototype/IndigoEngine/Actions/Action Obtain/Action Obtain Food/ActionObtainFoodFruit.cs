using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine.ActionsOld
{
    /// <summary>
    /// Action to Obtain fruit
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
					"Gathering",
					IsConflict = true,
					RequiresObject = true
				)]
    class ActionObtainFoodFruit : ActionObtainFood
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Constructors

			public ActionObtainFoodFruit(Agent argSubj, Agent argObj)
				: base(argSubj, argObj)
			{
			}

        #endregion		
		
		/// <summary>
		/// ITypicalAction
		/// </summary>
		public override Exception CheckForLegitimacy()
		{
			if(base.CheckForLegitimacy() != null)
			{
				return base.CheckForLegitimacy();
			}

			if(Object is AgentTree)
			{
				if (!Object.Inventory.ExistsAgentByType(typeof(AgentItemFoodFruit)))
				{
					return new NotLegimateExeception();
				}
			}

			return null;
		}

        /// <summary>
        /// ITypicalAction
        /// </summary>
        public override void CalculateFeedbacks()
        {
            base.CalculateFeedbacks();

			if(Object is AgentTree)
			{
				Subject.CurrentActionFeedback += new ActionFeedback(() =>
				{
					Subject.Inventory.AddAgentToStorage(Object.Inventory.PopAgentByType(typeof(AgentItemFoodFruit)));
				});
			}

        }

		/// <summary>
		/// Override Action.CompareTo
		/// </summary>
		public override int CompareTo(ActionAbstract argActionToCompare)
		{
			if(Object == (argActionToCompare as ActionObtainFoodFruit).Object)
			{
				return 0;
			}
			return 1;
		}
    }
}
