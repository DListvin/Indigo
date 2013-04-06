using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine
{
    class ActionEat : Action
    {
		#region Constructors

        public ActionEat(Agent argObj, Agent argSubj) 
			: base(argObj, argSubj)
        {
            MayBeConflict = true;
            AcceptedObj.Add( typeof(AgentLiving));
            AcceptedObj.Add(typeof(AgentLivingIndigo));
            AcceptedSubj.Add(typeof(AgentItemFruit));
        }

		#endregion

		/// <summary>
		/// ITypicalAction
		/// </summary>
        public override void Perform()
        {
            if (Object.Health.CurrentPercentValue > 60)
            {
                Object.CurrentActionFeedback = new ActionFeedback(() => 
				{
                    Object.Health.CurrentUnitValue = 0;
				});

                Subject.CurrentActionFeedback = new ActionFeedback(() => 
				{
                    (Subject as AgentLivingIndigo).Hunger.CurrentPercentValue = 100;
				});
            }
        }

		public override string ToString()
		{
            return "Action: eat";
		}
    }
}
