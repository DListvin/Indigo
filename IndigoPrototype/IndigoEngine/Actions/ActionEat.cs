using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine
{
    /// <summary>
    /// Action to Eat
    /// </summary>
    class ActionEat : Action
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

		#region Constructors

		public ActionEat(Agent argSubj, Agent argObj)
			: base(argSubj, argObj)
        {
            IsConflict = true;
            AcceptedSubj.Add(typeof(AgentLivingIndigo));
            AcceptedObj.Add(typeof(AgentItemFruit));
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
                Object.CommitSuicide();
            });

            Subject.CurrentActionFeedback = new ActionFeedback(() =>
            {
                (Subject as AgentLiving).CurrentState.Hunger.CurrentPercentValue = 100;
            });

        }

		public override string ToString()
		{
            return "Action: eat";
		}

		/// <summary>
		/// Override Action.CompareTo
		/// </summary>
		public override int CompareTo(Action argActionToCompare)
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
