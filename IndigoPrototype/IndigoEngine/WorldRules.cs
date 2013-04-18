using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IndigoEngine.Agents;
using IndigoEngine.Actions;
using NLog;

namespace IndigoEngine
{
	/// <summary>
	/// Class for storing some world laws and rules
	/// </summary>
	public static class WorldRules
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public static Dictionary<Type, Func<Agent>> CorpseDictionary = new Dictionary<Type, Func<Agent>>()  //Dictionary for storing links between agents and their corpses (tree - log e.c.)
		{
			{
				typeof(AgentTree),
				() => {return new AgentItemLog();}
			}
		};
	}
}
