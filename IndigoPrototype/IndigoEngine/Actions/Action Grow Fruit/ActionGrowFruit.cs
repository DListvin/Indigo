using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine.Actions
{
	/// <summary>
	/// Action for tree to grow a fruit on itself
	/// </summary>
    [Serializable]
	[ActionInfo(
					new Type[]
					{
						typeof(AgentTree),
					},
					new Type[]
					{
					},
					IsConflict = false,
					RequiresObject = false
				)]
	class ActionGrowFruit : ActionAbstract
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Constructors

			public ActionGrowFruit(Agent argSubj)
			: base(argSubj, null)
			{
				Name = "To grow a fruit on a tree";

				logger.Debug("Created new {0}", this.GetType());
				logger.Trace("Created new {0}", this);
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
            if (Subject.Inventory.ItemList.Count >= Subject.Inventory.StorageSize) //Cheking for item storage errors. May be it is extra checking?
            {
                return new NotLegimateExeception();
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
				//Realisation for tree
				if(Subject is AgentTree)
				{
					Agent newAgentToAdd = new AgentItemFoodFruit();
					newAgentToAdd.Name = "Fruit " + Subject.Inventory.NumberOfAgentsByType(typeof(AgentItemFoodFruit)) + " from " + Subject.Name;
					Subject.Inventory.AddAgentToStorage(newAgentToAdd);

					if(World.AskWorldForAddition(this, newAgentToAdd))
					{
						(Subject as AgentTree).CurrentState.Prolificacy.CurrentPercentValue = 100; 
					}
					else
					{
						Subject.Inventory.PopAgent(newAgentToAdd);
					}
				}
            });

			logger.Info("Action {0}. Fruit grown for {1}", this.Name, Subject.Name);
        }

		/// <summary>
		/// ITypicalAction
		/// </summary>
		public override NameableObject CharacteristicsOfSubject()
		{
			if(Subject is AgentTree)
			{
				return (Subject as AgentTree).CurrentState.Prolificacy; 
			}

			return base.CharacteristicsOfSubject();
		}

		public override int CompareTo(ActionAbstract argActionToCompare)
		{
			return 1; //Action isn't conflict
		}
	}
}
