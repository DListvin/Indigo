using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine
{
	/// <summary>
	/// Attack action.
	/// </summary>
    public class ActionAttack : Action
    {
        int hitPointsToTakeOff;

		#region Constructors

			public ActionAttack(Agent argObj, Agent argSubj, int argHitPointsToTakeOff) 
				: base(argObj, argSubj)
			{
				HitPointsToTakeOff = argHitPointsToTakeOff;
				MayBeConflict = false;
				AcceptedObj.Add(typeof(AgentLiving));
				AcceptedObj.Add(typeof(AgentLivingIndigo));
				AcceptedSubj.Add(typeof(AgentLiving));
				AcceptedSubj.Add(typeof(AgentLivingIndigo));
			}

		#endregion

		#region Properties

		public int HitPointsToTakeOff
		{
			get { return hitPointsToTakeOff; }
			set { hitPointsToTakeOff = value; }
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
					(Object as AgentLiving).AgentsShortMemory.StoreAction(Subject, this);
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

		public override string ToString()
		{
			return "Action: " + Name + "; hp: " + HitPointsToTakeOff.ToString();
		}
    }
}
