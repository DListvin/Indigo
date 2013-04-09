using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Delegate for storing feedback from the action
	/// </summary>
    public delegate void ActionFeedback();

	/// <summary>
	/// Basic class of the agent. Used for inheritance for other agents
	/// </summary>
	public abstract class Agent : NameableObject, ITypicalAgent 
	{                    		
		#region Constructors
			
			public Agent() 
				: base()
			{
				CurrentState = new State();
			
				Location = new Point(0, 0);

				Inventory = new ItemStorage();

				HomeWorld = null;

                NeedFromCharacteristic = new Dictionary<Characteristic, Need>();
			}

		#endregion

		#region Properties
					
			#region ITypicalAgent realisation
				
				public State CurrentState { get; set; }    //Current state of the agent

				public Point? Location { get; set; }       //Agent location in the world grid - (X, Y), if null - agent is in some ItemStorage

				public ItemStorage Inventory { get; set; } //Agent inventory

				public ActionFeedback CurrentActionFeedback { get; set; }  //Current action result, that is needed to be perform

				public IWorldToAgent HomeWorld { get; set; }       //Agent's world

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
				if (!agent1.Location.HasValue || !agent2.Location.HasValue)
				{
					return Double.NaN;
				}
				return Math.Sqrt(Math.Pow(agent1.Location.Value.X - agent2.Location.Value.X, 2) + Math.Pow(agent1.Location.Value.Y - agent2.Location.Value.Y, 2));
			}

		#endregion

		/// <summary>
		/// ITypicalAgent
		/// </summary>
		public void CommitSuicide()
		{
			CurrentState.Health.CurrentUnitValue = CurrentState.Health.MinValue;
		}
				
		/// <summary>
		/// ITypicalAgent
		/// </summary>
		public virtual void Decide()
		{
		}
		
		/// <summary>
		/// ITypicalAgent
		/// </summary>
        public virtual void StateRecompute()
        {
            if (CurrentState.Health.CurrentUnitValue == this.CurrentState.Health.MinValue)
			{
				HomeWorld.AskWorldForDeletion(this);
			}
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
        }

        public override string ToString()
        {
            return Name + " " + CurrentState.ToString() + "   " + Location.ToString();
        }
	}
}