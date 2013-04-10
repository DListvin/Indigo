using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine
{
    /// <summary>
    /// Action to Obtain Resourse
    /// </summary>
    class ActionObtainResourse : Action
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

        Type resourseType;
        #region Constructors

		public ActionObtainResourse(Agent argSubj, Agent argObj, Type resType)
			: base(argSubj, argObj)
        {
            MayBeConflict = true;
            AcceptedSubj.Add(typeof(AgentLivingIndigo));
            AcceptedObj.Add(typeof(AgentTree));
            resourseType = resType;
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

            if (!Object.Inventory.ExistsAgentByType(resourseType))
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

            Subject.CurrentActionFeedback = new ActionFeedback(() =>
            {
                Subject.Inventory.AddAgentToStorage(Object.Inventory.GetAgentByTypeFromStorage(resourseType));
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
				if(Object == argActionToCompare.Object)
				{
					return 0;
				}
			}
			return 1;
		}
    }
}
