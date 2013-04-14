using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using System.Drawing;
using NLog;

namespace IndigoEngine.Actions
{
    /// <summary>
    /// Action to go
    /// </summary>
    [Serializable]
    class ActionGo : ActionAbstract
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public static InfoAboutAction CurrentActionInfo = new InfoAboutAction
																		(
																			new List<Type>()
																			{
																				typeof(AgentLivingIndigo),
																			},
																			new List<Type>()
																			{
																			},
																			new List<Skill>()
																			{
																			},
																			true,
																			false
																		);

        #region Constructors

			public ActionGo(Agent argSubj, params object[] argDir)
				: base(argSubj, null)
			{
				if(argDir.Length == 0)
				{
					Direction = new Point();
				}
				else
				{
					Direction = Normilize((Point)argDir[0], argSubj.CurrentLocation.Coords);
				}
			}

        #endregion
		
		#region Properties

			public Point Direction { get; set; } // point where to go

		#endregion

		/// <summary>
		/// ITypicalAction
		/// </summary>
		public override bool CheckForLegitimacy()
		{
			if(!base.CheckForLegitimacy())
			{
				return false;
			}
			 
			if(!ActionAbstract.CheckForSkills(Subject, CurrentActionInfo.RequiredSkills))
			{
				return false;
			}
			return true;
		}

        /// <summary>
        /// ITypicalAction
        /// </summary>
        public override void Perform()
        {
            base.Perform();

            Subject.CurrentActionFeedback = new ActionFeedback(() =>
            {
				Subject.CurrentLocation = new Location(Subject.CurrentLocation.Coords.X + Direction.X, 
				                             Subject.CurrentLocation.Coords.Y + Direction.Y); 
				(Subject as AgentLiving).CurrentState.Stamina.CurrentUnitValue--;
            });
        }

        public override string ToString()
        {
            return "Action: go";
        }

		/// <summary>
		/// Override Action.CompareTo
		/// </summary>
		public override int CompareTo(ActionAbstract argActionToCompare)
		{
			return 1; //Action isn't conflict
		}
    }
}
