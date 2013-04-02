using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine
{
	class Skill : ITypicalSkill, INamabelObject
	{
		private string name;   //Characteristic name

		private int skillQuality;	//Level of skill

		#region Constructors

			public Skill()
			{
				Name = "Untitled skill";
				SkillQuality = 0;
			}

		#endregion

		#region Properties

			#region INamabelObject realisation
				
				public string Name
				{
					get
					{
						return name;
					}
					set
					{
						name = value;
					}
				}

			#endregion

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
