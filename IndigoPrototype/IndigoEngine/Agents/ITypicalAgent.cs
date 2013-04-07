using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IndigoEngine.Agents
{
	interface ITypicalAgent
	{
		
		/// <summary>
		/// Operations with health characteristic
		/// </summary>
		State CurrentState { get; set; }

		/// <summary>
		/// Operations with agent location in the world grid
		/// </summary>
		Point? Location { get; set; }

		/// <summary>
		/// Operations with agent inventory
		/// </summary>
		ItemStorage Inventory { get; set; }
		
		/// <summary>
		/// Operations with action feeeback
		/// </summary>
		ActionFeedback CurrentActionFeedback { get; set; }

		/// <summary>
		/// Operations with agent's world
		/// </summary>
        IWorldToAgent HomeWorld{ get; set; }

		/// <summary>
		/// Making a decision about action in the current phase
		/// </summary>
		void Decide();	
		
        /// <summary>
        /// Modify it's charecterictics each turn.
        /// </summary>
        void StateRecompute();

		/// <summary>
		/// Apply action feedback to agent
		/// </summary>
		void PerformFeedback();
	}
}
