using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{	
	/// <summary>
	/// Basic class for alive agents
	/// </summary>
	public abstract class AgentLiving : Agent
	{		
		#region Constructors

			public AgentLiving()
				:base()
			{
                CurrentState = new StateLiving();
				AgentsShortMemory = new ShortMemory();
				AgentsLongMemory = new LongMemory();
			}

		#endregion

        #region Properties
			
			public int RangeOfView { get; set; }                    //Range of view of the agent (in cells around agent, apparently)
			public List<NameableObject> FieldOfView { get; set; }   //Agent's field ov view. Includes all agents & actions, that current agent can see
			public List<Skill> SkillsList { get; set; }             //List of skills that are available to agent
			public ShortMemory AgentsShortMemory { get; set; }      //Agent's short memory
			public LongMemory AgentsLongMemory { get; set; }        //Agent's long memory

        #endregion
		
        /// <summary>
        /// This function is the brain of agent, it decide what to do
        /// </summary>
        public override void Decide()
        {
            base.Decide();
            //Kostya's logick here
            //World must control everything, that I'm trying to do
            //I can try to do everything.

            //looking through ShortMemory to find active tasks
            //if 1 step requied - complite
            Need mainNeed = EstimateMainNeed();
            MakeAction(mainNeed);
        }
		
        /// <summary>
        /// Calculate the best decision of action to satisfy need
        /// </summary>
        /// <param name="argNeed">need, that must be satisfied</param>
        protected virtual void MakeAction(Need argNeed)
        {
            bool worldResponseToAction = false;	//World response if the action is accepted
            if (argNeed.SatisfyingActions.Count == 0)
                throw (new Exception(String.Format("Number of Action to satisfy need {0} is 0", argNeed)));

            foreach (Action act in argNeed.SatisfyingActions)
            {
                bool fl = false;
                act.Subject = this;
                foreach (Agent ag in FieldOfView.Where(val => { return val.GetType() == typeof(Agent); }))
                {
                    foreach (Type t in act.AcceptedObj)
                    {
                        if (t == ag.GetType())
                            fl = true;
                    }
                    if (!fl)
                        continue;
                    act.Object = ag;
                    worldResponseToAction = HomeWorld.AskWorldForAction(act);
                    if (worldResponseToAction)
                        break;
                    fl = false;
                }
                if (worldResponseToAction)
                    break;
            }
        }

        /// <summary>
        /// Calclate resourse, which requied for satisfying need  
        /// </summary>
        /// <param name="need">need that must be satisfyied</param>
        /// <returns></returns>
        protected virtual Type FindNecessaryAgent(Need need)
        {
            return typeof(Agent);
        }

        /// <summary>
        /// Calculate one main need of agent at this moment
        /// </summary>
        /// <returns> main need</returns>
        protected virtual Need EstimateMainNeed()
        {
            //List<Need> allNeed = new List<Need>();
            if ((CurrentState as StateLiving).Hunger.CurrentUnitValue < (CurrentState as StateLiving).Hunger.CriticalValue)
            {
                return Needs.NeedEat;
            }
            else
            {
                return Needs.NeedExample;
            }

        }

		public override void StateRecompute()
		{
			base.StateRecompute();

			AgentsLongMemory.StoreShortMemory(AgentsShortMemory);
			AgentsShortMemory.ForgetAll();
            (CurrentState as StateLiving).Hunger.CurrentUnitValue--;
		}
	}
}
