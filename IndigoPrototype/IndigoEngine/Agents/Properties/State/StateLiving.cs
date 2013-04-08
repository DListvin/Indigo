using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Class for storaging characteristics of alive agents
	/// </summary>
	public class StateLiving : State
	{
		#region Constructors

			public StateLiving()
				:base()
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
			}

		#endregion

		#region Properties		

			public Characteristic Strenght { get; set; }

			public Characteristic Stamina { get; set; }

			public Characteristic Intelegence { get; set; }

			public Characteristic Hunger { get; set; }

			public Characteristic Thirst { get; set; }

			public Characteristic Aggressiveness { get; set; }

		#endregion

		#region Static methods

			public static StateLiving operator+(StateLiving argState1, StateLiving argState2)
			{
				StateLiving result = new StateLiving();

				result.Health = argState1.Health + argState2.Health;
				result.Strenght = argState1.Strenght + argState2.Strenght;
				result.Stamina = argState1.Stamina + argState2.Stamina;
				result.Intelegence = argState1.Intelegence + argState2.Intelegence;
				result.Hunger = argState1.Hunger + argState2.Hunger;
				result.Thirst = argState1.Thirst + argState2.Thirst;
				result.Aggressiveness = argState1.Aggressiveness + argState2.Aggressiveness;

				return result;
			}

			public static StateLiving operator-(StateLiving argState1, StateLiving argState2)
			{
				StateLiving result = new StateLiving();				

				result.Health = argState1.Health - argState2.Health;
				result.Strenght = argState1.Strenght - argState2.Strenght;
				result.Stamina = argState1.Stamina - argState2.Stamina;
				result.Intelegence = argState1.Intelegence - argState2.Intelegence;
				result.Hunger = argState1.Hunger - argState2.Hunger;
				result.Thirst = argState1.Thirst - argState2.Thirst;
				result.Aggressiveness = argState1.Aggressiveness - argState2.Aggressiveness;

				return result;
			}

		#endregion

            public override string ToString()
            {
                return Health.ToString() + ';' + Stamina.ToString() + ';' + Hunger.ToString() + ';' + Thirst.ToString();
            }
	}
}
