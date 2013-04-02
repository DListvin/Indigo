using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine
{
	class AgentIndigo : Agent
	{
		#region Characteristics

			private Characteristic strenght;       //Streght, controling by physical activities
			private Characteristic stamina;        //Stamina, controling by physical activities

			private Characteristic intelegence;	   //Intelegence, controling by mental activities

			private Characteristic hunger;         //Hunger, controling by time
			private Characteristic thirst;         //Thirst, controling by time

			private Characteristic aggressiveness; //Aggressiveness, controling by surroundings

		#endregion

		private List<Skill> skillsList;  //List of skills that are available to indigo
		private int rangeOfView;         //Range of view of the indigo

		#region Constructors
			
			public AgentIndigo() : base()
			{
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

				SkillsList = new List<Skill>();
				SkillsList.Add(Skills.Woodcutting);
				SkillsList.Add(Skills.Gathering);
				SkillsList.Add(Skills.Communicationing);
			}

		#endregion

		#region Properties

			public Characteristic Strenght
			{
				get
				{
					return strenght;
				}
				set
				{
					strenght = value;
				}
			}

			public Characteristic Stamina
			{
				get
				{
					return stamina;
				}
				set
				{
					stamina = value;
				}
			}

			public Characteristic Intelegence
			{
				get
				{
					return intelegence;
				}
				set
				{
					intelegence = value;
				}
			}

			public Characteristic Hunger
			{
				get
				{
					return hunger;
				}
				set
				{
					hunger = value;
				}
			}

			public Characteristic Thirst
			{
				get
				{
					return thirst;
				}
				set
				{
					thirst = value;
				}
			}			

			public Characteristic Aggressiveness
			{
				get
				{
					return aggressiveness;
				}
				set
				{
					aggressiveness = value;
				}
			}

			public List<Skill> SkillsList
			{
				get
				{
					return skillsList;
				}
				set
				{
					skillsList = value;
				}
			}

			public int RangeOfView
			{
				get
				{
					return rangeOfView;
				}
				set
				{
					rangeOfView = value;
				}
			}

		#endregion

		public override void Decide()
		{
			base.Decide();
			//Kostya's logick here
		}

		public override string ToString()
		{
			return "Indigo: " + Name;
		}
	}
}
