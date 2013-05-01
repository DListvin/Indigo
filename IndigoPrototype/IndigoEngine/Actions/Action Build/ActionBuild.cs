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
    /// Action to build something. Necessary for structure
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
	public abstract class ActionBuild : ActionAbstract
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		#region Constructors

			public ActionBuild(Agent argSubj, params object[] argDir)
				: base(argSubj, null)
			{
				if(argDir.Length == 0)
				{
					BuildDirection = Normilize(new Point(), Subject.CurrentLocation.Coords);
				}
				else
				{
					BuildDirection = Normilize((Point)argDir[0], Subject.CurrentLocation.Coords);
				}

				BuildingLocation = new Location(Subject.CurrentLocation.Coords.X + BuildDirection.X, Subject.CurrentLocation.Coords.Y + BuildDirection.Y);
			}

		#endregion

        #region Properties

			public Point BuildDirection { get; set; }    //Where from object wi	ll be building

			public Location BuildingLocation { get; set; }   //Where is the new camp in the world grid

        #endregion	
	}
}
