using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine.Actions
{
	/// <summary>
	/// Attack action.
	/// </summary>
    [Serializable]
    public class ActionAttack : ActionAbstract
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
																				typeof(AgentLivingIndigo),
																			},
																			new List<Skill>()
																			{
																			},
																			false,
																			true
																		);

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

            if (Object.CurrentState.Health.CurrentPercentValue <= 10)
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
