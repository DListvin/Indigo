using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine.ActionsOld
{
    /// <summary>
    /// Action to Obtain log
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
					"Woodcutting",
					IsConflict = true,
					RequiresObject = true
				)]
    class ActionObtainResLog : ActionObtainRes
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Constructors

			public ActionObtainResLog(Agent argSubj, Agent argObj)
				: base(argSubj, argObj)
			{
			}

        #endregion		

        /// <summary>
        /// ITypicalAction
        /// </summary>
        public override void CalculateFeedbacks()
        {
            base.CalculateFeedbacks();			

			if(Object is AgentTree)
			{
				Object.CurrentActionFeedback += new ActionFeedback(() =>
				{
					Object.CurrentState.Health.CurrentUnitValue -= 50;
				});
			}	
        }

		/// <summary>
		/// Override Action.CompareTo
		/// </summary>
		public override int CompareTo(ActionAbstract argActionToCompare)
		{
			if(Object == (argActionToCompare as ActionObtainResLog).Object)
			{
				return 0;
			}
			return 1;
		}
    }
}
