using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Agent for puddle
	/// </summary>
    [Serializable]
	public class AgentPuddle : Agent
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		#region Constructors
			
			public AgentPuddle()
				: base()
			{
				NeedFromCharacteristic.Add(CurrentState.Health, Needs.NeedNothing);
                Quality = new Quality(Qualities.Drinkable, Qualities.Destroyable);
			}

		#endregion
	}
}
