using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Agent for indigo
	/// </summary>
    public class AgentLivingIndigo : AgentLiving
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Constructors

			public AgentLivingIndigo()
				: base()
			{
				#region Adding skills

					SkillsList = new List<Skill>();
					SkillsList.Add(Skills.Woodcutting);
					SkillsList.Add(Skills.Gathering);
					SkillsList.Add(Skills.Communicationing);

				#endregion
				
				#region Adding needs

					NeedFromCharacteristic.Add(CurrentState.Aggressiveness, Needs.NeedAttack);
					NeedFromCharacteristic.Add(CurrentState.Health,         Needs.NeedCamp);
					NeedFromCharacteristic.Add(CurrentState.Hunger,         Needs.NeedEat);
					NeedFromCharacteristic.Add(CurrentState.Stamina,        Needs.NeedRest);
					NeedFromCharacteristic.Add(CurrentState.Strenght,       Needs.NeedRest);
					NeedFromCharacteristic.Add(CurrentState.Thirst,         Needs.NeedDrink);

				#endregion
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
            return Name + ' ' + CurrentState.ToString() + "   " + Location.Value.ToString();
        }
    }
}
