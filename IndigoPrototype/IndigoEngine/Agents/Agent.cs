using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using NLog;
using IndigoEngine.ActionsNew;

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
				CurrentVision.Owner = this;
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
                public Quality Quality;                                    //Agent's permanent quality like thinkable and others 
                protected Dictionary<Characteristic, Need> NeedFromCharacteristic { get; set; } // Agent's spesific association Needs from Characteristic
				
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
        public virtual ActionAbstract Decide()
		{
            //Kostya's logick here
            //World must control everything, that I'm trying to do
            //I can try to do everything.

            //looking through ShortMemory to find active tasks
            //if 1 step requied - complite
				
			Attribute isDeciding = Attribute.GetCustomAttribute(this.GetType(), typeof(DecidingAttribute));  // getting attributes for this class
			if(isDeciding != null)
			{
				/*Need mainNeed = EstimateMainNeed();
				MakeAction(mainNeed);*/
                if (this is AgentLivingIndigo)
                {
                    logger.Debug("Desiding for {0}", this.Name);
                    return MakeAction(EstimateMainCharacteristic());
                }
			}
            return ActionsNew.Actions.DoNothing(this);
		}

        /// <summary>
        /// Calculate one main need of agent at this moment
        /// </summary>
        /// <returns> main need</returns>
        protected virtual Characteristic EstimateMainCharacteristic()
        {
            List<Characteristic> allCharacteristic = new List<Characteristic>();
            Characteristic characteristic = new Characteristic();
            foreach (Characteristic ch in CurrentState)
            {
                if (ch.CurrentPercentValue < ch.CriticalPercentValue)
                {
                    allCharacteristic.Add(characteristic);
                }
            }
            if (allCharacteristic.Count == 0)
			{
                return null;
			}
            allCharacteristic.Sort(new Comparison<Characteristic>(Characteristic.Comparing));

            return allCharacteristic[0];

        }
		
        /// <summary>
        /// Calculate the best decision of action to satisfy need
        /// </summary>
        /// <param name="argNeed">need, that must be satisfied</param>
        protected virtual ActionAbstract MakeAction(Characteristic argCharacteristic)
        {
            if (argCharacteristic == null)
                return ActionsNew.Actions.DoNothing( this );
            List<ActoinFunc> allActions = ActionsNew.Actions.GetActionsEstimating(this, argCharacteristic);
            return ActionsNew.Actions.GetBestActionEstimating(this, argCharacteristic);

        }
		
		/// <summary>
		/// ITypicalAgent
		/// </summary>
        public virtual void StateRecompute()
        {
			logger.Trace("Base state recomputing for {0}", this);

			CurrentState.Reduct();

            if (CurrentState.Health.CurrentUnitValue == this.CurrentState.Health.MinValue)
			{
				HomeWorld.AskWorldForEuthanasia(this);
			}
			/*foreach(ActionAbstract act in CurrentVision.CurrentViewActions)
			{
				CurrentMemory.StoreAction(act.Subject, act);
			}*/

			logger.Debug("Base state recomputed for {0}", this.Name);
			logger.Trace("{0}", this);

        }

        #region ObjectMethodsOverride

            public override string ToString()
            {
                return Name + " " + CurrentState.ToString() + "   " + CurrentLocation.ToString() + "\n" + Inventory.ToString();
            }

        #endregion
	}
}