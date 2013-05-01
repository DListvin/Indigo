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
    public class ActionBuildShelterCamp : ActionBuildShelter
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();	

        #region Constructors

			public ActionBuildShelterCamp(Agent argSubj, params object[] argDir)
				: base(argSubj, argDir)
			{
				Name = "To Break a Camp";
			}

        #endregion		
		
		/// <summary>
		/// ITypicalAction
		/// </summary>
		public override Exception CheckForLegitimacy()
		{
			if(base.CheckForLegitimacy() != null)
			{
				return base.CheckForLegitimacy();
			}  

            if (Subject.Inventory.NumberOfAgentsByType(typeof(AgentItemResLog)) < 2) //Number of logs int subject's inventory
            {
                return new ResourceRequiredException();
            }

			return null;
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
				addingCamp.CurrentLocation = BuildingLocation;
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

		/// <summary>
		/// Override Action.CompareTo
		/// </summary>
		public override int CompareTo(ActionAbstract argActionToCompare)
		{
			if(BuildingLocation == (argActionToCompare as ActionBuildShelterCamp).BuildingLocation)
			{
				return 0;
			}
			return 1;
		}
    }
}
