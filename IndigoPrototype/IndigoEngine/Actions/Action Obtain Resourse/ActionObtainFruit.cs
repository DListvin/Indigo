﻿using System;
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
    [Serializable]
    class ActionObtainFruit : Action
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public static InfoAboutAction CurrentActionInfo = new InfoAboutAction
																		(
																			new List<Type>()
																			{
																				typeof(AgentLivingIndigo),
																			},
																			new List<Type>()
																			{
																				typeof(AgentTree),
																			},
																			true,
																			true
																		);

        #region Constructors

			public ActionObtainFruit(Agent argSubj, Agent argObj)
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

            if (!Object.Inventory.ExistsAgentByType(typeof(AgentItemFruit)))
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
                Subject.Inventory.AddAgentToStorage(Object.Inventory.GetAgentByTypeFromStorage(typeof(AgentItemFruit)));
            });

        }

        public override string ToString()
        {
            return "Action: Obtain fruit";
        }

		/// <summary>
		/// Override Action.CompareTo
		/// </summary>
		public override int CompareTo(Action argActionToCompare)
		{
			if(Object == (argActionToCompare as ActionObtainFruit).Object)
			{
				return 0;
			}
			return 1;
		}
    }
}
