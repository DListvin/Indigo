﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine.Actions
{
    /// <summary>
    /// Action to Eat
    /// </summary>
    [Serializable]
    class ActionEat : ActionAbstract
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
																				typeof(AgentItemFruit),
																			},
																			new List<Skill>()
																			{
																			},
																			true,
																			true
																		);

		#region Constructors

		public ActionEat(Agent argSubj, Agent argObj)
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
		public override int CompareTo(ActionAbstract argActionToCompare)
		{
			if(Object == (argActionToCompare as ActionEat).Object)
			{
				return 0;
			}
			return 1;
		}
    }
}