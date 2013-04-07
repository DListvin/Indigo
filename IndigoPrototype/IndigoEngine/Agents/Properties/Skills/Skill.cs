using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Class for some skill of the agent
	/// </summary>
	public class Skill : NameableObject, ITypicalSkill
	{
		private int skillQuality;	//Level of skill

		#region Constructors

			public Skill() 
				: base()
			{
				SkillQuality = 0;
			}

		#endregion

		#region Properties
		
			#region ITypicalSkill realisation

				public int SkillQuality
				{
					get
					{
						return skillQuality; 
					}
					set
					{
						if(value < 0)
						{
							throw(new Exception(String.Format("Level of skill {0} is less than 0: {1}", this, value)));
						}

						skillQuality = value;
					}
				}

			#endregion

		#endregion
	}
}
