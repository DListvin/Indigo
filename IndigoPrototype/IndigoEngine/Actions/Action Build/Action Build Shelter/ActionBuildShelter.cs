using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine.ActionsOld
{
    /// <summary>
    /// Action to build some shelter. Necessary for structure
    /// </summary>
    [Serializable]
	[ActionInfo(
					new Type[]
					{
						typeof(AgentLivingIndigo),
					},
					new Type[]
					{
					},
					IsConflict = true,
					RequiresObject = false
				)]
	public abstract class ActionBuildShelter : ActionBuild
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		
        #region Constructors

			public ActionBuildShelter(Agent argSubj, params object[] argDir)
				: base(argSubj, argDir)
			{
			}

		#endregion
	}
}
