using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine.Actions
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
		public override bool CheckForLegitimacy()
		{
			if(!base.CheckForLegitimacy())
			{
				return false;
			}

			if(Object is AgentTree)
			{
				if (!Object.Inventory.ExistsAgentByType(typeof(AgentItemFoodFruit)))
				{
					return false;
				}
			}

			return true;
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
