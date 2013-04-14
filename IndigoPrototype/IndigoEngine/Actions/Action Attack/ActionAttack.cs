using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine
{
	/// <summary>
	/// Attack action.
	/// </summary>
    [Serializable]
    public class ActionAttack : Action
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
																			false,
																			true
																		);

		static ActionAttack()
		{
			CurrentActionInfo = new InfoAboutAction
												(
													new List<Type>()
													{
														typeof(AgentLivingIndigo),
													},
													new List<Type>()
													{
														typeof(AgentLivingIndigo),
													},
													false,
													true
												);
		}

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

            if (Object.CurrentState.Health.CurrentUnitValue <= 60)
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
				Object.CurrentState.Health.CurrentUnitValue -= HitPointsToTakeOff; 
				if(Object is AgentLiving)
				{
					(Object as AgentLiving).CurrentMemory.StoreAction(Subject, this);
				}
			});

            Subject.CurrentActionFeedback = new ActionFeedback(() => 
			{
				if(Subject.CurrentState.Health.CurrentUnitValue + HitPointsToTakeOff <= Subject.CurrentState.Health.MaxValue) 
				{
					Subject.CurrentState.Health.CurrentUnitValue += HitPointsToTakeOff; 
				}
			});
        }

		/// <summary>
		/// ITypicalAction
		/// </summary>
		public override NameableObject CharacteristicsOfSubject()
		{
			if(Object is AgentLivingIndigo && Subject is AgentLivingIndigo)
			{
				return (Subject as AgentLivingIndigo).CurrentState.Aggressiveness; 
			}

			return base.CharacteristicsOfSubject();
		}

		public override int CompareTo(Action argActionToCompare)
		{
			return 1; //Action isn't conflict
		}

		public override string ToString()
		{
			return "Action: " + Name + "; hp: " + HitPointsToTakeOff.ToString();
		}
    }
}
