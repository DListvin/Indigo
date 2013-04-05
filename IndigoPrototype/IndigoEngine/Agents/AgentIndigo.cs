using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
    class AgentIndigo : Agent
    {
        #region Characteristics variables

        private Characteristic strenght;       //Streght, controling by physical activities
        private Characteristic stamina;        //Stamina, controling by physical activities

        private Characteristic intelegence;	   //Intelegence, controling by mental activities

        private Characteristic hunger;         //Hunger, controling by time
        private Characteristic thirst;         //Thirst, controling by time

        private Characteristic aggressiveness; //Aggressiveness, controling by surroundings

        #endregion

        private List<Skill> skillsList;  //List of skills that are available to indigo
		private int rangeOfView;         //Range of view of the agent (in cells around agent, apparently)
        private List<Agent> fieldOfView; //Agent's field ov view. Includes all agents, that current agent can see

        #region Constructors

        public AgentIndigo()
			: base()
        {
			RangeOfView = 0;
            fieldOfView = new List<Agent>();

			#region Characteristics set up

            Strenght = new Characteristic();
            Strenght.Name = "Strenght";

            Stamina = new Characteristic();
            Stamina.Name = "Stamina";

            Intelegence = new Characteristic();
            Intelegence.Name = "Intelegence";

            Hunger = new Characteristic();
            Hunger.Name = "Hunger";

            Thirst = new Characteristic();
            Thirst.Name = "Thirst";

            Aggressiveness = new Characteristic();
            Aggressiveness.Name = "Aggressiveness";

			#endregion

            SkillsList = new List<Skill>();
            SkillsList.Add(Skills.Woodcutting);
            SkillsList.Add(Skills.Gathering);
            SkillsList.Add(Skills.Communicationing);
        }

        #endregion

        #region Properties

        public int RangeOfView
        {
            get { return rangeOfView; }
            set { rangeOfView = value; }
        }

        public List<Agent> FieldOfView
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
            if (fieldOfView.Count > 0)
            {
                Need mainNeed = EstimateMainNeed();
                MakeAction(mainNeed);
            }

        }

        /// <summary>
        /// Calculate one main need of Indigo at this moment
        /// </summary>
        /// <returns> main need</returns>
        Need EstimateMainNeed()
        {
            return Needs.NeedExample;
        }

        /// <summary>
        /// Calculate the best decision of action to satisfy need
        /// </summary>
        /// <param name="need">need, that must be satisfied</param>
        void MakeAction(Need need)
        {
            if (need.SatisfyingActionIDs.Count == 0)
                throw (new Exception(String.Format("Number of Action to satisfy need {0} is 0", need)));
            Action act = need.SatisfyingActionIDs[0];
            act.Object = this;
            act.Subject = fieldOfView[0];
            HomeWorld.AskWorldForAnAction(act);
        }

        public override string ToString()
        {
            return "Indigo: " + Name + " Health: " + Health.CurrentUnitValue.ToString();
        }
    }
}
