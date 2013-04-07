using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine
{
    /// <summary>
    /// Action to Eat
    /// </summary>
    class ActionEat : Action
    {
		#region Constructors

        public ActionEat(Agent argObj, Agent argSubj) 
			: base(argObj, argSubj)
        {
            MayBeConflict = true;
            AcceptedSubj.Add( typeof(AgentLiving));
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
                ((StateLiving)Subject.CurrentState).Hunger.CurrentPercentValue = 100;
            });

        }

		public override string ToString()
		{
            return "Action: eat";
		}

		/// <summary>
		/// Override Action.CompareTo
		/// </summary>
		public int CompareTo(ActionBreakCamp argActionToCompare)
		{
			if (base.CompareTo(argActionToCompare) == 0)
			{
				if(Subject == argActionToCompare.Subject)
				{
					return 0;
				}
			}
			return 1;
		}
    }
}
