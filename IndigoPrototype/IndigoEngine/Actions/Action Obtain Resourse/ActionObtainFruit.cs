using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine.Actions
{
    /// <summary>
    /// Action to Obtain Resourse
    /// </summary>
    [Serializable]
    class ActionObtainFruit : ActionAbstract
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
																				typeof(AgentItemFruit)
																			},
																			new List<Skill>()
																			{
																				Skills.Gathering,
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
			 
			if(!ActionAbstract.CheckForSkills(Subject, CurrentActionInfo.RequiredSkills))
			{
				return false;
			}

			if(Object is AgentTree)
			{
				if (!Object.Inventory.ExistsAgentByType(typeof(AgentItemFruit)))
				{
					return false;
				}
			}
			if(Object is AgentItemFruit)
			{
				if(Object.CurrentLocation.HasOwner)
				{
					return false;
				}
			}

			return true;
		}

        /// <summary>
        /// ITypicalAction
        /// </summary>
        public override void CalculateFeedbacks()
        {
            base.CalculateFeedbacks();

			if(Object is AgentTree)
			{
				Subject.CurrentActionFeedback += new ActionFeedback(() =>
				{
					Subject.Inventory.AddAgentToStorage(Object.Inventory.PopAgentByType(typeof(AgentItemFruit)));
				});
			}
			if(Object is AgentItemFruit)
			{
				Subject.CurrentActionFeedback += new ActionFeedback(() =>
				{
					Subject.Inventory.AddAgentToStorage(Object);
				});
			}

        }

        public override string ToString()
        {
            return "Action: Obtain fruit";
        }

		/// <summary>
		/// Override Action.CompareTo
		/// </summary>
		public override int CompareTo(ActionAbstract argActionToCompare)
		{
			if(Object == (argActionToCompare as ActionObtainFruit).Object)
			{
				return 0;
			}
			return 1;
		}
    }
}
