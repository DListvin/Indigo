﻿using System;
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
				AgentsMemory = new Memory();
                NeedFromCharacteristic.Add(CurrentState.Aggressiveness, Needs.NeedAttack);
                NeedFromCharacteristic.Add(CurrentState.Health,         Needs.NeedCamp);
                NeedFromCharacteristic.Add(CurrentState.Hunger,         Needs.NeedEat);
                NeedFromCharacteristic.Add(CurrentState.Stamina,        Needs.NeedRest);
                NeedFromCharacteristic.Add(CurrentState.Strenght,       Needs.NeedRest);
                NeedFromCharacteristic.Add(CurrentState.Thirst,         Needs.NeedDrink);
			}

		#endregion

        #region Properties
				
			/// <summary>
			/// Current state of the agent
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

			public int RangeOfView { get; set; }                    //Range of view of the agent (in cells around agent, apparently)
			public List<NameableObject> FieldOfView { get; set; }   //Agent's field ov view. Includes all agents & actions, that current agent can see
			public List<Skill> SkillsList { get; set; }             //List of skills that are available to agent
			public Memory AgentsMemory { get; set; }            //Agent's memory
        
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
                foreach (Agent ag in FieldOfView.Where(val => { return val is Agent; }))
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
                if (worldResponseToAction)
                    break;
            }
        }

        /// <summary>
        /// Computes distance between two agents (May be we shoud make static class AgentAlgebra? We'll see if there will be more computational funcs with agents)
        /// </summary>
        /// <param name="agent1">First agent</param>
        /// <param name="agent2">Second agent</param>
        /// <returns>Distance or NaN, if any of agents doesn'n have location</returns>
        double Distance(Agent agent1, Agent agent2)
        {
            if (!agent1.Location.HasValue || !agent2.Location.HasValue)
            {
                return Double.NaN;
            }
            return Math.Sqrt(Math.Pow(agent1.Location.Value.X - agent2.Location.Value.X, 2) + Math.Pow(agent1.Location.Value.Y - agent2.Location.Value.Y, 2));
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
            allNeed.Sort(new Comparison<Need>(Need.Comparing));
            if ((CurrentState as StateLiving).Hunger.CurrentUnitValue < (CurrentState as StateLiving).Hunger.CriticalUnitValue)
            {
                return Needs.NeedEat;
            }
            else
            {
                return Needs.NeedAttack;
            }

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
