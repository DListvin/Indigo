using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine
{
    /// <summary>
    /// Action to Obtain Resourse
    /// </summary>
    class ActionObtainResourse : Action
    {
        Type resourseType;
        #region Constructors

        public ActionObtainResourse(Agent argObj, Agent argSubj, Type resType)
            : base(argObj, argSubj)
        {
            MayBeConflict = true;
            AcceptedSubj.Add(typeof(AgentLiving));
            AcceptedSubj.Add(typeof(AgentLivingIndigo));
            AcceptedObj.Add(typeof(AgentTree));
            resourseType = resType;
        }

        #endregion

        /// <summary>
        /// ITypicalAction
        /// </summary>
        public override void Perform()
        {
            base.Perform();
            Agent res = Object.Inventory.GetNoDeleteAgentByType(resourseType);
            if (!Object.Inventory.ExistsAgentByType(resourseType))
                return;//may be wrong;
            Object.CurrentActionFeedback = new ActionFeedback(() =>
            {
                Object.Inventory.DeleteAgentsByType(resourseType);
            });

            Subject.CurrentActionFeedback = new ActionFeedback(() =>
            {
                Subject.Inventory.AddAgentToStorage(res);
            });

        }

        public override string ToString()
        {
            return "Action: eat";
        }
    }
}
