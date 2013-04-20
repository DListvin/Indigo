using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine.Actions
{
    /// <summary>
    /// Action to Eat
    /// </summary>
    [Serializable]
	[ActionInfo(
					new Type[]
					{
						typeof(AgentLivingIndigo),
					},
					new Type[]
					{
						typeof(AgentItemFruit),
					},
					IsConflict = true,
					RequiresObject = true
				)]
    class ActionEat : ActionAbstract
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

		#region Constructors

		public ActionEat(Agent argSubj, Agent argObj)
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
			return true;
		}

		/// <summary>
		/// ITypicalAction
		/// </summary>
        public override void CalculateFeedbacks()
        {
            base.CalculateFeedbacks();

            Object.CurrentActionFeedback += new ActionFeedback(() =>
            {
                Object.CommitSuicide();
            });

            Subject.CurrentActionFeedback += new ActionFeedback(() =>
            {
                (Subject as AgentLiving).CurrentState.FoodSatiety.CurrentPercentValue = 100;
            });

        }

		public override string ToString()
		{
            return "Action: eat";
		}

		/// <summary>
		/// Override Action.CompareTo
		/// </summary>
		public override int CompareTo(ActionAbstract argActionToCompare)
		{
			if(Object == (argActionToCompare as ActionEat).Object)
			{
				return 0;
			}
			return 1;
		}
    }
}
