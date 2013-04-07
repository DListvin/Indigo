using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine
{
    /// <summary>
    /// Action to drink
    /// </summary>
    class ActionDrink : Action
    {
        #region Constructors

        public ActionDrink(Agent argObj, Agent argSubj)
            : base(argObj, argSubj)
        {
            MayBeConflict = true;
            AcceptedSubj.Add(typeof(AgentLiving));
            AcceptedSubj.Add(typeof(AgentLivingIndigo));
            AcceptedObj.Add(typeof(AgentPuddle));
        }

        #endregion

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
                (Subject.CurrentState as StateLiving).Thirst.CurrentPercentValue = 100;
            });

        }

        public override string ToString()
        {
            return "Action: drink ";
        }
    }
}
