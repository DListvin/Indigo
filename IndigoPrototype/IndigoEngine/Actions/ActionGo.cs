using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using System.Drawing;
using NLog;

namespace IndigoEngine
{
    /// <summary>
    /// Action to go
    /// </summary>
    class ActionGo : Action
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Constructors

        public ActionGo(Agent argSubj, Point dir)
            : base()
        {
            Subject = argSubj;
            Direction = Normilize(dir, argSubj.Location.Value);
            MayBeConflict = true;
			RequiresObject = false;
            AcceptedSubj.Add(typeof(AgentLivingIndigo));
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
				Subject.Location = new Point(Subject.Location.Value.X + Direction.X, 
				                             Subject.Location.Value.Y + Direction.Y); 
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
		public override int CompareTo(Action argActionToCompare)
		{
			if (base.CompareTo(argActionToCompare) == 0)
			{
				if(Direction == ((ActionGo)argActionToCompare).Direction)
				{
					return 0;
				}
			}
			return 1;
		}
    }
}
