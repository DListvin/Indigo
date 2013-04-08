using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Class for storaging the characteristics of agents
	/// </summary>
	public class State
	{
		#region Constructors

			public State()
			{
				Health = new Characteristic();
				Health.Name = "Health";
			}

		#endregion

		#region Properties

			public Characteristic Health { get; set; } //Agent health

			protected int NumberOfCharacteristicsInState //Number of characteristics in the state
			{
				get
				{
					int result = 0; //result of the function

					foreach(System.Reflection.FieldInfo info in (typeof(State)).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
					{
						if(info.GetType().BaseType == typeof(Characteristic))
						{	
							++result;
						}
					}

					return result;
				}
			}

		#endregion

		#region Static methods

			public static State operator+(State argState1, State argState2)
			{
				State result = new State();

				result.Health = argState1.Health + argState2.Health;

				return result;
			}

			public static State operator-(State argState1, State argState2)
			{
				State result = new State();

				result.Health = argState1.Health - argState2.Health;

				return result;
			}

		#endregion		

        /// <summary>
        /// IEnumerator for foreach
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Characteristic> GetEnumerator()
		{
			return ((typeof(State)).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(field => 
			{
				return field.GetType().BaseType == typeof(Characteristic);
			})).GetEnumerator() as IEnumerator<Characteristic>;
		}
	}
}
