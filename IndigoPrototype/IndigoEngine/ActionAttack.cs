using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine
{
	/// <summary>
	/// Example of action. Delete it after you invent some actual actions
	/// </summary>
    public class ActionAttack : Action
    {
        int hitPointsToTakeOff;

		#region Constructors

		public ActionAttack(Agent argObj, Agent argSubj, int argHitPointsToTakeOff) 
			: base(argObj, argSubj)
        {
            HitPointsToTakeOff = argHitPointsToTakeOff;
            MayBeConflict = true;
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
        public override void Perform()
        {
            base.Perform();
            if (Object.CurrentState.Health.CurrentUnitValue > 60)
            {
                Object.CurrentActionFeedback = new ActionFeedback(() => 
				{
					Object.CurrentState.Health.CurrentUnitValue -= HitPointsToTakeOff; 
					if(Object is AgentLiving)
					{
						((AgentLiving)Object).AgentsShortMemory.StoreAction(Subject, this);
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
        }

		/// <summary>
		/// ITypicalAction
		/// </summary>
		public override NameableObject CharacteristicsOfSubject()
		{
			if(Object is AgentLivingIndigo && Subject is AgentLivingIndigo)
			{
				return (Subject.CurrentState as StateLiving).Aggressiveness; 
			}

			return base.CharacteristicsOfSubject();
		}

		public override string ToString()
		{
			return "Action: " + Name + "; hp: " + HitPointsToTakeOff.ToString();
		}
    }
}
