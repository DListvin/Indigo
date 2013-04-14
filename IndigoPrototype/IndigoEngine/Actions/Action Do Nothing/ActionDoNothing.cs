using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine
{
    [Serializable]
	class ActionDoNothing : Action
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public static InfoAboutAction CurrentActionInfo = new InfoAboutAction
																		(
																			new List<Type>()
																			{
																				typeof(AgentLivingIndigo),
																				typeof(AgentCamp),
																				typeof(AgentItemFruit),
																				typeof(AgentItemLog),
																				typeof(AgentPuddle),
																				typeof(AgentTree)
																			},
																			new List<Type>()
																			{
																			},
																			false,
																			false
																		);

		#region Constructors

			public ActionDoNothing(Agent argSubj)
			: base(argSubj, null)
			{
			}

		#endregion

		public override int CompareTo(Action argActionToCompare)
		{
			return 1; //Action isn't conflict
		}
	}
}
