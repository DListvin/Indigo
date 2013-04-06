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
        #region Characteristics variables

        private Characteristic strenght;       //Streght, controling by physical activities
        private Characteristic stamina;        //Stamina, controling by physical activities

        private Characteristic intelegence;	   //Intelegence, controling by mental activities

        private Characteristic hunger;         //Hunger, controling by time
        private Characteristic thirst;         //Thirst, controling by time

        private Characteristic aggressiveness; //Aggressiveness, controling by surroundings

        #endregion

        private List<Skill> skillsList;  //List of skills that are available to agent
		private int rangeOfView;         //Range of view of the agent (in cells around agent, apparently)
        private List<NameableObject> fieldOfView; //Agent's field ov view. Includes all agents & actions, that current agent can see

		private ShortMemory agentsShortMemory;  //Agent's short memory
		private LongMemory agentsLongMemory;    //Agent's long memory

		#region Constructors

		public AgentLiving()
			:base()
		{
			AgentsShortMemory = new ShortMemory();
			AgentsLongMemory = new LongMemory();
		}

		#endregion

        #region Properties

        public int RangeOfView
        {
            get { return rangeOfView; }
            set { rangeOfView = value; }
        }

        public List<NameableObject> FieldOfView
        {
            get { return fieldOfView; }
			set { fieldOfView = value; }
        }

        public Characteristic Strenght
        {
            get { return strenght; }
            set { strenght = value; }
        }

        public Characteristic Stamina
        {
            get { return stamina; }
            set { stamina = value; }
        }

        public Characteristic Intelegence
        {
            get { return intelegence; }
            set { intelegence = value; }
        }

        public Characteristic Hunger
        {
            get { return hunger; }
            set { hunger = value; }
        }

        public Characteristic Thirst
        {
            get { return thirst; }
            set { thirst = value; }
        }

        public Characteristic Aggressiveness
        {
            get { return aggressiveness; }
            set { aggressiveness = value; }
        }

        public List<Skill> SkillsList
        {
            get { return skillsList; }
            set { skillsList = value; }
        }		

		public ShortMemory AgentsShortMemory
		{
			get { return agentsShortMemory; }
			set { agentsShortMemory = value; }
		}		

		public LongMemory AgentsLongMemory
		{
			get { return agentsLongMemory; }
			set { agentsLongMemory = value; }
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
            if (FieldOfView.Count > 0)
            {
                Need mainNeed = EstimateMainNeed();
                MakeAction(mainNeed);
            }

        }
		

        /// <summary>
        /// Calculate the best decision of action to satisfy need
        /// </summary>
        /// <param name="argNeed">need, that must be satisfied</param>
        protected virtual void MakeAction(Need argNeed)
        {
			bool worldResponseToAction = false;	//Worl response if the action is accepted
			
            if (argNeed.SatisfyingActionIDs.Count == 0)
			{
                throw (new Exception(String.Format("Number of Action to satisfy need {0} is 0", argNeed)));
			}
			foreach(Action act in argNeed.SatisfyingActionIDs)
			{
				act.Object = this;
				foreach(Agent ag in FieldOfView.Where( val => { return val is Agent;}))
				{
					act.Subject = ag;
					worldResponseToAction = HomeWorld.AskWorldForAnAction(act);
					if(worldResponseToAction)
					{
						break;
					}
				}
				if(worldResponseToAction)
				{
					break;
				}
			}
        }

        /// <summary>
        /// Calculate one main need of agent at this moment
        /// </summary>
        /// <returns> main need</returns>
        protected virtual Need EstimateMainNeed()
        {
            return Needs.NeedExample;
        }

		public override void StateRecompute()
		{
			base.StateRecompute();

			AgentsLongMemory.StoreShortMemory(AgentsShortMemory);
			AgentsShortMemory.ForgetAll();
		}
	}
}
