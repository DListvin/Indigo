using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine.Actions
{
	/// <summary>
	/// Action for agent to rest (mainly: restoring health)
	/// </summary>
    [Serializable]
	[ActionInfo(
					new Type[]
					{
						typeof(AgentLivingIndigo),
					},
					new Type[]
					{
						typeof(AgentManMadeShelterCamp),
					},
					IsConflict = true,
					RequiresObject = true
				)]
    class ActionRest : ActionAbstract
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Constructors

			public ActionRest(Agent argSubj, Agent argObj)
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
			return true;
		}

		public override void CalculateFeedbacks()
		{
			base.CalculateFeedbacks();
           
		    Object.CurrentActionFeedback += new ActionFeedback(() => 
			{
				Object.CurrentState.Health.CurrentUnitValue--; 
			});

            Subject.CurrentActionFeedback += new ActionFeedback(() => 
			{
				(Subject as AgentLivingIndigo).CurrentState.Stamina.CurrentUnitValue += 2;
				(Subject as AgentLivingIndigo).CurrentState.Strenght.CurrentUnitValue += 2;
				(Subject as AgentLivingIndigo).CurrentState.Health.CurrentUnitValue++;
			});
		}

		public override int CompareTo(ActionAbstract argActionToCompare)
		{
			if(Object == (argActionToCompare as ActionRest).Object)
			{
				return 0;
			}
			return 1;
		}

    }
}
