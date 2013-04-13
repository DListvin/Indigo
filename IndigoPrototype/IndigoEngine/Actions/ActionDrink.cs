using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine
{
    /// <summary>
    /// Action to drink
    /// </summary>
    [Serializable]
    class ActionDrink : Action
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Constructors

		public ActionDrink(Agent argSubj, Agent argObj)
			: base(argSubj, argObj)
        {
            IsConflict = true;
            AcceptedSubj.Add(typeof(AgentLivingIndigo));
            AcceptedObj.Add(typeof(AgentPuddle));
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
        public override void Perform()
        {
            base.Perform();
            Object.CurrentActionFeedback = new ActionFeedback(() =>
            {
                Object.CurrentState.Health.CurrentUnitValue--;
            });

            Subject.CurrentActionFeedback = new ActionFeedback(() =>
            {
                (Subject as AgentLiving).CurrentState.Thirst.CurrentPercentValue = 100;
            });

        }

        public override string ToString()
        {
            return "Action: drink ";
        }

		/// <summary>
		/// Override Action.CompareTo
		/// </summary>
		public int CompareTo(ActionBreakCamp argActionToCompare)
		{
			if (base.CompareTo(argActionToCompare) == 0)
			{
				if(Object == argActionToCompare.Object)
				{
					return 0;
				}
			}
			return 1;
		}
    }
}
