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
	}
}
