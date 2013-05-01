using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine.ActionsOld
{
	/// <summary>
	/// Attack action.
	/// </summary>
    [Serializable]
	[ActionInfo(
					new Type[]
					{
						typeof(AgentLivingIndigo),
					},
					new Type[]
					{
						typeof(AgentLivingIndigo),
					},
					IsConflict = false,
					RequiresObject = true					
				)]
    public class ActionAttack : ActionAbstract
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();		

		#region Constructors

		public ActionAttack(Agent argSubj, Agent argObj, params object[] argHitPointsToTakeOff)
			: base(argSubj, argObj)
		{
			if(argHitPointsToTakeOff.Length == 0)
			{
				HitPointsToTakeOff = 1;
			}
			else
			{
				HitPointsToTakeOff = (int)argHitPointsToTakeOff[0];
			}
		}

		#endregion

		#region Properties

			public int HitPointsToTakeOff { get; set; }

		#endregion
		
		/// <summary>
		/// ITypicalAction
		/// </summary>
		public override Exception CheckForLegitimacy()
		{
			if(base.CheckForLegitimacy() != null)
			{
				return base.CheckForLegitimacy();
			}

            if (Object.CurrentState.Health.CurrentPercentValue <= 10)
            {
				return new NotLegimateExeception();
            }
			return null;
		}
		
		/// <summary>
		/// ITypicalAction
		/// </summary>
        public override void CalculateFeedbacks()
        {
            base.CalculateFeedbacks();
           
		    Object.CurrentActionFeedback += new ActionFeedback(() => 
			{
				Object.CurrentState.Health.CurrentUnitValue -= HitPointsToTakeOff; 
				(Object as AgentLiving).CurrentState.Peacefulness.CurrentUnitValue--;
			});

            Subject.CurrentActionFeedback += new ActionFeedback(() => 
			{
				(Subject as AgentLiving).CurrentState.Stamina.CurrentUnitValue--;
				(Subject as AgentLiving).CurrentState.Peacefulness.CurrentUnitValue++;
			});
        }

		/// <summary>
		/// ITypicalAction
		/// </summary>
		public override NameableObject CharacteristicsOfSubject()
		{
			if(Object is AgentLivingIndigo && Subject is AgentLivingIndigo)
			{
				return (Subject as AgentLivingIndigo).CurrentState.Peacefulness; 
			}

			return base.CharacteristicsOfSubject();
		}

		public override int CompareTo(ActionAbstract argActionToCompare)
		{
			return 1; //Action isn't conflict
		}

		public override string ToString()
		{
			return "Action: " + Name + "; hp: " + HitPointsToTakeOff.ToString();
		}
    }
}
