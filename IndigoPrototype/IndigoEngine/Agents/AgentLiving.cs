using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace IndigoEngine.Agents
{	
	/// <summary>
	/// Basic class for alive agents
	/// </summary>
	public abstract class AgentLiving : Agent
	{		
		private static Logger logger = LogManager.GetCurrentClassLogger();

		#region Constructors

			public AgentLiving()
				:base()
			{
                CurrentState = new StateLiving();
				CurrentMemory = new Memory();
				CurrentVision = new Vision();
			}

		#endregion

        #region Properties
				
			/// <summary>
			/// Current state of the agent, override property from Agent to return StateLiving, instead of State
			/// </summary>	
			public new StateLiving CurrentState  
			{
				get
				{
					return base.CurrentState as StateLiving;
				}
				set
				{
					base.CurrentState = value;
				} 
			} 
			public Vision CurrentVision { get; set; }    //Agent's field ov view. Includes all agents & actions, that current agent can see
			public List<Skill> SkillsList { get; set; }  //List of skills that are available to agent
			public Memory CurrentMemory { get; set; }    //Agent's memory

			/// <summary>
			/// Agent's range of view, relative to CurrentVision. Necessary for easier agent management
			/// </summary>
			public int AgentsRangeOfView
			{
				get
				{
					return CurrentVision.RangeOfView;
				}
				set
				{
					CurrentVision.RangeOfView = value;
				}
			}
        
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
            CurrentActionFeedback = null;
            bool worldResponseToAction = false;	//World response if the action is accepted
            if (argNeed.SatisfyingActions.Count == 0)
                throw (new Exception(String.Format("Number of Action to satisfy need {0} is 0", argNeed)));

            foreach (Action act in argNeed.SatisfyingActions)
            {
                act.Subject = this;
                foreach (Agent ag in CurrentVision.CurrentViewAgents)
                {
					if(!act.AcceptedObj.Contains(ag.GetType()))
					{
						continue;
					}
                    act.Object = ag;
                    if (Distance(this, ag) > Math.Sqrt(2))
                    {
                        worldResponseToAction = HomeWorld.AskWorldForAction(new ActionGo( this, ag.Location.Value));
                        if (worldResponseToAction)
                            break;
                    }
                    else
                    {
                        worldResponseToAction = HomeWorld.AskWorldForAction(act);
                        if (worldResponseToAction)
                            break;
                    }
                }
                //if there are not object in currentVision
                //Do smth

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
            List<Need> allNeed = new List<Need>();
            Need need = new Need();
            foreach (Characteristic ch in CurrentState)
            {
                if (ch.CurrentUnitValue < ch.CriticalUnitValue)
                {
                    if (!NeedFromCharacteristic.TryGetValue(ch,out need))
                    {
                        throw new Exception("Ditionary in AgentLiving has error. Cant find need by characteristic!");
                    }
                    allNeed.Add(need);
                }
            }
            if (allNeed.Count == 0)
                return Needs.NeedNothing;
            allNeed.Sort(new Comparison<Need>(Need.Comparing));
            return allNeed[0];

        }

		public override void StateRecompute()
        {
            if (CurrentState.Hunger.CurrentUnitValue-- == CurrentState.Hunger.MinValue) 
            {
                CurrentState.Health.CurrentUnitValue--;
            }
            if (CurrentState.Thirst.CurrentUnitValue-- == CurrentState.Thirst.MinValue)
            {
                CurrentState.Health.CurrentUnitValue--;
            }
            base.StateRecompute();
        }

        public override string ToString()
        {
            return Name + ' ' + CurrentState.ToString() + Location.Value.ToString();
        }
	}
}
