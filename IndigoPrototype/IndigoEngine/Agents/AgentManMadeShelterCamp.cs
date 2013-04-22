using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace IndigoEngine.Agents
{	
	/// <summary>
	/// Agent for camp
	/// </summary>
    [Serializable]
	public class AgentManMadeShelterCamp : AgentManMadeShelter
	{	
		private static Logger logger = LogManager.GetCurrentClassLogger();

		#region Constructors
			
			public AgentManMadeShelterCamp() 
				: base()
			{
				NeedFromCharacteristic.Add(CurrentState.Health, Needs.NeedNothing);
			}

		#endregion
	}
}