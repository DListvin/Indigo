using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using NLog;
using IndigoEngine.Actions;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Delegate for storing feedback from the action
	/// </summary>
    public delegate void ActionFeedback();

	/// <summary>
	/// Basic class of the agent. Used for inheritance for other agents
	/// </summary>
    [Serializable]
	public abstract class Agent : NameableObject, ITypicalAgent 
	{         
		private static Logger logger = LogManager.GetCurrentClassLogger();
		           		
		#region Constructors
			
			public Agent() 
				: base()
			{
				CurrentState = new State();			
				CurrentLocation = new Location();
				Inventory = new ItemStorage();
				Inventory.Owner = this;
				HomeWorld = null;				
				CurrentMemory = new Memory();
				CurrentVision = new Vision();
                NeedFromCharacteristic = new Dictionary<Characteristic, Need>();
			}

		#endregion

		#region Properties
					
			#region ITypicalAgent realisation				
				
				public State CurrentState { get; set; }                    //Current state of the agent
				public Location CurrentLocation { get; set; }              //Agent location in the world grid - (X, Y), or agent - owner
				public ItemStorage Inventory { get; set; }                 //Agent inventory
				public ActionFeedback CurrentActionFeedback { get; set; }  //Current action result, that is needed to be perform
				public IWorldToAgent HomeWorld { get; set; }               //Agent's world				
				public Vision CurrentVision { get; set; }                  //Agent's field ov view. Includes all agents & actions, that current agent can see
				public Memory CurrentMemory { get; set; }                  //Agent's memory
				public List<Skill> SkillsList { get; set; }                //List of skills that are available to agent

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

                protected Dictionary<Characteristic, Need> NeedFromCharacteristic { get; set; } // Agent's spesific association Needs from Characteristic

			#endregion

		#endregion

		#region Static methods

			/// <summary>
			/// Computes distance between two agents
			/// </summary>
			/// <param name="agent1">First agent</param>
			/// <param name="agent2">Second agent</param>
			/// <returns>Distance or NaN, if any of agents doesn'n have location</returns>
			public static double Distance(Agent agent1, Agent agent2)
			{
				if (agent1.CurrentLocation.HasOwner || agent2.CurrentLocation.HasOwner)
				{
					return Double.NaN;
				}
				return Math.Sqrt(Math.Pow(agent1.CurrentLocation.Coords.X - agent2.CurrentLocation.Coords.X, 2) + Math.Pow(agent1.CurrentLocation.Coords.Y - agent2.CurrentLocation.Coords.Y, 2));
			}

		#endregion

		/// <summary>
		/// ITypicalAgent
		/// </summary>
		public void CommitSuicide()
		{
			CurrentState.Health.CurrentUnitValue = CurrentState.Health.MinValue;
			logger.Info("Agent {0} commited suicide", this.Name);
		}
				
		/// <summary>
		/// ITypicalAgent
		/// </summary>
		public virtual void Decide()
		{
            //Kostya's logick here
            //World must control everything, that I'm trying to do
            //I can try to do everything.

            //looking through ShortMemory to find active tasks
            //if 1 step requied - complite
            Need mainNeed = EstimateMainNeed();
            MakeAction(mainNeed);

			logger.Debug("Desided for {0}", this.Name);
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
                if (ch.CurrentPercentValue < ch.CriticalPercentValue)
                {
                    if (!NeedFromCharacteristic.TryGetValue(ch, out need))
                    {
						logger.Error("Dictionary of the {0} doesn't have info about needs for {1}", this.Name, ch.Name);
                        throw new Exception("Ditionary in Agent has error. Cant find need by characteristic!");
                    }
                    allNeed.Add(need);
                }
            }
            if (allNeed.Count == 0)
                return Needs.NeedNothing;
            allNeed.Sort(new Comparison<Need>(Need.Comparing));

			logger.Debug("Estimated main need for {0} it is {1}", this.Name, allNeed[0].Name);

            return allNeed[0];

        }
		
        /// <summary>
        /// Calculate the best decision of action to satisfy need
        /// </summary>
        /// <param name="argNeed">need, that must be satisfied</param>
        protected virtual void MakeAction(Need argNeed)
        {
            CurrentActionFeedback = null;
            bool worldResponseToAction = false;	//World response if the action is accepted.
			ActionAbstract newAction;           //New action to create
            if (argNeed.SatisfyingActions.Count == 0)
                throw (new Exception(String.Format("Number of Action to satisfy need {0} is 0", argNeed)));

            foreach (Type act in argNeed.SatisfyingActions)
            {
				System.Reflection.FieldInfo field = act.GetField("CurrentActionInfo", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
				InfoAboutAction currentInfo = field.GetValue(null) as InfoAboutAction;				

                if (currentInfo.RequiresObject)
                {
                    foreach (Agent ag in Inventory.ItemList.Concat(CurrentVision.CurrentViewAgents))
                    {
                        if (!currentInfo.AcceptedObjects.Contains(ag.GetType()))
                        {
                            continue;
                        }
                        if (Distance(this, ag) > Math.Sqrt(2))
                        {
							newAction = ActionsManager.GetThisActionForCurrentParticipants(typeof(ActionGo), ActionGo.CurrentActionInfo, this, null, ag.CurrentLocation.Coords);
							worldResponseToAction = HomeWorld.AskWorldForAction(newAction);
                            if (worldResponseToAction)
							{
                                break;
							}
                        }
						newAction = ActionsManager.GetThisActionForCurrentParticipants(act, currentInfo, this, ag);
                        worldResponseToAction = HomeWorld.AskWorldForAction(newAction);
                        if (worldResponseToAction)
                        {
                            break;
                        }
                    }
                }
                else
                {
					newAction = ActionsManager.GetThisActionForCurrentParticipants(act, currentInfo, this, null);
					worldResponseToAction = HomeWorld.AskWorldForAction(newAction);
                }

                if (worldResponseToAction)
                {
					logger.Debug("Made action for {0}: {1}", this.Name, act.Name);
                    break;
                }
            }

        }
		
		/// <summary>
		/// ITypicalAgent
		/// </summary>
        public virtual void StateRecompute()
        {
			logger.Trace("Base state recomputing for {0}", this);

            if (CurrentState.Health.CurrentUnitValue == this.CurrentState.Health.MinValue)
			{
				HomeWorld.AskWorldForDeletion(this);
			}

			logger.Debug("Base state recomputed for {0}", this.Name);
			logger.Trace("{0}", this);

        }

		/// <summary>
		/// ITypicalAgent
		/// </summary>
        public void PerformFeedback()
        {
            if (CurrentActionFeedback != null)
			{
                CurrentActionFeedback.Invoke();
			}			
			logger.Debug("Performed action feedback for {0}", this.Name);
        }

        #region ObjectMethodsOverride

            public override string ToString()
            {
                return Name + " " + CurrentState.ToString() + "   " + CurrentLocation.ToString() + "\n" + Inventory.ToString();
            }

        #endregion
	}
}