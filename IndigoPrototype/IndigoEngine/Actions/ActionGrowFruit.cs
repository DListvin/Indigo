using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine.Actions
{
    [Serializable]
	class ActionGrowFruit : Action
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Constructors

			public ActionGrowFruit(Agent argSubj)
			: base(argSubj, null)
			{
				IsConflict = false;
				RequiresObject = false;
				AcceptedSubj.Add(typeof(AgentTree));
				Name = "To grow a fruit on a tree";

				logger.Info("Created new {0}", this.GetType());
				logger.Trace("Created new {0}", this);
			}

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

            if (Subject.Inventory.ItemList.Count >= Subject.Inventory.StorageSize) //Cheking for item storage errors. May be it is extra checking?
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
				//Realisation for tree
				if(Subject is AgentTree)
				{
					AgentItemFruit newAgentToAdd = new AgentItemFruit();
					newAgentToAdd.Name = "Fruit from " + Subject.Name;

					if(World.AskWorldForAddition(this, newAgentToAdd))
					{
						Subject.Inventory.AddAgentToStorage(newAgentToAdd);
						(Subject as AgentTree).CurrentState.Prolificacy.CurrentPercentValue = 100; 
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

        public override string ToString()
        {
            return "Action: " + Name;
        }
	}
}
