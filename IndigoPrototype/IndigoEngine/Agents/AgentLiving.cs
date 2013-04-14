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
    [Serializable]
	public abstract class AgentLiving : Agent
	{		
		private static Logger logger = LogManager.GetCurrentClassLogger();

		#region Constructors

			public AgentLiving()
				:base()
			{
				CurrentState = new StateLiving();
				SkillsList = new List<Skill>();	
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
        
        #endregion
		
        /// <summary>
        /// This function is the brain of agent, it decide what to do
        /// </summary>
        public override void Decide()
        {
            base.Decide();
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
	}
}
