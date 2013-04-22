using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Agent for fruit
	/// </summary>
    [Serializable]
	public class AgentItemFoodFruit : AgentItemFood
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		#region Constructors
			
			public AgentItemFoodFruit()
				: base()
			{
				NeedFromCharacteristic.Add(CurrentState.Health, Needs.NeedNothing);
			}

		#endregion
	}
}
