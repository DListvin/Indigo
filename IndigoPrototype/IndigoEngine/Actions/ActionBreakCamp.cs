using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IndigoEngine.Agents;

namespace IndigoEngine
{
    /// <summary>
    /// Action to break the camp
    /// </summary>
    public class ActionBreakCamp : Action
    {
        #region Constructors

			public ActionBreakCamp(Agent argObj, Point dir)
				: base(argObj, null)
			{
				Direction = Normilize(dir, argObj.Location.Value);
				MayBeConflict = true;
				AcceptedObj.Add(typeof(AgentLiving));
				AcceptedObj.Add(typeof(AgentLivingIndigo));
				AcceptedSubj.Add(typeof(AgentCamp));
				Name = "To Break a Camp";
			}

        #endregion

        #region Properties

			public Point Direction { get; set; } // where from object will be camp

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

            if (Subject.Inventory.CountNumberOfAgentsByType(typeof(AgentItemLog)) < 2) //Number of logs int subject's inventory
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
                Subject.Inventory.DeleteAgentsByType(typeof(AgentItemLog), 2);
                //generate event to world to create the camp

                ((AgentLiving)Subject).AgentsShortMemory.StoreAction(Subject, this);
            });
        }

        public override string ToString()
        {
            return "Action: " + Name;
        }

		/// <summary>
		/// Override Action.CompareTo
		/// </summary>
		public int CompareTo(ActionBreakCamp argActionToCompare)
		{
			if (base.CompareTo(argActionToCompare) == 0)
			{
				if(Direction == argActionToCompare.Direction)
				{
					return 0;
				}
			}
			return 1;
		}
    }
}
