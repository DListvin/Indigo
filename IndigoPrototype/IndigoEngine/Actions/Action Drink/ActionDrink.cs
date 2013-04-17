using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine.Actions
{
    /// <summary>
    /// Action to drink
    /// </summary>
    [Serializable]
    class ActionDrink : ActionAbstract
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
																				typeof(AgentPuddle),
																			},
																			new List<Skill>()
																			{
																			},
																			true,
																			true
																		);

        #region Constructors

		public ActionDrink(Agent argSubj, Agent argObj)
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
                Object.CurrentState.Health.CurrentUnitValue--;
            });

            Subject.CurrentActionFeedback += new ActionFeedback(() =>
            {
                (Subject as AgentLiving).CurrentState.WaterSatiety.CurrentPercentValue = 100;
            });

        }

        public override string ToString()
        {
            return "Action: drink ";
        }

		/// <summary>
		/// Override Action.CompareTo
		/// </summary>
		public override int CompareTo(ActionAbstract argActionToCompare)
		{
			if(Object == (argActionToCompare as ActionDrink).Object)
			{
				return 0;
			}
			return 1;
		}
    }
}
