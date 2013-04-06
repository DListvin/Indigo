using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Agent for indigo
	/// </summary>
    class AgentLivingIndigo : AgentLiving
    {
        #region Constructors

        public AgentLivingIndigo()
			: base()
        {
			RangeOfView = 0;
            FieldOfView = new List<Agent>();

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

        /// <summary>
        /// This function is the brain of agent, it decide what to do
        /// </summary>
        public override void Decide()
        {
            base.Decide();
        }

        /// <summary>
        /// Calculate one main need of Indigo at this moment
        /// </summary>
        /// <returns> main need</returns>
        protected override Need EstimateMainNeed()
        {
			return base.EstimateMainNeed();
        }

        /// <summary>
        /// Calculate the best decision of action to satisfy need
        /// </summary>
        /// <param name="argNeed">need, that must be satisfied</param>
        protected override void MakeAction(Need argNeed)
        {
			base.MakeAction(argNeed);
        }

        public override string ToString()
        {
            return "Indigo: " + Name + " Health: " + Health.CurrentUnitValue.ToString();
        }
    }
}
