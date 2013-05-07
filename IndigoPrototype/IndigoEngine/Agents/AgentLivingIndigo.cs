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
    [Serializable]
	[Deciding]
    public class AgentLivingIndigo : AgentLiving
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Constructors

			public AgentLivingIndigo()
				: base()
			{
				#region Adding skills
					SkillsList.Add(Skills.Woodcutting);
					SkillsList.Add(Skills.Gathering);
					SkillsList.Add(Skills.Communicationing);
					SkillsList.Add(Skills.CampConstructing);
				#endregion
				
				#region Adding needs
					NeedFromCharacteristic.Add(CurrentState.Peacefulness, Needs.NeedAttack);
					NeedFromCharacteristic.Add(CurrentState.Health,       Needs.NeedRest);
					NeedFromCharacteristic.Add(CurrentState.FoodSatiety,  Needs.NeedEat);
					NeedFromCharacteristic.Add(CurrentState.Stamina,      Needs.NeedRest);
					NeedFromCharacteristic.Add(CurrentState.Strenght,     Needs.NeedRest);
					NeedFromCharacteristic.Add(CurrentState.WaterSatiety, Needs.NeedDrink);
				#endregion

                    Quality = new Quality(Qualities.Destroyable, Qualities.Desideable, Qualities.Moving, Qualities.Createable);
			}

        #endregion
    }
}
