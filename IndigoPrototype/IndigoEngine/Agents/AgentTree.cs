using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace IndigoEngine.Agents
{	
	/// <summary>
	/// Agent for tree
	/// </summary>
    [Serializable]
	public class AgentTree : Agent
	{	
		private static Logger logger = LogManager.GetCurrentClassLogger();
			
		#region Constructors
			
			public AgentTree() 
				: base()
			{
				CurrentState = new StateTree();

				Inventory.StorageSize = 5;
				//Inventory.AddAgentToStorage(new AgentItemLog());
				//Inventory.AddAgentToStorage(new AgentItemLog());

				NeedFromCharacteristic.Add(CurrentState.Health,      Needs.NeedNothing);
				NeedFromCharacteristic.Add(CurrentState.Prolificacy, Needs.NeedGrowFruit);

				logger.Info("Created new {0}", this.GetType());
				logger.Trace("Created new {0}", this);
			}

		#endregion

		#region Properties
		
			/// <summary>
			/// Current state of the agent, override property from Agent to return StateTree, instead of State
			/// </summary>	
			public new StateTree CurrentState  
			{
				get
				{
					return base.CurrentState as StateTree;
				}
				set
				{
					base.CurrentState = value;
				} 
			} 

		#endregion

		public override void StateRecompute()
		{
			int numberOfFruits = Inventory.CountNumberOfAgentsByType(typeof(AgentItemFruit));  //Number of fruits, that are on the tree now

			CurrentState.Prolificacy -= 3 - numberOfFruits;

			base.StateRecompute();
		}
	}
}
