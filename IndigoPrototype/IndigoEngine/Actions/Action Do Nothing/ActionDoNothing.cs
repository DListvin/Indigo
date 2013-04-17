using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine.Actions
{
    [Serializable]
	class ActionDoNothing : ActionAbstract
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public static InfoAboutAction CurrentActionInfo = new InfoAboutAction
																		(
																			new List<Type>()
																			{
																				typeof(AgentLivingIndigo),
																				typeof(AgentCamp),
																				typeof(AgentItemFruit),
																				typeof(AgentItemLog),
																				typeof(AgentPuddle),
																				typeof(AgentTree)
																			},
																			new List<Type>()
																			{
																			},
																			new List<Skill>()
																			{
																			},
																			false,
																			false
																		);

		#region Constructors

			public ActionDoNothing(Agent argSubj)
			: base(argSubj, null)
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

		public override void CalculateFeedbacks()
		{
			base.CalculateFeedbacks();
			if(Subject is AgentLivingIndigo)
			{
				(Subject as AgentLivingIndigo).CurrentState.Stamina.CurrentUnitValue++;
				(Subject as AgentLivingIndigo).CurrentState.Strenght.CurrentUnitValue++;
				(Subject as AgentLiving).CurrentState.Peacefulness.CurrentUnitValue--;  //They are becoming more aggressive. Spike for attackig each other
			}
		}

		public override int CompareTo(ActionAbstract argActionToCompare)
		{
			return 1; //Action isn't conflict
		}
	}
}
