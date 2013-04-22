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
	[ActionInfo(
					new Type[]
					{
						typeof(AgentLivingIndigo),
					},
					new Type[]
					{
					},
					"CampConstructing",
					IsConflict = true,
					RequiresObject = false
				)]
    public class ActionBreakCamp : ActionAbstract
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();	

        #region Constructors

			public ActionBreakCamp(Agent argSubj, params object[] argDir)
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

				CampLocation = new Location(Subject.CurrentLocation.Coords.X + BuildDirection.X, Subject.CurrentLocation.Coords.Y + BuildDirection.Y);
				Name = "To Break a Camp";
			}

        #endregion	

        #region Properties

			public Point BuildDirection { get; set; }    //Where from object wi	ll be camp

			public Location CampLocation { get; set; }   //Where is the new camp in the world grid

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

            if (Subject.Inventory.NumberOfAgentsByType(typeof(AgentItemResLog)) < 2) //Number of logs int subject's inventory
            {
                return false;
            }

			return true;
		}

        /// <summary>
        /// ITypicalAction
        /// </summary>
        public override void CalculateFeedbacks()
        {
            base.CalculateFeedbacks();

            Subject.CurrentActionFeedback += new ActionFeedback(() =>
            {
				AgentManMadeShelterCamp addingCamp = new AgentManMadeShelterCamp();
				addingCamp.CurrentLocation = CampLocation;
				addingCamp.Name = "Camp_by_" + Subject.Name;
				if(
					World.AskWorldForAddition(this, addingCamp) &&
					World.AskWorldForDeletion(this, Subject.Inventory.PopAgentByType(typeof(AgentItemResLog))) &&
					World.AskWorldForDeletion(this, Subject.Inventory.PopAgentByType(typeof(AgentItemResLog)))
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
			if(CampLocation == (argActionToCompare as ActionBreakCamp).CampLocation)
			{
				return 0;
			}
			return 1;
		}
    }
}
