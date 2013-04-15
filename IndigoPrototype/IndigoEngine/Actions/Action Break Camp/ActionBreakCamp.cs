using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine.Actions
{
    /// <summary>
    /// Action to break the camp
    /// </summary>
    [Serializable]
    public class ActionBreakCamp : ActionAbstract
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
																				Skills.CampConstructing,
																			},
																			true,
																			false
																		);

        #region Constructors

			public ActionBreakCamp(Agent argSubj, params object[] argDir)
			: base(argSubj, null)
			{
				if(argDir.Length == 0)
				{
					Direction = Normilize(new Point(), Subject.CurrentLocation.Coords);
				}
				else
				{
					Direction = Normilize((Point)argDir[0], Subject.CurrentLocation.Coords);
				}
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
			 
			if(!ActionAbstract.CheckForSkills(Subject, CurrentActionInfo.RequiredSkills))
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

            Subject.CurrentActionFeedback += new ActionFeedback(() =>
            {
				AgentCamp addingCamp = new AgentCamp();
				addingCamp.CurrentLocation = new Location(Subject.CurrentLocation.Coords.X + Direction.X, Subject.CurrentLocation.Coords.Y + Direction.Y);
				addingCamp.Name = "Camp_by_" + Subject.Name;
				if(
					World.AskWorldForAddition(this, addingCamp) &&
					World.AskWorldForDeletion(this, Subject.Inventory.GetAgentByTypeFromStorage(typeof(AgentItemLog))) &&
					World.AskWorldForDeletion(this, Subject.Inventory.GetAgentByTypeFromStorage(typeof(AgentItemLog)))
				)
				{						
					(Subject as AgentLiving).CurrentMemory.StoreAgent(addingCamp); 
				}

            });
        }

        public override string ToString()
        {
            return "Action: " + Name;
        }

		/// <summary>
		/// Override Action.CompareTo
		/// </summary>
		public override int CompareTo(ActionAbstract argActionToCompare)
		{
			if(Direction == (argActionToCompare as ActionBreakCamp).Direction)
			{
				return 0;
			}
			return 1;
		}
    }
}
